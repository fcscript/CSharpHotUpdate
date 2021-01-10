using System.Collections;

[export]
class TestAwait
{
    Transform transform;
    Text m_text;
    Button m_button2;
    Button m_button3;
    int m_nClickCount = 0;
    public void Start()
    {
        m_text = transform.Find("Text").GetComponent<Text>();
        m_button2 = transform.Find("Button").GetComponent<Button>();
        m_button2.onClick.AddListener(OnClieckButton);
        m_text.text = "脚本界面初化完成";
        m_button3 = transform.Find("Button3").GetComponent<Button>();
        m_button3.onClick.AddListener(OnClieckButton3);
    }
    async void OnClieckButton()
    {
        int nObjectIndex = ++m_nClickCount;
        m_text.text = "button is clicked." + nObjectIndex + ",Time:" + os.time_desc();
        int nRes = await TestD.LoadPrefab("abc.txt");
        m_text.text = "返回值是：" + nRes + ",Time:" + os.time_desc();
        GameObject obj = await TestD.LoadPrefabObj("test_await" + nObjectIndex);
        UnityObject o = (UnityObject)obj;
        m_text.text = "返回 Name:" + obj.name + ", Time:" + os.time_desc();
    }
    async void OnClieckButton3()
    {
        m_text.text = "你点我了,请等3秒，Time:" + os.time_desc();
        GameObject obj = await TestD.WaitLoadPrefab("Asset/test.prefab", 3.0f);
        m_text.text = "3秒到了, Name:" + obj.name + ", Time:" + os.time_desc();
    }
};