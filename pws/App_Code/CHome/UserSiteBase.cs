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
using System.Collections.Generic;
using System.Collections;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;
using Seika.Transform.Command.Client;
using Seika.COO.PageData;


namespace Seika.COO.Web.PG
{
    public class UserSiteBase : PageBase
    {
        UserSiteDataBase pageData = new UserSiteDataBase();
        private String flashType = String.Empty;
        private String imgName = String.Empty;
        private String imgHeight = String.Empty;
        private String LocalPath { get; set; }

        public UserSiteBase(){}
        public UserSiteBase(String localPath) 
        {
            LocalPath = localPath;
        }
        public void SetLanguage(String registId, String language, XmlDocMangContent xmlDocCon, String xslName, String type)
        {
            TransValue usCss = new TransValue();
            TransValue usJs = new TransValue();
            String jsType = String.Empty;
            String defaultLanguage = (m_sessionManager==null || String.IsNullOrEmpty(m_sessionManager.Page_DefaultUICultureID)) ? language : m_sessionManager.Page_DefaultUICultureID;
            String activeLanguage = String.Empty;
            TransValue LanguageList = null;
            List<TransValue> list = GetLanguageList(registId);

            //所有语言列表
            if(list.Count > 0) 
                LanguageList = list[0];

            //当前激活语言列表
            TransValue alang = null;
            if (list.Count > 1) 
            {
                alang = list[1];
            }

            //取得当前最新激活语言列表
            GetCurrActiveLang(type, ref alang, language);

            //取得当前最新语言信息
            GetCurrLangList(ref alang, ref LanguageList);

            //取得激活语言字符串
            if(alang != null && alang.v.Count > 0)
            {
                StringBuilder _alang = new StringBuilder();
                for (int i = 0; i < 10; i++)
                {
                    if (alang[i.ToString()] != null)
                    {
                        _alang.Append("\"");
                        _alang.Append(alang[i.ToString()].ToString());
                        _alang.Append("\"");
                        _alang.Append(",");
                    }
                }
                if (_alang.ToString().Length > 1)
                    activeLanguage = _alang.ToString().Substring(0,_alang.ToString().Length - 1);
            }

            switch (type)
            {
                case UserSiteLanguageType.ADD:
                    GetXmlCss(registId, language,xmlDocCon,ref jsType, ref usCss, ref usJs);
                    m_usrSiteMager.AddUserLang(registId, language, xmlDocCon, defaultLanguage, activeLanguage, LanguageList, xslName, usCss, jsType, usJs);
                    break;
                case UserSiteLanguageType.SET:
                    GetXmlCss(registId, language,xmlDocCon,ref jsType, ref usCss, ref usJs);
                    m_usrSiteMager.SetUserLang(registId, language, xmlDocCon, defaultLanguage, activeLanguage, LanguageList, xslName, usCss, jsType, usJs);
                    break;
                case UserSiteLanguageType.SETCONFIG:
                    m_usrSiteMager.SetUserLangConfig(registId, language, xmlDocCon, defaultLanguage, activeLanguage, LanguageList, xslName);
                    break;
                case UserSiteLanguageType.DIY:
                    GetXmlCss(registId, language, xmlDocCon, ref jsType, ref usCss, ref usJs);
                    m_usrSiteMager.SetUserLangDiyStyle(registId, language, xmlDocCon, jsType, xslName, usCss, usJs);
                    break;
                case UserSiteLanguageType.DEL:
                    m_usrSiteMager.UserLangDel(registId, language, xmlDocCon, defaultLanguage, activeLanguage, LanguageList, xslName);
                    break;
            }
        }

