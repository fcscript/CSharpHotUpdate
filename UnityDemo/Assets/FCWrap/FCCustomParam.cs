using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;


class FCCustomParam
{
    public static byte[] GetArray(ref byte[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new byte[nArraySize];
            FCLibHelper.fc_get_array_byte(ptr, rList, 0, nArraySize);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static int[] GetArray(ref int[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new int[nArraySize];
            FCLibHelper.fc_get_array_int(ptr, rList, 0, nArraySize);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<byte> GetList(ref List<byte> rList, long L, int nIndex)
    {
        try
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
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<int> GetList(ref List<int> rList, long L, int nIndex)
    {
        try
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
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Vector2> GetList(ref List<Vector2> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Vector2>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            Vector2[] buffer = new Vector2[nArraySize];
            FCLibHelper.fc_get_array_vector2(ptr, buffer, 0, nArraySize);
            rList.AddRange(buffer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Vector3> GetList(ref List<Vector3> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Vector3>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            Vector3[] buffer = new Vector3[nArraySize];
            FCLibHelper.fc_get_array_vector3(ptr, buffer, 0, nArraySize);
            rList.AddRange(buffer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Vector4> GetList(ref List<Vector4> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Vector4>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            Vector4[] buffer = new Vector4[nArraySize];
            FCLibHelper.fc_get_array_vector4(ptr, buffer, 0, nArraySize);
            rList.AddRange(buffer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<UnityEngine.Component> GetList(ref List<UnityEngine.Component> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<UnityEngine.Component>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Component item = FCGetObj.GetObj<UnityEngine.Component>(item_ptr);
                rList.Add(item);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Color[] GetArray(ref Color[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Color[nArraySize];
            FCLibHelper.fc_get_array_color(ptr, rList, 0, nArraySize);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Color32[] GetArray(ref Color32[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Color32[nArraySize];
            FCLibHelper.fc_get_array_color32(ptr, rList, 0, nArraySize);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static UnityEngine.Texture2D[] GetArray(ref UnityEngine.Texture2D[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new UnityEngine.Texture2D[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Texture2D item = FCGetObj.GetObj<UnityEngine.Texture2D>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Vector2[] GetArray(ref Vector2[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Vector2[nArraySize];
            FCLibHelper.fc_get_array_vector2(ptr, rList, 0, nArraySize);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Rect> GetList(ref List<Rect> rList, long L, int nIndex)
    {
        try
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
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static System.Type[] GetArray(ref System.Type[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new System.Type[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                System.Type item = FCGetObj.GetObj<System.Type>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static UnityEngine.Material[] GetArray(ref UnityEngine.Material[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new UnityEngine.Material[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Material item = FCGetObj.GetObj<UnityEngine.Material>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<UnityEngine.Rendering.ReflectionProbeBlendInfo> GetList(ref List<UnityEngine.Rendering.ReflectionProbeBlendInfo> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<UnityEngine.Rendering.ReflectionProbeBlendInfo>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Rendering.ReflectionProbeBlendInfo item = FCGetObj.GetObj<UnityEngine.Rendering.ReflectionProbeBlendInfo>(item_ptr);
                rList.Add(item);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static UnityEngine.Transform[] GetArray(ref UnityEngine.Transform[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new UnityEngine.Transform[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Transform item = FCGetObj.GetObj<UnityEngine.Transform>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static UnityEngine.AccelerationEvent[] GetArray(ref UnityEngine.AccelerationEvent[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new UnityEngine.AccelerationEvent[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.AccelerationEvent item = FCGetObj.GetObj<UnityEngine.AccelerationEvent>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static UnityEngine.Touch[] GetArray(ref UnityEngine.Touch[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new UnityEngine.Touch[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                UnityEngine.Touch item = FCGetObj.GetObj<UnityEngine.Touch>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static string[] GetArray(ref string[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new string[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                string item = FCLibHelper.fc_get_value_string_a(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<float> GetList(ref List<float> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<float>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            float[] buffer = new float[nArraySize];
            FCLibHelper.fc_get_array_float(ptr, buffer, 0, nArraySize);
            rList.AddRange(buffer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Color> GetList(ref List<Color> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Color>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            Color[] buffer = new Color[nArraySize];
            FCLibHelper.fc_get_array_color(ptr, buffer, 0, nArraySize);
            rList.AddRange(buffer);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<Matrix4x4> GetList(ref List<Matrix4x4> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Matrix4x4>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Matrix4x4 item = new Matrix4x4();
                FCLibHelper.fc_get_value_matrix(item_ptr, ref item);
                rList.Add(item);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static void ReturnArray(byte []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_byte(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnList(List<byte> rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Count : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_byte(pItem, rList[i]);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityObject []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Component []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Color []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_color(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Color32 []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_color32(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Rect []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_rect(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.GameObject []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Material []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Transform []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.AccelerationEvent []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Touch []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(string []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_string_a(pItem, rList[i]);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Rendering.CommandBuffer []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(UnityEngine.Light []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                FCLibHelper.fc_set_value_intptr(pItem, FCGetObj.PushObj(rList[i]));
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(float []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_float(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Vector4 []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_vector4(ptr, rList, 0, nCount);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Matrix4x4 []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            Matrix4x4 v;
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                v = rList[i];
                FCLibHelper.fc_set_value_matrix(pItem, ref v);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
}

