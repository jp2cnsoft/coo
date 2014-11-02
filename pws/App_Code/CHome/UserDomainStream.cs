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
using System.IO;
using Seika;
using Seika.Db;
using Seika.COO.DBA.BS;

/// <summary>
/// Summary description for LoginManager
/// </summary>
namespace Seika.COO.Web.PG
{
    public class UserDomainStream : PageBase
    {
        DBConnect conn = null;

        public UserDomainStream() 
        {
            conn = DBConnectPool.GetConnect();
            conn.open();
        }

        public void AddDomain(String bs_serviceId, String webdomain, String userdomain)
        {
            //(new DE_USERDOMAIN(conn)).AddUserDomain(bs_serviceId, webdomain, userdomain);
            //conn.Commit();
        }

        public Stream GetDomainStream() 
        {
            DataTable dt = (new BS_DOMAIN(conn)).GetDomainList();
            dt.TableName = "userdomain";
            dt.Columns[0].ColumnName = "id";
            dt.Columns[1].ColumnName = "webdomain";
            dt.Columns[2].ColumnName = "coodomain";

            m_xmlManage = new XmlDocManage("");
            ArrayList tPropert = new ArrayList();
            tPropert.Add("id");
            m_xmlManage.UpdateLocalXml("chxml/domain", dt, tPropert,true,null);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(m_xmlManage.xml.OuterXml);

            MemoryStream memStream = new MemoryStream(buffer);
            memStream.Seek(0, SeekOrigin.Begin);

            return memStream;
        }

        public void Close()
        {
            conn.close();
        }
    }
}