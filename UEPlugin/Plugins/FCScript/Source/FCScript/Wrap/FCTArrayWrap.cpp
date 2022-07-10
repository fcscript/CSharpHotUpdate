#include "FCTArrayWrap.h"
#include "Containers/ScriptArray.h"
#include "FCTemplateType.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"

void FCTArrayWrap::Register(fc_intptr VM)
{
    int nClassName = fc_get_inport_class_id(VM, "TArray");
    fc_register_class_new(VM, nClassName, obj_new);
    fc_register_class_del(VM, nClassName,obj_del);
    fc_register_class_release_ref(VM, nClassName,obj_release);
    fc_register_class_hash(VM, nClassName,obj_hash);
    fc_register_class_equal(VM, nClassName,obj_equal);
    fc_register_class_func(VM, nClassName,"TArray",obj_new);
    fc_register_class_func(VM, nClassName,"GetNumb",GetNumb_wrap);
    fc_register_class_func(VM, nClassName,"SetNumb",SetNumb_wrap);
    fc_register_class_func(VM, nClassName,"GetAt",GetAt_wrap);
    fc_register_class_func(VM, nClassName,"SetAt",SetAt_wrap);
    fc_register_class_func(VM, nClassName,"Add",Add_wrap);
    fc_register_class_func(VM, nClassName,"Remove",Remove_wrap);
	fc_register_class_func(VM, nClassName, "Clear", Clear_wrap);
	fc_register_class_func(VM, nClassName, "ToList", ToList_wrap);
	fc_register_class_func(VM, nClassName, "SetList", SetList_wrap);
	
	// 属性方法
	fc_register_class_attrib(VM, nClassName, "[]", GetIndex_wrap, SetIndex_wrap);
	fc_register_class_attrib(VM, nClassName, "Length", GetNumb_wrap, SetNumb_wrap);
	// 兼容stl接口
    fc_register_class_func(VM, nClassName,"size",GetNumb_wrap);
    fc_register_class_func(VM, nClassName,"resize",SetNumb_wrap);
    fc_register_class_func(VM, nClassName,"push_back",Add_wrap);
}

int FCTArrayWrap::obj_new(fc_intptr L)
{
	// FScriptArray *ScriptArray = new FScriptArray;
	// 这个还是不要让动态构建的好了
	// 因为不管怎么样，就算是相同的，也是需要拷贝的
	fc_intptr RetPtr = fc_get_return_ptr(L);
	fc_intptr VM = fc_get_vm_ptr(L);
	FCDynamicProperty *DynamicProperty = GetTArrayDynamicProperty(VM, RetPtr);
	if(DynamicProperty)
	{
		FScriptArray *ScriptArray = new FScriptArray();
		int64 ObjID = FCGetObj::GetIns()->PushTemplate(DynamicProperty, ScriptArray, EFCObjRefType::NewTArray);
		fc_set_value_wrap_objptr(VM, RetPtr, ObjID);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, RetPtr, 0);
	}

	return 0;
}
int FCTArrayWrap::obj_del(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->DeleteValue(nIntPtr);
	return 0;
}
int FCTArrayWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
	return 0;
}
int FCTArrayWrap::obj_hash(fc_intptr nIntPtr)
{
	return FCGetObj::GetIns()->GetValueHash(nIntPtr);
}
bool FCTArrayWrap::obj_equal(fc_intptr L, fc_intptr R)
{
	return FCGetObj::GetIns()->EqualValue(L, R);
}
int FCTArrayWrap::GetNumb_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef)
	{
		FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
		int Num = ScriptArray->Num();
		fc_intptr RetPtr = fc_get_return_ptr(L);
		fc_set_value_uint(RetPtr, Num);
	}
	else
	{
		fc_intptr RetPtr = fc_get_return_ptr(L);
		fc_set_value_uint(RetPtr, 0);
	}
	return 0;
}

