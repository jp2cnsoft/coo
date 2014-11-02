using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
/// <summary>
/// SESSION管理类
/// </summary>
namespace Seika.COO.Web.PG
{
    public class SessionManager
    {
        private HttpSessionState session = null;

        private String SESSION_PAGE = "SESSION_PAGE";
        private String SESSION_UICULTURE_ID = "SESSION_UICULTURE_ID";
        private String SESSION_DEFAULT_UICULTURE_ID = "SESSION_DEFAULT_UICULTURE_ID";
        //private String SESSION_HOME_UICULTURE_ID = "SESSION_HOME_UICULTURE_ID";
        //private String SESSION_HOME_UICULTURE = "SESSION_HOME_UICULTURE";
        private String SESSION_HP_DOMAIN = "SESSION_HP_DOMAIN";

        private String SESSION_UICULTURE = "SESSION_UICULTURE";
        private String SESSION_UISYSCULTURE_ID = "SESSION_UISYSCULTURE_ID";
        private String SESSION_UISYSCULTURE = "SESSION_UISYSCULTURE";
        private String SESSION_LOGIN_REGISTTYPE = "SESSION_LOGIN_REGISTTYPE";
        private String SESSION_LOGIN_REGISTID = "SESSION_LOGIN_REGISTID";
        private String SESSION_LOGIN_HTMLID = "SESSION_LOGIN_HTMLID";
        private String SESSION_LOGIN_HTMLSTYLE = "SESSION_LOGIN_HTMLSTYLE";
        private String SESSION_PAGE_SUBMIT = "SESSION_PAGE_SUBMIT";
        private String SESSION_SYS_EXCEPTIONID = "SESSION_SYS_EXCEPTIONID";
        private String SESSION_PAGE_SERVICELIST = "SESSION_PAGE_SERVICELIST";

        private String SESSION_PAGE_SERVICEAUTO = "SESSION_PAGE_SERVICEAUTO";

        private String SESSION_BANK_OID = "SESSION_BANK_OID";
        private String SESSION_BANK_AMOUNT = "SESSION_BANK_AMOUNT";
        private String SESSION_BANK_REGISTER = "SESSION_BANK_REGISTER";

        private String SESSION_MESSAGE_TIME = "SESSION_MESSAGE_TIME";

        public SessionManager(HttpSessionState session)
        {
            this.session = session;
            if (PageLogin_RegistId == null)
            {
                Initialize();
            }
        }

        public static SessionManager GetSessionManager(HttpSessionState session)
        {
            return new SessionManager(session);
        }

        public void Initialize()
        {
            if (session != null)
                this.PageData = new Hashtable();
        }

        public Hashtable PageData
        {
            set { session[SESSION_PAGE] = value; }
            get { return (Hashtable)session[SESSION_PAGE]; }
        }

        public String PageMessageTime
        {
            set { session[SESSION_MESSAGE_TIME] = value; }
            get { return (String)session[SESSION_MESSAGE_TIME]; }
        }
        //默认语言系统指定(CHINA,JAPAN)
        public String Page_DefaultUICultureID
        {
            set { session[SESSION_DEFAULT_UICULTURE_ID] = value; }
            get { return (String)session[SESSION_DEFAULT_UICULTURE_ID]; }
        }
        //智能网站域名
        public String Page_Hp_Domain
        {
            set { session[SESSION_HP_DOMAIN] = value; }
            get { return (String)session[SESSION_HP_DOMAIN]; }
        }
        ////首页语言系统指定(CHINA,JAPAN)
        //public String Page_Home_UICultureID
        //{
        //    set { session[SESSION_HOME_UICULTURE_ID] = value; }
        //    get { return (String)session[SESSION_HOME_UICULTURE_ID]; }
        //}
        ////首页语言编码(ZH-CN,JP)
        //public String Page_Home_UICulture
        //{
        //    set { session[SESSION_HOME_UICULTURE] = value; }
        //    get { return (String)session[SESSION_HOME_UICULTURE]; }
        //}
        //语言系统指定(CHINA,JAPAN)
        public String Page_UICultureID
        {
            set { session[SESSION_UICULTURE_ID] = value; }
            get { return (String)session[SESSION_UICULTURE_ID]; }
        }
        //语言编码(ZH-CN,JP)
        public String Page_UICulture
        {
            set { session[SESSION_UICULTURE] = value; }
            get { return (String)session[SESSION_UICULTURE]; }
        }
        //主页面语言系统指定(CHINA,JAPAN)
        public String Page_UISysCultureID
        {
            set { session[SESSION_UISYSCULTURE_ID] = value; }
            get { return (String)session[SESSION_UISYSCULTURE_ID]; }
        }
        //默认主页面语言编码(ZH-CN,JP)
        public String Page_UISysCulture
        {
            set { session[SESSION_UISYSCULTURE] = value; }
            get { return (String)session[SESSION_UISYSCULTURE]; }
        }

