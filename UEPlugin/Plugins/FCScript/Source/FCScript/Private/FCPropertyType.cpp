#include "FCPropertyType.h"
#include "FCStringCore.h"

typedef  std::unordered_map<void*, FCPropertyType>   CPropertyTypeMap;
typedef  std::unordered_map<FCPropertyType, const char*>   CPropertyClassNameMap;
typedef std::unordered_map<const char*, char*, FCStringHash, FCStringEqual> CCppName2NameMap;
typedef  std::unordered_map<FName, FCPropertyType, FCFNameHash, FCFNameEqual>   CFName2PropertyTypeMap;
typedef std::unordered_map <FCDoubleKey, int>  CDoubleName2IntMap;
CPropertyTypeMap  gPropertyTypeMap;
CFName2PropertyTypeMap  gInnerGraphyTypeMap;
CPropertyTypeMap gCachePropertyTypeMap;
CPropertyClassNameMap gPropertyClassNameMap;
CCppName2NameMap  GCppName2NameMap;
CDoubleName2IntMap    GDoubleName2IntMap;

void  InitPropertyTable()
{
	if(!gPropertyTypeMap.empty())
	{
		return ;
	}
	gPropertyTypeMap[FBoolProperty::StaticClass()]   = FCPROPERTY_BoolProperty;
	gPropertyTypeMap[FByteProperty::StaticClass()]   = FCPROPERTY_ByteProperty;
	gPropertyTypeMap[FInt8Property::StaticClass()]   = FCPROPERTY_Int8Property;
	gPropertyTypeMap[FInt16Property::StaticClass()]  = FCPROPERTY_Int16Property;
	gPropertyTypeMap[FIntProperty::StaticClass()]    = FCPROPERTY_IntProperty;
	gPropertyTypeMap[FUInt32Property::StaticClass()] = FCPROPERTY_UInt32Property;
	gPropertyTypeMap[FInt64Property::StaticClass()]  = FCPROPERTY_Int64Property;
	gPropertyTypeMap[FFloatProperty::StaticClass()]  = FCPROPERTY_FloatProperty;
	gPropertyTypeMap[FDoubleProperty::StaticClass()] = FCPROPERTY_DoubleProperty;
	gPropertyTypeMap[FNumericProperty::StaticClass()] = FCPROPERTY_NumericProperty;

	gPropertyTypeMap[FProperty::StaticClass()]       = FCPROPERTY_FILED;
	gPropertyTypeMap[FEnumProperty::StaticClass()]   = FCPROPERTY_Enum;
	gPropertyTypeMap[FStructProperty::StaticClass()] = FCPROPERTY_ScriptStruct;
	gPropertyTypeMap[UClass::StaticClass()]          = FCPROPERTY_Class;
	gPropertyTypeMap[FClassProperty::StaticClass()]  = FCPROPERTY_ClassProperty;

	gPropertyTypeMap[FObjectProperty::StaticClass()] = FCPROPERTY_ObjectProperty;
#if ENGINE_MAJOR_VERSION >= 5
    gPropertyTypeMap[FObjectPtrProperty::StaticClass()] = FCPROPERTY_ObjectPtrProperty; // TObjectPtr<class>
#endif
	gPropertyTypeMap[FWeakObjectProperty::StaticClass()] = FCPROPERTY_WeakObjectPtr;
	gPropertyTypeMap[FLazyObjectProperty::StaticClass()] = FCPROPERTY_LazyObjectPtr;
	gPropertyTypeMap[FInterfaceProperty::StaticClass()] = FCPROPERTY_Interface;

	gPropertyTypeMap[FSoftObjectProperty::StaticClass()]  = FCPROPERTY_SoftObjectReference;
    gPropertyTypeMap[FSoftClassProperty::StaticClass()] = FCPROPERTY_SoftClassReference;

	gPropertyTypeMap[FNameProperty::StaticClass()]   = FCPROPERTY_NameProperty;
	gPropertyTypeMap[FStrProperty::StaticClass()]    = FCPROPERTY_StrProperty;
    gPropertyTypeMap[FTextProperty::StaticClass()] = FCPROPERTY_TextProperty;

	gPropertyTypeMap[FStructProperty::StaticClass()] = FCPROPERTY_StructProperty;
	gPropertyTypeMap[FArrayProperty::StaticClass()]  = FCPROPERTY_Array;
	gPropertyTypeMap[FMapProperty::StaticClass()]    = FCPROPERTY_Map;
	gPropertyTypeMap[FSetProperty::StaticClass()]    = FCPROPERTY_Set;
	gPropertyTypeMap[FDelegateProperty::StaticClass()] = FCPROPERTY_DelegateProperty;
	gPropertyTypeMap[FMulticastDelegateProperty::StaticClass()] = FCPROPERTY_MulticastDelegateProperty;
	#if OLD_UE_ENGINE == 0
	gPropertyTypeMap[FMulticastInlineDelegateProperty::StaticClass()] = FCPROPERTY_MulticastDelegateProperty;
	gPropertyTypeMap[FMulticastSparseDelegateProperty::StaticClass()] = FCPROPERTY_MulticastSparseDelegateProperty;
	#endif

    gInnerGraphyTypeMap["Vector2"] = FCPROPERTY_Vector2;
    gInnerGraphyTypeMap["Vector3"] = FCPROPERTY_Vector3;
    gInnerGraphyTypeMap["Vector4"] = FCPROPERTY_Vector4;
    gInnerGraphyTypeMap["Vector2D"] = FCPROPERTY_Vector2;
    gInnerGraphyTypeMap["Vector4D"] = FCPROPERTY_Vector4;
    gInnerGraphyTypeMap["Vector"] = FCPROPERTY_Vector3;
}

