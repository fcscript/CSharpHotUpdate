// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.Collections.Generic;

public class FCProjectTarget : TargetRules
{
	public FCProjectTarget( TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		DefaultBuildSettings = BuildSettingsVersion.V2;
		ExtraModuleNames.AddRange( new string[] { "FCProject" } );

        GlobalDefinitions.Add("ALLOW_LOW_LEVEL_MEM_TRACKER_IN_TEST=1");
    }
}
