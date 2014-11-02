using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;
using Seika.Transform.Command.Client;
using Seika.Transform.Command.Enum;

/// <summary>
/// 页面处理用xml共通
/// </summary>
/// <remarks>
/// 2008/12/24  于作伟  新规作成
namespace Seika.COO.Web.PG
{
    public class UserSiteXmlManager
    {
        //基础xml类型
        public const String xmlRegisterType = "REGISTER";
        public const String xmlCompanyType = "COMPANY";
        public const String xmlWebSiteType = "WEBSITE";

        static PageXmlSymbol m_pageXmlSym = new PageXmlSymbol();
        public UserSiteXmlManager()
        {
        }

        /// <summary>
        /// 从基础xml复制数据到用户xml表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="tradeName"></param>
        public static void CopyCommonXml(String registId,String language,String tradeName)
        {
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            da.InsertCommonUserXml(registId, language, tradeName, xmlRegisterType);
        }

        /// <summary>
        /// 取得基础xml
        /// </summary>
        /// <param name="language"></param>
        /// <param name="havingFlg"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public static XmlDocMangContent GetBasicXml(String language, String havingFlg,String[] userXmlId)
        {
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            XmlDocMangContent xmlm = new XmlDocMangContent();
            DataTable dt = da.GetBasicXmlList(language, havingFlg, userXmlId);
            foreach (DataRow dr in dt.Rows)
            {
                string xmlText = dr["XMLCON"].ToString();
                XmlReaderSettings settings = new XmlReaderSettings();
                StringReader stringReader = new StringReader(xmlText);
                XmlReader xmlReader = XmlReader.Create(stringReader, settings);

                XmlDocManage xmld = new XmlDocManage(xmlReader);
                xmlm[dr["XMLNAME"].ToString()] = xmld;
            }
            return xmlm;
        }
        /// <summary>
        /// 取得基础行业xml列表
        /// </summary>
        /// <param name="language"></param>
        /// <param name="partitionflg"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public static XmlDocMangContent GetBasicXmlPart(String language, String partitionflg, String[] userXmlId)
        {
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            XmlDocMangContent xmlm = new XmlDocMangContent();
            DataTable dt = da.GetBasicXmlListPart(language, partitionflg, userXmlId);
            foreach (DataRow dr in dt.Rows)
            {
                string xmlText = dr["XMLCON"].ToString();
                XmlReaderSettings settings = new XmlReaderSettings();
                StringReader stringReader = new StringReader(xmlText);
                XmlReader xmlReader = XmlReader.Create(stringReader, settings);

                XmlDocManage xmld = new XmlDocManage(xmlReader);
                xmlm[dr["XMLNAME"].ToString()] = xmld;
            }
            return xmlm;
        }

        /// <summary>
        /// 取得用户xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public static XmlDocMangContent GetUserXml(String registId, String language, String[] userXmlId) 
        {
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            XmlDocMangContent xmlm = new XmlDocMangContent();
            DataTable dt = da.GetUserXmlList(registId, language,userXmlId);
            foreach (DataRow dr in dt.Rows) 
            {
                string xmlText = dr["XMLCON"].ToString();
                XmlReaderSettings settings = new XmlReaderSettings();
                StringReader stringReader = new StringReader(xmlText);
                XmlReader xmlReader = XmlReader.Create(stringReader, settings);

                XmlDocManage xmld = new XmlDocManage(xmlReader);
                xmlm[dr["MA_USERXMLID"].ToString()] = xmld;
            }
            xmlm.SeoKeyword = da.GetUserXmlListSeoKeyword(registId, language);
            return xmlm;
        }

        /// <summary>
        /// 取得用户全部xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public static XmlDocMangContent GetUserXml(String registId, String language)
        {
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            XmlDocMangContent xmlm = new XmlDocMangContent();
            List<DataTable> dtlist = da.GetUserXmlList(registId, language, xmlCompanyType);
            foreach (DataTable dt in dtlist)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string xmlText = dr["XMLCON"].ToString();
                    XmlReaderSettings settings = new XmlReaderSettings();
                    StringReader stringReader = new StringReader(xmlText);
                    XmlReader xmlReader = XmlReader.Create(stringReader, settings);

                    XmlDocManage xmld = new XmlDocManage(xmlReader);
                    if (dt.Columns.Contains("MA_USERXMLID"))
                    {
                        xmlm[dr["MA_USERXMLID"].ToString()] = xmld;
                    }
                    else if (dt.Columns.Contains("XMLNAME")) 
                    {
                        xmlm[dr["XMLNAME"].ToString()] = xmld;
                    }
                }
            }
            xmlm.SeoKeyword = da.GetUserXmlListSeoKeyword(registId, language);
            return xmlm;
        }

        /// <summary>
        /// 保存用户xml
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="xmlCont"></param>
        public static void SaveUserXml(String registId, String language,XmlDocMangContent xmlCont)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XMLCON");
            dt.Columns.Add("MA_USERXMLID");

            if (xmlCont != null)
            {
                IEnumerator names = xmlCont.v.GetEnumerator();

                while (names.MoveNext())
                {
                    String key = ((System.Collections.DictionaryEntry)names.Current).Key.ToString();
                    XmlDocManage xmlc = (XmlDocManage)(xmlCont.v[key]);
                    String value = xmlc.xml.InnerXml.ToString();
                    //不对baseXml进行更新
                    if (key.ToLower() != m_pageXmlSym.usCurrency.ToLower())
                    {
                        DataRow dr = dt.NewRow();
                        dr["XMLCON"] = value.Replace("'","''");
                        dr["MA_USERXMLID"] = key;
                        dt.Rows.Add(dr);
                    }
                }
            }
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            da.SaveUserXmlList(registId, language, dt);

        }

        public static void SaveUserXml(String registId, String language, XmlDocMangContent xmlCont, String[] xmlId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XMLCON");
            dt.Columns.Add("MA_USERXMLID");

            if (xmlCont != null)
            {
                IEnumerator names = xmlCont.v.GetEnumerator();

                while (names.MoveNext())
                {
                    String key = ((System.Collections.DictionaryEntry)names.Current).Key.ToString();
                    XmlDocManage xmlc = (XmlDocManage)(xmlCont.v[key]);
                    String value = xmlc.xml.InnerXml.ToString();
                    //不对baseXml进行更新
                    if ((key.ToLower() != m_pageXmlSym.usCurrency.ToLower()) && (Array.IndexOf(xmlId,key) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["XMLCON"] = value.Replace("'", "''");
                        dr["MA_USERXMLID"] = key;
                        dt.Rows.Add(dr);
                    }
                }
            }
            Seika.COO.PageData.UserSiteXmlManagerData da = new Seika.COO.PageData.UserSiteXmlManagerData();
            da.SaveUserXmlList(registId, language, dt);

        }
    }

    public class XmlDocMangContent
    {
        public Hashtable v = new Hashtable();

        public XmlDocManage this[string name]
        {
            get { return (XmlDocManage)v[name]; }
            set { v[name] = value; }
        }
        public DataTable SeoKeyword
        {
            get;
            set;
        }
    }
}
