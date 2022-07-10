========================================================================

========================================================================

优化思路：
  减少操作符的数据类型， 修改成INT,FLOAT,INT64,DOUBLE,使用这4种数据类型时，效果会更高
  
  取消常数操作指令，将常数转换到Local变量
  
  取消变量ID，直接使用变量的偏移量来取操作数

/////////////////////////////////////////////////////////////////////////////
1、基础数据类型
   char, bool, byte, short, ushort, wchar, int, uint, float, int64, uint64, double
   
   操作符，与C, C++, C#一致
   支持所有C操作符
   ++, --, +, -, *, /, %, ?, (), =, +=, -=, *=, /=, >>, <<, >>=, <<=, >, <, ==, >=, <=, &&, ||, &, |, ^, !, ~, ->, . , 
   说明：基础数据类型中, int类型效率最高，有额外的优化
2、字符串类
   StringA    utf8编码的字符串
   StringW    utf16编码的字符串
   StringA 与 StringW 可以相互赋值
   
   // 目前只支持以下接口，其他接口正在扩展
   template<class _Ty>
   class StringT
   {
        // 功能：字符串与基础数据类型的连接
        // 参数：base_type - 基础数据类型
        public static StringT a + b + c + ... + n ; // 多个字符串连接 a, b 其中一必须是字符串类型，(c, ... n为任意基础数据类型)
        
        public void operator = (base_type other);
        public StringT operator += (base_type other);
        
        public StringT Clone(); // 克隆自己
        
        public _Ty    operator[](int nIndex);
        public int    GetLength();    // 
        pulbic int    Length;         // 取字符串的长度
        public char   GetLastChar();  // 得到最后一个字符
        pulbic void   SetAt(int nIndex, _Ty ch); // 设置字符
        public bool   IsEmpty();
        public void   Empty();
        public void   Swap(StringT other);
        public void   Preallocate(int nSize); // 预分配内存
        public void   Reserve(nSize);  // 预分配内存
        public void   Format(StringT format, arg0, ...); // Format("{0},{1}", i, i); // 参考C#的String.Format
        public void   AppendFormat(StringT format, arg0, ...); // Format("{0},{1}", i, i); // 参考C#的String.Format
        public void   SetString(StringA str, int iStart, int nLen);
        public void   SetString(StringW str, int iStart, int nLen);
        public void   AppendChar( _Ty ch, int nCount);
        public void   Append( StringT, int iStart, int nLen );
        public int    ToInt();
        public int64  ToInt64();
        public float  ToFloat();
        public double ToDouble();
        
        public StringT Left(int nCount);
        public StringT Right(int nCount);
        public StringT Mid(int iStart);
        public StringT Mid(int iStart, int nCount);
        public void TrimRight(); // 删除右侧的空格
        public void TrimLeft();  // 删除左侧的空格
        public void Trim();  // 删除两端的空格
        public StringT Tokenize( StringT szTokens, int& iStart );  // 分隔符查找
        public void  Delete( int iIndex, int nCount);
        public void  DelMiddle( int lowCh,int hightCh,int nBegin,int nEnd );
        public int Insert( int iIndex, _Ty ch, int nCount );
        public int Insert( int iIndex, _Ty ch );
        public int Insert( int iIndex, StringT psz );
        pulbic int Replace( _Ty chOld, _Ty chNew );
        public int Replace( StringT szOld, StringT szNew );
        public int Remove( _Ty chRemove );
        public void MakeUpper(); // 转换成大写
        public void MakeLower(); // 转换成小写
        public int  Compare( StringT other );  // 比较字符串, 返回-1是小于，0是相等，1是大于
        public int  CompareNoCase( StringT other ); // 比较字符串，忽略大小写
        public bool  CompareWithWildcard( StringT szWildcard, bool bNoCase ); // 使用通配符判断两个字符串是不是相似, bNoCase : true忽略大小写; false比较大小写
        public int   Find( _Ty ch, int iStart );  // 从前向后查找指定的字符
        public int   Find( StringT substr, int iStart);
        public int   FindNoCase( StringT substr, int iStart );
        public int   ReverseFind( _Ty ch ); // 从最后一个字符开始向前查找
        public int   ReverseFind( _Ty ch, int nStart );  // 从nStart位置向前查找
        public void  Inserve(); // 将字符串反序
        
        public bool  operator == ( StringT  other );
        public bool  operator != ( StringT  other );
        public bool  operator > ( StringT  other );
        public bool  operator < ( StringT  other );
        public bool  operator >= ( StringT  other );
        public bool  operator <= ( StringT  other );
   };   
   
