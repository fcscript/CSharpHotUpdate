@echo off

set CurPath=%cd%

set ScriptPath=%CurPath%\Source
fc_cmd.exe --script_path=%ScriptPath% --run=main


pause

exit /B 0

