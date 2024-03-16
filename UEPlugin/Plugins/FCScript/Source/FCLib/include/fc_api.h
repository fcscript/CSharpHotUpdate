
#ifndef  __FC_API_H__
#define  __FC_API_H__

// 一个用于C#调用的接口

#ifdef WIN32
typedef __int64 fc_int64;
typedef unsigned __int64 fc_uint64;
typedef __int64 fc_intptr;
#else
typedef long long fc_int64;
typedef unsigned long long fc_uint64;
typedef long long fc_intptr;
#endif

typedef unsigned char  fc_byte;
typedef unsigned short fc_ushort;
typedef unsigned int   fc_uint;
typedef const char *   fc_pcstr;
typedef const unsigned short *   fc_pcwstr;
typedef bool *        fc_bool_ptr;
typedef fc_byte *     fc_byte_ptr;
typedef short *       fc_short_ptr;
typedef fc_ushort *   fc_ushort_ptr;
typedef fc_uint *     fc_uint_ptr;
typedef int *         fc_int_ptr;
typedef fc_int64 *    fc_int64_ptr;
typedef float *       fc_float_ptr;
typedef double *      fc_double_ptr;
typedef void *        fc_void_ptr;
typedef void *        fc_unsafe_ptr;

// C#中IntPtr 对应C++中的 void *

struct fc_vector2
{
    float  x;
    float  y;
};
struct fc_vector3
{
    float  x;
    float  y;
    float  z;
};
struct fc_vector4
{
    float  x;
    float  y;
    float  z;
    float  w;
};
struct fc_plane
{
    float  a;
    float  b;
    float  c;
    float  d;
};
struct fc_matrix
{
    union
    {
        struct
        {
            float    _11,_21, _31, _41;
            float    _12,_22, _32, _42;
            float    _13,_23, _33, _43;
            float    _14,_24, _34, _44;
        };
        float m[4][4];
    };
};
struct fc_quaternion
{
	float  x;
	float  y;
	float  z;
	float  w;
};
struct fc_bounds
{
	fc_vector3   center;    // 中心点
	fc_vector3   extents;   // 大小
};
struct fc_ray
{
    fc_vector3  dir;     // 方向
    fc_vector3  origin;  // 起点
};
struct fc_sphere
{
	fc_vector3  center;    // 中心点
	float       radius;    // 半径
};
struct fc_color32 // ==> Color32
{
	unsigned char  r;
	unsigned char  g;
	unsigned char  b;
	unsigned char  a;
};
struct fc_color
{
	float  r;
	float  g;
	float  b;
	float  a;
};
struct fc_intrect
{
	int   left;
	int   top;
	int   right;
	int   bottom;
};
struct fc_rect
{
	float  left;
	float  top;
	float  right;
	float  bottom;
};

typedef fc_color32 *       fc_color32_ptr;
typedef fc_color *         fc_color_ptr;
typedef fc_vector2 *       fc_vector2_ptr;
typedef fc_vector3 *       fc_vector3_ptr;
typedef fc_vector4 *       fc_vector4_ptr;
typedef fc_rect *          fc_rect_ptr;

#ifndef  FC_EXPORT
#define  FC_UNSAFE

//#if defined( __cplusplus)  && defined(WIN32)
//#define  FC_EXPORT    extern "C"
//#define  FC_API       
//
//#else
//#define  FC_EXPORT
//#define  FC_API   
//#endif

#if defined(WIN32) || defined(__APPLE__)
#define FC_API  __cdecl
#else
#define FC_API  
#endif

#define FC_EXPORT extern "C"


#endif

#define FC_CPP_ONLY
#define FC_RUNTIME_API   FC_API

extern "C" typedef void (*LPCustomPrintCallback)(fc_pcstr pcsInfo);

// 功能：脚本回调函数
// 参数：L - 参数对象指针
extern "C" typedef  int (*fc_call_back)(fc_intptr L);

// 功能：脚本回调外部导入类的接口函数
// 参数：L - 参数对象指针
extern "C" typedef  int (*fc_call_back_inport_class_func)(fc_intptr L);
// 功能：脚本回调外部导入类的比较函数
// 参数：L - 左变量的指针地址
//       R - 右变量的指针地址
extern "C" typedef bool     (*fc_call_back_inport_class_equal)(fc_intptr L, fc_intptr R);

// 功能：脚本函数重载
// 参数：nClassName - 类名ID
//       pcsFuncName - 类名
//       UserData1 - 用户传入的参数1
//       UserData2 - 用户传入的参数2
extern "C" typedef void     (*fc_call_back_override)(fc_intptr L, int nClassName, fc_pcstr pcsFuncName, fc_intptr UserData1, fc_intptr UserData2);

// 功能：UE或C#反射函数的输入输出回调
extern "C" typedef void (*fc_input_outer_callback)(fc_intptr L, fc_intptr UserData);

#if defined(WIN32) || defined(__APPLE__) || defined(__linux__)

//---------------------------------------------------------------------
// 参数说明：VM 指虚拟机的指针
//           L  指脚本调用C#端Wrap接口时，传递的参数列表(fc_call_param_array)
//           ptr 一般指具体的参数(fc_c_param_ins)
//---------------------------------------------------------------------
// 功能：初始化脚本系统，创建一个线程主虚拟机
// 参数：bMainThread - true表示复用主线程虚拟机, false表示使用独立虚拟机(可以用于其他线程)
FC_EXPORT  fc_intptr FC_RUNTIME_API fc_init(bool bMainThread);
// 功能：释放脚本组件, 释放主线程虚拟机
FC_EXPORT  void FC_RUNTIME_API fc_release(fc_intptr VM);
FC_EXPORT  bool FC_API fc_is_init();

