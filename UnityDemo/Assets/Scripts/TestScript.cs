using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class TestExport
{    
    public enum ValueType
    {
        value_none,
        value_int = 2,
        value_float = 3,
    }
    int m_nType;
    public static TestCallback onPostRender; // 这个也于属性一样，有get, set
    //public void SetList(List<int>  aNumb)
    //{
    //}
    // out 仅输出
    public void GetIntArray(out List<int> pList)
    {
        TestExport szName = GetChild<TestExport, int >("abc");
        TestExport t2 = GetChild<TestExport, int>();
        pList = null;
    }
    // ref 输入也输出
    public List<int> GetRefList(ref List<int> pList)
    {
        UnityEngine.Events.UnityAction  ac;
        byte[] buffer = new byte[100];
        return null;
    }
    public T  GetChild<T, V>(string szName)
    {
        return default(T); // 返回默认值
    }
    public T GetChild<T, V>()
    {
        return default(T);
    }   
    void  Test()
    {
        TestExport ret = this;
        long v = FCGetObj.PushObj(TestExport.onPostRender);
        FCDll.PushReturnParam(v);
        //FCDll.PushReturnParam(ret.onPostRender);
        List<int> arg0;
        float f1;
        //this.GetRefList(ref arg0);
    }

    public delegate void TestCallback(int nType);
}

class TestExport_Handle_Wrap
{
    void  TestFunc(long L)
    {
        long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
        TestExport ret = get_obj(nThisPtr);
        TestExport.TestCallback arg0 = FCGetObj.GetObj<TestExport.TestCallback>(FCLibHelper.fc_get_intptr(L, 0));
        TestExport.onPostRender = arg0;
    }

    static TestExport get_obj(long nThisPtr)
    {
        return FCGetObj.GetObj<TestExport>(nThisPtr);
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back_inport_class_func))]
    public static int GetChild_T_V_string(long L)
    {
        // 这个是
        try
        {
            long nThisPtr = FCLibHelper.fc_get_inport_obj_ptr(L);
            TestExport obj = get_obj(nThisPtr);
            int nTemplateParamCount = FCLibHelper.fc_get_template_param_count(L);
            string T = FCLibHelper.fc_get_string_a(L, 0);
            string V = FCLibHelper.fc_get_string_a(L, 1);

            // 函数参数
            string arg0 = FCLibHelper.fc_get_string_a(L, 2);
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
        return 0;
    }

};

class TestScript : MonoBehaviour
{
    static string s_szFromFC = string.Empty;
    static List<string> m_ScriptLog = new List<string>();

    void  Start()
    {
        InitDll();
    }   
        
