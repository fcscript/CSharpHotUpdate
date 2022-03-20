#include "FCTMapIteratorWrap.h"
#include "Containers/Map.h"
#include "FCTemplateType.h"

#include "FCObjectManager.h"
#include "FCGetObj.h"

void FCTMapIteratorWrap::Register(fc_intptr VM)
{
	int nClassName = fc_get_inport_class_id(VM, "TMapIterator");
	fc_register_class_new(VM, nClassName, obj_new);
	fc_register_class_del(VM, nClassName, obj_del);
	fc_register_class_release_ref(VM, nClassName, obj_release);

	// 属性方法
	fc_register_class_attrib(VM, nClassName, "key", Key_wrap, nullptr);
	fc_register_class_attrib(VM, nClassName, "value", GetValue_wrap, nullptr);

	fc_register_class_func(VM, nClassName, "IsValid", IsValid_wrap);
	fc_register_class_func(VM, nClassName, "ToNext", ToNext_wrap);
	fc_register_class_func(VM, nClassName, "Reset", Reset_wrap);
}

int FCTMapIteratorWrap::obj_new(fc_intptr L)
{
	// FScriptArray *ScriptArray = new FScriptArray;
	// 这个还是不要让动态构建的好了
	// 因为不管怎么样，就算是相同的，也是需要拷贝的
	fc_intptr RetPtr = fc_get_return_ptr(L);
	fc_intptr VM = fc_get_vm_ptr(L);

	return 0;
}
int FCTMapIteratorWrap::obj_del(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->DeleteValue(nIntPtr);
	return 0;
}
int FCTMapIteratorWrap::obj_release(fc_intptr nIntPtr)
{
	FCGetObj::GetIns()->ReleaseValue(nIntPtr);
	return 0;
}

int FCTMapIteratorWrap::Key_wrap(fc_intptr nIntPtr)
{
	return 0;
}
int FCTMapIteratorWrap::GetValue_wrap(fc_intptr nIntPtr)
{
	return 0;
}
int FCTMapIteratorWrap::SetValue_wrap(fc_intptr nIntPtr)
{
	return 0;
}

int FCTMapIteratorWrap::IsValid_wrap(fc_intptr nIntPtr)
{
	return 0;
}

int FCTMapIteratorWrap::ToNext_wrap(fc_intptr nIntPtr)
{
	return 0;
}

int FCTMapIteratorWrap::Reset_wrap(fc_intptr nIntPtr)
{
	return 0;
}