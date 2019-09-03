using System;
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
        WrapCustomAttribClass(pWrap); // 导出打有[ClassAutoWrap]标签的类

        pWrap.EndExport();
    }
    [MenuItem("FCScript/清除Wrap脚本", false, 5)]
    static void ClearWrapFile()
    {
        string szDataPath = Application.dataPath;
        string szExportPath = szDataPath + "/FCWrap/";
        string szFCScriptPath = szDataPath.Substring(0, szDataPath.Length - 6) + "Script/inport/";
        FCClassWrap.DeletePath(szExportPath);
        FCClassWrap.DeletePath(szFCScriptPath);
    }
    static void WrapUnityClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("Unity");

        //pWrap.WrapClass(typeof(System.Type));
        pWrap.WrapClass(typeof(UnityEngine.Object));
        pWrap.WrapClass(typeof(UnityEngine.Transform));
        pWrap.WrapClass(typeof(UnityEngine.Component));
        pWrap.WrapClass(typeof(UnityEngine.Texture2D));
        pWrap.WrapClass(typeof(UnityEngine.GameObject));
        pWrap.WrapClass(typeof(UnityEngine.Behaviour));
        pWrap.PushCurrentDontWrapName("Item");
        pWrap.WrapClass(typeof(UnityEngine.Animation));
        pWrap.WrapClass(typeof(UnityEngine.Transform));
        pWrap.WrapClass(typeof(UnityEngine.SkinnedMeshRenderer));
        pWrap.WrapClass(typeof(UnityEngine.Input));

        pWrap.EndModleWrap();
    }
    static void WrapCustomClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("Custom");

        pWrap.WrapClass(typeof(TestExport)); // 导出一个测试的类

        pWrap.EndModleWrap();
    }

    // 功能：导出带有标签的类
    static void WrapCustomAttribClass(FCClassWrap pWrap)
    {
        pWrap.BeginModleWrap("AutoClass");

        Assembly assembly = Assembly.Load("Assembly-CSharp");
        Type[] types = assembly.GetExportedTypes();
        foreach (Type t in types)
        {
            if (t.IsDefined(typeof(AutoWrapAttribute), false))
            {
                pWrap.WrapClass(t, false);
            }
            else if (t.IsDefined(typeof(PartWrapAttribute), false))
            {
                pWrap.WrapClass(t, true);
            }
        }
        pWrap.EndModleWrap();
    }

    [MenuItem("FCScript/调试C#类型", false, 5)]
    static void LookAll()
    {
        Type nType1 = typeof(UnityEngine.GameObject);
        //System.Object o1 = new UnityEngine.GameObject();
        //nType1 = o1.GetType();
        Type []allps = nType1.GetInterfaces();
        Type  nt1 = nType1.GetInterface("Object");
        Type  b = nType1.BaseType;

        FCValueType a0 = new FCValueType(typeof(Action));
        FCValueType a1 = new FCValueType(typeof(Action<int>));
        FCValueType a2 = new FCValueType(typeof(Action<int, int>));
        FCValueType a3 = new FCValueType(typeof(Action<int, int, int>));
        FCValueType a4 = new FCValueType(typeof(Action<int, int, int, int>));

        FCValueType u0 = new FCValueType(typeof(UnityEngine.Events.UnityAction));
        FCValueType u1 = new FCValueType(typeof(UnityEngine.Events.UnityAction<int>));
        FCValueType u2 = new FCValueType(typeof(UnityEngine.Events.UnityAction<int, int>));
        FCValueType u3 = new FCValueType(typeof(UnityEngine.Events.UnityAction<int, int, int>));
        FCValueType u4 = new FCValueType(typeof(UnityEngine.Events.UnityAction<int, int, int, int>));

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

    static void LoadAssembly(string name)
    {
        Assembly assembly = Assembly.Load(name);
        if (assembly == null)
        {
            assembly = Assembly.Load(AssemblyName.GetAssemblyName(name));
        }
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
            nParamCount = pFuncParam != null ? pFuncParam.Length : 0;
            for(int k = 0; k<nParamCount; ++k)
            {
                nParamType = pFuncParam[k].ParameterType;
            }
        }
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
}