        //取得当前激活语言
        private void GetCurrActiveLang(String type, ref TransValue alang, String currLanguage) 
        {
            switch (type)
            {
                case UserSiteLanguageType.ADD:
                    alang[alang.v.Count.ToString()] = currLanguage;
                    break;
                case UserSiteLanguageType.SET:
                    alang[alang.v.Count.ToString()] = currLanguage;
                    break;
                case UserSiteLanguageType.DEL:
                    for (int i = 0; i < alang.v.Count; i++) 
                    {
                        if (alang[i.ToString()] != null) 
                        {
                            if (alang[i.ToString()].ToString() == currLanguage) 
                            {
                                alang.v.Remove(i.ToString());
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //取得当前最新语言信息
        private void GetCurrLangList(ref TransValue alang, ref TransValue LanguageList) 
        {
            object[] keys = new object[LanguageList.v.Keys.Count];
            LanguageList.v.Keys.CopyTo(keys, 0);

            foreach (object key in keys)
            {
                if(!alang.v.ContainsValue(key))
                {
                    LanguageList.v.Remove(key);
                }
            }
        }

        public void GetXmlCss(String registId,String language,XmlDocMangContent xmlDocCon,ref String jsType,ref TransValue usCss, ref TransValue usJs)
        {
            m_pageXmlSym = new PageXmlSymbol();
            XmlDocManage xmlDoc = xmlDocCon[m_pageXmlSym.P3090P0900];
            //构造函数默认自动建立一个DataTable
            DataSetManage dsc = new DataSetManage(xmlDoc);
            DataTable dtHead = dsc.Get.Tables["headstyle"];
            DataTable dtBody = dsc.Get.Tables["bodystyle"];
            imgName = dtHead.Rows[0]["headres"].ToString();
            imgHeight = dtHead.Rows[0]["height"].ToString();
            if (!String.IsNullOrEmpty(imgName))
            {
                String[] sname = imgName.Split('.');
                if (sname.Length > 0)
                    flashType = sname[sname.Length - 1];
            }

            if(dtBody.Rows.Count > 0)
            {
                usCss[TransKey.USCSS_HEAD_BACKGROUND_HEIGHT_KEY] = imgHeight;
                usCss[TransKey.USCSS_HEAD_BACKGROUND_IMG_KEY] = imgName;
                usCss[TransKey.USCSS_BODY_BACKGROUND_COLOR_KEY] = dtBody.Rows[0]["background-color"].ToString();
                usCss[TransKey.USCSS_BODY_BACKGROUND_IMG_KEY] = dtBody.Rows[0]["background-image"].ToString();
                usCss[TransKey.USCSS_BODY_BACKGROUND_REPEAT_KEY] = dtBody.Rows[0]["background-repeat"].ToString();
            }

            if (flashType == "swf" || flashType == "SWF")
            {
                jsType = TransType.USJS_FLASH;
                usJs[TransKey.FLASH_FLASHNAME] = imgName;
                usJs[TransKey.FLASH_FLASH_HEIGHT] = imgHeight;
            }
            else
            {
                jsType = TransType.USJS_NONE_FLASH;
                usJs[TransKey.FLASH_FLASHNAME] = "";
                usJs[TransKey.FLASH_FLASH_HEIGHT] = "";
            }

        }

        private List<TransValue> GetLanguageList(String registId)
        {
            List<TransValue> tlist = new List<TransValue>();
            TransValue tv = new TransValue();
            TransValue dtv = new TransValue();
            DataTable dt = pageData.GetLanguageList(registId);
            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;
                for (int j = dt.Rows.Count - 1; j >= 0; j--)
                {
                    tv[dt.Rows[j]["MA_LANGUAGEID"].ToString()] = dt.Rows[j]["LANGNAME"].ToString();
                    if (!String.IsNullOrEmpty(dt.Rows[j]["REGISTID"].ToString()))
                    {
                        dtv[i.ToString()] = dt.Rows[j]["MA_LANGUAGEID"].ToString();
                        i++;
                    }
                }
                //if(i == 0)
                //    dtv[0] = language;
            }
            tlist.Add(tv);
            tlist.Add(dtv);
            return tlist;
        }
    }
}

public class UserSiteLanguageType
{
    public const String ADD = "add";
    public const String SET = "set";
    public const String SETCONFIG = "setconfig";
    public const String DIY = "diy";
    public const String DEL = "del";
}