3、数组, 模板类
   数组不分动态与固定，都是动态数据，不管是new出来的, 都是数组对象，都可以调用数组的接口方法
   初始化方法一：
   char []array = new char[100];
   初始化方法以二
   list<char>  array = new list<char>;
   array.resize(100);
   初始化方法三
   int []array = {1, 2, 3, 5, 8, 9};
   数组的显式释放
   array = NULL;
   
   数组的方法
   
     template<_Ty>
     class CompareFunc
     {
         // 功能：排序函数
         // 参数：p1, p2是要比较的对象
         // 返回值：-1表示p1 < p2; 0是相等;1表示p1 > p2         
         public int Compare(_Ty p1, _Ty p2);
     };
     
     template<_Ty>  // _Ty 不支持list与 map的嵌套, 仅支持基础数据类型与自定义的class
     class list
     {
         // 功能：取数组的长度
         public int   Length;
         public int   Length();
         // 功能：指定数组的长度
         public void  resize(int nSize);
         // 功能：预分配
         public void  reserve(int nSize);
         // 功能：反序
         public void  Inserve();
         // 功能：插入一个节点
         public void  Insert(int nIndex, _Ty value);
         // 功能：移除值相等的节点
         public void  Remove(_Ty value); 
         // 功能：删除指定下标的节点
         public void  RemoveAt(int nIndex);
         // 功能：删除指定下标开始，指定数量的节点
         public void  RemoveAt(int nIndex, int nCount);
         // 功能：删除所有的节点
         public void  RemoveAll();
         // 功能：添加一个变量
         public void  push_back(_Ty value);
         // 功能：数组连接（添加一个数组）
         public void  push_back(list &other);
         
         // 功能：默认的排序(升序)
         public void  Sort();         
        
         // 功能：指定排序实例与排序函数
         // 参数：pIns - 自定义类实例
         //       func_name : _TyOther类的成员函数, 必须是 CompareFunc::Compare声明样式
         template <class _TyOther>
         public void  Sort(_TyOther pIns, func_anme);
         
         // 功能：下标引用
         // 参数：nIndex - 可以常数或变量
         public _Ty  &operator[](int nIndex);
     };
     
4、hash_map, 模板类

   声明：
   map<int, StringA>   aID2Name = new map<int, StringA>();
   
   说明：
       如果_TyKey为自定义的class, 需要实现 GetHashCode与Equals函数
       例
       class map_key
       {
           int  m_nID;
           uint  GetHashCode()
           {
               return m_nID; // 返回一个hash值
           }
           bool Equals(map_key other)
           {
               return m_nID == other.m_nID;
           }
       };
   
   templalte <class _TyKey, class _TyValue>   // _TyKey与_TyValue 不支持list与 map的嵌套, 仅支持基础数据类型与自定义的class
   class  map
   {
       // 取map的节点数量
       public int  Length;
       public int  Length();
       // 功能：下标引用, 如果节点不存在，就插入一个节点
       public _TyValue &operator[](_TyKey key);
       // 功能：从后面有序插入
       // 说明：如果已经存在key, 就更新变量
       //       如果key节点不存在，就从末尾追加, 你可以将map理解成一个list
       public void push_back(_TyKey key, _TyValue  value);
       // 功能：从前面有序插入
       // 说明：如果已经存在key, 就更新变量
       //       如果key节点不存在，就从前端插入, 你可以将map理解成一个list
       public void push_front(_TyKey key, _TyValue  value);
       // 功能：从指定位置后面插入
       public void insert_back(_TyKey where, _TyKey key, _TyValue  value);
       // 功能：从指定位置前面插入
       public void insert_front(_TyKey where, _TyKey key, _TyValue  value);
       // 功能：删除指定的节点
       public void remove(_TyKey key);
       // 功能：删除所有的节点
       public void RemoveAll();
       // 功能：返回首节点
       public iterator  begin();
       // 功能：查找指定KEY值的节点
       public iterator  find(_TyKey key);
       // 功能：测试是不是存在指定Key值的节点
       public bool  ContainKey(_TyKey key);
       // 功能：预分配hash数组，用于性能优化
       // 说明：如果你大致知道总的节点的数量，预先设置一下总的节点数量，有利于优化插入的性能
       public void  Reserve(int nSize);
       // 功能：插入完成的优化
       // 参数：nMaxSize - hash数组的最大长度（内存限制，以免优化后内存大副增加)
       // 说明：调用这个接口可以优化hash数组，减少冲突，提升查询性能，对于节点数量巨大且查询非常频繁的map来说，是很有效的
       public void  Optimize(int nMaxSize);
   };
   
   // hash_map专用迭代器
   class  iterator
   {
       public void operator ++(); // 自增
       // 功能：测试迭代器是不是有效
       public bool IsValid();
       // 功能：测试迭代器是不是有效
       // 说明：直接将对象作bool变量测试, 如果为true表示有效, 为false表示无效
       pulbic operator bool();
       // 功能：返回迭代器指向的节点的key
       public _TyKey key;
       // 功能：返回迭代器指向的节点的value
       public _TyValue value;
   };
   // 说明：iterator是安全的，相对于C#或C++的std::map来说，可以在循环体中删除map的节点而不影响迭代器遍历操作，是不是很方便啊，哈哈
   例:
     for(iterator it = map.begin(); it; ++it)
     {
         if(it.key == 5)
         {
            map.remove(it.key);  // 这样删除不影响遍历，也不会崩溃
            map.remove(12); // 这样删除也不会影响遍历，也不会崩溃
         }
     }
   
