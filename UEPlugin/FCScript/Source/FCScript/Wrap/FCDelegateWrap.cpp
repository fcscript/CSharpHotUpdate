#include "FCDelegateWrap.h"
#include "FCObjectManager.h"
#include "FCGetObj.h"

void FCDelegateWrap::Register(fc_intptr VM)
{
	RegisterDelegate(VM, "DelegateEvent");
	RegisterDelegate(VM, "MulticastDelegateEvent");
}

void FCDelegateWrap::RegisterDelegate(fc_intptr VM, const char *ClassName)
{	
    int nClassName = fc_get_inport_class_id(VM, ClassName);
    //fc_register_class_new(VM, nClassName, obj_new);
    //fc_register_class_del(VM, nClassName,obj_del);
    fc_register_class_release_ref(VM, nClassName,obj_release);
    //fc_register_class_hash(VM, nClassName,obj_hash);
    //fc_register_class_equal(VM, nClassName,obj_equal);
    fc_register_class_func(VM, nClassName,"AddListener",AddListener_wrap);
    fc_register_class_func(VM, nClassName,"RemoveListener",RemoveListener_wrap);
    fc_register_class_func(VM, nClassName,"ClearLinstener",ClearLinstener_wrap);
    fc_register_class_func(VM, nClassName,"Invoke",Invoke_wrap);
}

int  FCDelegateWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
	return 0;
}

int FCDelegateWrap::AddListener_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef && EFCObjRefType::RefProperty == ObjRef->RefType)
	{
        fc_intptr pDelegatePtr = fc_get_param_ptr(L, 0); // 得到脚本委托参数（临时的，不可保留)
        fc_intptr nObjPtr = fc_inport_delegate_get_obj_ptr(VM, pDelegatePtr); // 得到脚本对象地址
        int nClassNameID = fc_inport_delegate_get_class_name_id(VM, pDelegatePtr);  // 类名
        int nFuncNameID = fc_inport_delegate_get_func_name_id(VM, pDelegatePtr); // 函数名

		UObject  *Object = ObjRef->GetParentObject();
		FFCObjectdManager::GetSingleIns()->RegisterScriptDelegate(Object, ObjRef->DynamicProperty, nObjPtr, nClassNameID, nFuncNameID);
	}
	return 0;
}

int FCDelegateWrap::RemoveListener_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef && EFCObjRefType::RefProperty == ObjRef->RefType)
	{
        fc_intptr pDelegatePtr = fc_get_param_ptr(L, 0); // 得到脚本委托参数（临时的，不可保留)
        fc_intptr nObjPtr = fc_inport_delegate_get_obj_ptr(VM, pDelegatePtr); // 得到脚本对象地址
        int nClassNameID = fc_inport_delegate_get_class_name_id(VM, pDelegatePtr);  // 类名
        int nFuncNameID = fc_inport_delegate_get_func_name_id(VM, pDelegatePtr); // 函数名

		UObject  *Object = ObjRef->GetParentObject();
		FFCObjectdManager::GetSingleIns()->RemoveScriptDelegate(Object, ObjRef->DynamicProperty, nObjPtr, nClassNameID, nFuncNameID);
	}
	return 0;
}

int FCDelegateWrap::ClearLinstener_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef && EFCObjRefType::RefProperty == ObjRef->RefType)
	{
        fc_intptr pDelegatePtr = fc_get_param_ptr(L, 0); // 得到脚本委托参数（临时的，不可保留)
        fc_intptr nObjPtr = fc_inport_delegate_get_obj_ptr(VM, pDelegatePtr); // 得到脚本对象地址
        int nClassNameID = fc_inport_delegate_get_class_name_id(VM, pDelegatePtr);  // 类名
        int nFuncNameID = fc_inport_delegate_get_func_name_id(VM, pDelegatePtr); // 函数名
        UObject* Object = ObjRef->GetParentObject();
        FFCObjectdManager::GetSingleIns()->ClearScriptDelegate(Object, ObjRef->DynamicProperty);
	}
	return 0;
}

int FCDelegateWrap::Invoke_wrap(fc_intptr L)
{
    fc_intptr VM = fc_get_vm_ptr(L);
    fc_intptr nThisPtr = fc_get_inport_obj_ptr(L);
	FCObjRef *ObjRef = FCGetObj::GetIns()->FindValue(nThisPtr);
	if(ObjRef && EFCObjRefType::RefProperty == ObjRef->RefType)
	{
        fc_intptr pDelegatePtr = fc_get_param_ptr(L, 0); // 得到脚本委托参数（临时的，不可保留)
        fc_intptr nObjPtr = fc_inport_delegate_get_obj_ptr(VM, pDelegatePtr); // 得到脚本对象地址
        int nClassNameID = fc_inport_delegate_get_class_name_id(VM, pDelegatePtr);  // 类名
        int nFuncNameID = fc_inport_delegate_get_func_name_id(VM, pDelegatePtr); // 函数名
	}
	return 0;
}
