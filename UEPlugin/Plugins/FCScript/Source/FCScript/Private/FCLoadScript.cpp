#include "FCLoadScript.h"
#include "Misc/Paths.h"
#include "Misc/FileHelper.h"
#include "Logging/LogCategory.h"
#include "Logging/LogMacros.h"
#include "FCRunTimeRegister.h"
#include "FCInitWrap.h"
#include "FCBrigeHelper.h"

#include "../../FCLib/include/fc_api.h"

DEFINE_LOG_CATEGORY(LogFCScript);

void  UE_ScriptError(fc_pcstr pcsInfo)
{
	UE_LOG(LogFCScript, Error, TEXT("%s"), UTF8_TO_TCHAR(pcsInfo));
}
void  UE_ScriptPrint(fc_pcstr pcsInfo)
{
	UE_LOG(LogFCScript, Log, TEXT("%s"), UTF8_TO_TCHAR(pcsInfo));
}

bool  LoadFCScript(FCScriptContext *Context, const FString  &ScriptPathName)
{
	if(Context->m_bInit)
	{
		return Context->m_ScriptVM != 0;
	}
	Context->m_bInit = true;

	fc_set_output_error_func(UE_ScriptError);
	fc_set_debug_print_func(UE_ScriptPrint);

	int64 VM = fc_init(fc_get_main_vm() == 0);
	Context->m_ScriptVM = VM;

	if(IFileManager::Get().FileExists(*ScriptPathName))
	{
		TArray<uint8> Data;
		FFileHelper::LoadFileToArray(Data, *ScriptPathName, 0);

		fc_set_code_data(VM, (fc_byte_ptr)Data.GetData(), Data.Num(), 0);
	}
	else
	{
		UE_LOG(LogTemp, Warning, TEXT("not find script file from dir : %s"), *ScriptPathName);
	}

	// 开始注册吧
	RunTimeRegisterScript(Context);

	// 静态注册
	FCInitWrap::Register(VM);

	// 注册全局C++类
	FCExportedClass::RegisterAll(VM);
	
	fc_set_output_error_func(UE_ScriptError);
	fc_set_debug_print_func(UE_ScriptPrint);

	// 加载UE类注册信息

	return true;
}