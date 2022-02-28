#pragma once

#include <vector>
#include <string>
#include <hash_map>

#include "../StringCore/FDList.h"

template<> struct stdext::hash_compare<const char *>
{
    enum
    {	// parameters for hash table
        bucket_size = 1		// 0 < bucket_size
    };
    size_t operator()(const char * Key) const
    {	// hash _Keyval to size_t value by pseudorandomizing transform
        if(!Key)
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

    bool operator()(const char * key1, const char * key2) const
    {	// test if _Keyval1 ordered before _Keyval2
        if(!key1)
            key1 = "";
        if(!key2)
            key2 = "";
        return strcmp(key1, key2) < 0;
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