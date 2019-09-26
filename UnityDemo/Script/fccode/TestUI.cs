
[export]
class TestUI
{
    void OnButtonClicked(string szName)
    {
        os.print("you clicked button : {0}----", szName);
        // to do
        switch(szName)
        {
            case "button0":
                os.print("button0");
                break;
            case "button1":
                os.print("button1");
                break;
            case "Button":
                os.print("case button :  nam is {0}", szName);
                break;
            default:
                os.print("default ...");
                break;
        }
    }
	public static async void  AsyncLoad()
	{
        os.print("begin LoadResource");
        //int nRet = await TestExport.LoadResource("abc.txt");
        //print("after LoadResource, nRet = {0}", nRet);
	}
}