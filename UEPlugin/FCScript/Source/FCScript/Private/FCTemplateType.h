#pragma once
#include "FCDynamicClassDesc.h"
#include "UObject/UObjectGlobals.h"
#include "Containers/ScriptArray.h"
#include "Containers/Map.h"

enum FCInnerBaseType
{
    FC_VALUE_TYPE_Unknow,
    FC_VALUE_TYPE_bool,            // bool变量 支持(0， 1， TRUE， FALSE, true, false)
    FC_VALUE_TYPE_CHAR,            // char
    FC_VALUE_TYPE_BYTE,            // byte
    FC_VALUE_TYPE_WCHAR,           // wchar
    FC_VALUE_TYPE_SHORT,           // short
    FC_VALUE_TYPE_USHORT,          // unsigned short
    FC_VALUE_TYPE_INT,             // int
    FC_VALUE_TYPE_UINT,            // uint
    FC_VALUE_TYPE_FLOAT,           // float
    FC_VALUE_TYPE_DOUBLE,          // double
    FC_VALUE_TYPE_INT64,           // int64
    FC_VALUE_TYPE_UINT64,          // uint64
    FC_VALUE_TYPE_STRING_A,        // 字符串
    FC_VALUE_TYPE_STRING_W,        // 宽字符串
    FC_VALUE_TYPE_VOID,            // void指针(无类型)
    FC_VALUE_TYPE_OBJECT,          // class对象,  提供一个通用的FCObject关键字
	FC_VALUE_TYPE_FCTYPE,          // type类型, FCType type = typeof(xxxx);
	FC_VALUE_TYPE_FCOBJECT,        // fcobject_ins 通用的class对象, FCObject = 任意class;   FCObject.Create("类名");  FCObject.Create(typedef(类名));  FCObject.IsTypeOf(typedef(类名)); FCObject.IsTypeOf("类名")
    FC_VALUE_TYPE_INPORT_CLASS,    // 外部导入的class
	FC_VALUE_TYPE_EXCEL,           // 数据表对象
    FC_VALUE_TYPE_SERIALIZE,       // CSerialize
    FC_VALUE_TYPE_MAP_ITERATOR,    // 迭代器iterator
    FC_VALUE_TYPE_DELEGATE,        // 函数指针(fc_class_ins_weak_ptr + class_name_id, + func_name_id + 参数列表); delegate
    FC_VALUE_TYPE_IENUMERATOR,     // IEnumerator(fc_coroutine协程)
    
    FC_VALUE_TYPE_VECTOR2,         // Vector2
    FC_VALUE_TYPE_VECTOR3,         // Vector3
    FC_VALUE_TYPE_VECTOR4,         // Vector4
    FC_VALUE_TYPE_PANEL,           // Plane
    FC_VALUE_TYPE_MATRIX,          // Matrix
    FC_VALUE_TYPE_BOUNDBOX,        // BoundBox
    FC_VALUE_TYPE_RAY,             // Ray
    FC_VALUE_TYPE_FRUSTUMBOX,      // FrustumBox
    FC_VALUE_TYPE_SPHERE,          // Sphere
    FC_VALUE_TYPE_INT_RECT,        // IntRect
    FC_VALUE_TYPE_FLOAT_RECT,      // Rect{left, top, right, bottom} width;height;
    FC_VALUE_TYPE_COLOR,           // Color(r,g, b, a)
    FC_VALUE_TYPE_COLOR32,         // Color32(r,g,b,a)
    FC_VALUE_TYPE_QUATERNION,      // Quaternion
    FC_VALUE_TYPE_BEZIER2D,        // Bezier2D
    FC_VALUE_TYPE_BEZIER3D,        // Bezier3D
};

FProperty  *CreateClassProperty(const char *InClassName);
FCDynamicProperty *GetCppDynamicProperty(const char *InClassName);
FCDynamicProperty* GetStructDynamicProperty(UStruct* Struct);
FCDynamicProperty* GetDynamicPropertyByUEProperty(FProperty* InProperty);

FArrayProperty* CreateTArrayProperty(fc_intptr VM, fc_intptr Ptr);

FCDynamicProperty *GetTArrayDynamicProperty(fc_intptr VM, fc_intptr Ptr);

FCDynamicProperty *GetTMapDynamicProperty(fc_intptr VM, fc_intptr Ptr);
void ReleaseTempalteProperty();

void TArray_Clear(FScriptArray *ScriptArray, FProperty *Inner);

void TMap_Clear(FScriptMap* ScriptMap, FMapProperty* MapProperty);