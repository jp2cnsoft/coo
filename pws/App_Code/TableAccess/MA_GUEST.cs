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
using Seika;
using Seika.COO.Util;
using Seika.Db;

namespace Seika.COO.DBA.BS
{
    public class MA_GUEST : Seika.COO.DBA.DateBaseAccess
    {
        public MA_GUEST(DBConnect sql)
            : base(sql)
	    {
	    }

        public bool Adduser(String ma_guestid, String username, String userpost, String usertel, String useremail)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT ");
            cmd.AppendLine(" MA_GUEST ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       MA_GUESTID");
            cmd.AppendLine("       ,TITLE");
            cmd.AppendLine("       ,HEADSHIP");
            cmd.AppendLine("       ,PHONE");
            cmd.AppendLine("       ,EMAIL)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}')",ma_guestid, username, userpost, usertel, useremail);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
    }
}