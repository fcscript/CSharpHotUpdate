﻿using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

public static class FCExport
{
    [MenuItem("FCScript/导出脚本类", false, 5)]
    static void ExportAll()
    {
        FCClassWrap pWrap = new FCClassWrap();
        pWrap.BeginExport("");

        WrapUnityClass(pWrap);
        WrapCustomClass(pWrap);

        pWrap.EndExport();
    }
    static void WrapUnityClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("Unity");

        //pWrap.WrapClass(typeof(UnityEngine.Transform));
        //pWrap.WrapClass(typeof(UnityEngine.Texture2D));
        //pWrap.WrapClass(typeof(UnityEngine.GameObject));
        //pWrap.WrapClass(typeof(UnityEngine.Transform));
        //pWrap.WrapClass(typeof(UnityEngine.SkinnedMeshRenderer));

        pWrap.EndModleWrap();
    }
    static void WrapCustomClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("Custom");

        pWrap.WrapClass(typeof(TestExport));

        pWrap.EndModleWrap();
    }

    [MenuItem("FCScript/调试C#类型", false, 5)]
    static void LookAll()
    {
        FCValueType v1 = new FCValueType(typeof(TestExport));
        FCValueType v4 = new FCValueType(typeof(TestExport.TestCallback));
        FCValueType v2 = new FCValueType(typeof(Vector2));
        FCValueType v3 = new FCValueType(typeof(Texture2D));

        List<int> aInt = new List<int>();
        GetListIntPtr(aInt);
        LookClass(typeof(TestExport));
        LookClass(typeof(int[]));
        LookClass(typeof(Texture2D));
        LookClass(typeof(UnityEngine.GameObject));
        LookClass(typeof(TextureFormat));
        LookClass(typeof(Texture2D.EXRFlags));
        LookClass(typeof(List<TestExport>));
        LookClass(typeof(Dictionary<int, TestExport>));
        LookClass(typeof(List<bool>));
        LookClass(typeof(List<int>));
    }

    static long GetListIntPtr(List<int> aInt)
    {
        int[] pBuffer = aInt.ToArray();
        IntPtr p = Marshal.GetComInterfaceForObject(pBuffer, typeof(int[]));
        long n2 = p.ToInt64();
        Marshal.Release(p);
        p = Marshal.GetComInterfaceForObject(pBuffer, typeof(int[]));
        long n1 = p.ToInt64();
        Marshal.Release(p);
        // 测试结果是 n1 == n2, 对于普通数组可以这样求指针地址，再传递给C++
        int s1 = sizeof(int);
        int s2 = sizeof(bool);
        int s3 = sizeof(short);
        int s4 = sizeof(long);

        return n2;
    }

    static void LookClass(Type  nClassType)
    {
        Assembly  ab = nClassType.Assembly;
        MethodInfo[] methods = nClassType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (methods == null)
            return;
        string szClassName = nClassType.Name;
        Module  md = nClassType.Module;
        EventInfo[] events = nClassType.GetEvents();
        FieldInfo[] fields = nClassType.GetFields(); // 所有成员变量(只有public的)
        PropertyInfo[] properties = nClassType.GetProperties(); // 属性方法 get/set
        MethodInfo[] allMethods = nClassType.GetMethods();  // 函数+get/set方法
        Type[] subTypes = nClassType.GetNestedTypes(); // 内部类型
        Type[] argtypes = nClassType.GetGenericArguments(); // 模板的参数
        ConstructorInfo[]  conInfos = nClassType.GetConstructors(); // 得到构造函数信息
        string szAllName = nClassType.ToString();
        bool bArray = nClassType.IsArray;
        bool bEnum = nClassType.IsEnum;
        Type nArrayType = nClassType.GetElementType();
        Type RefType = nClassType.ReflectedType;
        string szRefNamespace = RefType != null ? RefType.Namespace : string.Empty;

        MethodInfo pMethod = null;
        ParameterInfo[] pFuncParam = null;
        int nParamCount = 0;
        Type nParamType;
        string szParamType;
        string szRetType;

        //LookAssembly(ab);
        //LookModule(md);

        // get / set 
        if (properties != null)
        {
            Type nVaueType;
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo property = properties[i];
                nVaueType = property.PropertyType;
                // get方法
                if (properties[i].CanRead)
                {
                }
                // set方法
                if (properties[i].CanWrite)
                {
                }
            }
        }

        for (int i = 0; i<methods.Length; ++i)
        {
            pMethod = methods[i];
            // 如果是属性方法
            if (0 != (MethodAttributes.SpecialName & pMethod.Attributes))
            {
                continue;
            }

            pFuncParam = pMethod.GetParameters();
            szRetType = GetTypeDesc(pMethod.ReturnType);
            nParamCount = pFuncParam != null ? pFuncParam.Length : 0;
            for(int k = 0; k<nParamCount; ++k)
            {
                nParamType = pFuncParam[k].ParameterType;
                szParamType = GetTypeDesc(nParamType);
            }
        }
        int iii = 0;
    }
    static void LookAssembly(Assembly am)
    {
        Type[] at = am.GetExportedTypes();

        Module[] amd = am.GetModules();
        if(amd != null)
        {
            for (int i = 0; i < amd.Length; ++i)
            {
                LookModule(amd[i]);
            }
        }
    }

    // 这个函数没有什么用
    static void LookModule(Module md)
    {
        if (md == null)
            return;
        string szName = md.Name;
        FieldInfo[] fields = md.GetFields(); // 所有成员变量
        MethodInfo[] allMethods = md.GetMethods();  // 函数+get/set方法
        if(fields != null)
        {

        }
        if(allMethods != null)
        {

        }
    }
    static string  GetTypeDesc(Type nType)
    {
        string szType = nType.ToString();
        if (nType.Equals(typeof(int)))
            return "int";
        if (nType.Equals(typeof(float)))
            return "float";
        if (nType.Equals(typeof(byte)))
            return "byte";
        if (nType.Equals(typeof(char)))
            return "char";
        if (nType.Equals(typeof(bool)))
            return "bool";
        if (nType.Equals(typeof(short)))
            return "short";
        if (nType.Equals(typeof(ushort)))
            return "short";
        if (nType.Equals(typeof(uint)))
            return "uint";
        if (nType.Equals(typeof(long)))
            return "long";
        if (nType.Equals(typeof(ulong)))
            return "ulong";
        if (nType.Equals(typeof(double)))
            return "double";
        if (nType.Equals(typeof(void)))
            return "void";
        if (nType.Equals(typeof(string)))
            return "string";
        return nType.ToString();
    }
}