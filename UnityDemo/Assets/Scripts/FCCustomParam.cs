using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// 这个可以由项目者自己实现相关接口


class FCCustomParam
{
    //----------------------------------------------------------------------------
    public static List<bool> GetList(ref List<bool> rList, long L, int nIndex)
    {
        if (rList == null)
            rList = new List<bool>();
        else
            rList.Clear();
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        //for(int i = 0; i< nArraySize; ++i)
        //{
        //    long  pNode = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
        //    rList.Add(FCLibHelper.fc_get_value_bool(pNode));
        //}
        bool[] buffer = new bool[nArraySize];
        FCLibHelper.fc_get_array_bool(ptr, buffer, 0, nArraySize);
        rList.AddRange(buffer);
        return rList;
    }
    public static List<byte> GetList(ref List<byte> rList, long L, int nIndex)
    {
        if (rList == null)
            rList = new List<byte>();
        else
            rList.Clear();
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        byte[] buffer = new byte[nArraySize];
        FCLibHelper.fc_get_array_byte(ptr, buffer, 0, nArraySize);
        rList.AddRange(buffer);
        return rList;
    }
    public static List<int> GetList(ref List<int> rList, long L, int nIndex)
    {
        if (rList == null)
            rList = new List<int>();
        else
            rList.Clear();
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        int[] buffer = new int[nArraySize];
        FCLibHelper.fc_get_array_int(ptr, buffer, 0, nArraySize);
        rList.AddRange(buffer);
        return rList;
    }
    public static List<Rect> GetList(ref List<Rect> rList, long L, int nIndex)
    {
        if (rList == null)
            rList = new List<Rect>();
        else
            rList.Clear();
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        Rect[] buffer = new Rect[nArraySize];
        FCLibHelper.fc_get_array_rect(ptr, buffer, 0, nArraySize);
        rList.AddRange(buffer);
        return rList;
    }

    //----------------------------------------------------------------------------
    public static byte[] GetArray(ref byte[] rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new byte[nArraySize];
        FCLibHelper.fc_get_array_byte(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static int []GetArray(ref int []rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new int[nArraySize];
        FCLibHelper.fc_get_array_int(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static Vector2[] GetArray(ref Vector2[] rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new Vector2[nArraySize];
        FCLibHelper.fc_get_array_vector2(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static Color[] GetArray(ref Color[] rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new Color[nArraySize];
        FCLibHelper.fc_get_array_color(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static Color32[] GetArray(ref Color32[] rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new Color32[nArraySize];
        FCLibHelper.fc_get_array_color32(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static Rect[] GetArray(ref Rect[] rList, long L, int nIndex)
    {
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nArraySize = FCLibHelper.fc_get_array_size(ptr);
        rList = new Rect[nArraySize];
        FCLibHelper.fc_get_array_rect(ptr, rList, 0, nArraySize);
        return rList;
    }
    public static Texture2D[] GetArray(ref Texture2D[] rList, long L, int nIndex)
    {
        // 这个暂时是不支持的噢
        return rList;
    }

    //----------------------------------------------------------------------------
    public static Dictionary<int, int> GetDictionary(ref Dictionary<int, int> rList, long L, int nIndex)
    {
        if (rList == null)
            rList = new Dictionary<int, int>();
        else
            rList.Clear();
        long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
        int nCount = FCLibHelper.fc_get_map_size(ptr);
        FCLibHelper.fc_map_prepare_view(ptr);
        for(; nCount > 0; ++nCount)
        {
            FCLibHelper.fc_map_to_next_pair();
            long key_ptr = FCLibHelper.fc_map_get_cur_key_ptr();
            long value_ptr = FCLibHelper.fc_map_get_cur_value_ptr();
            int key = FCLibHelper.fc_get_value_int(key_ptr);
            int value = FCLibHelper.fc_get_value_int(value_ptr);
            rList[key] = value;
        }
        return rList;
    }
    public static Dictionary<long, long> GetDictionary(ref Dictionary<long, long> rList, long L, int nIndex)
    {
        return rList;
    }
    public static Dictionary<long, string> GetDictionary(ref Dictionary<long, string> rList, long L, int nIndex)
    {
        return rList;
    }
    //----------------------------------------------------------------------------

    public static void PushReturnList(List<int> rList)
    {
        // 目前暂时不支持在返回值中返回List
    }

    public static void PushReturnDictionary(Dictionary<int, int> rList)
    {
    }
    
    //----------------------------------------------------------------------------
}