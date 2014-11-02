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
    public class MA_INVOICE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_INVOICE(DBConnect sql)
            : base(sql)
        {
            
        }

        /// <summary>
        /// 取得开发票金额
        /// </summary>
        public DataTable GetInvoiceBalance(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ISNULL(SUM(REQUISITIONMONEY),0) AS INVOICEBALANCE");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_INVOICE ");
            cmd.AppendLine("  WHERE ");
            cmd.AppendFormat("  REGISTID = '{0}' ", register);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        //添加发票数据
        public bool UpdateInvoice(String id, String registId, String money, String postcode, String postaddress, String postname, String comname, String sendflg)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_INVOICE");
            orgsql.AppendLine("       (MA_INVOICEID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,COMNAME");
            orgsql.AppendLine("       ,REQUISITIONMONEY");
            orgsql.AppendLine("       ,REQUISITIONDATE");
            orgsql.AppendLine("       ,POST_CODE");
            orgsql.AppendLine("       ,POST_ADDRESS");
            orgsql.AppendLine("       ,POST_NAME");
            orgsql.AppendLine("       ,SENDFLG)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}',getdate(),'{4}','{5}','{6}','{7}')",
                 id, registId, comname, money, postcode, postaddress, postname, sendflg);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}