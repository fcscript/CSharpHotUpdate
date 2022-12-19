#include "FCRunTimeRegister.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"
#include "FCDynamicOverrideFunc.h"
#include "FCPropertyType.h"

#include "FCCallUEFunc.h"
#include "FCCore.h"

// 反射类的强制转换
int WrapUClassCast(fc_intptr L)
{
	int nClassName = fc_get_current_call_class_name_id(L);
	const char *ClassName = fc_cpp_get_current_call_class_name(L);

	return 0;
}

// 反射类的构建函数
int WrapUClassNew(fc_intptr L)
{
	fc_intptr VM = fc_get_vm_ptr(L);
	int nClassName = fc_get_current_call_class_name_id(L);
	// 先尝试查找静态对象，如FVector这类的
	FCDynamicClassDesc *ClassDesc = GetScriptContext()->FindClassByID(nClassName);
	if(!ClassDesc)
	{
		const char* ClassName = fc_cpp_get_current_call_class_name(L);  // 这个只是单纯的UEClass，不是脚本的，这里创建的也是UE对象
		ClassDesc = GetScriptContext()->RegisterWrapClass(ClassName, nClassName);
	}
    int64 ObjID = 0;
    fc_intptr  ret_ptr = fc_get_return_ptr(L);
	if (ClassDesc)
	{
		if(ClassDesc->m_Class)
		{
			UObject* Outer = (UObject*)GetTransientPackage();
			ObjID = FCGetObj::GetIns()->PushNewObject(ClassDesc, NAME_None, Outer, VM, ret_ptr);
		}
		else if(ClassDesc->m_Struct)
		{
			ObjID = FCGetObj::GetIns()->PushNewStruct(ClassDesc);
		}
	}
	fc_set_value_wrap_objptr(VM, ret_ptr, ObjID);

	return 0;
}

// 反射类的对象删除接口
int WrapUClassDel(fc_intptr nObjPtr)
{;
	FCGetObj::GetIns()->DeleteValue(nObjPtr);
	return 0;
}

// 反射类的对象释放引用
int WrapUClassReleaseRef(fc_intptr nObjPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nObjPtr);
	return 0;
}
// 反射类的对象Hash函数
int WrapUClassHash(fc_intptr nObjPtr)
{
	return FCGetObj::GetIns()->GetValueHash(nObjPtr);
}
// 反射类的对象Equal函数
bool WrapUClassEqual(fc_intptr LeftObjPtr, fc_intptr RightObjPtr)
{
	// 这个地方，需要处理一下，如果是FVector这类的，感觉要调用对应的比较函数
	return FCGetObj::GetIns()->EqualValue(LeftObjPtr, RightObjPtr);
}

// 反射类的对象属性读取
int WrapUClassGetAttrib(fc_intptr L)
{
	fc_intptr VM = fc_get_vm_ptr(L);
	int nClassName = fc_get_current_call_class_name_id(L);
	int nFuncName = fc_get_current_call_class_function_name_id(L);
	
	// 只支持对象属性，这里不支持全局属性
	fc_intptr ThisPtr = fc_get_inport_obj_ptr(L); // this 对象
	fc_intptr ret_ptr = fc_get_return_ptr(L);  // 返回值(需要设置的对象)
	FCObjRef  *ObjRef = FCGetObj::GetIns()->FindValue(ThisPtr);
	if(ObjRef)
	{
		FCDynamicClassDesc *ClassDesc = ObjRef->ClassDesc;
		FCDynamicProperty *DynamicProperty = ClassDesc->FindAttribByID(nFuncName);
		if(!DynamicProperty)
		{
			const char *AttribName = fc_cpp_get_current_call_class_function_name(L);
			DynamicProperty = ClassDesc->RegisterProperty(AttribName, nFuncName);
		}
		if(DynamicProperty && ObjRef->IsValid())
		{	
			uint8 *ObjAddr = (uint8 *)(ObjRef->GetPropertyAddr());
			uint8 *ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
			UObject *ThisObj = ObjRef->GetUObject();
			DynamicProperty->m_WriteScriptFunc(VM, ret_ptr, DynamicProperty, ValueAddr, ThisObj, ObjRef);
		}
	}
	return 0;
}
// 反射类的对象属性设置
int WrapUClassSetAttrib(fc_intptr L)
{
	fc_intptr VM = fc_get_vm_ptr(L);
	int nClassName = fc_get_current_call_class_name_id(L);
	int nFuncName = fc_get_current_call_class_function_name_id(L);
	
	// 只支持对象属性，这里不支持全局属性
	fc_intptr ThisPtr = fc_get_inport_obj_ptr(L); // this 对象
	fc_intptr value_ptr = fc_get_param_ptr(L, 0);

	FCObjRef  *ObjRef = FCGetObj::GetIns()->FindValue(ThisPtr);
	if(ObjRef)
	{
		FCDynamicClassDesc *ClassDesc = ObjRef->ClassDesc;
		FCDynamicProperty *DynamicProperty = ClassDesc->FindAttribByID(nFuncName);
		if(!DynamicProperty)
		{
			const char *AttribName = fc_cpp_get_current_call_class_function_name(L);
			DynamicProperty = ClassDesc->RegisterProperty(AttribName, nFuncName);
		}
		if(DynamicProperty && ObjRef->IsValid())
		{	
			uint8 *ObjAddr = (uint8 *)(ObjRef->GetPropertyAddr());
			uint8* ValueAddr = ObjAddr + DynamicProperty->Offset_Internal;
			UObject* ThisObj = ObjRef->GetUObject();
			DynamicProperty->m_ReadScriptFunc(VM, value_ptr, DynamicProperty, ValueAddr, ThisObj, ObjRef);
		}
	}
	return 0;
}
// 反射类的对象属性 += 重载
int WrapUClassAddSetAttrib(fc_intptr L)
{
	return 0;
}
// 反射类的对象属性 -= 重载
int WrapUClassSubSetAttrib(fc_intptr L)
{
	return 0;
}