5、系统函数接口
   class System
   {
       // 功能：返回当前系统时钟，单位是微秒(1000微秒 = 1毫秒)
       public static int64 clock();
       // 功能：返回当前系统时钟，单位是毫秒(1000毫秒 = 1秒)
       public static int64 GetTickCount();
       // 功能：设置随机函数的初始种子,相当于C函数的srand
       public static void srand(uint nRandSeed);
       // 功能：随机一个整数, 区间[nMin, nMax)
       public static int  RandInt(int nMin, int nMax);
       // 功能：随机一个浮点数,区间[fMin, fMax)
       public static float RandFloat(float fMin, float fMax);
       // 功能：随机一个双精浮点数,区间[min, max)
       public static double RandDouble(double min, double max);   
       // 功能：取utc时间
       public static int64 time();   
       // 功能：取时间描述的字符串，如 xxxx-xx-xx xx:xx:xx
       // 说明：将当前utc时间转换成字符串，格式如：xxxx-xx-xx xx:xx:xx
       public static StringA time_desc();
       // 功能：将当前时间的六个分量拷贝到一个INT数组
       //       a[0] = year; a[1] = month; a[2] = day; a[3] = hour; a[4] = minute; a[5] = second;
       public static void TimeToArray(list<int> array);
       // 功能：返回class实例ins的引用计数,调试用的接口
       pulbic static int  GetRef(ins);
       // 功能：返回class实例ins的弱引用计数,调试用的接口
       pulbic static int  GetWeakRef(ins);
       // 功能：开方
       pulbic static float sqrt(float fValue);
       pulbic static float sin(float fValue);
       pulbic static float cos(float fValue);
       pulbic static float tan(float fValue);
       pulbic static float atan(float fValue);
       pulbic static float asin(float fValue);
       pulbic static float acos(float fValue);
       
       public bool ReadXml(_Ty pXmlRoot, const StringA &szRootName, const StringA &szFileData);
       pulbic bool WriteXml(_Ty pXmlRoot,const StringA &szRootName, StringA &szFileData);
       public bool ReadBin( _Ty pXmlRoot, const StringA &szFileData);
       public bool XmlToBin(_Ty pXmlRoot, StringA &szFileData);
   };
   // 说明：以上指令均是内嵌的指令，不是扩展的函数的调用，开销较小
   
