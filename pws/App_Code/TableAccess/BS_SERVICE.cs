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
    public class BS_SERVICE : Seika.COO.DBA.DateBaseAccess
    {
        public BS_SERVICE(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        public bool AddService(String serviceID,String ma_accountid, String ma_serviceid, String servicenum
            , String startdate, String enddate, String state, String remark)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" BS_SERVICE ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       BS_SERVICEID");
            cmd.AppendLine("       ,MA_ACCOUNTID");
            cmd.AppendLine("       ,MA_SERVICEID");
            cmd.AppendLine("       ,SERVICENUM");
            cmd.AppendLine("       ,STARTDATE");
            cmd.AppendLine("       ,ENDDATE");
            cmd.AppendLine("       ,STATE");
            cmd.AppendLine("       ,REMARK)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
               serviceID, ma_accountid, ma_serviceid, servicenum, startdate, enddate, state, remark);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        public DataTable GetServiceId(String register,String serviceId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT BS_SERVICEID ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        BS_SERVICE a");
            cmd.AppendLine("    INNER JOIN MA_ACCOUNT b ");
            cmd.AppendLine("        ON a.MA_ACCOUNTID = b.MA_ACCOUNTID ");
            cmd.AppendLine("    INNER JOIN MA_SERVICE c ");
            cmd.AppendLine("        ON a.MA_SERVICEID = c.MA_SERVICEID ");
            cmd.AppendFormat("  WHERE b.REGISTID = '{0}' ", register);
            cmd.AppendLine("    AND ");
            cmd.AppendFormat("        a.MA_SERVICEID = '{0}' ", serviceId);
            cmd.AppendLine("    AND ");
            cmd.AppendLine("          c.TYPE = '20' ");
            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        public DataTable GetServiceIdHost(String register, String serviceId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT BS_SERVICEID ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        BS_SERVICE a");
            cmd.AppendLine("    INNER JOIN MA_ACCOUNT b ");
            cmd.AppendLine("        ON a.MA_ACCOUNTID = b.MA_ACCOUNTID ");
            cmd.AppendLine("    INNER JOIN MA_SERVICE c ");
            cmd.AppendLine("        ON a.MA_SERVICEID = c.MA_SERVICEID ");
            cmd.AppendFormat("  WHERE b.REGISTID = '{0}' ", register);
            cmd.AppendLine("    AND ");
            cmd.AppendFormat("        a.REMARK = '{0}' ", serviceId);
            cmd.AppendLine("    AND ");
            cmd.AppendLine("          a.REMARK IS NOT NULL ");
            cmd.AppendLine("    AND ");
            cmd.AppendLine("          c.TYPE = '30' ");
            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
		public bool Delservice(String bs_serviceid)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" delete");
            cmd.AppendLine(" BS_SERVICE ");
            cmd.AppendLine(" where ");
            cmd.AppendFormat("      bs_serviceid  = '{0}'", bs_serviceid);
            return sql.ExecuteStrQuery(cmd.ToString()); 
        }
    }
}