FCDynamicFunction  *GetCallClassFunc(fc_intptr L, UObject *&ThisObject )
{
    int nClassName = fc_get_current_call_class_name_id(L);
    int nFuncName = fc_get_current_call_class_function_name_id(L);
    fc_intptr ThisPtr = fc_get_inport_obj_ptr(L);

    ThisObject = nullptr;
    FCDynamicFunction  *DynamicFunc = nullptr;
    if (ThisPtr)
    {
        FCObjRef  *ObjRef = FCGetObj::GetIns()->FindValue(ThisPtr);
        if (ObjRef)
        {
            FCDynamicClassDesc *ClassDesc = ObjRef->ClassDesc;
            DynamicFunc = ClassDesc->FindFunctionByID(nFuncName);
            if (!DynamicFunc)
            {
                const char *FuncName = fc_cpp_get_current_call_class_function_name(L);
                DynamicFunc = ClassDesc->RegisterFunc(FuncName, nFuncName);
            }
            ThisObject = ObjRef->GetUObject();
        }
    }
    else
    {
        // 全局函数
        FCDynamicClassDesc* ClassDesc = GetScriptContext()->FindClassByID(nClassName);
        if (!ClassDesc)
        {
            const char* ClassName = fc_cpp_get_current_call_class_name(L);  // 这个只是单纯的UEClass，不是脚本的，这里创建的也是UE对象
            ClassDesc = GetScriptContext()->RegisterWrapClass(ClassName, nClassName);
        }
        if (ClassDesc)
        {
            DynamicFunc = ClassDesc->FindFunctionByID(nFuncName);
            if (!DynamicFunc)
            {
                const char* FuncName = fc_cpp_get_current_call_class_function_name(L);
                DynamicFunc = ClassDesc->RegisterFunc(FuncName, nFuncName);
            }
            if (ClassDesc->m_Class)
            {
                ThisObject = ClassDesc->m_Class->GetDefaultObject();                // get CDO for static function
            }
        }
    }
    return DynamicFunc;
}