6、CSerialize 流

   class CSerialize
   {
       // 功能：设置读模式, 流的数据由外部指定
       public void ReadMode(list<char> array, int nStart, int nSize);
       // 功能：设置读模式, 流的数据由外部指定
       public void ReadMode(StringA szData);
       // 功能：设置写模式, 流的内存外部指定
       public void WriteMode(list<char> array, int nStart, int nSize);
       // 功能：设置写模式, 流的内存由自己维护
       public void OwnWriteMode(int nSize);
       // 功能：测试写模式，不会发生真正的数据写入，用于求对象序列化后的长度
       public void TestWriteMode();
       // 功能：跳过指定字节
       public void Skip(int nSkipSize);
       // 功能：设置相对于首地址的偏移
       public void Seek(int nPos);
       // 功能：将序列化后的数据拷贝到数组
       public void CopyTo(list<char> aOut);
       public void CopyTo(StringA out szOut);
       // 功能：取当前流的指针位置
       public int  Position();
       // 功能：测试当前流是不是读模式
       public bool IsReadMode();
       // 功能：测试是不是可以读取指定字节大小的数据
       //       测试当前是不是有指定大小数据可读
       public bool IsCanRead(int nReadSize);
       // 功能：将当前指针跳到流数据末尾
       public void SkipToEnd();
       // 功能：读或写一个变量
       // 参数：value - 任意数据类型（基本数据类型 + 自定义的class)
       // 说明：value默认是引用传递的，如果class，读模式下如果为NULL，就会自动创建一个       
       public void ReadWrite(_Ty value);
   };
   
7、if条件语句, 与C, C++, C#语法一致
   if(exp){}
   if(exp1) exp2;
   if(exp1) ...
   else if(exp2) ...
   else ...
   例:
      if(n1 + 3 < n2) n1 += 5;
      if((n1 + 3 < n2) && (n2 > 8)) { n1 += 5; n2 += 3;}
      if(n1 > 50) n2 = 1;
      else if(n1 > 40) n2 = 2;
      else n2 = 3;
8、for循环体, 与C, C++, C#语法一致
   for( ;exp; ) exp2;
   例：
      for(int i = 0; i<100; ++i) s += i;
9、while循环体, 与C, C++, C#语法一致
   while(exp){...}
   例:
       while((--nLoop) > 0){ s += nLoop; };
10、do while 循环体, 与C, C++, C#语法一致
   do{exp}while(exp2);
   例：
   do{ ... } while(n1 > 5);
11、switch语句
   (1)整数的switch, 与C, C++, C#语法一致
      例：n1为int整数
      switch(n1)
      {
         case 0:
            return v1;
         case 1:
            return v2;
         default:
            break;
      }
      说明：
      int的swith是经过高度优化的，编译器会根据case 情况，优化成数组下标跳转，hash表跳转, 有序二叉树跳转，三种情况
   (2)整数区间的switch
      switch(n1)
      {
      case [0, 100):  // 如果 n1 >= 0 && n1 < 100
          return 1;
      case [150, 200): // 如果 n1 >= 150 && n1 < 200
          return 2;
      case [350, 400): // 如果 n1 >= 350 && n1 < 400
          break;
      default:
          break;
      }
      说明：
      区段的switch仅支持二分跳转，使用二分跳转表
   
   (3)字符串的switch, 与C#的相同, str仅支持utf8的 StringA
      case变量只能是常量
      switch(str)
      {
      case "name1":  // 这个只能是常量
           break;
      case "name2":
           break;
      case "name3":
           break;
      default:
          break;
      }
      说明：
      字符串的switch支持hash或二分跳转, 根据case的数量与hash冲突数来决定, 如果hash表过长，就编译成二分表，二分查找
      终于所述, 如果switch的case分支太少，性能有可能不如if, else语句，需要注意一下
12、return 语句, 与C, C++, C#语法一致
    可以返回基础数据类型，自定义数据类型(class), 数组, map对象
    例：
    return "abc" + n1;
    return n1;
    
13、this 指针, 支持
    例  this.m_value = 1;  或  this->m_value = 1;
        this.func(...);    或  this->func(...);
        func(this, xxx);
        
14、class 的支持
    支持 class 与 C#, C++一致
    支持特性：
    a, 单继承与多继承              -    支持
    b, 虚函数（多态）              -  不支持
    c, 运行时转换                  -  不支持
    d, 权限控制(public,private)    -  可支持
    e, 属性方法                    -  考虑增加  get/set方法
