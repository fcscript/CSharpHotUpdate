using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCProj
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void  Test()
        {
            Vector3 v = new Vector3(1, 2, 3);
            v *= 10;
            v /= 3;
            StringA str = "abc";
            map<int, StringA> m = new map<int, StringA>();
            m[1] = "a";
            m[2] = "b";
            for(iterator<int, StringA> it = m.begin(); it; ++it)
            {
            }
        }
    }
}


//自动导出标签 [export]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct)]
public class exportAttribute : System.Attribute
{
    // 自动导出标签[export]
    public exportAttribute()
    {
    }
}