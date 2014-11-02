using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Seika.COO.Web.PG
{
    /// <summary>
    /// Summary description for CodeCategorySymbol
    /// </summary>
    public class PageXmlSymbol : Page
    {
        SessionManager session = null;
        String xmlExt = ".xml";
        public String UiCulture
        {
            get;
            set;
        }
        public PageXmlSymbol()
        {
        }

        public PageXmlSymbol(SessionManager session) 
        {
            this.session = (session == null) ? new SessionManager(Session) : session;
            if (!String.IsNullOrEmpty(session.Page_UICultureID))
            {
                UiCulture = session.Page_UICultureID.ToUpper();
            }
        }

        public PageXmlSymbol(String uiCulture)
        {
            this.UiCulture = uiCulture;
        }
        
        public String P3010P0310 
        {
            get { return "P3010P0310"; }
        }
        public String P3010P0320
        {
            get { return "P3010P0320"; }
        }
        public String P3010P0330
        {
            get { return "P3010P0330"; }
        }
        public String P3010P0340
        {
            get { return "P3010P0340"; }
        }
        public String P3010P0350 
        {
            get { return "P3010P0350"; }
        }
        public String P3010P0360
        {
            get { return "P3010P0360"; }
        }
        public String P3010P0370
        {
            get { return "P3010P0370"; }
        }
        public String P3010P0940
        {
            get { return "P3010P0940"; }
        }
        public String P3010P0950
        {
            get { return "P3010P0950"; }
        }
        public String P3040P0020 
        {
            get { return "P3040P0020"; }
        }
        public String P3040P0900
        {
            get { return "P3040P0900"; }
        }
        public String P3050P0020
        {
            get { return "P3050P0020"; }
        }
        public String P3050P0900
        {
            get { return "P3050P0900"; }
        }
        public String P3060P0020
        {
            get { return "P3060P0020"; }
        }
        public String P3060P0050
        {
            get { return "P3060P0050"; }
        }
        public String P3060P0900
        {
            get { return "P3060P0900"; }
        }
        public String P3070P0020
        {
            get { return "P3070P0020"; }
        }
        public String P3070P0900
        {
            get { return "P3070P0900"; }
        }
        public String P3090P0900
        {
            get { return "P3090P0900"; }
        }
        public String System
        {
            get { return "System"; }
        }
        public String Menu
        {
            get { return "MENU"; }
        }
        public String navi
        {
            get { return "NAVI"; }
        }
        public String serviceClause
        {
            get { return "serviceClause"; }
        }
        public String blankHomepage
        {
            get { return "blankHomepage"; }
        }
        public String usCurrency
        {
            get { return "USCURRENCY"; }
        }

        public String P3010P0340_LANG
        {
            get { return "P3010P0340_{0}" + xmlExt; }
        }
        public String P3040P0020_LANG
        {
            get { return "P3040P0020_{0}" + xmlExt; }
        }

        /// <summary>
        /// 程序所需XML文件列表
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public List<String> PageXmlList(String lang) 
        {
            UiCulture = lang;
            List<String> xlist = new List<String>();

            //公司简介
            xlist.Add(P3010P0310);
            xlist.Add(P3010P0320);
            xlist.Add(P3010P0330);
            xlist.Add(P3010P0340);
            xlist.Add(P3010P0350);
            xlist.Add(P3010P0360);
            xlist.Add(P3010P0370);

            //设置
            xlist.Add(P3010P0940);
            xlist.Add(P3010P0950);

            //新闻
            xlist.Add(P3040P0020);
            xlist.Add(P3040P0900);

            //产品
            xlist.Add(P3050P0020);
            xlist.Add(P3050P0900);

            //招聘
            xlist.Add(P3060P0020);
            xlist.Add(P3060P0050);
            xlist.Add(P3060P0900);

            //留言
            xlist.Add(P3070P0020);
            xlist.Add(P3070P0900);

            //其它
            xlist.Add(System);
            xlist.Add(Menu);
            xlist.Add(navi);
            xlist.Add(serviceClause);
            xlist.Add(blankHomepage);

            //自定义样式
            xlist.Add(P3090P0900);

            return xlist;
        }
    }
}