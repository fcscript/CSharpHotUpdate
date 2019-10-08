using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;


[XmlRootAttribute("Root")]
public class FCRefClassCfg
{
    [XmlElementAttribute("RefClass")]
    public List<FCRefClass> RefClass = new List<FCRefClass>();

    public static FCRefClassCfg  LoadCfg(string szPathName)
    {
        StreamReader stream = new StreamReader(szPathName, Encoding.UTF8);
        XmlSerializer xs = new XmlSerializer(typeof(FCRefClassCfg));
        FCRefClassCfg  cfg = xs.Deserialize(stream) as FCRefClassCfg;
        return cfg;
    }
    public static void SaveCfg(FCRefClassCfg cfg, string szPathName)
    {
        UTF8Encoding utf8 = new UTF8Encoding(false);
        StreamWriter stream = new StreamWriter(szPathName, false, utf8);
        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(FCRefClassCfg));
        xs.Serialize(stream, cfg);
        stream.Close();
    }
};

[XmlRootAttribute("RefClass")]
public class FCRefClass
{
    [XmlAttribute("ClassName")]
    public string ClassName;  // 类名
    [XmlElementAttribute("Names")]
    public List<string> names = new List<string>();  // 引用的成员名字(成员函数+属性变量+全局函数)
    [XmlElementAttribute("TemplateParams")]
    public List<FCTemplateParams> TemplateParams;
};
[XmlRootAttribute("TemplateParams")]
public class FCTemplateParams
{
    [XmlAttribute("FuncName")]
    public string FuncName;  // 函数名
    [XmlElementAttribute("Params")]
    public List<string> names = new List<string>();  // 引用的模板参数
};
