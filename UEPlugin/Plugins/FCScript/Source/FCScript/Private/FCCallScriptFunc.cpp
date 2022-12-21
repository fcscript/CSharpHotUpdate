#include "FCCallScriptFunc.h"
#include "UObject/UnrealType.h"
#include "Templates/Casts.h"
#include "Engine/Level.h"
#include "Engine/Blueprint.h"
#include "GameFramework/Actor.h"
#include "GameFramework/Pawn.h"
#include "Components/PrimitiveComponent.h"
#include "Components/StaticMeshComponent.h"
#include "FCTemplateType.h"

#include "FCGetObj.h"

//---------------------------------------------------------------------------------
void  PushScriptDefault(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	// 什么也不做，进到这里也许是一个错误，不支持的类型噢
}
void  PushScriptBool(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_bool(ValuePtr, *((bool*)ValueAddr));
}
void  PushScriptInt8(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_byte(ValuePtr, *((fc_byte*)ValueAddr));
}
void  PushScriptInt16(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_short(ValuePtr, *((short*)ValueAddr));
}
void  PushScriptInt32(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_int(ValuePtr, *((int32*)ValueAddr));
}
void  PushScriptInt64(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_int64(ValuePtr, *((fc_int64*)ValueAddr));
}
void  PushScriptFloat(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_float(ValuePtr, *((float*)ValueAddr));
}
void  PushScriptDouble(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_double(ValuePtr, *((double*)ValueAddr));
}
void  PushScriptFName(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    FString value = ((FName*)ValueAddr)->ToString();
    fc_set_value_string_w(ValuePtr, (fc_ushort_ptr)(*value), value.Len());
}
void  PushScriptFString(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FString &value = *((FString*)ValueAddr);
	fc_set_value_string_w(ValuePtr, (fc_ushort_ptr)(*value), value.Len());
}
void  PushScriptFText(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    FString value = ((FText*)ValueAddr)->ToString();
    fc_set_value_string_w(ValuePtr, (fc_ushort_ptr)(*value), value.Len());
}
void  PushScriptFVector(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_vector3(ValuePtr, *((fc_vector3*)ValueAddr));
}
void  PushScriptFVector2D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_vector2(ValuePtr, *((fc_vector2*)ValueAddr));
}
void  PushScriptFVector4D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_set_value_vector4(ValuePtr, *((fc_vector4*)ValueAddr));
}

// 功能：根据脚本变量得到UE对象的描述类
FCDynamicClassDesc* GetScriptValueClassDesc(int64 VM, int64  ValuePtr)
{
	int nClassName = fc_get_wrap_class_name_id(ValuePtr);
	FCDynamicClassDesc* ClassDesc = GetScriptContext()->FindClassByID(nClassName);
	if (!ClassDesc)
	{
		const char* ClassName = fc_cpp_get_wrap_class_name(VM, ValuePtr);
		if (ClassName)
		{
			ClassDesc = GetScriptContext()->RegisterWrapClass(ClassName, nClassName);
		}
	}
	return ClassDesc;
}

bool  IsCanCastToScript(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty)
{
	FCDynamicClassDesc *ClassDesc = GetScriptValueClassDesc(VM, ValuePtr);
	if(!ClassDesc)
	{
		return false;
	}

	UStruct *PropertyStruct = DynamicProperty->Property->GetOwnerStruct();
	if( ClassDesc->m_Struct != PropertyStruct)
	{
		// 必须是子类(这个地方如果会慢的话，可以用hash_map优化)
		if(PropertyStruct->IsChildOf(ClassDesc->m_Struct))
		{
			return true;
		}

		const char* Name = GetUEClassName(ClassDesc->m_UEClassName);
        if(DynamicProperty->Name == Name || strcmp(DynamicProperty->Name, Name) == 0)
		{
			return true;
		}
		return false;
	}
	return true;
}