// 功能：返回主线程虚拟机
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_main_vm();

// 功能：设置脚本的字节码
// 参数：pFileData - 字节码数据指针
//       nFileDataSize - 字节码的长度
//       nProjCode - 项目编号（这个起初设计是用于加解密，目前未使用, 可以是任意值）
FC_EXPORT  void FC_RUNTIME_API fc_set_code_data(fc_intptr VM,fc_byte_ptr pFileData, int nFileDataSize, int nProjCode);

// 功能：切换到调试模式, 默认端口是2600
FC_EXPORT  void FC_API fc_switch_debug(bool bDebug);

// 功能：启动调试器,  用户指定调试端口
// 说明：调试端口从指定的开始，如果有占用，就向上加1，直到找到一个合适的。
FC_EXPORT  void FC_API fc_start_debug(int nPort);

// 功能：返回是不是调试模式
FC_EXPORT  bool FC_API fc_is_debug_mode();

// 功能：返回本地监控的端口
FC_EXPORT  int  FC_API fc_get_debug_port();

// 功能：得到脚本的版本号
FC_EXPORT  int FC_RUNTIME_API  fc_get_version();

// 功能：创建一个脚本对象, 并增加引用计数,由外部脚本来释放
FC_EXPORT  fc_intptr FC_RUNTIME_API fc_instance(fc_intptr VM, fc_pcstr pcsClassName);

// 功能：释放一个脚本对象
FC_EXPORT  void FC_RUNTIME_API fc_relese_ins(fc_intptr VM, fc_intptr ptr);

// 功能：检测脚本对象是不是合法的
// 参数：ptr - 脚本对象ID(或C++指针)
FC_EXPORT  bool FC_RUNTIME_API fc_is_valid_ins(fc_intptr VM, fc_intptr ptr);

// 功能：执行协程逻辑
// 说明：这个函数必须中上层主动调用，不调用就不会执行协程代码
FC_EXPORT  void FC_RUNTIME_API fc_coroutine_udpate(fc_intptr VM);


// 功能：设置自定义的错误日志的函数
FC_EXPORT void FC_API fc_set_output_error_func(LPCustomPrintCallback pFunc);

// 功能：设置脚本中用于打印调试的函数
FC_EXPORT void FC_API fc_set_debug_print_func(LPCustomPrintCallback pFunc);

// 功能：数据异或加密或解密
FC_EXPORT void FC_API fc_pack_data(fc_byte_ptr pData, int nOffset, int nSize, fc_int64 nKey);

// 功能：随机加密或解密（由3个密钥组成）
// 返回值：返回新的密钥
FC_EXPORT fc_uint FC_API fc_rand_pack_data(fc_byte_ptr pData, int nOffset, int nSize, fc_uint nKey, fc_uint key2, fc_uint key3);

// 功能：产生一下随机的KEY
FC_EXPORT fc_int64 FC_API fc_get_next_key(fc_int64 nKey, fc_uint &nRandKey);

//--------------------------------------------------------------------------------------------------
// 以下fc_test开头的是测试代码接口，没有意义
FC_EXPORT  void  FC_API fc_test_int_ptr();
FC_EXPORT  void  FC_API fc_test_color32(fc_color32 c);
FC_EXPORT  void  FC_API fc_test_color(fc_color &c);
FC_EXPORT  void  FC_API fc_test_plane(fc_plane &p);
FC_EXPORT  void  FC_API fc_test_matrix(fc_matrix &mat);
FC_EXPORT  void  FC_API fc_test_box(fc_bounds &box);
FC_EXPORT  void  FC_API fc_test_ray(fc_ray &ray);
FC_EXPORT  void  FC_API fc_test_quaternion(fc_quaternion &qua);
FC_UNSAFE  FC_EXPORT  void  FC_API fc_test_struct(fc_unsafe_ptr ptr, int nSize);

//--------------------------------------------------------------------------------------------------

// 功能：注册C#委托回调函数
FC_EXPORT void FC_RUNTIME_API fc_register_func(fc_intptr VM, fc_pcstr pcsFuncName, fc_call_back func);

// 功能：得到导入类的名字ID
FC_EXPORT int FC_RUNTIME_API fc_get_inport_class_id(fc_intptr VM, fc_pcstr pcsClassName);

