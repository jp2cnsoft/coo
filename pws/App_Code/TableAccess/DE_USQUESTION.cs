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
    public class DE_USQUESTION : Seika.COO.DBA.DateBaseAccess
    {
        public DE_USQUESTION(DBConnect sql)
            : base(sql)
	    {
		  
	    }
        public bool Addquestion(String de_usquestionid, String ma_guestid, String qname, String answer, String answer_1)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT ");
            cmd.AppendLine(" DE_USQUESTION ");
            cmd.AppendLine("       (");
            cmd.AppendLine("        DE_USQUESTIONID");
            cmd.AppendLine("       ,MA_GUESTID");
            cmd.AppendLine("       ,QUESTION");
            cmd.AppendLine("       ,PARTICULAR_1");
            cmd.AppendLine("       ,PARTICULAR_2)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}')", de_usquestionid, ma_guestid, qname, answer, answer_1);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
    }
}