// 将UE对象写入脚本对象
void  PushScriptStruct(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FStructProperty* StructProperty = (FStructProperty*)DynamicProperty->Property;
	FCDynamicClassDesc* ClassDesc = GetScriptValueClassDesc(VM, ValuePtr);
    if(ClassDesc)
    {
        if (ClassDesc->m_Struct == StructProperty->Struct)
        {
            fc_intptr PtrID = fc_get_value_wrap_objptr(ValuePtr);
            //if (PtrID)
            //{
            //	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(PtrID);
            //	if (ObjRef)
            //	{
            //		StructProperty->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, StructProperty->ArrayDim);
            //	}
            //}
            //else
            {
                if (ThisObj)
                {
                    PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
                }
                else
                {
                    FCObjRef* ParentRef = (FCObjRef*)ObjRefPtr;
                    if (ParentRef)
                        PtrID = FCGetObj::GetIns()->PushChildProperty(ParentRef, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
                    else
                        PtrID = FCGetObj::GetIns()->PushStructValue((const FCDynamicProperty*)DynamicProperty, ValueAddr);
                }
                fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
            }
        }
        else
        {
            fc_set_value_wrap_objptr(VM, ValuePtr, 0);
        }
    }
    else
    {
        LPPushScriptValueFunc   ReadScriptFunc = nullptr;    // 将脚本变量写入到UE对象
        int Type = fc_get_value_type(ValuePtr);
        const char* UEClassName = "";
        switch (Type)
        {
        case FC_VALUE_TYPE_VECTOR2:
            UEClassName = "FVector2D";
            ReadScriptFunc = PushScriptFVector2D;
            break;
        case FC_VALUE_TYPE_VECTOR3:
            UEClassName = "FVector";
            ReadScriptFunc = PushScriptFVector;
            break;
        case FC_VALUE_TYPE_VECTOR4:
            UEClassName = "FVector4";
            ReadScriptFunc = PushScriptFVector4D;
            break;
        default:
            break;
        }
        ClassDesc = GetScriptContext()->RegisterUClass(UEClassName);
        if (ClassDesc && ClassDesc->m_Struct == StructProperty->Struct)
        {
            ReadScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, ThisObj, ObjRefPtr);
        }
        else
        {
            fc_set_value_wrap_objptr(VM, ValuePtr, 0);
        }
    }
}

void  PushScriptUObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{	
	UObject *value = *((UObject **)ValueAddr);

	FObjectProperty* ObjectProperty = (FObjectProperty*)DynamicProperty->Property;
	UClass* InClass = value ? value->GetClass() : UObject::StaticClass();
	UClass* Class = ObjectProperty->PropertyClass;
	//if (ObjectProperty->PropertyClass->IsChildOf(UClass::StaticClass()))
	//{
	//	FClassProperty* ClassProperty = (FClassProperty*)ObjectProperty;
	//	UClass* tempClass = Cast<UClass>(ObjectProperty->GetPropertyValue(ValueAddr));
	//	UClass* MetaClass = tempClass ? tempClass : ClassProperty->MetaClass;
	//	int iii = 0;
	//}
	//else
	//{
	//	UObject* Object = ObjectProperty->GetPropertyValue(ValueAddr);
	//	UClass* tempClass = Object ? Object->GetClass() : ObjectProperty->PropertyClass;
	//	int iii = 0;
	//}

	if (Class == InClass || Class->IsChildOf(InClass))
	{
		fc_intptr PtrID = FCGetObj::GetIns()->PushUObject(value);
		fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
	}
	else
	{
		fc_set_value_wrap_objptr(VM, ValuePtr, 0);
	}

	// 做类型检查
	//if(IsCanCastToScript(VM, ValuePtr, DynamicProperty))
	//{
	//	fc_intptr PtrID = FCGetObj::GetIns()->PushUObject(value);
	//	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
	//}
	//else
	//{
	//	fc_set_value_wrap_objptr(VM, ValuePtr, 0);
	//}
}

