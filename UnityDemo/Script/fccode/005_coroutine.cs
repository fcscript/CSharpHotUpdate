using System.Collections;

[export]
class TestCoroutine
{
    Transform transform;
    Text m_text;
    Button m_button;
    Button m_button2;
    int m_nClickCount = 0;
    public void Start()
    {
        m_text = transform.Find("Text").GetComponent<Text>();
        m_button = transform.Find("Button").GetComponent<Button>();
        m_button2 = transform.Find("Button2").GetComponent<Button>();
        m_button2.onClick.AddListener(OnClieckButton);
        m_text.text = "OnStart";
    }
    public void  TestFunc()
    {
        FCEnumerator.StopCoroutine("Step1");
        FCEnumerator.StartCoroutine(this, Step1());
    }
    IEnumerator Step1()
    {
        ++m_nClickCount;
        m_text.text = "开始执行协程" + m_nClickCount;
        yield return new WaitForSeconds(1.0f);
        m_text.text = "协程执行结束" + m_nClickCount;
        yield break;
    }
    IEnumerator Step2()
    {
        yield return new WaitForSeconds(3.0f);
        m_text.text = "自动委托";
        yield break;
    }
    void  OnClieckButton()
    {
        ++m_nClickCount;
        m_text.text = "button2 is clicked." + m_nClickCount;
        //FCEnumerator.StartCoroutine(this, Step2());
    }

    public void OnButtonClicked(string szButtonName)
    {
        switch(szButtonName)
        {
            case "Button":
                TestFunc();
                break;
            //case "Button2":
            //    {
            //        OnClieckButton();
            //    }
            //    break;
            default:
                break;
        }
    }
}
