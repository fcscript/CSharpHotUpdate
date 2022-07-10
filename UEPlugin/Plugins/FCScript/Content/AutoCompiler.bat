@echo off

set RootPath=%cd%

::--run=main --script_path=aa//bb --fcproj=xxx/xxx/aa.fcproj
:: --output_path=
:: --auto_quit
:: --export_proto
:: --proto_path=
:: --script_path

call ScriptEditor.exe --fcproj=$(curpath)UEScript.fcproj

pause

exit /B 0