void  PushScriptCppPtr(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
	fc_intptr PtrID = FCGetObj::GetIns()->PushCppPtr(ValueAddr);
	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptMapIterator(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    fc_intptr PtrID = FCGetObj::GetIns()->PushMapIterator(ValueAddr);
    fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptWeakObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FWeakObjectPtr *WeakPtr = (FWeakObjectPtr *)ValueAddr;
	fc_intptr PtrID = 0;
	if(ThisObj)
	{
		PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);		
	}
	else
	{
		PtrID = FCGetObj::GetIns()->PushTemplate((const FCDynamicProperty*)DynamicProperty, ValueAddr, EFCObjRefType::NewTWeakPtr);
	}
	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptLazyObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FLazyObjectPtr *LazyPtr = (FLazyObjectPtr *)ValueAddr;
	fc_intptr PtrID = 0;
	if(ThisObj)
	{
		PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);		
	}
	else
	{
		PtrID = FCGetObj::GetIns()->PushTemplate((const FCDynamicProperty*)DynamicProperty, ValueAddr, EFCObjRefType::NewTLazyPtr);
	}
	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptDelegate(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FStructProperty* StructProperty = (FStructProperty*)DynamicProperty->Property;

	fc_intptr PtrID = 0;
	// 如果变量是UObject的属性
	if (ThisObj)
	{
		PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
	}
	else
	{
		PtrID = FCGetObj::GetIns()->PushStructValue((const FCDynamicProperty*)DynamicProperty, ValueAddr);
	}
	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptTArray(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
	FArrayProperty* Property = (FArrayProperty*)DynamicProperty->Property;
	int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if(ObjRef)
	{
		if(ObjRef->GetPropertyAddr() == ValueAddr)
		{
			return ;
		}
		if(EFCObjRefType::NewTArray == ObjRef->RefType)
		{
            Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
			return ;
		}
		else if(EFCObjRefType::RefProperty == ObjRef->RefType)
		{			
			UStruct* Struct = DynamicProperty->Property->GetOwnerStruct();
            if(ObjRef->GetPropertyType() == DynamicProperty->Type)
			{
                Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
				return ;
			}
		}
	}

	fc_intptr PtrID = 0;
	// 如果变量是UObject的属性
	if (ThisObj)
	{
		PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
	}
	else
	{
        ObjID = FCGetObj::GetIns()->PushNewTArray((const FCDynamicProperty*)DynamicProperty, ValueAddr);
	}
	fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptTMap(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    FMapProperty* Property = (FMapProperty*)DynamicProperty->Property;
    int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        if (ObjRef->GetPropertyAddr() == ValueAddr)
        {
            return;
        }
        if (EFCObjRefType::NewTMap == ObjRef->RefType)
        {
            Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
            return;
        }
        else if (EFCObjRefType::RefProperty == ObjRef->RefType)
        {
            UStruct* Struct = DynamicProperty->Property->GetOwnerStruct();
            if (ObjRef->GetPropertyType() == DynamicProperty->Type)
            {
                Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
                return;
            }
        }
    }

    fc_intptr PtrID = 0;
    // 如果变量是UObject的属性
    if (ThisObj)
    {
        PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
    }
    else
    {
        ObjID = FCGetObj::GetIns()->PushNewTMap((const FCDynamicProperty*)DynamicProperty, ValueAddr);
    }
    fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  PushScriptTSet(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    FSetProperty* Property = (FSetProperty*)DynamicProperty->Property;
    int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        if (ObjRef->GetPropertyAddr() == ValueAddr)
        {
            return;
        }
        if (EFCObjRefType::NewTSet == ObjRef->RefType)
        {
            Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
            return;
        }
        else if (EFCObjRefType::RefProperty == ObjRef->RefType)
        {
            UStruct* Struct = DynamicProperty->Property->GetOwnerStruct();
            if (ObjRef->GetPropertyType() == DynamicProperty->Type)
            {
                Property->CopyValuesInternal(ObjRef->GetPropertyAddr(), ValueAddr, DynamicProperty->Property->ArrayDim);
                return;
            }
        }
    }

    fc_intptr PtrID = 0;
    // 如果变量是UObject的属性
    if (ThisObj)
    {
        PtrID = FCGetObj::GetIns()->PushProperty(ThisObj, (const FCDynamicProperty*)DynamicProperty, ValueAddr);
    }
    else
    {
        ObjID = FCGetObj::GetIns()->PushNewTSet((const FCDynamicProperty*)DynamicProperty, ValueAddr);
    }
    fc_set_value_wrap_objptr(VM, ValuePtr, PtrID);
}

void  InitDynamicPropertyWriteFunc(FCDynamicProperty *DynamicProperty, FCPropertyType Flag)
{
	if(DynamicProperty->m_WriteScriptFunc)
	{
		return ;
	}
	DynamicProperty->m_WriteScriptFunc = PushScriptDefault;
	switch(Flag)
	{
		case FCPROPERTY_BoolProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptBool;
			break;
		case FCPROPERTY_Int8Property:
		case FCPROPERTY_ByteProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptInt8;
			break;
		case FCPROPERTY_Int16Property:
			DynamicProperty->m_WriteScriptFunc = PushScriptInt16;
			break;
		case FCPROPERTY_IntProperty:
		case FCPROPERTY_UInt32Property:
			DynamicProperty->m_WriteScriptFunc = PushScriptInt32;
			break;
		case FCPROPERTY_FloatProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptFloat;
			break;
		case FCPROPERTY_DoubleProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptDouble;
			break;
		case FCPROPERTY_Int64Property:
		case FCPROPERTY_UInt64Property:
			DynamicProperty->m_WriteScriptFunc = PushScriptInt64;
			break;
		case FCPROPERTY_NameProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptFName;
			break;
		case FCPROPERTY_StrProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptFString;
			break;
        case FCPROPERTY_TextProperty:
            DynamicProperty->m_WriteScriptFunc = PushScriptFText;
            break;
		case FCPROPERTY_ObjectProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptUObject;
			break;
		case FCPROPERTY_WeakObjectPtr:
			DynamicProperty->m_WriteScriptFunc = PushScriptWeakObject;
			break;
		case FCPROPERTY_LazyObjectPtr:
			DynamicProperty->m_WriteScriptFunc = PushScriptLazyObject;
			break;
		case FCPROPERTY_StructProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptStruct;
			break;
		case FCPROPERTY_Vector2:
			DynamicProperty->m_WriteScriptFunc = PushScriptFVector2D;
			break;
		case FCPROPERTY_Vector3:
			DynamicProperty->m_WriteScriptFunc = PushScriptFVector;
			break;
		case FCPROPERTY_Vector4:
			DynamicProperty->m_WriteScriptFunc = PushScriptFVector4D;
			break;
		case FCPROPERTY_Array:
			DynamicProperty->m_WriteScriptFunc = PushScriptTArray;
			break;
        case FCPROPERTY_Map:
            DynamicProperty->m_WriteScriptFunc = PushScriptTMap;
            break;
        case FCPROPERTY_Set:
            DynamicProperty->m_WriteScriptFunc = PushScriptTSet;
            break;
		case FCPROPERTY_DelegateProperty:
		case FCPROPERTY_MulticastDelegateProperty:
			DynamicProperty->m_WriteScriptFunc = PushScriptDelegate;
			break;
		default:
			break;
	}
}

//---------------------------------------------------------------------------------

void  ReadScriptDefault(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
}

void  ReadScriptBool(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((bool *)ValueAddr) = fc_get_value_bool(ValuePtr);
}

void  ReadScriptInt8(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((uint8 *)ValueAddr) = fc_get_value_byte(ValuePtr);
}
void  ReadScriptInt16(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((uint16 *)ValueAddr) = fc_get_value_ushort(ValuePtr);
}
void  ReadScriptInt32(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((uint32 *)ValueAddr) = fc_get_value_uint(ValuePtr);
}
void  ReadScriptInt64(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((int64 *)ValueAddr) = fc_get_value_int64(ValuePtr);
}
void  ReadScriptFloat(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((float *)ValueAddr) = fc_get_value_float(ValuePtr);
}
void  ReadScriptDouble(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((double *)ValueAddr) = fc_get_value_double(ValuePtr);
}
void  ReadScriptFName(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    *((FName*)ValueAddr) = FName((WIDECHAR*)fc_cpp_get_value_string_w(VM, ValuePtr));
}
void  ReadScriptFString(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	*((FString*)ValueAddr) = fc_cpp_get_value_string_w(VM, ValuePtr);
}
void  ReadScriptFText(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    *((FText*)ValueAddr) = FText::FromString(fc_cpp_get_value_string_w(VM, ValuePtr));
}
void  ReadScriptFVector2D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_get_value_vector2(ValuePtr, *((fc_vector2*)ValueAddr));
}
void  ReadScriptFVector(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    fc_get_value_vector3(ValuePtr, *((fc_vector3*)ValueAddr));
}
void  ReadScriptFVector4D(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	fc_get_value_vector4(ValuePtr, *((fc_vector4*)ValueAddr));
}
// 将脚本对象写入到UE对象
void  ReadScriptStruct(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FStructProperty* StructProperty = (FStructProperty*)DynamicProperty->Property;
	fc_intptr PtrID = fc_get_value_wrap_objptr(ValuePtr);
    if(PtrID)
    {
        FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(PtrID);
        if (ObjRef && ObjRef->ClassDesc && ObjRef->ClassDesc->m_Struct == StructProperty->Struct)
        {
            StructProperty->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), StructProperty->ArrayDim);
        }
    }
    else
    {
        LPPushScriptValueFunc   ReadScriptFunc = nullptr;    // 将脚本变量写入到UE对象
        int Type = fc_get_value_type(ValuePtr);
        const char *UEClassName = "";
        switch(Type)
        {
            case FC_VALUE_TYPE_VECTOR2:
                UEClassName = "FVector2D";
                ReadScriptFunc = ReadScriptFVector2D;
            break;
            case FC_VALUE_TYPE_VECTOR3:
                UEClassName = "FVector";
                ReadScriptFunc = ReadScriptFVector;
                break;
            case FC_VALUE_TYPE_VECTOR4:
                UEClassName = "FVector4";
                ReadScriptFunc = ReadScriptFVector4D;
                break;
            default:
                break;
        }
        FCDynamicClassDesc* ClassDesc = GetScriptContext()->RegisterUClass(UEClassName);
        if(ClassDesc && ClassDesc->m_Struct == StructProperty->Struct)
        {
            ReadScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, ThisObj, ObjRefPtr);
        }
    }
}
void  ReadScriptUObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FObjectProperty* ObjectProperty = (FObjectProperty*)DynamicProperty->Property;
	fc_intptr PtrID = fc_get_value_wrap_objptr(ValuePtr);
	UObject *SrcObj = FCGetObj::GetIns()->GetUObject(PtrID);
	UClass *InClass = SrcObj ? SrcObj->GetClass() : UObject::StaticClass();
	UClass *Class = ObjectProperty->PropertyClass;
	if(Class == InClass || InClass->IsChildOf(Class))
	{
		*((UObject**)ValueAddr) = SrcObj;
	}
	else
	{
		*((UObject**)ValueAddr) = nullptr;
	}
}