void  InitProperyNameTable()
{
    if (!gPropertyClassNameMap.empty())
    {
        return;
    }
    gPropertyClassNameMap[FCPROPERTY_BoolProperty] = "bool";
    gPropertyClassNameMap[FCPROPERTY_ByteProperty] = "byte";
    gPropertyClassNameMap[FCPROPERTY_Int8Property] = "int8";
    gPropertyClassNameMap[FCPROPERTY_Int16Property] = "int16";
    gPropertyClassNameMap[FCPROPERTY_IntProperty] = "int";
    gPropertyClassNameMap[FCPROPERTY_UInt32Property] = "uint";
    gPropertyClassNameMap[FCPROPERTY_Int64Property] = "int64";
    gPropertyClassNameMap[FCPROPERTY_FloatProperty] = "float";
    gPropertyClassNameMap[FCPROPERTY_DoubleProperty] = "double";
    gPropertyClassNameMap[FCPROPERTY_NumericProperty] = "double";
    gPropertyClassNameMap[FCPROPERTY_FILED] = "FProperty";
    gPropertyClassNameMap[FCPROPERTY_Enum] = "FEnumProperty";
    //gPropertyClassNameMap[FCPROPERTY_ScriptStruct] = "UStruct";
    //gPropertyClassNameMap[FCPROPERTY_Class] = "UClass";
    //gPropertyClassNameMap[FCPROPERTY_ClassProperty] = "FClassProperty";
    gPropertyClassNameMap[FCPROPERTY_ObjectProperty] = "FObject";
    gPropertyClassNameMap[FCPROPERTY_WeakObjectPtr] = "TWeakObjectPtr";
    gPropertyClassNameMap[FCPROPERTY_LazyObjectPtr] = "TLazyObjectPtr";
    gPropertyClassNameMap[FCPROPERTY_Interface] = "Interface";
    gPropertyClassNameMap[FCPROPERTY_SoftObjectReference] = "TSoftObjectPtr";
    gPropertyClassNameMap[FCPROPERTY_SoftClassReference] = "TSoftClassPtr";
    gPropertyClassNameMap[FCPROPERTY_NameProperty] = "FName";
    gPropertyClassNameMap[FCPROPERTY_StrProperty] = "FString";
    gPropertyClassNameMap[FCPROPERTY_TextProperty] = "FText";
    //gPropertyClassNameMap[FCPROPERTY_StructProperty] = "UStruct";
    gPropertyClassNameMap[FCPROPERTY_Array] = "TArray";
    gPropertyClassNameMap[FCPROPERTY_Map] = "TMap";
    gPropertyClassNameMap[FCPROPERTY_Set] = "TSet";
    gPropertyClassNameMap[FCPROPERTY_DelegateProperty] = "DelegateEvent";
    gPropertyClassNameMap[FCPROPERTY_MulticastDelegateProperty] = "MulticastDelegateEvent";

#if OLD_UE_ENGINE == 0
    gPropertyClassNameMap[FCPROPERTY_MulticastDelegateProperty] = "MulticastDelegateEvent";
    gPropertyClassNameMap[FCPROPERTY_MulticastSparseDelegateProperty] = "MulticastSparseDelegateEvent";
#endif
}

