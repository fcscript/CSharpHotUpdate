#include "FCPropertyType.h"
#include "FCStringCore.h"

typedef  std::unordered_map<void*, FCPropertyType>   CPropertyTypeMap;
typedef  std::unordered_map<const char *, FCPropertyType, FCStringHash, FCStringEqual>   CGraphyTypeMap;
typedef std::unordered_map<const char*, char*, FCStringHash, FCStringEqual> CCppName2NameMap;
CPropertyTypeMap  gPropertyTypeMap;
CGraphyTypeMap   gGraphyTypeMap;
CPropertyTypeMap gCachePropertyTypeMap;
CCppName2NameMap  GCppName2NameMap;

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
	gPropertyTypeMap[FWeakObjectProperty::StaticClass()] = FCPROPERTY_WeakObjectPtr;
	gPropertyTypeMap[FLazyObjectProperty::StaticClass()] = FCPROPERTY_LazyObjectPtr;
	gPropertyTypeMap[FInterfaceProperty::StaticClass()] = FCPROPERTY_Interface;

	gPropertyTypeMap[FSoftObjectProperty::StaticClass()]  = FCPROPERTY_SoftObjectReference;

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
	gPropertyTypeMap[FMulticastSparseDelegateProperty::StaticClass()] = FCPROPERTY_MulticastDelegateProperty;
	#endif

	gGraphyTypeMap["Vector2"] = FCPROPERTY_Vector2;
	gGraphyTypeMap["Vector3"] = FCPROPERTY_Vector3;
	gGraphyTypeMap["Vector4"] = FCPROPERTY_Vector4;
    gGraphyTypeMap["Vector2D"] = FCPROPERTY_Vector2;
    gGraphyTypeMap["Vector4D"] = FCPROPERTY_Vector4;
    gGraphyTypeMap["Vector"] = FCPROPERTY_Vector3;
}

void  ReleasePropertyTable()
{
	gPropertyTypeMap.clear();
	gGraphyTypeMap.clear();
	gCachePropertyTypeMap.clear();
    ReleasePtrMap(GCppName2NameMap);
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
	if(itFind != gPropertyTypeMap.end())
	{
		FCPropertyType Type = itFind->second;
		if(Type == FCPROPERTY_StructProperty)
		{
			FStructProperty *StructProperty = (FStructProperty *)Property;
			CPropertyTypeMap::iterator itCacheType = gCachePropertyTypeMap.find(StructProperty->Struct);
			if(itCacheType != gCachePropertyTypeMap.end())
			{
				return itCacheType->second;
			}
			FString StructName = StructProperty->Struct->GetName();
            const char *PropertyName = TCHAR_TO_UTF8(*StructName);
			CGraphyTypeMap::iterator itGraphy = gGraphyTypeMap.find(PropertyName);
			if(itGraphy != gGraphyTypeMap.end())
			{
				Type = itGraphy->second;
			}
			gCachePropertyTypeMap[StructProperty->Struct] = Type;
		}
		return Type;
	}
	return FCPROPERTY_Unkonw;
}