15、Delegate 委托

    Delegate
    {
        public Delegate(AnyType obj, funcname(param1, param2, param3...));  // func_name 是 AnyType的成员函数
        public Delegate(AnyType obj, funcname, param1, param2, param3...);  
        public void Call(); // 调用委托的方法
        public void SetParam(int nIndex, AnyType param); // 修改委托函数的第N个参数
    }
    Delegate  ptr = new Delegate(obj, func_name(param1, param2, param3, ...));  // 方式1
    Delegate  ptr = new Delegate(obj, func_name, param1, param2, param3, ...);  // 方式2
    
16、反射, 目前只支持流式的读写的，通过 CSerialize 来读写一个变量或自定义的class
    注意，这个不是真正的反射，主要用于消息包的解码或包装，但性能比反射要高。
    这个主要用于客户端与服务器后台通信使用，不支持前后版本兼容，也就是客户端与服务器必须同时修改消息结构
    所以不要用这个来做持久化数据，这是不安全的, 但如果自己手写序列化的代码，其实也是可以做数据兼容的
17、XML支持
    已经实现
    如果如果确实需要这个，打算实现一个异步多线程加载的功能，与C#一样，通过属性标签去反射加载。
    如：
    [XmlRootAttribute("BUFFSDESC")]
    class BuffConfigure
    {
        [XmlElementAttribute("DOT_BUFF")]
        list<BuffData> BuffList_Dot = new list<BuffData>();
    }
    优化，可以提供一个将XML转换成二进制XML的组件，在打包前将文本转换成二进制格式，可以优化掉XML的文本解析模块的开销
    
    或者直接将XML用流式序列化到二进制，直接通过CSerialize这个类去加载，但这个必须在每次更新时强制转换一次，因为不能版本兼容，但效率是最高的
    
    另外，还可以提供一个excel制表符的加载，提供一个基于行列访问的excel表格接口, 这些都可以做为扩展组件提供
    
    扩展，支持 list<StringA> , map<key, value>的样式
    class TestConnfigure
    {
        [XmlElementAttribute("NAME_ID")]
        list<int>    NameID;
        [XmlElementAttribute("NAME_MAP")]
        map<int, StringA>   IDToName;
    };
    <root>
       <NAME_ID value="1"><NAME_ID/>
       <NAME_ID value="2"><NAME_ID/>
       <NAME_ID value="3"><NAME_ID/>
       <NAME_ID value="4"><NAME_ID/>
       <NAME_ID value="5"><NAME_ID/>
       <NAME_MAP key="1" value="name1"><NAME_MAP/>
       <NAME_MAP key="2" value="name2"><NAME_MAP/>
       <NAME_MAP key="3" value="name3"><NAME_MAP/>
       <NAME_MAP key="4" value="name4"><NAME_MAP/>
       <NAME_MAP key="5" value="name5"><NAME_MAP/>
    </root>
    
    bool System.ReadXml(pRootValue, StringA szFileData);
    
18、inline 字段
    目前全局函数与类成员函数支持 inline 段，标记inline的函数，调用时会内嵌到调用处，可以省去函数调用的开销，是优化的神器
    但目前类构造函数，构造函数不支持 inline
    函数体内可以有 for, while, do while; switch 
    如果递归调用深度超过6，就执行普通调用，不再内联
19、全局变量
    在类外声明的，目前都是全局变量，可以跨文件访问    
20、typedef 支持
    typedef 的声明不分顺序，可以乱序
    例 typedef  int64  long
    typedef map<int, StringA>   ID2Name;
21、enum 支持
    支持全局的 enum 与类内 enum, enum 的语法与C#一致
    enum NUMB_VALUE{ NUMB1 = 1, NUMB2, NUMB3 = NUMB1 + 10, NUMB4};
    int  n1 = NUMB_VALUE.NUMB1;
    class CTest
    {
        enum { NUMB1, NUMB2, NUMB3 };
    };
    // 第一个枚举值如果没有指定，默认从0开始
22、const 常量
    const 常量可以是类外声明，也可以是类内声明
    支持const常量，支持 const常量的运算, const常量的引用不分顺序，可以是乱序的
    class CTest
    {
         const int  NUMB_MAX = NUMB1;
    };
    const int NUMB1 = NUMB2 + 10;
    const int NUMB2 = NUMB3 + 10;
    const int NUMB3 = 100;
    const StringA  TEST_STR1 = TEST_STR2 + "ABC";   // 编译时合并成一个字符串 "CC_ABC"  
    const StringA  TEST_STR2 = "CC_";
