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

    Dictionary<string, FCRefClass> m_Finder;

    public static FCRefClassCfg  LoadCfg(string szPathName)
    {
        FCRefClassCfg cfg = null;
        try
        {
            if(!File.Exists(szPathName))
            {
                Debug.Log(szPathName + "不存在，请先编译或手动配置。");
                return null;
            }
            StreamReader stream = new StreamReader(szPathName, Encoding.UTF8);
            XmlSerializer xs = new XmlSerializer(typeof(FCRefClassCfg));
            cfg = xs.Deserialize(stream) as FCRefClassCfg;
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
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
    void  MakerFinder(List<FCRefClass>  rList)
    {
        if (rList == null)
            return;
        if (m_Finder == null)
            m_Finder = new Dictionary<string, FCRefClass>();
        foreach (FCRefClass r in rList)
        {
            FCRefClass old = null;
            if(m_Finder.TryGetValue(r.ClassName, out old))
            {
                old.MergeFinder(r);
            }
            else
            {
                m_Finder[r.ClassName] = r;
            }
        }
    }
    public void MergeFinder(FCRefClassCfg other)
    {
        // 合并
        m_Finder = null;
        MakerFinder(RefClass);
        if (other == null)
            return;
        MakerFinder(other.RefClass);
    }
    public FCRefClass  FindClass(string szClassName)
    {
        FCRefClass ptr = null;
        if (m_Finder != null && m_Finder.TryGetValue(szClassName, out ptr))
            return ptr;
        return null;
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

    Dictionary<string, bool> m_namesFinder;
    Dictionary<string, FCTemplateParams> m_TemplateFinder;

    void  MakeNamesFinder(List<string>  rList)
    {
        if (rList == null)
            return;
        if (m_namesFinder == null)
            m_namesFinder = new Dictionary<string, bool>();
        foreach(string r in rList)
        {
            m_namesFinder[r] = true;
        }
    }
    void   MakeTemplateFinder(List<FCTemplateParams> rList)
    {
        if (rList == null)
            return;
        if (m_TemplateFinder == null)
            m_TemplateFinder = new Dictionary<string, FCTemplateParams>();
        foreach(FCTemplateParams r in rList)
        {
            FCTemplateParams old = null;
            if(m_TemplateFinder.TryGetValue(r.FuncName, out old))
            {
                old.MergeFinder(r);
            }
            else
            {
                m_TemplateFinder[r.FuncName] = r;
            }
        }
    }

    public bool FindMember(string szName)
    {
        if (m_namesFinder == null)
            return true;
        return m_namesFinder.ContainsKey(szName);
    }

    public void MergeFinder(FCRefClass other)
    {
        m_namesFinder = null;
        m_TemplateFinder = null;
        MakeNamesFinder(names);
        MakeNamesFinder(other.names);
        MakeTemplateFinder(TemplateParams);
        MakeTemplateFinder(other.TemplateParams);
    }
};
[XmlRootAttribute("TemplateParams")]
public class FCTemplateParams
{
    [XmlAttribute("FuncName")]
    public string FuncName;  // 函数名
    [XmlElementAttribute("Params")]
    public List<string> names = new List<string>();  // 引用的模板参数

    Dictionary<string, bool> m_namesFinder;

    void MakeNamesFinder(List<string> rList)
    {
        if (rList == null)
            return;
        if (m_namesFinder == null)
            m_namesFinder = new Dictionary<string, bool>();
        foreach (string r in rList)
        {
            m_namesFinder[r] = true;
        }
    }
    public void MergeFinder(FCTemplateParams other)
    {
        m_namesFinder = null;
        MakeNamesFinder(names);
        MakeNamesFinder(other.names);
    }
};