void  ReadScriptCppPtr(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
	// 理论上不能走到这里来，什么也不做吧
	fc_intptr PtrID = fc_get_value_wrap_objptr(ValuePtr);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(PtrID);
	if(ObjRef && ObjRef->RefType == EFCObjRefType::CppPtr)
	{
		*((void**)ValueAddr) = ObjRef->GetThisAddr();
	}
}

void  ReadScriptMapIterator(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    fc_intptr PtrID = fc_get_value_wrap_objptr(ValuePtr);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(PtrID);
    if (ObjRef && ObjRef->RefType == EFCObjRefType::MapIterator)
    {
        *((void**)ValueAddr) = ObjRef->GetThisAddr();
    }
}

void  ReadScriptWeakObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FWeakObjectPtr *WeakPtr = (FWeakObjectPtr *)ValueAddr;

	int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if(ObjRef)
	{
		if (FCPropertyType::FCPROPERTY_WeakObjectPtr == ObjRef->DynamicProperty->Type)
		{
			FWeakObjectPtr* ScriptPtr = (FWeakObjectPtr*)ObjRef->GetPropertyAddr();
			*WeakPtr = ScriptPtr->Get();
		}
		else if (FCPROPERTY_ObjectProperty == ObjRef->DynamicProperty->Type)
		{
			*WeakPtr = ObjRef->GetUObject();
		}
	}
}
void  ReadScriptLazyObject(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase *DynamicProperty, uint8  *ValueAddr, UObject *ThisObj, void* ObjRefPtr)
{
	FLazyObjectPtr *LazyPtr = (FLazyObjectPtr *)ValueAddr;
	int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if (ObjRef)
	{
		if (FCPropertyType::FCPROPERTY_LazyObjectPtr == ObjRef->DynamicProperty->Type)
		{
			FLazyObjectPtr* ScriptPtr = (FLazyObjectPtr*)ObjRef->GetPropertyAddr();
			*LazyPtr = ScriptPtr->Get();
		}
		else if (FCPROPERTY_ObjectProperty == ObjRef->DynamicProperty->Type)
		{
			*LazyPtr = ObjRef->GetUObject();
		}
	}
}
void  ReadScriptTArray(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
	int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
	FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
	if (ObjRef)
	{
		if(EFCObjRefType::NewTArray == ObjRef->RefType)
		{
			FArrayProperty* Property = (FArrayProperty*)DynamicProperty->Property;
            //Property->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
            if (DynamicProperty->bTempNeedRef)
            {
                ((FCDynamicPropertyBase*)DynamicProperty)->bTempRealRef = true;
                FMemory::Memcpy(ValueAddr, ObjRef->GetPropertyAddr(), sizeof(FScriptMap));
            }
            else
            {
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
            }
		}
		else if(DynamicProperty->Type == FCPropertyType::FCPROPERTY_Array)
		{
			FArrayProperty* Property = (FArrayProperty*)DynamicProperty->Property;
            if (ObjRef->GetPropertyType() == DynamicProperty->Type)
			{
				//ArrayProperty->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
			}
		}
	}
}

