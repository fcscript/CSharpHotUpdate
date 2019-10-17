using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

class FCExclude
{
    static List<string> exclude = new List<string>
    {
        "HideInInspector", "ExecuteInEditMode",
        "AddComponentMenu", "ContextMenu",
        "RequireComponent", "DisallowMultipleComponent",
        "SerializeField", "AssemblyIsEditorAssembly",
        "Attribute", "Types",
        "UnitySurrogateSelector", "TrackedReference",
        "TypeInferenceRules", "FFTWindow",
        "RPC", "Network", "MasterServer",
        "BitStream", "HostData",
        "ConnectionTesterStatus", "GUI", "EventType",
        "EventModifiers", "FontStyle", "TextAlignment",
        "TextEditor", "TextEditorDblClickSnapping",
        "TextGenerator", "TextClipping", "Gizmos",
        "ADBannerView", "ADInterstitialAd",
        "Android", "Tizen", "jvalue",
        "iPhone", "iOS", "Windows", "CalendarIdentifier",
        "CalendarUnit", "CalendarUnit",
        "ClusterInput", "FullScreenMovieControlMode",
        "FullScreenMovieScalingMode", "Handheld",
        "LocalNotification", "NotificationServices",
        "RemoteNotificationType", "RemoteNotification",
        "SamsungTV", "TextureCompressionQuality",
        "TouchScreenKeyboardType", "TouchScreenKeyboard",
        "MovieTexture", "UnityEngineInternal",
        "Terrain", "Tree", "SplatPrototype",
        "DetailPrototype", "DetailRenderMode",
        "MeshSubsetCombineUtility", "AOT", "Social", "Enumerator",
        "SendMouseEvents", "Cursor", "Flash", "ActionScript",
        "OnRequestRebuild", "Ping",
        "ShaderVariantCollection", "SimpleJson.Reflection",
        "CoroutineTween", "GraphicRebuildTracker",
        "Advertisements", "UnityEditor", "WSA",
        "EventProvider", "Apple",
        "ClusterInput", "Motion",
        "UnityEngine.UI.ReflectionMethodsCache", "NativeLeakDetection",
        "NativeLeakDetectionMode", "WWWAudioExtensions", "UnityEngine.Experimental",
        "UnityEngine.AudioClip","WaitUntil","LayoutUtility",
        "ToggleGroup", "WaitWhile", "GUILayout",
        "DefaultControls", "HumanPoseHandler",
        "TerrainData", 
        "WaitForSeconds",
    };

    // 支持的模板函数的接口，需要其他的在这里扩展
    public static List<Type> SupportTemplateTypes = new List<Type>
    {
        typeof(UnityEngine.SkinnedMeshRenderer),
        typeof(UnityEngine.MeshRenderer),
        typeof(UnityEngine.Animation),
        typeof(UnityEngine.Light),
        typeof(UnityEngine.UI.Button),
        typeof(UnityEngine.UI.Text),
    };

    // 函数的黑名单
    static Dictionary<Type, List<string>> m_BlackList;
    static void  PushBlackName(Type nClassType, string szFuncName)
    {
        if (m_BlackList == null)
            m_BlackList = new Dictionary<Type, List<string>>();
        List<string> rList = null;
        if(m_BlackList.TryGetValue(nClassType, out rList))
        {
            rList.Add(szFuncName);
        }
        else
        {
            rList = new List<string>();
            rList.Add(szFuncName);
            m_BlackList[nClassType] = rList;
        }
    }
    static void InitBlackList()
    {
        if (m_BlackList != null)
            return;
        PushBlackName(typeof(UnityEngine.Texture2D), "alphaIsTransparency");
        PushBlackName(typeof(UnityEngine.Animation), "Item");
        PushBlackName(typeof(UnityEngine.Renderer), "allowOcclusionWhenDynamic");
        PushBlackName(typeof(UnityEngine.Input), "IsJoystickPreconfigured");
        PushBlackName(typeof(UnityEngine.Light), "areaSize");
        PushBlackName(typeof(UnityEngine.Light), "lightmapBakeType");
        PushBlackName(typeof(UnityEngine.Light), "bakingOutput");

        PushBlackName(typeof(UnityEngine.WWW), "movie");
        PushBlackName(typeof(UnityEngine.WWW), "MovieTexture");
        PushBlackName(typeof(UnityEngine.WWW), "GetMovieTexture");
        PushBlackName(typeof(UnityEngine.Security), "GetChainOfTrustValue");
        PushBlackName(typeof(UnityEngine.CanvasRenderer), "onRequestRebuild");
        PushBlackName(typeof(UnityEngine.AnimatorOverrideController), "PerformOverrideClipListCleanup");
        PushBlackName(typeof(UnityEngine.AnimatorOverrideController), "ApplyOverrides");
        PushBlackName(typeof(UnityEngine.AnimatorOverrideController), "GetOverrides");
        PushBlackName(typeof(UnityEngine.AnimatorOverrideController), "Item");
        PushBlackName(typeof(UnityEngine.AnimationCurve), "Item");

        PushBlackName(typeof(UnityEngine.Application), "ExternalEval");

        PushBlackName(typeof(UnityEngine.GameObject), "networkView");
        PushBlackName(typeof(UnityEngine.Component), "networkView");

        PushBlackName(typeof(UnityEngine.MonoBehaviour), "runInEditMode");

        PushBlackName(typeof(UnityEngine.AssetBundle), "GetAllLoadedAssetBundles");
        PushBlackName(typeof(UnityEngine.AudioRenderer), "Render");
        PushBlackName(typeof(UnityEngine.HumanPoseHandler), "GetHumanPose");
        PushBlackName(typeof(UnityEngine.Sprite), "OverridePhysicsShape");
        PushBlackName(typeof(UnityEngine.UI.GraphicRegistry), "GetGraphicsForCanvas");        
    }

