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

namespace Seika.COO.DBA.MA
{
    public class DE_SERVICE : Seika.COO.DBA.DateBaseAccess
    {
        public DE_SERVICE(DBConnect sql)
            : base(sql)
        {
            
        }

        public bool AddService(String maAccountid, String maServiceid, String inMoney,String outMoney, String balance)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" DE_SERVICE ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       DE_SERVICEID");
            cmd.AppendLine("       ,MA_ACCOUNTID");
            cmd.AppendLine("       ,MA_SERVICEID");
            cmd.AppendLine("       ,IN_MONEY");
            cmd.AppendLine("       ,OUT_MONEY");
            cmd.AppendLine("       ,BALANCE");
            cmd.AppendLine("       ,CREATEDATE)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
               GetNextSeqNumber(), maAccountid, maServiceid, inMoney, outMoney, balance, System.DateTime.Now);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
    }
}