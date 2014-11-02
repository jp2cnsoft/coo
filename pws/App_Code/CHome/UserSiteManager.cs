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
using Seika.Transform.Command.Enum;

/// <summary>
/// 与转换服务通信共通类
/// </summary>
/// <remarks>
/// 2008/01/02  于作伟  新规作成
/// 2008/10/08          追加新接口
namespace Seika.COO.Web.PG
{
    public class UserSiteManager : UserSiteTrans
    {
        PageXmlSymbol m_pageXmlSym = new PageXmlSymbol();

        public UserSiteManager( String serverUrl)
        {
            base.Init(serverUrl);
            m_cs = new CommandSender();
        }

        /// <summary>
        /// 打开操作接口
        /// </summary>
        public void Open() 
        {
            m_cs = new CommandSender();
            //base.Open();
        }
        /// <summary>
        /// 关闭操作接口
        /// </summary>
        public void Close()
        {
            //base.Close();
        }

        /// <summary>
        /// 生成新公司需要建立的页
        /// </summary>
        public void InitNewCompanyPage(String registId, String language, XmlDocMangContent xmlDocList, String xslName) 
        {
            //空白主页
            CreateInitHtml(registId, language, xmlDocList, xslName,
                "blankHomepage", "blankHomepage", "homepage", "chxml/content", "content");
        }

        /// <summary>
        /// 系统初期化页面生成
        /// </summary>
        public void InitSystemHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            //公司介绍
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0310", "P3010P0310", "P3010P0310", "chxml/content", "content");
            //发展历程
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0320", "P3010P0320", "P3010P0320", "chxml/content", "content");
            //主要领导
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0330", "P3010P0330", "P3010P0330", "chxml/content", "content");
            //联系方法
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0340", "P3010P0340", "P3010P0340", "chxml/content", "content");
            //成功案例
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0350", "P3010P0350", "P3010P0350", "chxml/content", "content");
            //组织结构
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0360", "P3010P0360", "P3010P0360", "chxml/content", "content");
            //荣誉奖励
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3010P0370", "P3010P0370", "P3010P0370", "chxml/content", "content");

            //xsl自定义页面
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3080P0000", "P3080P0100", "P3080P0100", "chxml/content", "content");
            CreateInitHtml(registId, language, xmlDocList, xslName, "P3080P0000", "P3080P0110", "P3080P0110", "chxml/content", "content");
        }

