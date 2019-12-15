using System;


class  SceneManager : Object
{
    public SceneManager(){}
    public static int sceneCount { get; }
    public static int sceneCountInBuildSettings { get; }
    public static Scene GetActiveScene(){ return default(Scene); }
    public static bool SetActiveScene(Scene scene){ return default(bool); }
    public static Scene GetSceneByPath(StringA scenePath){ return default(Scene); }
    public static Scene GetSceneByName(StringA name){ return default(Scene); }
    public static Scene GetSceneByBuildIndex(int buildIndex){ return default(Scene); }
    public static Scene GetSceneAt(int index){ return default(Scene); }
    public static void LoadScene(StringA sceneName){}
    public static void LoadScene(StringA sceneName,LoadSceneMode mode){}
    public static void LoadScene(int sceneBuildIndex){}
    public static void LoadScene(int sceneBuildIndex,LoadSceneMode mode){}
    public static AsyncOperation LoadSceneAsync(StringA sceneName){ return default(AsyncOperation); }
    public static AsyncOperation LoadSceneAsync(StringA sceneName,LoadSceneMode mode){ return default(AsyncOperation); }
    public static AsyncOperation LoadSceneAsync(int sceneBuildIndex){ return default(AsyncOperation); }
    public static AsyncOperation LoadSceneAsync(int sceneBuildIndex,LoadSceneMode mode){ return default(AsyncOperation); }
    public static Scene CreateScene(StringA sceneName){ return default(Scene); }
    public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex){ return default(AsyncOperation); }
    public static AsyncOperation UnloadSceneAsync(StringA sceneName){ return default(AsyncOperation); }
    public static AsyncOperation UnloadSceneAsync(Scene scene){ return default(AsyncOperation); }
    public static void MergeScenes(Scene sourceScene,Scene destinationScene){}
    public static void MoveGameObjectToScene(GameObject go,Scene scene){}
};

