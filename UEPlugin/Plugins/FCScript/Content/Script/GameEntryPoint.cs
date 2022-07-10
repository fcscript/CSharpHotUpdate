using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[export]
class GameEntryPoint
{
    //
    // 摘要:
    //     ///
    //     ///
    public static void OnGameStartup()
	{
		FCDemoSample.TestSample();
	}
	public static void OnPreLoadMap(StringA szMapName)
	{
		os.print("[FCTestScript]OnPreLoadMap:{0}", szMapName);
	}
}