        /// <summary>
        /// 系统初期化页面生成(带详细)
        /// </summary>
        public void InitSystemDetailHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            //新闻一览
            //CreateHtmlList("P3040P0020", "P3040P0900", "P3040P0010", "P3040P0010_000_0", "chxml/content", "content");
            //产品一览
            CreateHtmlList(registId, language, xmlDocList, xslName, "P3050P0020", "P3050P0900", "P3050P0010", "P3050P0010_000_0", "chxml/content", "content");
            //留言一览
            CreateHtmlList(registId, language, xmlDocList, xslName, "P3070P0020", "P3070P0900", "P3070P0010", "P3070P0010_000_0", "chxml/content", "content");
            //招聘一览
            CreateHtmlList(registId, language, xmlDocList, xslName, "P3060P0020", "P3060P0900", "P3060P0010", "P3060P0010_000_0", "chxml/content", "content");
        }

        /// <summary>
        /// 首页生成
        /// </summary>
        public void InitIndexHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            //保存xml集合
            String[] xmlid = new string[] { "P3010P0310", "P3010P0340", "P3050P0020", "P3040P0020" };
            //xpath路径
            String xpath = "chxml/content";
            //调用xsl
            String xslId = "homepage";
            //生成html
            String htmlid = "homepage";
            //操作xpath
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_INDEX, xmlDocList, xmlid, 
                null, null, xpath, xslId, m_opType, m_opExt, xslName, htmlid);
        }

        public void CreateInitHtml(String registId, String language,XmlDocMangContent xmlDocList, String xslName,
            String xmlId, String xslId, String htmlid, String xpath, String root)
        {
            //保存xml集合
            String[] xmlid = new string[] { xmlId };
            //操作xpath
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_SINGLE, xmlDocList, xmlid, 
                root, null, xpath, xslId, m_opType, m_opExt, xslName, htmlid);
        }

        public void CreateHtmlList(String registId, String language, XmlDocMangContent xmlDocList, String xslName,  
            String xmlId, String otherId, String xslId, String htmlid, String xpath, String root)
        {
            //保存xml集合
            String[] xmlid = new string[] { xmlId, otherId };
            //操作xpath
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_SINGLE, xmlDocList, xmlid, 
                root, null, xpath, xslId, m_opType, m_opExt, xslName, htmlid);
        }

        /// <summary>
        /// 新闻生成
        /// </summary>
        public void InitNewsHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            CreateNewsListHtml(registId, language, xmlDocList, xslName);
            CreateNewsDetailHtml(registId, language, xmlDocList, xslName);
        }

        public void CreateNewsListHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName) 
        {
            m_dsManage = new DataSetManage(xmlDocList[m_pageXmlSym.P3040P0900]);
            String tempId = "000";
            if (m_dsManage.Get.Tables["name"] != null)
            {
                foreach (DataRow drId in m_dsManage.Get.Tables["name"].Rows)
                {
                    if (drId["typeid"].ToString() != "")
                    {
                        tempId += ("," + drId["typeid"].ToString());
                    }
                }
            }
            //类别字串设置
            String typeid = String.Format("typeid={0}", tempId);
            //保存xml集合
            String[] xmlid = new string[] { "P3040P0020", "P3040P0900" };
            //生成html的id
            String htmlid = "P3040P0010";

            //删除全部数据
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_DEL_RECORD, "", xmlDocList, xmlid,
                "", "", null, "", m_opType, m_opExt, "", htmlid + "*.html");

            //生成单一类别
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_ALL_DESC, xmlDocList, xmlid,
                "news", typeid, "chxml/content/news[@id='{0}']", "P3040P0010", m_opType, m_opExt, xslName, htmlid);
        }

        public void CreateNewsDetailHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            m_dsManage = new DataSetManage(xmlDocList[m_pageXmlSym.P3040P0020]);
            //保存xml集合
            String[] xmlid = new string[] { "P3040P0020", "P3040P0900" };
            //生成html的id
            String htmlid = "P3040P0030";
            if (m_dsManage.Get.Tables["news"] == null)return;
            DataTable news = m_dsManage.Get.Tables["news"];
            foreach (DataRow r in news.Rows)
            {
                String xpath = String.Format("chxml/content/news[@id='{0}']", r["id"].ToString());
                UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_SINGLE, xmlDocList, xmlid, 
                    "news", null, xpath, "P3040P0030", m_opType, m_opExt, xslName, htmlid);
            }
        }

        /// <summary>
        /// 产品生成
        /// </summary>
        public void InitProHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            CreateProListHtml(registId, language, xmlDocList, xslName);
            CreateProDetailHtml(registId, language, xmlDocList, xslName);
        }

        public void CreateProListHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            m_dsManage = new DataSetManage(xmlDocList[m_pageXmlSym.P3050P0900]);
            String tempId = "000";
            if (m_dsManage.Get.Tables["name"] != null)
            {
                foreach (DataRow drId in m_dsManage.Get.Tables["name"].Rows)
                {
                    if (drId["typeid"].ToString() != "")
                    {
                        tempId += ("," + drId["typeid"].ToString());
                    }
                }
            }
            //类别字串设置
            String typeid = String.Format("group={0}", tempId);
            //保存xml集合
            String[] xmlid = new string[] { "P3050P0020", "P3050P0900" };
            //生成html的id
            String htmlid = "P3050P0010";

            //删除全部数据
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_DEL_RECORD, "", xmlDocList, xmlid, 
                "", "", null, "", m_opType, m_opExt, "", htmlid + "*.html");
            
            //生成单一类别
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_ALL_DESC, xmlDocList, xmlid, 
                "prod", typeid, "chxml/content/prod[@id='{0}']", "P3050P0010", m_opType, m_opExt, xslName, htmlid);
        }

        public void CreateProDetailHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            m_dsManage = new DataSetManage(xmlDocList[m_pageXmlSym.P3050P0020]);
            //保存xml集合
            String[] xmlid = new string[] { "P3050P0020", "P3050P0900" };
            //生成html的id
            String htmlid = "P3050P0030";
            if (m_dsManage.Get.Tables["prod"] == null) return;
            DataTable prod = m_dsManage.Get.Tables["prod"];
            foreach (DataRow r in prod.Rows)
            {
                String xpath = String.Format("chxml/content/prod[@id='{0}']", r["id"].ToString());
                UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_SINGLE, xmlDocList, xmlid,
                    "prod", null, xpath, "P3050P0030", m_opType, m_opExt, xslName, htmlid);
            }
        }

        /// <summary>
        /// 留言生成
        /// </summary>
        public void InitMessageHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            CreateMessageListHtml(registId, language, xmlDocList, xslName);
        }

        /// <summary>
        /// 生成留言一览
        /// </summary>
        public void CreateMessageListHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName) 
        {
            String tempId = "000";
            //类别字串设置
            String typeid = String.Format("typeid={0}", tempId);
            //保存xml集合
            String[] xmlid = new string[] { "P3070P0020", "P3070P0900" };
            //生成html的id
            String htmlid = "P3070P0010";
            //生成单一类别
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_ALL_DESC, xmlDocList, xmlid, 
                "message", typeid, "chxml/content/message[@id='{0}']", "P3070P0010", m_opType, m_opExt, xslName, htmlid);

        }

        /// <summary>
        /// 招聘生成
        /// </summary>
        public void InitJobHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            CreateJobListHtml(registId, language, xmlDocList, xslName);
            CreateJobDetailHtml(registId, language, xmlDocList, xslName);
        }

        /// <summary>
        /// 生成招聘一览
        /// </summary>
        public void CreateJobListHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            String tempId = "000";
            //类别字串设置
            String typeid = String.Format("typeid={0}", tempId);
            //保存xml集合
            String[] xmlid = new string[] { "P3060P0020", "P3060P0900", "P3060P0050" };
            //生成html的id
            String htmlid = "P3060P0010";

            //删除全部数据
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_DEL_RECORD, "", xmlDocList, xmlid, 
                "", "", null, "", m_opType, m_opExt, "", htmlid + "*.html");

            //生成单一类别
            UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_ALL_DESC, xmlDocList, xmlid, 
                "jobList", typeid, "chxml/content/jobList[@id='{0}']", "P3060P0010", m_opType, m_opExt, xslName, htmlid);

        }
        /// <summary>
        /// 生成招聘详细
        /// </summary>
        public void CreateJobDetailHtml(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            m_dsManage = new DataSetManage(xmlDocList[m_pageXmlSym.P3060P0020]);
            //保存xml集合
            String[] xmlid = new string[] { "P3060P0020" };
            //生成html的id
            String htmlid = "P3060P0030";
            if (m_dsManage.Get.Tables["jobList"] == null) return;
            DataTable job = m_dsManage.Get.Tables["jobList"];
            foreach (DataRow r in job.Rows)
            {
                String xpath = String.Format("chxml/content/jobList[@id='{0}']", r["id"].ToString());
                UpdateUserSiteTrans(registId, language, UserSiteTransCommand.COMMAND_ADD_RECORD, UserSiteTransParam.PARAM_SINGLE, xmlDocList, xmlid, 
                    "jobList", null, xpath, "P3060P0030", m_opType, m_opExt, xslName, htmlid);
            }
        }

        /// <summary>
        /// 生成系统全部页面
        /// </summary>
        public void InitAllPage(String registId, String language, XmlDocMangContent xmlDocList, String xslName)
        {
            InitIndexHtml(registId, language, xmlDocList, xslName);
            InitSystemHtml(registId, language, xmlDocList, xslName);
            InitMessageHtml(registId, language, xmlDocList, xslName);
            InitNewsHtml(registId, language, xmlDocList, xslName);
            InitProHtml(registId, language, xmlDocList, xslName);
            InitJobHtml(registId, language, xmlDocList, xslName);

            SetUserLangSiteMap(registId, language, xmlDocList, xslName);
        }

        /// <summary>
        /// 激活网站语言(首次激活)
        /// </summary>
        public void AddUserLang(String registId, String language, XmlDocMangContent xmlDocList, String defaultLanguage, String activeLanguage, TransValue LanguageList, String xslName
            ,TransValue usCss, String jsType, TransValue usJs) 
        {
            //追加用户
            m_cs.AddUser(registId);
            //激活网站语言
            SetUserLang(registId, language, xmlDocList, defaultLanguage, activeLanguage, LanguageList, xslName, usCss, jsType, usJs);
        }

        /// <summary>
        /// 设置网站语言
        /// </summary>
        public void SetUserLang(String registId, String language, XmlDocMangContent xmlDocList, String defaultLanguage, String activeLanguage, TransValue LanguageList, String xslName
            , TransValue usCss, String jsType, TransValue usJs) 
        {
            //追加用户不同语言站点
            m_cs.AddUserLanguage(registId, language);
            //设置网站语言风格
            SetUserLangStyle(registId, language, xslName, true);
            //设置网站语言所需的配置文件
            SetUserLangConfig(registId, language, xmlDocList, defaultLanguage, activeLanguage, LanguageList, xslName);
            //更新设置用户指定语言网站的风格
            SetUserLangDiyStyle(registId, language, xmlDocList, jsType, xslName, usCss, usJs);
        }

        /// <summary>
        /// 设置网站语言所需的配置文件
        /// </summary>
        /// <param name="defaultLanguage"></param>
        /// <param name="activeLanguage"></param>
        /// <param name="LanguageList"></param>
        /// <param name="xslName"></param>
        public void SetUserLangConfig(String registId, String language, XmlDocMangContent xmlDocList, String defaultLanguage, String activeLanguage, TransValue LanguageList, String xslName) 
        {
            //生成首页PHP
            UserSiteTransFile usrSiteTraFile = new UserSiteTransFile(xmlDocList[m_pageXmlSym.usCurrency]);
            TransValue transValue = new TransValue();
            transValue[TransKey.PHP_ACTIVE_LANGUAGE_KEY] = activeLanguage;
            transValue[TransKey.PHP_DEFAULT_LANGUAGE_KEY] = defaultLanguage;
            //取得追加的xml文档数据流
            MemoryStream smPhp = usrSiteTraFile.GetStream("uscurrency/configure[@id='php']", TransType.PHP, transValue);
            smPhp.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, "", smPhp, xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_PHP, UserSiteTransExt.EXT_PHP, "index.php", false);

            //生成错误PHP
            MemoryStream err_smPhp = usrSiteTraFile.GetStream("uscurrency/configure[@id='err_php']", TransType.PHP, null);
            err_smPhp.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, "", err_smPhp, xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_PHP, UserSiteTransExt.EXT_PHP, "error.php", false);

            //生成语言JS
            //取得追加的xml文档数据流
            MemoryStream smJs = usrSiteTraFile.GetStream("uscurrency/configure[@id='js']", TransType.JS, LanguageList);
            smJs.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, "", smJs, xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_JS, UserSiteTransExt.EXT_JS, "language.js", false);
            //关闭xml数据流
            smPhp.Close();
            err_smPhp.Close();
            smJs.Close();
        }

        /// <summary>
        /// 设置网站语言风格
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xslName"></param>
        /// <param name="delMyStyle">true不保留自定义,false保留自定义</param>
        public void SetUserLangStyle(String registId, String language, String xslName,bool delMyStyle)
        {
            //更新设置用户指定语言网站的风格
            m_cs.SetUserLanguageStyle(registId, language, xslName, delMyStyle);
        }

        /// <summary>
        /// 设置网站语言自定义风格
        /// </summary>
        public void SetUserLangDiyStyle(String registId,String language,XmlDocMangContent xmlDocList, String jsType,String xslName, TransValue transValueCss,
            TransValue transValueJs)
        {
            UserSiteTransFile usrSiteTraFile = new UserSiteTransFile(xmlDocList[m_pageXmlSym.usCurrency]);
            //自定义样式CSS处理
            MemoryStream smcss = null;
            smcss = usrSiteTraFile.GetStream("uscurrency/configure[@id='uscss']", TransType.USCSS, transValueCss);
            smcss.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, language, smcss,xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_CSS, UserSiteTransExt.EXT_CSS, "mystyle/styleSeting.css", false);

            //自定义样式JS处理
            MemoryStream smjs = null;
            switch (jsType) 
            {
                case TransType.USJS_FLASH:
                    //取得追加的xml文档数据流
                    smjs = usrSiteTraFile.GetStream("uscurrency/configure[@id='usjs_flash']", TransType.USJS_FLASH, transValueJs);
                    smjs.Seek(0, SeekOrigin.Begin);
                    //生成首页跳转
                    UpdateUserSiteTrans(registId, language, smjs, xmlDocList, xslName, "currencyXSL", 
                        UserSiteTransType.TYPE_JS, UserSiteTransExt.EXT_JS, "mystyle/flashSeting.js", false);
                    break;
                case TransType.USJS_NONE_FLASH:
                    //取得追加的xml文档数据流
                    smjs = usrSiteTraFile.GetStream("uscurrency/configure[@id='usjs_none_flash']", TransType.USJS_NONE_FLASH, transValueJs);
                    smjs.Seek(0, SeekOrigin.Begin);
                    //生成首页跳转
                    UpdateUserSiteTrans(registId, language, smjs, xmlDocList, xslName, "currencyXSL", 
                        UserSiteTransType.TYPE_JS, UserSiteTransExt.EXT_JS, "mystyle/flashSeting.js", false);
                    break;
            }
            //关闭xml数据流
            smcss.Close();
            if (smjs != null)
                smjs.Close();
        }

        /// <summary>
        /// 删除网站语言
        /// </summary>
        public void UserLangDel(String registId,String language, XmlDocMangContent xmlDocList, String defaultLanguage, String activeLanguage, TransValue LanguageList, String xslName)
        {
            //生成首页PHP
            UserSiteTransFile usrSiteTraFile = new UserSiteTransFile(xmlDocList[m_pageXmlSym.usCurrency]);
            TransValue transValue = new TransValue();
            transValue[TransKey.PHP_ACTIVE_LANGUAGE_KEY] = activeLanguage;
            transValue[TransKey.PHP_DEFAULT_LANGUAGE_KEY] = defaultLanguage;
            //取得追加的xml文档数据流
            MemoryStream smPhp = usrSiteTraFile.GetStream("uscurrency/configure[@id='php']", TransType.PHP, transValue);
            smPhp.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, "", smPhp, xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_PHP, UserSiteTransExt.EXT_PHP, "index.php", false);

            //生成语言JS
            //取得追加的xml文档数据流
            MemoryStream smJs = usrSiteTraFile.GetStream("uscurrency/configure[@id='js']", TransType.JS, LanguageList);
            smJs.Seek(0, SeekOrigin.Begin);
            //生成首页跳转
            UpdateUserSiteTrans(registId, "", smJs, xmlDocList, xslName, "currencyXSL", 
                UserSiteTransType.TYPE_JS, UserSiteTransExt.EXT_JS, "language.js", false);
            //关闭xml数据流
            smPhp.Close();
            smJs.Close();

            //删除网站语言
            m_cs.DeleteUserLanguage(registId, language);
        }

        /// <summary>
        /// 关闭网站语言
        /// </summary>
        public void UserLangClose(String registId,String language)
        {
            //关闭网站
            m_cs.SetUserLanguageClose(registId, language);
        }

        /// <summary>
        /// 公开网站语言
        /// </summary>
        public void UserLangOpen(String registId, String language)
        {
            //公开网站
            m_cs.SetUserLanguageOpen(registId, language);
        }

        /// <summary>
        /// 用户不同语言网站上传图片
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        public void UserLangUploadPicture(String registId,String language,Stream fileStream, String targetFileName) 
        {
            m_cs.SendPictureToUserSite(registId, language, fileStream, targetFileName, true);
        }

        /// <summary>
        /// 用户不同语言网站上传图片
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        public void UserLangUploadMyStyle(String registId, String language, Stream fileStream, String targetFileName)
        {
            m_cs.SendMyStyleFileToUserSite(registId, language, fileStream, targetFileName);
        }

        /// <summary>
        /// 用户不同语言网站删除图片
        /// </summary>
        /// <param name="targetFilePath"></param>
        public void UserLangDeletePicture(String registId, String language, String targetFilePath)
        {
            m_cs.DeleteUserFiles(registId, language, targetFilePath, false);
        }

        /// <summary>
        /// 取得默认样式配置文件 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xslName"></param>
        public Stream UserLangDefaultStyle(String language,String xslName)
        {
            return m_cs.GetDefaultStyleConfigXml(language, xslName);
        }

        /// <summary>
        /// 取得默认样式文件
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xslName"></param>
        public Stream UserLangDefaultFile(String language, String xslName,String fileName)
        {
            return m_cs.GetFileStream("", language, xslName, "resources/mystyle/" + fileName, COMMAND_POSITION.LOCAL);
        }

        /// <summary>
        /// 生成前台网站地图
        /// </summary>
        public void SetUserLangSiteMap(String registId,String language,XmlDocMangContent xmlDocList, String xslName) 
        {
            UserSiteSiteMap sitmap = new UserSiteSiteMap(registId, language, "sitemap", xmlDocList);
            MemoryStream ms = sitmap.GetStream();
            UpdateUserSiteTrans(registId, language, ms, xmlDocList, xslName, "sitemap", 
                UserSiteTransType.TYPE_HTML, UserSiteTransExt.EXT_HTML, "sitemap", true);
            ms.Close();
        }


        /// <summary>
        /// 取得默认样式flash 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xslName"></param>
        public Stream UserLangDefaultFlash(String language, String xslName)
        {
            return m_cs.GetFileStream("", language, xslName, "resources/mystyle/headFlash.swf", COMMAND_POSITION.LOCAL);
        }

        /// <summary>
        /// 取得默认样式图片 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xslName"></param>
        public Stream UserLangDefaultImg(String language, String xslName)
        {
            return m_cs.GetFileStream("", language, xslName, "resources/mystyle/bgImg.png", COMMAND_POSITION.LOCAL);
        }

        /// <summary>
        /// 生成域名配置文件
        /// </summary>
        /// <param name="s"></param>
        public void SetCommonSiteDomain(Stream s) 
        {
            m_cs.SendFileToUserSite(s, CodeSymbol.domain_name, CodeSymbol.domain_host);
        }
        
        /// <summary>
        /// 检测用户下指定目录是否存在
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckUserSite(String registId) 
        {
            return m_cs.ExistUser(registId);
        }

        /// <summary>
        /// 设置ICP认证文件
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="scert"></param>
        public void SetUserSiteCert(String registId, Stream scert) 
        {
            m_cs.SendFileToUserSite(scert, registId, "cert/bazs.cert");
        }
        
    }
}
