using System.Collections;

[export]
class TestAwait
{
    Transform transform;
    Text m_text;
    Button m_button2;
    int m_nClickCount = 0;
    public void Start()
    {
        m_text = transform.Find("Text").GetComponent<Text>();
        m_button2 = transform.Find("Button").GetComponent<Button>();
        m_button2.onClick.AddListener(OnClieckButton);
        m_text.text = "脚本界面初化完成";
    }
    void OnClieckButton()
    {
        ++m_nClickCount;
        m_text.text = "button is clicked." + m_nClickCount + ",Time:" + os.time_desc();
        int nRes = await TestD.LoadPrefab("abc.txt");
        m_text.text = "返回值是：" + nRes + ",Time:" + os.time_desc();
        GameObject obj = await TestD.LoadPrefabObj("test_await" + m_nClickCount);
        UnityObject o = (UnityObject)obj;
        m_text.text = "返回GameObject:" + obj.name + ", Time:" + os.time_desc();
    }    
};