void ReadScriptTMap(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        if (EFCObjRefType::NewTMap == ObjRef->RefType)
        {
            FMapProperty* Property = (FMapProperty*)DynamicProperty->Property;
            //Property->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
            if (DynamicProperty->bTempNeedRef)
            {
                ((FCDynamicPropertyBase*)DynamicProperty)->bTempRealRef = true;
                FMemory::Memcpy(ValueAddr, ObjRef->GetPropertyAddr(), sizeof(FScriptMap));
            }
            else
            {
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
            }
        }
        else if (DynamicProperty->Type == FCPropertyType::FCPROPERTY_Map)
        {
            FMapProperty* Property = (FMapProperty*)DynamicProperty->Property;
            if (ObjRef->GetPropertyType() == DynamicProperty->Type)
            {
                //ArrayProperty->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
            }
        }
    }
}

void ReadScriptTSet(int64 VM, int64  ValuePtr, const FCDynamicPropertyBase* DynamicProperty, uint8* ValueAddr, UObject* ThisObj, void* ObjRefPtr)
{
    int64 ObjID = fc_get_value_wrap_objptr(ValuePtr);
    FCObjRef* ObjRef = FCGetObj::GetIns()->FindValue(ObjID);
    if (ObjRef)
    {
        if (EFCObjRefType::NewTSet == ObjRef->RefType)
        {
            FMapProperty* Property = (FMapProperty*)DynamicProperty->Property;
            //Property->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
            if (DynamicProperty->bTempNeedRef)
            {
                ((FCDynamicPropertyBase*)DynamicProperty)->bTempRealRef = true;
                FMemory::Memcpy(ValueAddr, ObjRef->GetPropertyAddr(), sizeof(FScriptMap));
            }
            else
            {
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
            }
        }
        else if (DynamicProperty->Type == FCPropertyType::FCPROPERTY_Set)
        {
            FMapProperty* Property = (FMapProperty*)DynamicProperty->Property;
            if (ObjRef->GetPropertyType() == DynamicProperty->Type)
            {
                //ArrayProperty->CopyValuesInternal(ValueAddr, ObjRef->GetPropertyAddr(), DynamicProperty->Property->ArrayDim);
                Property->CopyCompleteValue(ValueAddr, ObjRef->GetPropertyAddr());
            }
        }
    }
}

