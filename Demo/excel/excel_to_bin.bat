@echo off

set curpath=%cd%

call excel_exportR32.exe --curpath=%curpath% --excel_path={curpath}/xlsx --output_path={curpath}/bin --code_enum_path=CodeEnum --excel_enum_path=ExcelEnum
::call excel_exportR32.exe --excel_path=xlsx --output_path=bin --ext=bin --code_enum_path=CodeEnum --excel_enum_path=ExcelEnum

pause