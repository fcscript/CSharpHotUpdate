数据转换工具：
1、支持将制表符格式的文本文件转换成二进制数据表
2、支持将原始的xlsx数据表直接转换成二进制数据表
3、支持将多个源表或二进制文件合并成一个大的数据表文件
   合并后，可以在脚本中将组合的文件一次性加载出来，减少IO开销
4、支持将二进制文件再转换成制表符格式的文本文件
5、支持枚举类型，可以将数据表的中枚举类型直接转换成整数
   
6、支持分表，可以将同一个功能（格式相同）的源表放一个子目录下


转表工具参数解释：

excel_exportR32.exe
		--curpath=xxx         设置工作目录，可选, 默认是当前路径, 可以是绝对路径，也可是相对路径
		                      如 --curpath="D:\excel\"
					             --curpath="../../"
		--output_path=xxx     设置输出目录(或输出文件，取决于是按目录转，还是直接转文件)
		--code_enum_path=xxx  设置源码的枚举目录,这个是脚本中最终使用的枚举
		--excel_enum_path=xxx 设置数据表的枚举定义目录（这个可以依赖code_enum_path), 这个是中间转换的，一般是中文枚举, 用来将中文字符串转换成枚举
		--skip_row_num=0      设置跳过源表的行数（不导出），默认是0，不需要处理。如果源表是xlsx, 自动跳过第一行
		--bin_to_text=false   是不是转换成text, 默认是false
		--group=false         是不是群组，默认是false
		                      如果这个设置成true, 并且是按目录转二进制，则会将所有的文件合并一个大文件
		--ext=bin             可选，设置二进制文件的扩展名, 默认是bin
		
		如果文件路径太长，可以使用相对路径或{curpath}替换,
		如 --code_enum_path={curpath}/CodeEnum
		   --code_enum_path=../../Scrip/CodeEnum
							  
数据表说明：
1、如果xlsx表只有一个table，默认按这个导出
2、如果xlsx存在多个table, 需要指定导出那个table
   关键字：
   export    --如果table名字中包含了export，则表示当前表需要导出
   dontexport -- 表名中包含了该关键字，则表示不导出
3、可以在表中指定数据列的类型
   关键字：int8, int16, int32, int64, float, double, string  这些表示数据类型
           dontexport 表示该列不导出
		   keycol 表示该列是关键例(唯一)
		   enum 含该关键字表示该列是枚举
		   
    如：第一行，是中文注释
	    第二行，是类型定义
		第三行，是列的名字
	
	ID	           名字     类型
	int32(keycol)  string	ObjType(enum)
	id	           name     type
   