void  InitDynamicPropertyReadFunc(FCDynamicProperty *DynamicProperty, FCPropertyType Flag)
{
	if(DynamicProperty->m_ReadScriptFunc)
	{
		return ;
	}
	DynamicProperty->m_ReadScriptFunc = ReadScriptDefault;
	switch(Flag)
	{
		case FCPROPERTY_BoolProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptBool;
			break;
		case FCPROPERTY_Int8Property:
		case FCPROPERTY_ByteProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptInt8;
			break;
		case FCPROPERTY_Int16Property:
			DynamicProperty->m_ReadScriptFunc = ReadScriptInt16;
			break;
		case FCPROPERTY_IntProperty:
		case FCPROPERTY_UInt32Property:
			DynamicProperty->m_ReadScriptFunc = ReadScriptInt32;
			break;
		case FCPROPERTY_FloatProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFloat;
			break;
		case FCPROPERTY_DoubleProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptDouble;
			break;
		case FCPROPERTY_Int64Property:
		case FCPROPERTY_UInt64Property:
			DynamicProperty->m_ReadScriptFunc = ReadScriptInt64;
			break;
		case FCPROPERTY_NameProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFName;
			break;
		case FCPROPERTY_StrProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFString;
			break;
        case FCPROPERTY_TextProperty:
            DynamicProperty->m_ReadScriptFunc = ReadScriptFText;
            break;
		case FCPROPERTY_ObjectProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptUObject;
			break;
		case FCPROPERTY_WeakObjectPtr:
			DynamicProperty->m_ReadScriptFunc = ReadScriptWeakObject;
			break;
		case FCPROPERTY_LazyObjectPtr:
			DynamicProperty->m_ReadScriptFunc = ReadScriptLazyObject;
			break;
		case FCPROPERTY_StructProperty:
			DynamicProperty->m_ReadScriptFunc = ReadScriptStruct;
			break;
		case FCPROPERTY_Vector2:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFVector2D;
			break;
		case FCPROPERTY_Vector3:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFVector;
			break;
		case FCPROPERTY_Vector4:
			DynamicProperty->m_ReadScriptFunc = ReadScriptFVector4D;
			break;
		case FCPROPERTY_Array:
			DynamicProperty->m_ReadScriptFunc = ReadScriptTArray;
			break;
        case FCPROPERTY_Map:
            DynamicProperty->m_ReadScriptFunc = ReadScriptTMap;
            break;
        case FCPROPERTY_Set:
            DynamicProperty->m_ReadScriptFunc = ReadScriptTSet;
            break;
		default:
			break;
	}
}

