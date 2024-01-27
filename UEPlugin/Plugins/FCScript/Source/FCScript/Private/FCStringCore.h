#pragma once

#include <vector>
#include <string>
#include <unordered_map>

#include "../StringCore/FDList.h"

struct fc_hash_string_key
{
    static size_t  Hash(const char* Key)
    {
        if (!Key)
        {
            return 0;
        }
        size_t  nHash = 0;
        while (*Key)
        {
            nHash = (nHash << 5) + nHash + *Key++;
        }
        return nHash;
    }
    static bool Equal(const char* Key1, const char* Key2)
    {
        if (!Key1)
            Key1 = "";
        if (!Key2)
            Key2 = "";
        return strcmp(Key1, Key2) == 0;
    }
    static bool Less(const char* Key1, const char* Key2)
    {
        if (!Key1)
            Key1 = "";
        if (!Key2)
            Key2 = "";
        return strcmp(Key1, Key2) < 0;
    }
};

//template<> struct std::hash<const char*>
//{
//    size_t operator()(const char* Key) const
//    {
//        return fc_hash_string_key::Hash(Key);
//    }
//};
//
//template<> struct std::equal_to<const char*>
//{
//    // this is operator ==
//    bool operator()(const char* key1, const char* key2) const
//    {
//        return fc_hash_string_key::Equal(key1, key2);
//    }
//};

struct FCStringHash
{
    size_t operator()(const char* Key) const
    {
        return fc_hash_string_key::Hash(Key);
    }
};

struct FCStringEqual
{
    bool operator()(const char* key1, const char* key2) const
    {
        return fc_hash_string_key::Equal(key1, key2);
    }
};

struct FCDoubleKey
{
    const char* KeyType;
    const char* ValueType;
    FCDoubleKey() :KeyType(nullptr), ValueType(nullptr) {}
    FCDoubleKey(const char* InKeyType, const char* InValueType) :KeyType(InKeyType), ValueType(InValueType) {}
};

template<> struct std::hash<FCDoubleKey>
{
    size_t operator()(const FCDoubleKey& Key) const
    {
        return fc_hash_string_key::Hash(Key.KeyType) + fc_hash_string_key::Hash(Key.ValueType);
    }
};

template<> struct std::equal_to<FCDoubleKey>
{
    bool operator()(const FCDoubleKey& key1, const FCDoubleKey& key2) const
    {
        return fc_hash_string_key::Equal(key1.KeyType, key2.KeyType) && fc_hash_string_key::Equal(key1.ValueType, key2.ValueType);
    }
};

struct ObjRefKey
{
    const unsigned char* ParentAddr;    // 
    const unsigned char* OffsetPtr;    //
    ObjRefKey() :ParentAddr(nullptr), OffsetPtr(nullptr) {}
    ObjRefKey(const void* InParentAddr, const void* InOffsetPtr) : ParentAddr((const unsigned char*)InParentAddr), OffsetPtr((const unsigned char*)InOffsetPtr) {}
};

template<> struct std::hash<ObjRefKey>
{
    size_t operator()(const ObjRefKey& Key) const
    {
        return (size_t)Key.ParentAddr + (size_t)Key.OffsetPtr;
    }
};
template<> struct std::equal_to<ObjRefKey>
{
    bool operator()(const ObjRefKey& key1, const ObjRefKey& key2) const
    {
        return key1.ParentAddr == key2.ParentAddr
            && key1.OffsetPtr == key2.OffsetPtr;
    }
};


template <class _TyPtrMap>
void  ReleasePtrMap(_TyPtrMap &PtrMap)
{
	while (PtrMap.size() > 0)
	{
		_TyPtrMap::iterator itPtr = PtrMap.begin();
		auto Ptr = itPtr->second;
		PtrMap.erase(itPtr);
        if(Ptr)
        {
		    delete Ptr;
        }
	}
}

#ifdef UE_BUILD_DEBUG
#define  FC_ASSERT(exp)  if(exp) { UE_DEBUG_BREAK(); }
#else
#define  FC_ASSERT(exp)  
#endif
