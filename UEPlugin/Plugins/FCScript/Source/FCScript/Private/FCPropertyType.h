#pragma once
#include "UObject/UnrealType.h"
#include "UObject/ObjectMacros.h"
#include "CoreUObject.h"

enum FCPropertyType
{
	FCPROPERTY_Unkonw,          // 未知的
	FCPROPERTY_BoolProperty,    // bool
	FCPROPERTY_Int8Property,    // int8
	FCPROPERTY_ByteProperty,    // UByteProperty
	FCPROPERTY_Int16Property,   // int16
	FCPROPERTY_IntProperty,     // int32
	FCPROPERTY_UInt32Property,  // uint32
	FCPROPERTY_Int64Property,   // int64
	FCPROPERTY_UInt64Property,  // uint64
	FCPROPERTY_FloatProperty,   // float
	FCPROPERTY_DoubleProperty,  // double
	FCPROPERTY_NumericProperty,  // double ? int ? or other ?

	FCPROPERTY_Vector2,         // 扩展类型(本质还是FCPROPERTY_StructProperty)
	FCPROPERTY_Vector3,
	FCPROPERTY_Vector4,

	FCPROPERTY_FILED,			// UField
	FCPROPERTY_Enum,            // enum
	FCPROPERTY_ScriptStruct,    // struct
	FCPROPERTY_Class,           // class

	FCPROPERTY_ClassProperty,   // 
	FCPROPERTY_InterfaceProperty, 
	FCPROPERTY_NameProperty,    // FName
	FCPROPERTY_StrProperty,     // FString
    FCPROPERTY_TextProperty,    // FText
	FCPROPERTY_Property,        // 
	FCPROPERTY_ObjectProperty,  // UObject*  // CPT_ObjectReference // CPT_WeakObjectReference // CPT_LazyObjectReference
	FCPROPERTY_WeakObjectPtr,   // FWeakObjectProperty, TWeakObjectPtr<_Ty>, FWeakObjectPtr
	FCPROPERTY_LazyObjectPtr,   // FLazyObjectProperty, TLazyObjectPtr<_Ty>, FLazyObjectPtr
	FCPROPERTY_Interface,       // FInterfaceProperty
	FCPROPERTY_SoftObjectReference, // CPT_SoftObjectReference
	FCPROPERTY_Function,        // Fucntion
	FCPROPERTY_StructProperty,  // Struct, FVector 也是这个类型噢
	FCPROPERTY_Array,           // TArray
	FCPROPERTY_Map,             // TMap
	FCPROPERTY_Set,             // TSet -- FSetProperty
	FCPROPERTY_DelegateProperty, // CPT_Delegate
	FCPROPERTY_MulticastDelegateProperty, // CPT_MulticastDelegate
	FCPROPERTY_ObjectPropertyBase, // 
};

#if (ENGINE_MAJOR_VERSION < 5) && (ENGINE_MINOR_VERSION < 25)
#define OLD_UE_ENGINE 1
#else
#define OLD_UE_ENGINE 0
#endif


#if OLD_UE_ENGINE
#define CastField Cast
#define GetPropertyOuter(Property) (Property)->GetOuter()
#define GetChildProperties(Function) (Function)->Children

typedef UProperty FProperty;
typedef UByteProperty FByteProperty;
typedef UInt8Property FInt8Property;
typedef UInt16Property FInt16Property;
typedef UIntProperty FIntProperty;
typedef UInt64Property FInt64Property;
typedef UUInt16Property FUInt16Property;
typedef UUInt32Property FUInt32Property;
typedef UUInt64Property FUInt64Property;
typedef UFloatProperty FFloatProperty;
typedef UDoubleProperty FDoubleProperty;
typedef UNumericProperty FNumericProperty;
typedef UEnumProperty FEnumProperty;
typedef UBoolProperty FBoolProperty;
typedef UObjectPropertyBase FObjectPropertyBase;
typedef UObjectProperty FObjectProperty;
typedef UClassProperty FClassProperty;
typedef UWeakObjectProperty FWeakObjectProperty;
typedef ULazyObjectProperty FLazyObjectProperty;
typedef USoftObjectProperty FSoftObjectProperty;
typedef USoftClassProperty FSoftClassProperty;
typedef UInterfaceProperty FInterfaceProperty;
typedef UNameProperty FNameProperty;
typedef UStrProperty FStrProperty;
typedef UTextProperty FTextProperty;
typedef UArrayProperty FArrayProperty;
typedef UMapProperty FMapProperty;
typedef USetProperty FSetProperty;
typedef UStructProperty FStructProperty;
typedef UDelegateProperty FDelegateProperty;
typedef UMulticastDelegateProperty FMulticastDelegateProperty;
#if ENGINE_MINOR_VERSION > 22
typedef UMulticastInlineDelegateProperty FMulticastInlineDelegateProperty;
typedef UMulticastSparseDelegateProperty FMulticastSparseDelegateProperty;
#endif
#else
#define GetPropertyOuter(Property) (Property)->GetOwnerUObject()
#define GetChildProperties(Function) (Function)->ChildProperties
#endif

void  InitPropertyTable();

void  ReleasePropertyTable();

const char* GetConstName(const char* InName);

// 功能：得到反射属性的类型（脚本所支持的）
FCPropertyType  GetScriptPropertyType(const FProperty *Property);