void  FCTArrayWrap_SetNumb(FScriptArray *ScriptArray, FProperty *Inner, int NewNum)
{
	if(NewNum < 0)
	{
		NewNum = 0;
	}
	int ElementSize = Inner->GetSize();
	int32 OldNum = ScriptArray->Num();
	uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
	uint8 *ValueAddr = ObjAddr;
	if (OldNum < NewNum)
	{
		int32 Index = ScriptArray->Add(NewNum - OldNum, ElementSize);
		for (; Index < NewNum; ++Index)
		{
			ValueAddr = ObjAddr + Index * ElementSize;
			Inner->InitializeValue(ValueAddr);
		}
	}
	else
	{
		for (int32 Index = OldNum; Index < NewNum; ++Index)
		{
			ValueAddr = ObjAddr + Index * ElementSize;
			Inner->DestroyValue(ValueAddr);
		}
		ScriptArray->Remove(OldNum, NewNum - OldNum, ElementSize);
	}
}
int FCTArrayWrap::SetNumb_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if(ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
			int32 NewNum = fc_get_value_int(ArgPtr0);
			FCTArrayWrap_SetNumb(ScriptArray, Inner, NewNum);
		}
	}
	return 0;
}
void FCTArrayWrap_GetAt_Wrap(fc_intptr L, fc_intptr KeyPtr, fc_intptr ValuePtr)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			int Index = fc_get_value_int(KeyPtr);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			int32 Num = ScriptArray->Num();
			if(Index >= 0 && Index < Num)
			{
				FProperty *Inner = ArrayProperty->Inner;
				int ElementSize = Inner->GetSize();
				uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
				uint8 *ValueAddr = ObjAddr + Index * ElementSize;

				FCDynamicProperty * ElementProperty = GetDynamicPropertyByUEProperty(Inner);

				fc_intptr VM = fc_get_vm_ptr(L);
				ElementProperty->m_WriteScriptFunc(VM, ValuePtr, ElementProperty, ValueAddr, NULL, ObjRef);
			}
		}
	}
}

int FCTArrayWrap::GetAt_wrap(fc_intptr L)
{
	fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
	fc_intptr RetPtr = fc_get_return_ptr(L);
	FCTArrayWrap_GetAt_Wrap(L, KeyPtr, RetPtr);
	return 0;
}

void FCTArrayWrap_SetAt_warp(fc_intptr L, fc_intptr KeyPtr, fc_intptr ValuePtr)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			int Index = fc_get_value_int(KeyPtr);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			int32 Num = ScriptArray->Num();
			if (Index >= 0 && Index < Num)
			{
				FProperty *Inner = ArrayProperty->Inner;
				int ElementSize = Inner->GetSize();
				uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
				uint8 *ValueAddr = ObjAddr + Index * ElementSize;
				Inner->InitializeValue(ValueAddr);

				FCDynamicProperty* ElementProperty = GetDynamicPropertyByUEProperty(Inner);

				fc_intptr VM = fc_get_vm_ptr(L);
				ElementProperty->m_ReadScriptFunc(VM, ValuePtr, ElementProperty, ValueAddr, NULL, ObjRef);
			}
		}
	}
}

int FCTArrayWrap::SetAt_wrap(fc_intptr L)
{
	fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
	fc_intptr ArgPtr1 = fc_get_param_ptr(L, 1);
	FCTArrayWrap_SetAt_warp(L, ArgPtr0, ArgPtr1);
	return 0;
}

// value = array[key]
int FCTArrayWrap::GetIndex_wrap(fc_intptr L)
{
	fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
	fc_intptr ValuePtr = fc_get_return_ptr(L);
	FCTArrayWrap_GetAt_Wrap(L, KeyPtr, ValuePtr);
	return 0;
}

// array[key] = value
int FCTArrayWrap::SetIndex_wrap(fc_intptr L)
{
	fc_intptr KeyPtr = fc_get_param_ptr(L, 0);
	fc_intptr ValuePtr = fc_get_return_ptr(L);
	FCTArrayWrap_SetAt_warp(L, KeyPtr, ValuePtr);
	return 0;
}