23、static_cast
    仅C基础数据类型支持强转, 大部分情况并不需要强制转换, 因为数据类型都是自动转换的
    int64  i = 999999999999;
    int  k = (int)i + 10;    // 等价于 int k = i + 10;
    StringA  str = (StringA)i + "__test";  // 等价于  str = i + "__test";
    尽量不要强制转换，这有可能会导致额外的指令, 从而降低性能

24、图形对象的扩展
    Vector2, Vector3, Vector4, Plane, Matrix, BoundBox, Ray, FrustumBox, Sphere, IntRect, Rect, Color, Color32, Bezier2D, Bezier3D
25、协程

    IEnumerator  StartCoroutine(obj, func(param1, param2, param3, param4));
    StopCoroutine(IEnumerator);
    yield return StartCoroutine(obj, func(param1, param2, param3, param4));
    IEnumerator  DelayFunc(Param1, Param2, ....)
    {
         yield return 1;
         yield return ;
         yield break;
    }
    class  IEnumerator  // 这个类似于Delegate
    {        
        // 功能：启动协程
        public void Start();
        // 功能：停止协程
        public void Stop();        
        // 功能：唤醒wait的协程
        public void Wakeup();
    };
    
    yield wait 5.0f; // 等待5秒
        
    // 启动一个协程方式1：
    IEnumerator  coroutine = new IEnumerator(obj, func(param1, param2, param3, param4));
    coroutine.Start();
    // 启动一个协程方式2：
    IEnumerator coroutine = StartCoroutine(obj, func(param1, param2, param3, param4));  
    
    // 停止一个协程：
    // 方法1：通过IEnumerator参数停止
    StopCoroutine(coroutine);
    // 方法2：通过成员函数名停止
    //        如果当前函数不是类的成员函数，就按全局函数名搜索
    StopCoroutine(funcName);
    // 方法3：通过类名 + 函数名的方式
    //        如果指定类名，不存在，就默认是全局函数
    StopCoroutine(CTestD, func1);
    
    // 功能：停止所有的协程
    StopAllCoroutine();      
        
26、线程
    目前不支持，暂不打算支持
