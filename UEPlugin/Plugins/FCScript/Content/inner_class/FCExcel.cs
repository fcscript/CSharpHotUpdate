using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FCExcel
{
    // 功能：从一个内存Buffer中加载数据表
    public bool Load(StringA szFileData)
    {
        return true;
    }

    // 功能：从一个内存Buffer中加载数据表
    public static FCExcel LoadExel(StringA szFileData)
    {
        FCExcel obj = new FCExcel();
        obj.Load(szFileData);
        return obj;
    }
    // 功能：从一个合并的大文件中加载一组数据表
    public static List<FCExcel> LoadGroupExcel(StringA szFileData)
    {
        List<FCExcel> aResult = new List<FCExcel>();
        return aResult;
    }

    // 功能：查询数据表指定行列的值(整型)
    public int GetInt(int nRow, int nCol)
    {
        return 0;
    }
    // 功能：查询数据表指定行列的值(64整型)
    public Int64 GetInt64(int nRow, int nCol)
    {
        return 0;
    }
    // 功能：查询数据表指定行列的值(float)
    public float GetFloat(int nRow, int nCol)
    {
        return 0;
    }
    // 功能：查询数据表指定行列的值(double)
    public double GetDouble(int nRow, int nCol)
    {
        return 0;
    }
    // 功能：查询数据表指定行列的值(StringA)
    public StringA GetString(int nRow, int nCol)
    {
        return StringA("");
    }
    // 功能：通过关键ID查找行
    public int FindLineByID(int nID)
    {
        return 0;
    }
    // 功能：通过关键ID查找行
    //       nKeyCol - 指定关键列
    public int FindLineByID(int nID, int nKeyCol)
    {
        return 0;
    }
    // 功能：通过关键ID查找行
    public int FindLineByStr(StringA szKey)
    {
        return 0;
    }
    // 功能：通过关键ID查找行
    public int FindLineByStr(StringA szKey, int nKeyCol)
    {
        return 0;
    }

    // 功能：查找行
    public int Find(int nID)
    {
        return FindLineByID(nID);
    }
    public int Find(int nID, int nKeyCol)
    {
        return FindLineByID(nID);
    }
    public int Find(StringA szKey)
    {
        return FindLineByStr(szKey);
    }
    public int Find(StringA szKey, int nKeyCol)
    {
        return FindLineByStr(szKey, nKeyCol);
    }

    // 功能：按列名查找列索引, 没有找到的话返回-1
    public int FindColByName(StringA szColName)
    {
        return -1;
    }
    // 功能：按列名查找列索引, 没有找到的话返回-1
    //       nCol - 优先在这列查找(只是一个优化)
    public int FindColByName(StringA szColName, int nCol)
    {
        return -1;
    }

    // 功能：返回数据表的总行数
    public int GetRowNumb()
    {
        return 0;
    }
    // 功能：返回数据表的总列数
    public int GetColNumb()
    {
        return 0;
    }
    // 返回关键列
    public int GetKeyCol()
    {
        return -1;
    }

    // 功能：返回指定列的名字
    public StringA GetColName(int nCol)
    {
        return "";
    }

    // 功能：返回数据表的文件名
    public StringA GetExcelName()
    {
        return "";
    }
    public int RowNumb
    {
        get { return GetRowNumb(); }
    }
    public int ColNumb
    {
        get { return GetColNumb(); }
    }
    public int KeyCol
    {
        get { return GetKeyCol(); }
    }
    public StringA ExcelName
    {
        get { return GetExcelName(); }
    }

    //public static FCObject operator= (FCObject left, System.Object other)  // 重载赋值操作符，支持自定义class, 或外部平台导入的class
    //{
    //    return null;
    //}
    //public bool operator ==(T other);
    //public bool operator !=(T other);
    //public _Ty operator()(T);   // 强制转换接口 (CTestD)obj;    
};