void  ReleasePropertyTable()
{
	gPropertyTypeMap.clear();
    gPropertyClassNameMap.clear();
    gInnerGraphyTypeMap.clear();
	gCachePropertyTypeMap.clear();
    ReleasePtrMap(GCppName2NameMap);
    GDoubleName2IntMap.clear();
}

const char* GetConstName(const char* InName)
{
    if (!InName)
        return "";
    CCppName2NameMap::iterator itName = GCppName2NameMap.find(InName);
    if (itName != GCppName2NameMap.end())
    {
        return itName->second;
    }
    int  Len = strlen(InName);
    char* buffer = new char[Len + 1];
    memcpy(buffer, InName, Len);
    buffer[Len] = 0;
    GCppName2NameMap[buffer] = buffer;
    return buffer;
}

FCPropertyType  GetScriptPropertyType(const FProperty *Property)
{
    InitPropertyTable();
    CPropertyTypeMap::const_iterator itFind = gPropertyTypeMap.find(Property->GetClass());
    if (itFind != gPropertyTypeMap.end())
    {
        FCPropertyType Type = itFind->second;
        if (Type == FCPROPERTY_StructProperty)
        {
            FStructProperty* StructProperty = (FStructProperty*)Property;
            CPropertyTypeMap::iterator itCacheType = gCachePropertyTypeMap.find(StructProperty->Struct);
            if (itCacheType != gCachePropertyTypeMap.end())
            {
                return itCacheType->second;
            }

            UStruct* Super = StructProperty->Struct;
            while (Super)
            {
                FName StructPropertyName = Super->GetFName();
                CFName2PropertyTypeMap::iterator ItInner = gInnerGraphyTypeMap.find(StructPropertyName);
                if (ItInner != gInnerGraphyTypeMap.end())
                {
                    Type = ItInner->second;
                    break;
                }
                Super = Super->GetSuperStruct();
            }

            gCachePropertyTypeMap[StructProperty->Struct] = Type;
        }
        return Type;
    }
    return FCPROPERTY_Unkonw;
}

const char* GetScriptPropertyClassName(FCPropertyType PropertyType, const FProperty* Property)
{
    InitProperyNameTable();
    CPropertyClassNameMap::iterator itName = gPropertyClassNameMap.find(PropertyType);
    if (itName != gPropertyClassNameMap.end())
    {
        return itName->second;
    }
    if (FCPROPERTY_StructProperty == PropertyType)
    {
        FStructProperty* StructProperty = (FStructProperty*)Property;
        const char* Name = TCHAR_TO_UTF8(*StructProperty->Struct->GetName());
        return GetConstName(Name);
    }

    const char* Name = TCHAR_TO_UTF8(*(Property->GetClass()->GetName()));
    return GetConstName(Name);
}

int  GetMapTemplateParamNameID(const FProperty* KeyProperty, const FProperty* ValueProperty)
{
    FCPropertyType  KeyPropertyType = GetScriptPropertyType(KeyProperty);
    FCPropertyType  ValuePropertyType = GetScriptPropertyType(ValueProperty);
    const char* KeyName = GetScriptPropertyClassName(KeyPropertyType, KeyProperty);
    const char* ValueName = GetScriptPropertyClassName(ValuePropertyType, ValueProperty);

    FCDoubleKey  DoubleKey(KeyName, ValueName);
    CDoubleName2IntMap::iterator itFind = GDoubleName2IntMap.find(DoubleKey);
    if (itFind != GDoubleName2IntMap.end())
    {
        return itFind->second;
    }
    int NameID = GDoubleName2IntMap.size() + 1;
    GDoubleName2IntMap[DoubleKey] = NameID;
    return NameID;
}