27、调试
    打算实现一个比较强大的调试的功能，如下
    (1), 远程调试（可以调试真机代码）
    (2), 支持单步调试
    (3), 支持条件断点
    (4), 支持运行时修改变量，内存数据
    (5), 支持断住后，代码跳转的功能，可以在当前函数内任意位置跳转，像VC++调试器一样
    (6), 可以运行时切换调试与非调试的功能，优化性能。
    (7), 支持局部变量，全局变量，this成员变量的控制台显示与修改    
 28、其他平台(如C#)调用脚本接口
     export 关键字, 其他平台需要调用脚本的接口，需要先将脚本内的类名或函数名添加export标记
     使用fc_call
     
     如
     export  void  main()
     {
     }
     export  class TestA
     {
          export void  Func()
          {
          }
          void  HideFunc()  // 这个没有export导出，不可以在其他平台调用
          {
          }
     };
     
     // 功能：调用脚本函数
	 // 参数：pIns - 脚本对象指针, 如果为0，表示全局函数
	 //       pcsFuncName - 函数名
	 extern void  fc_call(fc_intptr pInsPtr, const char *pcsFuncName);
	
	 // 功能：创建一个类的实例对象, 返回一个与平台相关的64位整数
	 extern  fc_intptr  fc_instance(const char *pcsClassName);
	 
	 // 功能：释放一个脚本对象
     extern  void fc_relese_ins(fc_intptr ptr);
	
	 fc_call(0, "main"); // 调用全局函数main,  结果：成功
	 fc_intptr  ins = fc_instance("TestA");
	 fc_call(ins, "Func");      // 调用TestA实例华对象ins的成员函数Func,     结果：成功
	 fc_call(ins, "HideFunc");  // 调用TestA实例华对象ins的成员函数HideFunc, 结果：失败
	 fc_relese_ins(ins);        // 释放对象
	 ins = 0;                   // 置零，防止后面误用
	 
 28、导入类  inport class
     public void  stack_cast(const char *pcsClassName, fc_intptr this);
     public fc_intptr stack_new();
     pulbic fc_intptr stack_delete(fc_intptr this);
     public void  set_this_ptr(fc_intptr this);
     puliic fc_intptr get_this_ptr();
    
 28、指针引用分析
     class , Delegate, List, Map都使用引用型指
     如果是临时变量，必须是明引用计数
     如果是引用成员变量，则是使用弱引用计数
     如:
     class  Test
     {
          Test  m_pNext;
          void  TestWeak(Test pNext)  // 函数参数拷贝不增加引用计数，退出时也不释放引用计数
          {
               m_pNext = pNext;  // m_pNext 使用弱引用，增加弱引用计数
          }
     };
     void  main()
     {
          Test  t = new Test(); // 强引用变量
          t.m_pNext = t;  // t.m_pNext 使用引用，并增加弱引用
          Test  t2 = t;  // 临时变量或全局变量，都增加强引用计数
          t = NULL;  // 释放引用, 关于t的成员引用变量要全部清除
          t.m_pNext = NULL; // 释放引用（弱引用)
     }
     
     如果解决List, map, Delegate, 与class相互引用的问题
     List, Map, Delegate中的变量全部默认是弱引用计数
  29、Json解析
      [json] + public 
      使用了[json]标签的类表示支持Json反射, 变量为public才可以反射            
      
      
      // 修改思路：
      // (a), map对象内的class必须是 m_bNeedWeakRef, 参看 Serialize_ProtobufReadMap
              也就是说map必须是
              
              
 ----------------------------------------------------------
 namespace
 
 命名空间只内只支持枚举, 类，不支持常量, 变量
 
 namespace xxx
 {
     enum xxxx
     {
         xxx1 = 1,
         xxx2 = 2,
     };
     class BaseObject
     {
		public enum Type
		{
			TypeNpc,
		};
		public const int NUMB = 10; // 类内支持常量
		public int ID;
     };
     class Npc : BaseObject
     {
		public new int ID; // new 覆盖成员变量
		public new const int NUMB = 10; // 常量也可以覆盖
        static Npc s_pIns = new Npc();  // 全局变量
        public static Npc GetInstance()
        {
            return s_pIns;
        }
        public void PrintDesc()
        {
            int nID = base.ID;
            nID = BaseObject.NUMB;
            nID = Npc.NUMB;
            BaseObject.Type t1 = BaseObject.Type.TypeNpc;
            os.print("nID = {0}", nID);
            KK.EKType e2 = KK.EKType.EKNpc;
        }
     };
 };
 
 // C# class 内可以常量，枚举，class, static 变量，但函数内不能声明static变量
 // C# this 只可以访问成员变量，成员函数，不可以访问static变量，枚举，常量
 // C# 枚举，常量，类内static变量，只能通过类名访问
 

----------------------------------------------------------
后期重构的想法：
去掉Sfc_Value::m_nTemplateType, 将List, map的参数统一成模板参数, 将这个类型移到FC_VALUE_TYPE中，
增加FC_VALUE_LIST, FC_VALUE_MAP, FC_VALUE_WRAP_TEMPLATE, FC_VALUE_SCRIPT_TEMPLATE, 这四个类型

#pragma  pack(push,1)
struct    Sfc_Value
{
    unsigned char       m_type;
    unsigned char       m_unknow1;           // 
    unsigned short      m_nNameIndex;        // 变量名字索引（由编译时分配)
    unsigned short      m_nLocalOffset;      // 变量的相对地址偏移
    unsigned short      m_nLocalSize;        // 变量所占的空间大小
    unsigned short      m_nCustomCassNameID; // 自定义类的名字ID(通过管理器可以获取)或模板ID
    bool                m_bRef;              // 是不是引用类型（成员变量+参数列表)
    bool                m_bNeedWeakRef;      // 是不是弱引用
};

有几个地方需要修改
函数返回值，函数参数，函数内变量声明，类内声明，全局的typename, 全局变量声明
int *func(int *p1, float *p2);
List<int> func(List<int> p1,float *p2);
{
	int *value1 = new int *[10];
	List<int> value2 = new List<int>();
	Map<int, StringA>  value3 = new Map<int, StringA>();
	return value2;
}

class TestA
{
	int  *m_Value1, *m_Value2;
	int  *m_Value3 = new int[10];
	List<int> m_Value4 = new List<int>();
};

int *GlbValue1 = new int[10];
List<int> GlbValue2 = new List<int>();


             