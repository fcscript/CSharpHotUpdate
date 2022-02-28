// Tencent is pleased to support the open source community by making UnLua available.
// 
// Copyright (C) 2019 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the MIT License (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at
//
// http://opensource.org/licenses/MIT
//
// Unless required by applicable law or agreed to in writing, 
// software distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.

#include "Modules/ModuleManager.h"
#include "GameDelegates.h"
#include "Misc/CoreDelegates.h"
#include "FCDelegateModule.h"

#if WITH_EDITOR
#include "Editor.h"
#endif

#define LOCTEXT_NAMESPACE "FUnLuaModule"

class FFCScriptModule : public IModuleInterface
{
public:
    virtual void StartupModule() override
	{
		DelegateModule.OnStartupModule();
    }

    virtual void ShutdownModule() override
    {
	}

	FFCDelegateModule  DelegateModule;
};

#undef LOCTEXT_NAMESPACE

IMPLEMENT_MODULE(FFCScriptModule, FCScript)
