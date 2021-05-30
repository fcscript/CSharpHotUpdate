<p>Unity3D hot update, support C++ or C#</p>
<p>2019-7-11</p>
<p>完成单步调试，逐步调试，函数内跳转（运行至指定行），可以展开this, Class, List, map修改，可以轻松展开超过10万个以上的节点;</p>
<p>2019-8-11</p>
<p>完成Color32, Color, Rect, Ray等内置数据类型, 完成外部跨平台类的导入功能;</p>
<p>完成C#自动wrap的功能; 增加脚本中List, map与C#中List, Dictionary相互赋值的接口;</p>
<p>增加宿主平台语言与脚本对象数据交互的接口</p>
<p>2019-9-13</p>
<p>完成await异步方法</p>
<p>完成Unity插件导出异步方法</p>
<p>2019-9-22</p>
<p>解决new嵌套问题</p>
<p>增加内嵌struct的new操作符的支持</p>
<p>增加内嵌图形对象的参数构造函数</p>
<p>解决iterator模板的问题，兼容C#泛型写法</p>
<p>2019-9-29</p>
<p>增加JSON的支持</p>
<p>2019-10-06</p>
<p>重新实现对象引用机制，解决class与List, Map, Delegate, IEnumerator之间循环引用的释放的问题</p>
<p>修正StopCoroutine接口空指针崩溃的BUG</p>
<p>修正脚本与C#委托对象赋值不生效的BUG</p>
<p>修改C#导出插件,增加内嵌类的自动wrap</p>
<p>优化字节码文件格式，减少字节码文件与占用内存大小</p>
<p>2019-10-08</p>
<p>增加编译后，自动导出引用wrap类与接口配置的功能，用于发行时精简wrap接口</p>
<p>2019-12-07</p>
<p>增加手动注册wrap掊口的功能，可以将异步接口Task同步化,不使用真正的线程，简化逻辑</p>
<p>2019-12-08</p>
<p>增加脚本字节码加密功能</p>
<p>2019-12-09</p>
<p>修改导出插件，解决委托同参导出后重名的问题</p>
<p>2019-12-13</p>
<p>修改导出插件，修正动态库释放时崩溃的BUG，修正类全局函数没有加类名时，内部调用无效的BUG</p>
<p>2019-12-15</p>
<p>增加IntPtr支持，将脚本对象数据快速传递到宿主平台，让数据交互飞起</p>
<p>2019-12-20</p>
<p>修正脚本若干BUG，修正函数参数错误时编译不报错的BUG，增加Vector2, Vector3, Vector4的隐式转换</p>
<p>2020-07-19</p>
<p>增加Protobuf的支持，目前默认是Proto 3.0</p>
<p>FCSerialize 功能强化，增加读写定长数据的接口，增加PB相关接口</p>
<p>多变量声明支持, 如 int a, b = 3, c; </p>
<p>修正switch(exp), exp不支持复杂表达式的问题 </p>
<p>修正class成员map, list的错误初始化导致运行时崩溃的问题 </p>
<p>2020-09-05</p>
<p>修正对象赋空指令，空指针访问崩溃的BUG</p>
<p>2020-09-05</p>
<p>增加Protobuf的示例</p>
<p>修正 null == ptr 的编译错误</p>
<p>修改Protobuf的导出功能，修正一个接口导出的BUG</p>
<p>修改CSerialize名字为FCScriteze</p>
<p>2020-09-09</p>
<p>修正if(exp为常数)时的一个编译错误(笔误)</p>
<p>2020-09-13</p>
<p>修改导出插件，增加精简接口导出模式</p>
<p>优化for循环，测试用例大部分性能提升50%</p>
<p>修正一个引擎接口没有导出导致性能测试用例10异常的BUG</p>
<p>2020-09-15</p>
<p>修正class派生，基类名字没有导出，导致在Unity中访问不了的BUG</p>
<p>修正for优化后，就算条件不满足也会执行一次的BUG</p>
<p>增加脚本类成员函数的查询接口</p>
<p>2020-09-26</p>
<p>增加Unity重新编译脚本后的重置事件，解决Unity运行时重新编译后崩溃的BUG</p>
<p>2020-11-14</p>
<p>优化map<StringA, value>的查询速度，基本上与LUA同等开销</p>
<p>增加小额内存池，解决脚本中大量小内存动态分配的开销（如动态字符串）</p>
<p>加快编译速度</p>
<p>2020-12-06</p>
<p>调整new操作符，解决部分new表达式的编译错误</p>
<p>增加Protobuf的反射属性,完成Protobuf的全反射读写</p>
<p>2021-01-01</p>
<p>修正协程中中断自己时，调用新协程会导致this指针为null的BUG</p>
<p>修正IOS工程的编译报错</p>
<p>增强工具的调试功能</p>
<p>2021-02-18</p>
<p>重构GC模块，彻底解决自引用，循环引用对象的GC问题</p>
<p>修正调试模块中map节点的显示的问题</p>
<p>优化调试模块，增加引用计数的显示</p>
<p>增加Mac下编译与运行时插件支持</p>
<p>2021-04-11</p>
<p>增加多态的支持，增加虚表</p>
<p>支持父类与派生类的相互转换</p>
<p>增加C#的base关键字支持</p>
<p>2021-04-14</p>
<p>修正Unity2019版本的导出插件错误</p>
<p>2021-04-16</p>
<p>修正Unity2020版本的导出插件错误</p>
<p>2021-04-18</p>
<p>修正Unity2020版本的导出插件错误</p>
<p>2021-04-24</p>
<p>增加Broadcast广播功能</p>
<p>修正C++ ::域名访问成员变量的问题</p>
<p>修正派生类同名变量覆盖无法正确访问的问题</p>
<p>目前同名覆盖变量通过base.变量名的访问部分地方有BUG，只能访问最后一个同名变量</p>
<p>2021-05-09</p>
<p>修正示例导出有部分接口没有导出的问题</p>
<p>修正static变量没有初始化的BUG</p>
<p>修正一些语法解析的问题</p>
<p>2021-05-16</p>
<p>增加List的{}初始化语法支持</p>
<p>优化编译，百万行脚本代码的编译时间优化到1秒以内</p>
<p>2021-05-30</p>
<p>修改虚拟机，增加多实例，多线程的支持</p>
<p>修正for循环条件中多if指令的BUG</p>
<p>增强Broadcast广播功能，增加获取广播类的功能</p>
<p>增加动态调用函数功能System.Call</p>
