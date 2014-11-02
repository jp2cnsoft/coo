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
using Seika.COO.DBA.SV;
using Seika.Db;

/// <summary>
/// Summary description for LoginManager
/// </summary>
namespace Seika.COO.Web.PG
{
    public class ClientServerManager : PageBase
    {
        public ClientServerManager(){}

        /// <summary>
        /// 取得前台http地址
        /// </summary>
        /// <returns></returns>
        public String GetClientServerHttp(String registId)
        {
            String clientHost = GetClientServerDomainOrIp(registId);
            if (!String.IsNullOrEmpty(clientHost))
            {
                return @"http://" + @"{0}." + clientHost;
            }
            return String.Empty;
        }

        /// <summary>
        /// 取得前台页面域名或IP
        /// </summary>
        /// <returns></returns>
        public String GetClientServerDomainOrIp(String registId) 
        {
            DBConnect conn = DBConnectPool.GetConnect();
            conn.open();
            String serverDomainOrIp = String.Empty;
            //取得前台服务器信息
            DataTable dt = (new SV_SERVER(conn)).GetServerInfo(registId);
            foreach (DataRow row in dt.Rows) 
            {
                serverDomainOrIp = row["SERVERHOST"].ToString();
                break;
            }
            conn.close();
            return serverDomainOrIp;
        }
    }
}