        public String PageLogin_RegistType
        {
            set { session[SESSION_LOGIN_REGISTTYPE] = value; }
            get { return (String)session[SESSION_LOGIN_REGISTTYPE]; }
        }

        public String PageLogin_RegistId
        {
            set { session[SESSION_LOGIN_REGISTID] = value; }
            get {
                if (session == null) {return null;}
                return (String)session[SESSION_LOGIN_REGISTID];
            }
        }

        public String PageLogin_HtmlId
        {
            set { session[SESSION_LOGIN_HTMLID] = value; }
            get { return (String)session[SESSION_LOGIN_HTMLID]; }
        }

        public String PageLogin_HtmlStyle
        {
            set { session[SESSION_LOGIN_HTMLSTYLE] = value; }
            get { return (String)session[SESSION_LOGIN_HTMLSTYLE]; }
        }

        public System.Exception SystemExceptionId
        {
            set { session[SESSION_SYS_EXCEPTIONID] = value; }
            get { return (System.Exception)session[SESSION_SYS_EXCEPTIONID]; }
        }

        public DataTable PageServiceList
        {
            set { session[SESSION_PAGE_SERVICELIST] = value; }
            get {
                if (session[SESSION_PAGE_SERVICELIST] == null)
                    return null;
                return (DataTable)session[SESSION_PAGE_SERVICELIST]; 
            }
        }

        public DataTable PageServiceAuto
        {
            set { session[SESSION_PAGE_SERVICEAUTO] = value; }
            get
            {
                if (session[SESSION_PAGE_SERVICEAUTO] == null)
                    return null;
                return (DataTable)session[SESSION_PAGE_SERVICEAUTO];
            }
        }

        //网银ID
        public String SystemBankOid
        {
            set { session[SESSION_BANK_OID] = value; }
            get { return (String)session[SESSION_BANK_OID]; }
        }
        //网银交费金额
        public String SystemBankAmount
        {
            set { session[SESSION_BANK_AMOUNT] = value; }
            get { return (String)session[SESSION_BANK_AMOUNT]; }
        }

        //网银交费备注册用于记录用户名
        public String SystemBankRegister
        {
            set { session[SESSION_BANK_REGISTER] = value; }
            get { return (String)session[SESSION_BANK_REGISTER]; }
        }

        public bool PageSubmitSta
        {
            set { session[SESSION_PAGE_SUBMIT] = value; }
            get { return (session[SESSION_PAGE_SUBMIT] == null) ? false : (bool)session[SESSION_PAGE_SUBMIT]; }
        }

        public void Clear() 
        {
            //PageData = null;
            PageMessageTime = null;
            Page_DefaultUICultureID = null;
            //Page_UICulture = null;
            //Page_UICultureID = null;
            Page_UISysCultureID = null;
            Page_UISysCulture = null;
            PageLogin_RegistType = null;
            PageLogin_RegistId = null;
            PageLogin_HtmlId = null;
            PageLogin_HtmlStyle = null;
            PageServiceList = null;
            SystemExceptionId = null;
            SystemBankOid = null;
            SystemBankAmount = null;
            SystemBankRegister = null;
            //session.Clear();
        }
    }
}