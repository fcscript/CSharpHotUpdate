@echo off

set curpath=%cd%

call excel_exportR32.exe --curpath=%curpath% --excel_path={curpath}/bin --output_path={curpath}/text --bin_to_text=true --code_enum_path=CodeEnum --excel_enum_path=ExcelEnum
::call excel_exportR32.exe --excel_path=xlsx --output_path=bin --ext=bin --code_enum_path=CodeEnum --excel_enum_path=ExcelEnum

pause