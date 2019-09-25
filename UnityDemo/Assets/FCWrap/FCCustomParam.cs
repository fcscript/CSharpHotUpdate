using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
    public static List<Component> GetList(ref List<Component> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<Component>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Component item = FCGetObj.GetObj<Component>(item_ptr);
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
    public static Texture2D[] GetArray(ref Texture2D[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Texture2D[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Texture2D item = FCGetObj.GetObj<Texture2D>(item_ptr);
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
    public static Type[] GetArray(ref Type[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Type[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Type item = FCGetObj.GetObj<Type>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Material[] GetArray(ref Material[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Material[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Material item = FCGetObj.GetObj<Material>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static List<ReflectionProbeBlendInfo> GetList(ref List<ReflectionProbeBlendInfo> rList, long L, int nIndex)
    {
        try
        {
            if (rList == null)
                rList = new List<ReflectionProbeBlendInfo>();
            else
                rList.Clear();
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                ReflectionProbeBlendInfo item = FCGetObj.GetObj<ReflectionProbeBlendInfo>(item_ptr);
                rList.Add(item);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Transform[] GetArray(ref Transform[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Transform[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Transform item = FCGetObj.GetObj<Transform>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static AccelerationEvent[] GetArray(ref AccelerationEvent[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new AccelerationEvent[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                AccelerationEvent item = FCGetObj.GetObj<AccelerationEvent>(item_ptr);
                rList[i] = item;
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return rList;
    }
    public static Touch[] GetArray(ref Touch[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Touch[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Touch item = FCGetObj.GetObj<Touch>(item_ptr);
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
    public static float[] GetArray(ref float[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new float[nArraySize];
            FCLibHelper.fc_get_array_float(ptr, rList, 0, nArraySize);
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
    public static Vector4[] GetArray(ref Vector4[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Vector4[nArraySize];
            FCLibHelper.fc_get_array_vector4(ptr, rList, 0, nArraySize);
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
    public static Matrix4x4[] GetArray(ref Matrix4x4[] rList, long L, int nIndex)
    {
        try
        {
            long ptr = FCLibHelper.fc_get_param_ptr(L, nIndex);
            int nArraySize = FCLibHelper.fc_get_array_size(ptr);
            rList = new Matrix4x4[nArraySize];
            for (int i = 0; i < nArraySize; ++i)
            {
                long item_ptr = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
                Matrix4x4 item = new Matrix4x4();
                FCLibHelper.fc_get_value_matrix(item_ptr, ref item);
                rList[i] = item;
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
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Component []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
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
    public static void ReturnArray(GameObject []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Material []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Transform []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(AccelerationEvent []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Touch []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
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
    public static void ReturnArray(CommandBuffer []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
            }
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }
    public static void ReturnArray(Light []rList, long ptr)
    {
        try
        {
            int nCount = rList != null ? rList.Length : 0;
            FCLibHelper.fc_set_array_size(ptr, nCount);
            for(int i = 0; i<nCount; ++i)
            {
                long pItem = FCLibHelper.fc_get_array_node_temp_ptr(ptr, i);
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