void   WrapNativeCallFunction(fc_intptr L, int ParamIndex, UObject *ThisObject, FCDynamicFunction  *DynamicFunc, uint8 *Buffer, int BufferSize, FNativeFuncPtr NativeFuncPtr)
{
    if (!ThisObject)
    {
        return;
    }

    fc_intptr VM = fc_get_vm_ptr(L);
    int  StackSize = DynamicFunc->ParmsSize;
    UFunction* Function = DynamicFunc->Function;
    int nParamCount = DynamicFunc->ParamCount;
    FCDynamicProperty* BeginProperty = DynamicFunc->m_Property.data();
    FCDynamicProperty* EndProperty = BeginProperty + nParamCount;
    FCDynamicProperty* DynamicProperty = BeginProperty;
    uint8* Frame = Buffer;
    int nAllBuffSize = Function->PropertiesSize + Function->ParmsSize;
    if (nAllBuffSize > BufferSize)
    {
        Frame = (uint8*)FMemory_Alloca(nAllBuffSize);
    }
    FMemory::Memzero(Frame, nAllBuffSize);

    int Index = ParamIndex;
    fc_intptr  ValuePtr = 0;
    uint8  *Locals = Buffer;
    uint8  *ValueAddr = Locals;
    // 将脚本参数传给UE的函数
    for (; DynamicProperty < EndProperty; ++DynamicProperty, ++Index)
    {
        ValuePtr = fc_get_param_ptr(L, Index);
        ValueAddr = Locals + DynamicProperty->Offset_Internal;
        DynamicProperty->Property->InitializeValue(ValueAddr);
        DynamicProperty->bTempNeedRef = true;
        DynamicProperty->bTempRealRef = false;
        DynamicProperty->m_ReadScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, nullptr, nullptr);
        DynamicProperty->bTempNeedRef = false;
    }

    if (DynamicFunc->ReturnPropertyIndex >= 0)
    {
        DynamicProperty = BeginProperty + DynamicFunc->ReturnPropertyIndex;
        ValueAddr = Locals + DynamicProperty->Offset_Internal;
        DynamicProperty->Property->InitializeValue(ValueAddr);
    }

    FFrame NewStack(ThisObject, Function, Frame, NULL, GetChildProperties(Function));
    const bool bHasReturnParam = Function->ReturnValueOffset != MAX_uint16;
    uint8* ReturnValueAddress = bHasReturnParam ? ((uint8*)Frame + Function->ReturnValueOffset) : nullptr;
    if(NativeFuncPtr)
    {
        NativeFuncPtr(ThisObject, NewStack, ReturnValueAddress);
    }
    else
    {
        //Function->Invoke(ThisObject, NewStack, ReturnValueAddress);
        ThisObject->UObject::ProcessEvent(Function, Frame);
    }

    //ThisObject->UObject::ProcessEvent(Function, Frame);

    // 拷贝返回给脚本的变量
    if (DynamicFunc->bOuter)
    {
        DynamicProperty = BeginProperty;
        Index = 0;
        for (; DynamicProperty < EndProperty; ++DynamicProperty, ++Index)
        {
            if (DynamicProperty->bOuter && !DynamicProperty->Property->HasAnyPropertyFlags(CPF_ConstParm))
            {
                ValuePtr = fc_get_param_ptr(L, Index);
                ValueAddr = Locals + DynamicProperty->Offset_Internal;
                DynamicProperty->m_WriteScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, nullptr, nullptr);
            }
        }
    }

    // 释放临时变量
    DynamicProperty = BeginProperty;
    for (; DynamicProperty < EndProperty; ++DynamicProperty, ++Index)
    {
        ValueAddr = Locals + DynamicProperty->Offset_Internal;
        if (!DynamicProperty->bTempRealRef)
            DynamicProperty->Property->DestroyValue(ValueAddr);
    }

    // 将返回值传给脚本
    if (DynamicFunc->ReturnPropertyIndex >= 0)
    {
        DynamicProperty = BeginProperty + DynamicFunc->ReturnPropertyIndex;
        ValuePtr = fc_get_return_ptr(L);
        //ValueAddr = Locals + DynamicProperty->Offset_Internal;
        ValueAddr = ReturnValueAddress;
        DynamicProperty->m_WriteScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, nullptr, nullptr);
        DynamicProperty->Property->DestroyValue(ValueAddr);
    }

    // 释放临时内存
    if (Frame != Buffer)
    {
    }
}

// 反射类的对象函数调用
int WrapUClassFunction(fc_intptr L)
{
	// 成员函数
	UObject *ThisObject = nullptr;
	FCDynamicFunction  *DynamicFunc = GetCallClassFunc(L, ThisObject);
	if(DynamicFunc)
	{
        if(!ThisObject)
        {
            //if(FUNC_Static != (FUNC_Static & DynamicFunc->Function->FunctionFlags))
            //{
            //    return 0;
            //}
            return 0;
        }
		uint8   Buffer[256];
        WrapNativeCallFunction(L, 0, ThisObject, DynamicFunc, Buffer, sizeof(Buffer), nullptr);
	}
	return 0;
}

void   RunTimeRegisterScript(FCScriptContext *Context)
{
	fc_intptr  VM = Context->m_ScriptVM;
	fc_register_reflex_class_cast(VM, WrapUClassCast);
	fc_register_reflex_class_new(VM, WrapUClassNew);
	fc_register_reflex_class_del(VM, WrapUClassDel);
	fc_register_reflex_class_release_ref(VM, WrapUClassReleaseRef);
	fc_register_reflex_class_hash(VM, WrapUClassHash);
	fc_register_reflex_class_equal(VM, WrapUClassEqual);
	fc_register_reflex_class_attrib_ex(VM, WrapUClassGetAttrib, WrapUClassSetAttrib, WrapUClassAddSetAttrib, WrapUClassSubSetAttrib);
	fc_register_reflex_class_func(VM, WrapUClassFunction);
}