//---------------------------------------------------------------------------------
void  PushScriptValue(FCScriptContext* Context, const bool& value)
{
	fc_set_value_bool(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const int8& value)
{
	fc_set_value_byte(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const int16& value)
{
	fc_set_value_short(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const int32& value)
{
	fc_set_value_int(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const int64& value)
{
	fc_set_value_int64(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const float& value)
{
	fc_set_value_float(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const double& value)
{
	fc_set_value_double(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const char * value)
{
	fc_set_value_string(Context->m_TempValuePtr, value);
}

void  PushScriptValue(FCScriptContext* Context, const FString& value)
{
	fc_set_value_string_w(Context->m_TempValuePtr, (fc_ushort_ptr)(*value), value.Len());
}

void  PushScriptValue(FCScriptContext* Context, const FName& value)
{
	FString tempValue(value.ToString());
	fc_set_value_string_w(Context->m_TempValuePtr, (fc_ushort_ptr)(*tempValue), tempValue.Len());
}

void  PushScriptValue(FCScriptContext* Context, const FVector& value)
{
	fc_set_value_vector3(Context->m_TempValuePtr, *((const fc_vector3*)&value));
}

void  PushScriptValue(FCScriptContext* Context, const FVector2D& value)
{
	fc_set_value_vector2(Context->m_TempValuePtr, *((const fc_vector2*)&value));
}

void  PushScriptValue(FCScriptContext* Context, const FVector4& value)
{
	fc_set_value_vector4(Context->m_TempValuePtr, *((const fc_vector4*)&value));
}

void  PushScriptValue(FCScriptContext* Context, const UObject* value)
{
	int64  ObjID = FCGetObj::GetIns()->PushUObject((UObject *)value);
	fc_set_value_wrap_objptr(Context->m_ScriptVM, Context->m_TempValuePtr, (fc_intptr)ObjID);
}

//---------------------------------------------------------------------------------
fc_intptr  GlbCallKey = 0;
fc_intptr  QueryCallKey()
{
    return ++GlbCallKey;
}

//---------------------------------------------------------------------------------
void  FCInnerCallScriptFunc(FCScriptContext* Context, UObject *Object, int64 ScriptIns, int64 ParamPtr, FCDynamicFunction* DynamicFunction, FFrame& TheStack, fc_intptr Callkey)
{
	int64  VM = Context->m_ScriptVM;
	if(!VM)
	{
		return ;
	}
	int nParamCount = DynamicFunction->ParamCount;
	const FCDynamicProperty *BeginProperty = DynamicFunction->m_Property.data();
	const FCDynamicProperty *EndProperty = BeginProperty + nParamCount;
	const FCDynamicProperty *DynamicProperty = BeginProperty;

	uint8  *Locals = TheStack.Locals;
	uint8  *ValueAddr = Locals;
	// 输入脚本参数
	int Index = 0;
	fc_intptr  ValuePtr = 0;
	if(ParamPtr)
	{
		for (; DynamicProperty < EndProperty; ++DynamicProperty, ++Index)
		{
			ValuePtr = fc_get_script_param(ParamPtr, Index);
			ValueAddr = Locals + DynamicProperty->Offset_Internal;
			DynamicProperty->m_WriteScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, nullptr, nullptr);
		}
	}

	// 执行脚本代码
	fc_intptr  ReturnPtr = fc_ue_call(VM, Callkey);

	// 如果有返回值的话(这里脚本层限制上栈变量的访问范围，不可越过当前函数的设置)
	if(ParamPtr && DynamicFunction->bOuter)
	{
		DynamicProperty = BeginProperty;
		Index = 0;
		for (; DynamicProperty < EndProperty; ++DynamicProperty, ++Index)
		{
			if(DynamicProperty->bOuter)
			{
				ValuePtr = fc_get_param_ptr(ParamPtr, Index);
				ValueAddr = Locals + DynamicProperty->Offset_Internal;
				DynamicProperty->m_ReadScriptFunc(VM, ValuePtr, DynamicProperty, ValueAddr, nullptr, nullptr);
			}
		}
	}

	// 拷贝返回值
	if( ReturnPtr != 0 && DynamicFunction->ReturnPropertyIndex >= 0)
	{
		ValuePtr = fc_get_param_ptr(ReturnPtr, 0);
		const FCDynamicProperty *ReturnProperty = BeginProperty + DynamicFunction->ReturnPropertyIndex;
		ValueAddr = Locals + ReturnProperty->Offset_Internal;
		ReturnProperty->m_ReadScriptFunc(VM, ValuePtr, ReturnProperty, ValueAddr, nullptr, nullptr);
	}
	fc_end_ue_call(VM, Callkey);
}
bool  FCCallScriptFunc(FCScriptContext* Context, UObject *Object, int64 ScriptIns, const char *ScriptFuncName, FCDynamicFunction* DynamicFunction, FFrame& TheStack)
{
	int64  VM = Context->m_ScriptVM;
	if(!VM)
	{
		return false;
	}
	bool bValidClassFunc = fc_find_class_func(VM, ScriptIns, ScriptFuncName);
	if(ScriptIns)
	{
		if(!bValidClassFunc)
		{
			return false;
		}
	}
    fc_intptr Callkey = QueryCallKey();
	fc_intptr ParamPtr = fc_prepare_ue_call(VM, ScriptIns, ScriptFuncName, Callkey);
	FCInnerCallScriptFunc(Context, Object, ScriptIns, ParamPtr, DynamicFunction, TheStack, Callkey);
    return true;
}

void  FCCallScriptDelegate(FCScriptContext *Context, UObject *Object, int64 ScriptIns, int nClassNameID, int nFuncNameID, FCDynamicFunction* DynamicFunction, FFrame& TheStack)
{
	int64  VM = Context->m_ScriptVM;
	if(VM)
    {
        fc_intptr Callkey = QueryCallKey();
		fc_intptr ParamPtr = fc_prepare_ue_fast_call(VM, ScriptIns, nClassNameID, nFuncNameID, Callkey);
		FCInnerCallScriptFunc(Context, Object, ScriptIns, ParamPtr, DynamicFunction, TheStack, Callkey);
	}
}