int FCTArrayWrap::Add_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			int ElementSize = Inner->GetSize();
			int32 Index = ScriptArray->Add(1, ElementSize);
			uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
			uint8 *ValueAddr = ObjAddr + Index * ElementSize;
			Inner->InitializeValue(ValueAddr);

			FCDynamicProperty* ElementProperty = GetDynamicPropertyByUEProperty(Inner);

			fc_intptr VM = fc_get_vm_ptr(L);
			ElementProperty->m_ReadScriptFunc(VM, ArgPtr0, ElementProperty, ValueAddr, NULL, ObjRef);
		}
	}
	return 0;
}
int FCTArrayWrap::Remove_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);
			int Index = fc_get_value_int(ArgPtr0);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			int ElementSize = Inner->GetSize();
			int Numb = ScriptArray->Num();
			if(Index >= 0 && Index < Numb)
			{
				uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
				uint8 *ValueAddr = ObjAddr + Index * ElementSize;
				Inner->DestroyValue(ValueAddr);
				ScriptArray->Remove(Index, 1, ElementSize);
			}
		}
	}
	return 0;
}
int FCTArrayWrap::Clear_wrap(fc_intptr L)
{
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			TArray_Clear(ScriptArray, Inner);
		}
	}
	return 0;
}
int FCTArrayWrap::ToList_wrap(fc_intptr L)
{
	// 将TArray转换成脚本内置的List<T>
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			fc_intptr RetPtr = fc_get_return_ptr(L);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			int ElementSize = Inner->GetSize();
			int Numb = ScriptArray->Num();

			FCDynamicProperty* ElementProperty = GetDynamicPropertyByUEProperty(Inner);
			fc_intptr VM = fc_get_vm_ptr(L);
			fc_set_array_size(VM, RetPtr, Numb);

			uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
			uint8 *ValueAddr = ObjAddr;
			for(int Index = 0; Index<Numb; ++Index)
			{
				fc_intptr ItemPtr = fc_get_array_node_temp_ptr(VM, RetPtr, Index);
				ValueAddr = ObjAddr + Index * ElementSize;
				ElementProperty->m_WriteScriptFunc(VM, ItemPtr, ElementProperty, ValueAddr, NULL, NULL);
			}
		}
	}
	return 0;
}
int FCTArrayWrap::SetList_wrap(fc_intptr L)
{
	// 将脚本内置的List<T> 设置到TArray
	fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if (ObjRef && ObjRef->DynamicProperty)
	{
		if (ObjRef->DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			fc_intptr ArgPtr0 = fc_get_param_ptr(L, 0);

			FScriptArray *ScriptArray = (FScriptArray*)ObjRef->GetThisAddr();
			FArrayProperty  *ArrayProperty = (FArrayProperty *)ObjRef->DynamicProperty->Property;
			FProperty *Inner = ArrayProperty->Inner;
			int ElementSize = Inner->GetSize();

			// 需要的话，这里可以优化一下
			FCDynamicProperty* ElementProperty = GetDynamicPropertyByUEProperty(Inner);
			fc_intptr VM = fc_get_vm_ptr(L);
			int ScriptListNumb = fc_get_array_size(ArgPtr0);

			FCTArrayWrap_SetNumb(ScriptArray, Inner, ScriptListNumb);

			uint8 *ObjAddr = (uint8 *)ScriptArray->GetData();
			uint8 *ValueAddr = ObjAddr;
			for (int Index = 0; Index < ScriptListNumb; ++Index)
			{
				fc_intptr ItemPtr = fc_get_array_node_temp_ptr(VM, ArgPtr0, Index);
				ValueAddr = ObjAddr + Index * ElementSize;
				ElementProperty->m_ReadScriptFunc(VM, ItemPtr, ElementProperty, ValueAddr, NULL, ObjRef);
			}
		}
	}
	return 0;
}