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
using Seika.CooException;
using Seika.COO.Action;

/// <summary>
/// Summary description for LoginManager
/// </summary>
namespace Seika.COO.Web.PG
{
    public class LoginManager : PageBase
    {
        SessionManager m_session = null;

        public LoginManager(SessionManager session)
        {
            m_session = session;
        }

        public String LoginWorking(String type, String registId, String password, 
            String htmlId, String ip, ExceptionMess conError) 
        {
            ActionServer acs = new ActionServer(ACTION_NAMES.ORG_LOGIN);
            acs.ErrMess = conError;
            DataSet ds = LoginInfoDs(registId, password, ip);
           
            if (acs.ServerStart(ds) == null)
            {
                return LoginState.ERROR;
            }
            else
            {
                //判断用户是否被激活
                DataTable dt = ds.Tables["REGINFO"];
                //默认不成功状态
                String userWaitFlg = "1";
                foreach (DataRow row in dt.Rows) 
                {
                    userWaitFlg = row["WAITFLG"].ToString();
                }
                //失败
                if (!String.IsNullOrEmpty(userWaitFlg))
                {
                    return LoginState.ACTIVE;
                }
                else
                {
                    m_session.Clear();
                    m_session.PageLogin_RegistType = type;
                    m_session.PageLogin_RegistId = registId;
                    //m_session.Page_UICulture = m_session.Page_Home_UICulture;
                    //m_session.Page_UICultureID = m_session.Page_Home_UICultureID;
                }

            //    m_session.PageLogin_HtmlId = htmlId;
            //    if (m_session.PageServiceAuto != null)
            //        m_session.PageServiceList = m_session.PageServiceAuto;
            //    if (ds.Tables.Contains("REGINFO"))
            //    {
            //        DataTable regInfo = ds.Tables["REGINFO"];
            //        foreach (DataRow r in regInfo.Rows) 
            //        {
            //            m_session.Page_UICultureID = r["LANG"].ToString();
            //            m_session.Page_UICulture = r["LANGRES"].ToString();
            //        }
            //    }
                //if (ds.Tables.Contains("STYLEINFO"))
                //{
                //    DataTable dt = ds.Tables["STYLEINFO"];
                //    if (dt.Rows.Count > 0)
                //        m_session.PageLogin_HtmlStyle = dt.Rows[0]["XSLNAME"].ToString();
                //}
            }
            return LoginState.SUCCESS;
        }

        public bool IsChecked() 
        {
            if (m_session == null) return false;
            //if (String.IsNullOrEmpty(m_session.PageLogin_RegistType) || String.IsNullOrEmpty(m_session.PageLogin_RegistId)
            //    || String.IsNullOrEmpty(m_session.PageLogin_HtmlId) || m_session.PageLogin_HtmlStyle == null)
            if (String.IsNullOrEmpty(m_session.PageLogin_RegistType) || String.IsNullOrEmpty(m_session.PageLogin_RegistId))
            {
                return false;
            }
            return true;
        }

        public void LogoutWorking()
        {
            m_session.Clear();
        }

        /// <summary>
        /// 用户信息ds
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private DataSet LoginInfoDs(String registId, String password, String ip)
        {
            DataSetManage dsm = new DataSetManage("logininfo");
            dsm.NewRow();
            dsm.AddColumnAndValue("registId", registId);
            dsm.AddColumnAndValue("password", password);
            dsm.AddColumnAndValue("ip", ip);
            dsm.RowBind();
            return dsm.Get;
        }

    }

    public class LoginState 
    {
        public const String SUCCESS = "S";
        public const String ACTIVE = "A";
        public const String ERROR = "E";
    }
}