    void  InitDll()
    {
        if (!FCDll.IsInitDll())
        {
            try
            {
                FCLibHelper.fc_set_debug_print_func(print_error);
                FCLibHelper.fc_set_output_error_func(print_error);

                FCDll.InitDll();

                FCLibHelper.fc_set_debug_print_func(print_error);
                FCLibHelper.fc_set_output_error_func(print_error);
                
                // 注册两个测试函数
                FCLibHelper.fc_register_func("fc2csharp_set_vector3", fc2csharp_set_vector3);
                FCLibHelper.fc_register_func("fc2csharp_set_vector4", fc2csharp_set_vector4);
                FCLibHelper.fc_register_func("fc2csharp_set_string", fc2csharp_set_string);
            }
            catch(Exception e)
            {
                print_error(e.ToString());
            }            
            StartCoroutine(LoadByteCode());
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.LPCustomPrintCallback))]
    static void  print_error(string szInfo)
    {
        m_ScriptLog.Add(szInfo);
    }

    public static string GetAssetStreamingUrlForWWW(string szFileName)
    {
        string url = null;

#if UNITY_STANDALONE || UNITY_EDITOR
        url = string.Format("file:///{0}/{1}", Application.streamingAssetsPath, szFileName);
#elif UNITY_IPHONE
		url = string.Format( "file://{0}/{1}", Application.streamingAssetsPath, szFileName );
#elif UNITY_ANDROID
		url = string.Format( "{0}/{1}", Application.streamingAssetsPath, szFileName );
#endif
        return url;
    }
    public static string GetAssetStreamingPathName(string szFileName)
    {
        string szPathName = string.Format("{0}/{1}", Application.streamingAssetsPath, szFileName);
        return szPathName;
    }

    public static bool LoadBinText(ref byte[] fileData, AssetBundle bunlde)
    {
        if (bunlde == null)
            return false;
        TextAsset text = bunlde.mainAsset as TextAsset;
        if (text == null)
        {
            string[] Names = bunlde.GetAllAssetNames();
            if (Names != null && Names.Length > 0)
            {
                text = bunlde.LoadAsset(Names[0], typeof(TextAsset)) as TextAsset;
            }
        }
        if (text != null)
        {
            fileData = text.bytes;
            bunlde.Unload(true);
            return true;
        }
        bunlde.Unload(true);
        return false;
    }

    bool  LoadByteCodeByFile()
    {
        string szPathName = "test.code";// GetAssetStreamingPathName("test.code");
        print_error("开始加载, 路径：" + szPathName);
        try
        {
            BetterStreamingAssets.Initialize();

            byte[] fileData = BetterStreamingAssets.ReadAllBytes(szPathName);
            if (fileData != null && fileData.Length > 0)
            {
                print_error("加载成功:" + szPathName + ", 字节大小：" + fileData.Length.ToString());
                FCLibHelper.fc_set_code_data(fileData, fileData.Length, 0);
                return true;
            }
        }
        catch(Exception e)
        {
            print_error(e.ToString());
        }
        print_error("加载失败:" + szPathName);
        return false;
    }

    IEnumerator   LoadByteCode()
    {
        if (LoadByteCodeByFile())
            yield break;
    }

    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector3(long L)
    {
        Vector3 v = Vector3.zero;
        FCLibHelper.fc_get_vector3(L, 0, ref v);
        print_error(string.Format("收到脚本回调：fc2csharp_set_vector3 ==>({0},{1},{2})", v.x, v.y, v.z));
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_vector4(long L)
    {
        Vector4 v = Vector4.zero;
        FCLibHelper.fc_get_vector4(L, 0, ref v);
        print_error(string.Format("收到脚本回调：fc2csharp_set_vector4 ==>({0},{1},{2},{3})", v.x, v.y, v.z, v.w));
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(FCLibHelper.fc_call_back))]
    static int fc2csharp_set_string(long L)
    {
        string v = FCLibHelper.fc_get_string_a(L, 0);
        print_error(string.Format("收到脚本回调：fc2csharp_set_string ==>{0}", v));
        s_szFromFC = v;
        return 1;
    }

    void  TestScriptFunc1()
    {
        try
        {
            int nV2 = FCLibHelper.fc_get_version();
            print_error("fc_get_version() is " + nV2);
            bool bDebugMode = FCLibHelper.fc_is_debug_mode();
            print_error("fc_is_debug_mode() is " + bDebugMode);
        }
        catch(Exception e)
        {
            print_error(e.ToString());
        }
        return;

        try
        {
            Vector3 v = new Vector3(1, 2, 3);
            FCDll.PushReturnParam(ref v);
            FCLibHelper.fc_call(0, "csharp2fc_set_vector3");

            Vector4 v2 = new Vector4(22, 33, 44, 55);
            FCDll.PushReturnParam(ref v2);
            FCLibHelper.fc_call(0, "csharp2fc_set_vector4");

            string szTest = "测试字符串传参";
            FCDll.PushReturnParam(szTest);
            FCLibHelper.fc_call(0, "csharp2fc_set_string");
        }
        catch (Exception e)
        {
            print_error(e.ToString());
        }
    }
    void LoadScript()
    {
        InitDll();
        FCLibHelper.fc_call(0, "main");
    }
    long m_nTestPtr;
    void TestObjectScript()
    {
        InitDll();
        if(m_nTestPtr == 0)
            m_nTestPtr = FCLibHelper.fc_instance("CTestD");
        string szLog = string.Format("脚本实例IntPtr = {0}", m_nTestPtr);
        print_error(szLog);
        FCLibHelper.fc_call(m_nTestPtr, "Start");
    }
    void InvalidObjectScriptCall()
    {
        InitDll();
        FCLibHelper.fc_call(m_nTestPtr, "Start");
    }
    void TestGraphicCall()
    {
        print_error("r, g, b, a ==>(60, 120, 180, 220)");
        Color32 c = new Color32(60, 120, 180, 220);
        FCLibHelper.fc_test_color32(c);
        Color c2 = new Color(1f, 2f, 3f, 4f);
        FCLibHelper.fc_test_color(ref c2);
        Vector3 vNormal = new Vector3(3, 2, 1);
        GLPlane p1 = new GLPlane();
        p1.vNormal = new Vector3(1, 2, 3);
        p1.fDist = 4.0f;
        Plane p2 = new Plane(p1.vNormal, p1.fDist);
        p1.vNormal = p2.normal;
        p1.fDist = p2.distance;
        FCLibHelper.fc_test_plane(ref p2);
        Ray r = new Ray(new Vector3(1.0f, 2.0f, 3.0f), new Vector3(0.0f, 1.0f, 0.5f));
        FCLibHelper.fc_test_ray(ref r);
        Bounds box = new Bounds();
        box.min = new Vector3(-1f, -2f, -3f);
        box.max = new Vector3(4f, 5f, 6f);
        FCLibHelper.fc_test_box(ref box);
        Matrix4x4 mat = new Matrix4x4();
        mat.m00 = 0f;
        mat.m01 = 1f;
        mat.m02 = 2f;
        mat.m03 = 3f;

        mat.m10 = 10f;
        mat.m11 = 11f;
        mat.m12 = 12f;
        mat.m13 = 13f;

        mat.m20 = 20f;
        mat.m21 = 21f;
        mat.m22 = 22f;
        mat.m23 = 23f;

        mat.m30 = 30f;
        mat.m31 = 31f;
        mat.m32 = 32f;
        mat.m33 = 33f;
        FCLibHelper.fc_test_matrix(ref mat);
    }
    void  DeleteScriptObject()
    {
        if(m_nTestPtr != 0)
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
            m_nTestPtr = 0;
        }
    }

    void OnGUI()
    {
        int nLeft = 300;
        int nTop = 200;
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试脚本接口"))
        {
            TestScriptFunc1();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "执行main函数"))
        {
            LoadScript();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试脚本对象"))
        {
            TestObjectScript();
        }
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象不置空"))
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "删除对象并置空"))
        {
            FCLibHelper.fc_relese_ins(m_nTestPtr);
            m_nTestPtr = 0;
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "调用对象函数"))
        {
            InvalidObjectScriptCall();
        }
        nLeft = 300;
        nTop += 80;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "清空LOG"))
        {
            m_ScriptLog.Clear();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "测试图形对象"))
        {
            TestGraphicCall();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "初始化DLL"))
        {
            if(!FCLibHelper.fc_is_init())
                FCLibHelper.fc_init();
        }
        nLeft += 160;
        if (GUI.Button(new Rect(nLeft, nTop, 120.0f, 30.0f), "释放DLL"))
        {
            if(FCLibHelper.fc_is_init())
                FCLibHelper.fc_release();
        }
        float fy = 10.0f;
        float fWidth = Screen.width - fy - 10;
        for(int i = 0; i<m_ScriptLog.Count; ++i)
        {
            GUI.Label(new Rect(10.0f, fy, fWidth, 20.0f), m_ScriptLog[i]);
            fy += 25;
        }
    }
}