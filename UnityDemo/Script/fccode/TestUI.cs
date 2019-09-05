
export class TestUI
{
    export void OnButtonClicked(string szName)
    {
        print("you clicked button : {0}----", szName);
        // to do
        switch(szName)
        {
            case "button0":
                print("button0");
                break;
            case "button1":
                print("button1");
                break;
            case "Button":
                print("case button :  nam is {0}", szName);
                break;
            default:
                print("default ...");
                break;
        }
    }
}