// 功能：注册C#导入类的回调函数
FC_EXPORT void FC_RUNTIME_API fc_register_class_func(fc_intptr VM, int nClassNameID, fc_pcstr pcsFuncName, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的属性get/set方法
FC_EXPORT void FC_API fc_register_class_attrib(fc_intptr VM, int nClassNameID, fc_pcstr pcsFuncName, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet);

// 功能：注册C#导入类的属性get/set方法, += , -=
FC_EXPORT void FC_RUNTIME_API fc_register_class_attrib_ex(fc_intptr VM, int nClassNameID, fc_pcstr pcsFuncName, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet, fc_call_back_inport_class_func pAddSet, fc_call_back_inport_class_func pSubSet);

// 功能：注册C#导入类的cast强制转换接口
FC_EXPORT void FC_RUNTIME_API fc_register_class_cast(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的new
FC_EXPORT void FC_RUNTIME_API fc_register_class_new(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的delete
FC_EXPORT void FC_RUNTIME_API fc_register_class_del(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的全局释放引用接口
FC_EXPORT void FC_RUNTIME_API fc_register_class_release_ref(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的全局的释放引用接口, 不分class
FC_EXPORT void FC_RUNTIME_API fc_register_class_global_release_ref(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的hash函数
FC_EXPORT void FC_RUNTIME_API fc_register_class_hash(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_func func);

// 功能：注册C#导入类的Equal函数
FC_EXPORT void FC_RUNTIME_API fc_register_class_equal(fc_intptr VM, int nClassNameID, fc_call_back_inport_class_equal func);

//--------------------------------------------------------------------------------------------------

// 反射类接口--------begin
// 功能：注册C#反射类的回调函数
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_func(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的属性get/set方法
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_attrib(fc_intptr VM, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet);

// 功能：注册C#反射类的属性get/set方法, += , -=
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_attrib_ex(fc_intptr VM, fc_call_back_inport_class_func pGet, fc_call_back_inport_class_func pSet, fc_call_back_inport_class_func pAddSet, fc_call_back_inport_class_func pSubSet);

// 功能：注册C#反射类的cast强制转换接口
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_cast(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的new
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_new(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的delete
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_del(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的释放引用接口
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_release_ref(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的hash函数
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_hash(fc_intptr VM, fc_call_back_inport_class_func func);

// 功能：注册C#反射类的Equal函数
FC_EXPORT void FC_RUNTIME_API fc_register_reflex_class_equal(fc_intptr VM, fc_call_back_inport_class_equal func);

// 功能：请求C#反射类的Override
FC_EXPORT FC_CPP_ONLY bool FC_RUNTIME_API fc_require_override(fc_intptr VM, fc_call_back_override func, fc_pcstr pcsClassName, fc_intptr UserData1, fc_intptr UserData2);

// 反射类接口--------end
//--------------------------------------------------------------------------------------------------

// 功能：平台注册的回调函数，取脚本所传的函数参数
FC_EXPORT int         FC_RUNTIME_API  fc_get_param_count(fc_intptr L);

// 功能：得到函数第N个参数的指针
// 说明：可以通过这个指针，调用fc_get_value_xxx接口获取对应的变量; 可以直接调用fc_get_xxx直接获取
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_param_ptr(fc_intptr L, int i);

// 功能：得到当前模板参数个数
FC_EXPORT   int       FC_RUNTIME_API  fc_get_template_param_count(fc_intptr L);

// 功能：得到外部导入对象的指针或句柄(外部对象的this指针, 由外部平台维护)
FC_EXPORT fc_int64    FC_RUNTIME_API  fc_get_inport_obj_ptr(fc_intptr L);

// 功能：得到对象类型
FC_EXPORT int   FC_RUNTIME_API  fc_get_value_type(fc_intptr ptr);

// 功能：得到map对象value类型
FC_EXPORT int   FC_RUNTIME_API  fc_get_map_value_type(fc_intptr ptr);

// 功能：得到对象模板类型
FC_EXPORT int   FC_RUNTIME_API  fc_get_value_template_type(fc_intptr ptr);

FC_EXPORT int   FC_RUNTIME_API  fc_get_value_class_name_id(fc_intptr ptr);


//--------------------------------------------------------------------------------------------------
// 以下接口脚本调用C#层wrap函数时，取脚本传递参数用的接口
// 
// 功能：取wrap函数的第i个变量（变量类型为char)
FC_EXPORT char        FC_RUNTIME_API  fc_get_char(fc_intptr L, int i);
// 功能：取wrap函数的第i个变量（变量类型为bool)
FC_EXPORT bool        FC_RUNTIME_API  fc_get_bool(fc_intptr L, int i);
// 功能：取wrap函数的第i个变量（变量类型为byte)
FC_EXPORT fc_byte     FC_RUNTIME_API  fc_get_byte(fc_intptr L, int i);
// 功能：取wrap函数的第i个变量（变量类型为short)
FC_EXPORT short       FC_RUNTIME_API  fc_get_short(fc_intptr L, int i);
// 功能：取wrap函数的第i个变量（变量类型为ushort)
FC_EXPORT fc_ushort   FC_RUNTIME_API  fc_get_ushort(fc_intptr L, int i);
// 功能：取wrap函数的第i个变量（变量类型为int)
FC_EXPORT int         FC_RUNTIME_API  fc_get_int(fc_intptr L, int i);
FC_EXPORT fc_uint     FC_RUNTIME_API  fc_get_uint(fc_intptr L, int i);
FC_EXPORT float       FC_RUNTIME_API  fc_get_float(fc_intptr L, int i);
FC_EXPORT double      FC_RUNTIME_API  fc_get_double(fc_intptr L, int i);
FC_EXPORT fc_int64    FC_RUNTIME_API  fc_get_int64(fc_intptr L, int i);
FC_EXPORT fc_uint64   FC_RUNTIME_API  fc_get_uint64(fc_intptr L, int i);

// 功能：返回C#平台对象的地址(在Unity的FC引擎中，这个是一个数字, 也就是FCGetObj管理对象的ID）
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_intptr(fc_intptr L, int i);

// 功能：返回宿主平台对象ID（或地址)
// 说明：在Unity的FC引擎中，这个是一个数字, 也就是FCGetObj管理对象的ID）
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_wrap_objptr(fc_intptr L, int i);

// 功能：返回脚本对象(C++ void 指针),对应的数据类型是C#中的IntPtr
// 功能：L - 脚本调用C#时的参数列表指针(fc_call_param_array)
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_void_ptr(fc_intptr L, int i);

// 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
// 功能：L - 脚本调用C#时的参数列表指针(fc_call_param_array)
// 返回值：返回指定参数的指针地址(fc_c_param_ins)
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_script_param(fc_intptr L, int i);

FC_EXPORT FC_CPP_ONLY fc_pcstr FC_RUNTIME_API  fc_cpp_get_string_a(fc_intptr L, int i);
FC_EXPORT void        FC_API  fc_get_string_a(fc_intptr L, int i, fc_byte_ptr pOutBuff, int nOutBuffSize);
FC_EXPORT int         FC_API  fc_get_string_len(fc_intptr L, int i);
FC_EXPORT FC_CPP_ONLY fc_byte_ptr FC_RUNTIME_API  fc_cpp_get_array_data(fc_intptr L, int i);
FC_EXPORT void        FC_RUNTIME_API  fc_get_byte_array(fc_intptr L, int i, fc_byte_ptr pOutBuff, int nOutBuffSize);
FC_EXPORT int         FC_RUNTIME_API  fc_get_byte_array_size(fc_intptr L, int i);

FC_EXPORT void        FC_RUNTIME_API  fc_get_vector2(fc_intptr L, int i, fc_vector2 &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_vector3(fc_intptr L, int i, fc_vector3 &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_vector4(fc_intptr L, int i, fc_vector4 &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_plane(fc_intptr L, int i, fc_plane &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_matrix(fc_intptr L, int i, fc_matrix &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_bounds(fc_intptr L, int i, fc_bounds &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_quaternion(fc_intptr L, int i, fc_quaternion &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_ray(fc_intptr L, int i, fc_ray &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_color32(fc_intptr L, int i, fc_color32 &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_color(fc_intptr L, int i, fc_color &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_intrect(fc_intptr L, int i, fc_intrect &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_rect(fc_intptr L, int i, fc_rect &v);
FC_EXPORT void        FC_RUNTIME_API  fc_get_sphere(fc_intptr L, int i, fc_sphere &v);

// 功能：通过不安全的方式，直接传递指针，拷贝struct对象
FC_UNSAFE  FC_EXPORT  void  FC_RUNTIME_API fc_get_struct_param(fc_intptr L, int i, fc_unsafe_ptr csharp_ptr, int nSize);

// 功能：添加C#的函数的返回值
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_return_ptr(fc_intptr L);

// 功能：根据当前调参数取虚拟机的地址
FC_EXPORT fc_intptr   FC_RUNTIME_API  fc_get_vm_ptr(fc_intptr L);

// 功能：得到当前调用wrap调用(脚本调用宿主语言)的类的名字ID
FC_EXPORT int   FC_RUNTIME_API  fc_get_current_call_class_name_id(fc_intptr L);

// 功能：得到当前调用wrap调用(脚本调用宿主语言)的类的函数的名字ID
FC_EXPORT int   FC_RUNTIME_API  fc_get_current_call_class_function_name_id(fc_intptr L);

// 功能：通过类名获取类的名字(id转字符串)
// 说明：这个接口仅在CPP类语言中调用(因为C#不支持与C原生字符串交互)
FC_EXPORT FC_CPP_ONLY fc_pcstr   FC_RUNTIME_API  fc_cpp_get_current_call_class_name(fc_intptr L);

// 功能：获取导入类的类名
FC_EXPORT FC_CPP_ONLY fc_pcstr   FC_RUNTIME_API  fc_cpp_get_import_class_name(int nClassName);

// 功能：通过函数ID获取类函数的名字(id转字符串)
// 说明：这个接口仅在CPP类语言中调用(因为C#不支持与C原生字符串交互)
FC_EXPORT FC_CPP_ONLY fc_pcstr   FC_RUNTIME_API  fc_cpp_get_current_call_class_function_name(fc_intptr L);

// 功能：通过类名获取类的名字(id转字符串)
// 参数：L - 当前调用的上下文参数
//       pOutBuff - 输出的字符串地址
//       nOutBuffSize - 缓冲区的最大容量
// 返回值：总是返回类名的长度(不管有没有拷贝成功）
// 说明：这个是给C#使用的接口
FC_EXPORT int FC_RUNTIME_API fc_csharp_get_current_call_class_name(fc_intptr L, fc_byte_ptr pOutBuff, int nOutBuffSize);

// 功能：通过类ID+函数ID获取函数的名字(id转字符串)
// 参数：L - 当前调用的上下文参数
//       pOutBuff - 输出的字符串地址
//       nOutBuffSize - 缓冲区的最大容量
// 返回值：总是返回函数名的长度(不管有没有拷贝成功）
// 说明：这个是给C#使用的接口
FC_EXPORT int FC_RUNTIME_API fc_csharp_get_current_call_class_function_name(fc_intptr L, fc_byte_ptr pOutBuff, int nOutBuffSize);

// 功能：判断变量地址是不是wrap的模板实例
// 参数：ptr - 变量的地址
FC_EXPORT bool   FC_RUNTIME_API  fc_is_wrap_template(fc_intptr VM, fc_intptr ptr);

// 功能：得到模板参数ID
FC_EXPORT int   FC_RUNTIME_API  fc_get_wrap_template_param_id(fc_intptr VM, fc_intptr ptr);

// 功能：得到指定变量的模板参数数量
FC_EXPORT int   FC_RUNTIME_API  fc_get_wrap_template_param_count(fc_intptr VM, fc_intptr ptr);

// 功能：得到指定模板实例的第N个参数的类型
FC_EXPORT int   FC_RUNTIME_API  fc_get_wrap_template_param_type(fc_intptr VM, fc_intptr ptr, int nIndex);

// 功能：检查指定模板实例的第N个参数的类型是不是wrap class类
FC_EXPORT bool   FC_RUNTIME_API  fc_is_wrap_class_type(int nValueType);

// 功能：得到指定模板实例的第N个参数的类的ID
FC_EXPORT int   FC_RUNTIME_API  fc_get_wrap_template_param_class_name_id(fc_intptr VM, fc_intptr ptr, int nIndex);

// 功能：得到指定模板实例的第N个参数的类的ID
FC_EXPORT  FC_CPP_ONLY fc_pcstr   FC_RUNTIME_API  fc_cpp_get_wrap_template_param_class_name(fc_intptr VM, fc_intptr ptr, int nIndex);

// 功能：得到指定模板实例的第N个参数的类名(C#使用)
// 返回值：返回名字的真实长度
// 说明：如果pOutBuff为NULL 或 nOutBuffSize <= 0, 就直接返回字符串的长度;
FC_EXPORT int  FC_API fc_csharp_get_wrap_template_param_class_name(fc_intptr VM, fc_intptr ptr, int nIndex, fc_byte_ptr pOutBuff, int nOutBuffSize);

// C#是应该这样声明

//#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
//    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
//    public delegate void LPCustomPrintCallback(string pcsInfo);
//    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
//    public delegate int fc_call_back(IntPtr L);
//#else
//    public delegate void LPCustomPrintCallback(string pcsInfo);
//    public delegate int fc_call_back(IntPtr L);
//#endif


// IntPtr fn = Marshal.GetFunctionPointerForDelegate(func);
// fc_register_func(name, fn);    

//------------------------------------
// 调用脚本函数传参接口

//--------------------------------------------------------------------------------------

// 功能：装备调用UE或C#反射函数调用脚本函数
// 参数：pIns - 脚本对象指针
//       pcsFuncName - 函数名
//       CallKey - 标记当前调用的一关键KEY(由用户上层产生, 最近1000内不相同就可以了)
// 返回值：返回L参数, 如果失败就返回NULL
// 说明：CallKey - 是用户管理的唯一值，可以是一个自增的ID, 只需要最近N次调用不相同就可以, 这个N的大小，取决于函数最大递归栈的数量
FC_EXPORT fc_intptr FC_API fc_prepare_ue_call(fc_intptr VM, fc_intptr pInsPtr, fc_pcstr pcsFuncName, fc_intptr CallKey);
FC_EXPORT fc_intptr FC_API fc_prepare_ue_fast_call(fc_intptr VM, fc_intptr pInsPtr, int nClassNameID, int nFuncNameID, fc_intptr CallKey);
// 功能：UE或C#反射函数调用脚本函数
// 参数：VM - 脚本设备上下文
//       CallKey - 标记当前调用的一关键KEY
// 返回值：返回L参数, 如果失败就返回NULL
// 说明：调用这个函数前，必须先调用fc_prepare_ue_call, 然后由上层自己Push函数参数, 最后调用fc_end_ue_call结束函数的调用
FC_EXPORT fc_intptr  FC_API fc_ue_call(fc_intptr VM, fc_intptr CallKey);

// 功能：结束UE或C#反射函数调用脚本函数
// 参数：VM - 脚本设备上下文
//       CallKey - 标记当前调用的一关键KEY
// 说明：调用这个函数前，必须先调用fc_prepare_ue_call ==> fc_ue_call
FC_EXPORT void  FC_API fc_end_ue_call(fc_intptr VM, fc_intptr CallKey);

// 功能：睡眠当前运行中的函数(虚拟器)
// 返回值：返回当前虚拟器的指针
FC_EXPORT fc_intptr FC_API fc_await(fc_intptr L);

// 功能：唤醒指定的函数(虚拟器), 继续当前虚拟器的运行
// 参数：pPtr - 指定的虚拟器，由fc_await返回
FC_EXPORT void FC_API fc_continue(fc_intptr VM,fc_intptr pPtr);

// 功能：是不是一个合法的await
FC_EXPORT bool FC_API fc_is_valid_await(fc_intptr VM,fc_intptr pPtr);

// 功能：调用指定消息读写的接口，参数必须是CSerialize
// 参数：pIns - 脚本对象指针
//       pcsFuncName - 函数名
//       msgPtr - C#消息指针
//       nStart - 开始位置
//       nLen - 消息包的长度
//       bReadMode - 是不是读模式,true表示读, false表示写
// 说明：脚本函数的声明必须是    void   xxxxx(CSerialize  ar); // 这样的形式
FC_EXPORT void  FC_API fc_serialize_msg_call(fc_intptr VM, fc_intptr pInsPtr, fc_pcstr pcsFuncName, fc_byte_ptr msgPtr, int nStart, int nLen, bool bReadMode);

//--------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------
// 以下是对一些常规变量的设置
// 
// 功能：设置对象成员变量，必须将这个类设置成导出类，或变量前标记export
// 
FC_EXPORT   fc_intptr  FC_API fc_get_class_value(fc_intptr VM, fc_intptr ptr, fc_pcstr value_name);

//--------------------------------------------------------------------------------------
// 功能：查找对象是不是有指定名字的成员函数
FC_EXPORT   bool       FC_API fc_find_class_func(fc_intptr VM, fc_intptr ptr, fc_pcstr func_name);

//--------------------------------------------------------------------------------------
// 以下是对常规变量的设置接口
// ptr来源：
// (1) fc_intptr  fc_get_param_ptr(fc_intptr L, int i)
// (2) fc_intptr  fc_get_class_value(fc_intptr ptr, fc_pcstr value_name);
// (3) fc_intptr  fc_get_array_node_temp_ptr(fc_intptr ptr, int nIndex);

// 功能：将一个bool值设置给脚本变量
// 参数：ptr - 脚本对象地址
//       v - 要设置的bool值
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_bool(fc_intptr ptr, bool v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_char(fc_intptr ptr, char v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_byte(fc_intptr ptr, fc_byte v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_short(fc_intptr ptr, short v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_ushort(fc_intptr ptr, fc_ushort v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_int(fc_intptr ptr, int v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_uint(fc_intptr ptr, fc_uint v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_float(fc_intptr ptr, float v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_double(fc_intptr ptr, double v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_int64(fc_intptr ptr, fc_int64 v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_uint64(fc_intptr ptr, fc_uint64 v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_intptr(fc_intptr VM, fc_intptr ptr, fc_intptr v);

//--------------------------------------------------------------------------------------
// 以下是对动态变量类型设置运行时的类型
// 
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_bool(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_char(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_byte(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_short(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_ushort(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_int(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_uint(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_float(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_double(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_int64(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_uint64(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_string_a(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_string_w(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_wrap_object(fc_intptr VM, fc_intptr ptr, int nClassName);

// 设置模板类型，基础数据类型为(bool, char, byte, short, ushort, int, uint, float, double, int64, uint64, StringA, StringW)
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_byname(fc_intptr VM, fc_intptr ptr, const char *pcsClassName);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_wrap_template_param1(fc_intptr VM, fc_intptr ptr, int nTemplateClassName, const char *FirstParam);
FC_EXPORT   void  FC_RUNTIME_API fc_change_value_type_wrap_template_params(fc_intptr VM, fc_intptr ptr, int nTemplateClassName, const char **Params, int nParamCount);

//--------------------------------------------------------------------------------------

// 功能：将跨平台的wrap对象(ID或地址)设置给脚本变量
// 参数：ptr - 脚本对象地址
//       v - wrap对象ID
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_wrap_objptr(fc_intptr VM, fc_intptr ptr, fc_intptr v);

// 功能：将外部创建的脚本对象设置给脚本变量
// 参数：ptr - 脚本对象地址
//       v - 脚本对象ID(由fc_instance接口创建的)
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_script_instance(fc_intptr L, fc_intptr ptr, fc_intptr v);

// 功能：得到wrap变量的类名
// 参数：ptr - 脚本变量地址
FC_EXPORT   int   FC_RUNTIME_API fc_get_wrap_class_name_id(fc_intptr ptr);

// 功能：得到wrap类名
// 参数：ptr - 脚本变量地址
// 说明：这个接口仅在CPP类语言中调用(因为C#不支持与C原生字符串交互)
FC_EXPORT FC_CPP_ONLY fc_pcstr   FC_RUNTIME_API  fc_cpp_get_wrap_class_name(fc_intptr VM, fc_intptr ptr);

// 功能：设置void *指针(IntPtr)设置给脚本变量
// 参数：ptr - 脚本对象地址
//       v - iNT
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_void_ptr(fc_intptr ptr, fc_void_ptr v);

FC_EXPORT   void  FC_RUNTIME_API fc_set_value_string(fc_intptr ptr, fc_pcstr v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_string_a(fc_intptr ptr, fc_pcstr v, int nLen);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_string_w(fc_intptr ptr, fc_ushort_ptr v, int nLen);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_vector2(fc_intptr ptr, const fc_vector2 &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_vector3(fc_intptr ptr, const fc_vector3 &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_vector4(fc_intptr ptr, const fc_vector4 &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_plane(fc_intptr ptr, const fc_plane &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_matrix(fc_intptr ptr, const fc_matrix &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_bounds(fc_intptr ptr, const fc_bounds &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_quaternion(fc_intptr ptr, const fc_quaternion &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_ray(fc_intptr ptr, const fc_ray &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_color32(fc_intptr ptr, const fc_color32 &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_color(fc_intptr ptr, const fc_color &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_intrect(fc_intptr ptr, const fc_intrect &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_rect(fc_intptr ptr, const fc_rect &v);
FC_EXPORT   void  FC_RUNTIME_API fc_set_value_sphere(fc_intptr ptr, const fc_sphere &v);

// 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
// 功能：VM - 脚本虚拟机
//       ptr - 脚本对象地址
//       csharp_ptr - C#端结构指针
//       nSize - 对象所占字节数
FC_UNSAFE  FC_EXPORT   void  FC_RUNTIME_API fc_set_struct_value(fc_intptr ptr, fc_unsafe_ptr csharp_ptr, int nSize);
//--------------------------------------------------------------------------------------
// 以下是对一些常规变量的获取
// ptr来源：
// (1) fc_intptr  fc_get_param_ptr(fc_intptr L, int i);  // 取wrap函数参数指针
// (2) fc_intptr  fc_get_class_value(fc_intptr ptr, fc_pcstr value_name); // 取对象成员指针
// (3) fc_intptr  fc_get_array_node_temp_ptr(fc_intptr ptr, int nIndex);  // 取数组对象的第N个节点的指针
// (4) fc_intptr  fc_map_get_cur_key_ptr();      // 取当前map迭代器的key指针
// (5) fc_intptr  fc_map_get_cur_value_ptr();    // 取当前map迭代器的value指针
FC_EXPORT   bool       FC_API fc_get_value_bool(fc_intptr ptr);
FC_EXPORT   char       FC_API fc_get_value_char(fc_intptr ptr);
FC_EXPORT   fc_byte    FC_API fc_get_value_byte(fc_intptr ptr);
FC_EXPORT   short      FC_API fc_get_value_short(fc_intptr ptr);
FC_EXPORT   fc_ushort  FC_API fc_get_value_ushort(fc_intptr ptr);
FC_EXPORT   int        FC_API fc_get_value_int(fc_intptr ptr);
FC_EXPORT   fc_uint    FC_API fc_get_value_uint(fc_intptr ptr);
FC_EXPORT   float      FC_API fc_get_value_float(fc_intptr ptr);
FC_EXPORT   double     FC_API fc_get_value_double(fc_intptr ptr);
FC_EXPORT   fc_int64   FC_API fc_get_value_int64(fc_intptr ptr);
FC_EXPORT   fc_uint64  FC_API fc_get_value_uint64(fc_intptr ptr);
FC_EXPORT   fc_intptr  FC_API fc_get_value_intptr(fc_intptr ptr);

// 功能：从脚本变量中，取出wrap对象的ID或地址
FC_EXPORT   fc_intptr  FC_API fc_get_value_wrap_objptr(fc_intptr ptr);

// 功能：从脚本对象中，取出void *指针(C# 中的 IntPtr)
FC_EXPORT   fc_intptr  FC_API fc_get_value_void_ptr(fc_intptr ptr);

FC_EXPORT FC_CPP_ONLY fc_pcstr FC_API  fc_cpp_get_value_string_a(fc_intptr VM, fc_intptr ptr);
FC_EXPORT FC_CPP_ONLY fc_pcwstr FC_API  fc_cpp_get_value_string_w(fc_intptr VM, fc_intptr ptr);

FC_EXPORT   void       FC_API fc_get_value_string(fc_intptr VM, fc_intptr ptr, fc_byte_ptr pOutBuff, int nOutBuffSize);
FC_EXPORT   int        FC_API fc_get_value_string_len(fc_intptr VM, fc_intptr ptr);
FC_EXPORT   void       FC_API fc_get_value_vector2(fc_intptr ptr, fc_vector2 &v);
FC_EXPORT   void       FC_API fc_get_value_vector3(fc_intptr ptr, fc_vector3 &v);
FC_EXPORT   void       FC_API fc_get_value_vector4(fc_intptr ptr, fc_vector4 &v);
FC_EXPORT   void       FC_API fc_get_value_plane(fc_intptr ptr, fc_plane &v);
FC_EXPORT   void       FC_API fc_get_value_matrix(fc_intptr ptr, fc_matrix &v);
FC_EXPORT   void       FC_API fc_get_value_bounds(fc_intptr ptr, fc_bounds &v);
FC_EXPORT   void       FC_API fc_get_value_quaternion(fc_intptr ptr, fc_quaternion &v);
FC_EXPORT   void       FC_API fc_get_value_ray(fc_intptr ptr, fc_ray &v);
FC_EXPORT   void       FC_API fc_get_value_color32(fc_intptr ptr, fc_color32 &v);
FC_EXPORT   void       FC_API fc_get_value_color(fc_intptr ptr, fc_color &v);
FC_EXPORT   void       FC_API fc_get_value_intrect(fc_intptr ptr, fc_intrect &v);
FC_EXPORT   void       FC_API fc_get_value_rect(fc_intptr ptr, fc_rect &v);
FC_EXPORT   void       FC_API fc_get_value_sphere(fc_intptr ptr, fc_sphere &v);

// 功能：当脚本调用C#Wrap接口时，取脚本指定参数变量的指针地址
// 功能：VM - 脚本虚拟机
//       ptr - 脚本对象地址
//       csharp_ptr - C#端结构指针
//       nSize - 对象所占字节数
FC_UNSAFE   FC_EXPORT   void       FC_API fc_get_struct_value(fc_intptr ptr, fc_unsafe_ptr csharp_ptr, int nSize);

//--------------------------------------------------------------------------------------
// 以下是数组相关操作的接口
//
// 功能：设置对象数组
// 功能：得到数组的长度
//       ptr - 脚本数组指针
FC_EXPORT   int   FC_API fc_get_array_size(fc_intptr ptr);

// 功能：分配数组大小, 指定数组长度
// 参数：VM - 虚拟机指针
//       ptr - 脚本数组指针
FC_EXPORT   void  FC_API fc_set_array_size(fc_intptr VM, fc_intptr ptr, int nSize);

// 功能：得到数组指定下标的节点
// 说明：这个是全局的，请不要在外部保存，只是临时的噢, 只是用来做拷贝或读取数组参数
// 参数：VM - 虚拟机指针
//       ptr - 数组变量指针(fc_c_param_ins)
// 返回值：返回数组下标指向的对象指针
FC_EXPORT   fc_intptr  FC_API fc_get_array_node_temp_ptr(fc_intptr VM, fc_intptr ptr, int nIndex);

FC_EXPORT   void  FC_API fc_set_array_bool(fc_intptr ptr, fc_bool_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_byte(fc_intptr ptr, fc_byte_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_short(fc_intptr ptr, fc_short_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_ushort(fc_intptr ptr, fc_ushort_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_int(fc_intptr ptr, fc_int_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_uint(fc_intptr ptr, fc_uint_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_float(fc_intptr ptr, fc_float_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_double(fc_intptr ptr, fc_double_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_color32(fc_intptr ptr, fc_color32_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_color(fc_intptr ptr, fc_color_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_vector2(fc_intptr ptr, fc_vector2_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_vector3(fc_intptr ptr, fc_vector3_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_vector4(fc_intptr ptr, fc_vector4_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_set_array_rect(fc_intptr ptr, fc_rect_ptr pArray, int nStart, int nCount);


FC_EXPORT   void  FC_API fc_get_array_bool(fc_intptr ptr, fc_bool_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_byte(fc_intptr ptr, fc_byte_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_short(fc_intptr ptr, fc_short_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_ushort(fc_intptr ptr, fc_ushort_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_int(fc_intptr ptr, fc_int_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_uint(fc_intptr ptr, fc_uint_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_float(fc_intptr ptr, fc_float_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_double(fc_intptr ptr, fc_double_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_color32(fc_intptr ptr, fc_color32_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_color(fc_intptr ptr, fc_color_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_vector2(fc_intptr ptr, fc_vector2_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_vector3(fc_intptr ptr, fc_vector3_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_vector4(fc_intptr ptr, fc_vector4_ptr pArray, int nStart, int nCount);
FC_EXPORT   void  FC_API fc_get_array_rect(fc_intptr ptr, fc_rect_ptr pArray, int nStart, int nCount);

//--------------------------------------------------------------------------------------
// 以下是对map的操作的接口
// 
// 功能：得到map的大小
FC_EXPORT   int   FC_API fc_get_map_size(fc_intptr VM, fc_intptr pMapPtr);

// 功能：准备初始化map迭代器
// 返回值：成功返回true, map为空时返回false
FC_EXPORT   bool  FC_API fc_map_prepare_view(fc_intptr VM, fc_intptr pMapPtr);

// 功能：将map的全局迭代器指向下一个节点
// 返回值：成功返回true, 失败返回false
// 说明：如果是首次调用，就指向第一个节点, 在调用这个函数之前，必须必须fc_map_prepare_view_pair,不然可能出现异常情况
FC_EXPORT   bool  FC_API fc_map_to_next_pair(fc_intptr VM);

// 功能：得到map的key节点的指针 
// 参数：
// 返回值 - 可以调用get_value_xxx(pPair)来获取 key的值，但切不可调用 set_value_xxx(pPair)来设置的噢
FC_EXPORT   fc_intptr  FC_API fc_map_get_cur_key_ptr(fc_intptr VM);

// 功能：得到map的value节点的指针 
// 参数：
// 返回值 - 可以调用get_value_xxx(pPair)来获取 key的值，也可以调用 set_value_xxx(pPair)来设置
FC_EXPORT   fc_intptr  FC_API fc_map_get_cur_value_ptr(fc_intptr VM);

// -----------------------------以下是对map的修改接口-----------------------------------
// 功能：将map清空
FC_EXPORT   void  FC_API fc_map_clear(fc_intptr VM, fc_intptr pMapPtr);

// 功能：得到map临时添加的key指针
FC_EXPORT   fc_intptr  FC_API fc_get_map_push_key_ptr(fc_intptr VM, fc_intptr pMapPtr);
// 功能：得到map临时添加的value指针
FC_EXPORT   fc_intptr  FC_API fc_get_map_push_value_ptr(fc_intptr VM, fc_intptr pMapPtr);

// 功能：添加一个map的key-value
// 参数：pMapPtr - map跨平台的指针
// 说明：key -  可由fc_get_map_push_key设置
//       value - 可由fc_get_map_push_value设置
FC_EXPORT   void  FC_API fc_map_push_key_value(fc_intptr VM, fc_intptr pMapPtr);


//--------------------------------------------------------------------------------------
// 以下是关于委托相关函数
// 
// 功能：得到委托函数的参数对象指针
// 参数：pDelegatePtr - 函数指针
// 返回值：返回参数对象的指针
FC_EXPORT   fc_intptr   FC_API  fc_inport_delegate_get_obj_ptr(fc_intptr VM, fc_intptr pDelegatePtr);

// 功能：得到委托函数的类ID
FC_EXPORT   int         FC_API  fc_inport_delegate_get_class_name_id(fc_intptr VM, fc_intptr pDelegatePtr);
// 功能：得到委托函数的函数ID
FC_EXPORT   int         FC_API  fc_inport_delegate_get_func_name_id(fc_intptr VM, fc_intptr pDelegatePtr);

FC_EXPORT   int         FC_API  fc_inport_delegate_get_class_name_len(fc_intptr VM, fc_intptr pDelegatePtr);
FC_EXPORT   int         FC_API  fc_inport_delegate_get_class_name(fc_intptr VM, fc_intptr pDelegatePtr, fc_byte_ptr pOutBuff, int nOutBuffSize);
FC_EXPORT   int         FC_API  fc_inport_delegate_get_func_name_len(fc_intptr VM, fc_intptr pDelegatePtr);
FC_EXPORT   int         FC_API  fc_inport_delegate_get_func_name(fc_intptr VM, fc_intptr pDelegatePtr, fc_byte_ptr pOutBuff, int nOutBuffSize);

//--------------------------------------------------------------------------------------

// 动态注册一个接口
FC_EXPORT FC_CPP_ONLY bool  FC_API  fc_runtime_register(fc_intptr info_ptr, int info_size, int nFC_Version);

//--------------------------------------------------------------------------------------

#endif

#endif