    public static List<string> GetClassBlackList(Type nClassType)
    {
        InitBlackList();
        List<string> rList = null;
        if (m_BlackList.TryGetValue(nClassType, out rList))
            return rList;
        return null;
    }

    static Dictionary<string, bool> m_excludeList;

    public static bool  IsExclude(Type nType)
    {
        if(m_excludeList == null)
        {
            m_excludeList = new Dictionary<string, bool>();
            List<string> rList = exclude;
            foreach (string name in rList)
            {
                m_excludeList[name] = true;
            }
        }
        if (m_excludeList.ContainsKey(nType.Name))
            return true;
        return m_excludeList.ContainsKey(nType.FullName);
    }

    public static bool  IsNeedExportNamespace(string szNamespace)
    {
        return szNamespace == "UnityEngine" || szNamespace == "UnityEngine.UI";
    }

    public static bool IsNeedExport(Type nType)
    {
        while (nType != null)
        {
            if (nType == typeof(UnityEngine.Object))
                return true;
            if (nType == typeof(UnityEngine.Component))
                return true;
            nType = nType.BaseType;
        }
        return false;
    }

    // 功能：得到所有要导出的类
    public static Dictionary<string, List<Type>> GetAllExportType()
    {
        Dictionary<string, List<Type>> allExportType = new Dictionary<string, List<Type>>();

        Assembly[] all_assemb = AppDomain.CurrentDomain.GetAssemblies();
        string szFullName = string.Empty;
        foreach (Assembly ab in all_assemb)
        {
            if (ab.ManifestModule is System.Reflection.Emit.ModuleBuilder)
                continue;
            szFullName = ab.FullName;
            foreach (Type t in ab.GetExportedTypes())
            {
                if (t.IsEnum)
                    continue;
                if (t.IsArray)
                    continue;
                if (t.IsInterface)
                    continue;
                if (t.IsValueType)
                    continue;
                if (!t.IsClass)
                    continue;
                if (t.IsNested) // 如果是内嵌类
                    continue;
                if (t.IsDefined(typeof(ObsoleteAttribute), false))
                    continue;
                if (!FCExclude.IsNeedExportNamespace(t.Namespace))
                    continue;
                // 如果是排除的类型
                if (FCExclude.IsExclude(t))
                    continue;
                if (t.BaseType == typeof(MulticastDelegate))
                    continue;
                if (string.IsNullOrEmpty(t.Namespace))
                    continue;
                PushExportType(allExportType, t);
            }
        }
        PushExportType(allExportType, typeof(UnityEngine.Events.UnityEvent));
        return allExportType;
    }
    static void PushExportType(Dictionary<string, List<Type>> allExportType, Type t)
    {
        List<Type> rList = null;
        if (allExportType.TryGetValue(t.Namespace, out rList))
        {
            rList.Add(t);
        }
        else
        {
            rList = new List<Type>();
            rList.Add(t);
            allExportType[t.Namespace] = rList;
        }
    }

    static Dictionary<Type, bool> s_InnerType;
    static void InitInnerType()
    {
        // 内部支持的类，不能导出
        s_InnerType = new Dictionary<Type, bool>();
        s_InnerType[typeof(IntPtr)] = true;
        s_InnerType[typeof(System.Collections.IEnumerator)] = true;
        s_InnerType[typeof(UnityEngine.Vector2)] = true;
        s_InnerType[typeof(UnityEngine.Vector3)] = true;
        s_InnerType[typeof(UnityEngine.Vector4)] = true;
        s_InnerType[typeof(UnityEngine.Matrix4x4)] = true;
        s_InnerType[typeof(UnityEngine.Plane)] = true;
        s_InnerType[typeof(UnityEngine.Color32)] = true;
        s_InnerType[typeof(UnityEngine.Color)] = true;
        s_InnerType[typeof(UnityEngine.Rect)] = true;
        s_InnerType[typeof(UnityEngine.Quaternion)] = true;
        s_InnerType[typeof(UnityEngine.Ray)] = true;
        s_InnerType[typeof(UnityEngine.Bounds)] = true;
        s_InnerType[typeof(UnityEngine.WaitForSeconds)] = true;
    }
    public static bool IsDontExportClass(Type nType)
    {
        if(s_InnerType == null)
        {
            InitInnerType();
        }
        return s_InnerType.ContainsKey(nType);
    }
}
