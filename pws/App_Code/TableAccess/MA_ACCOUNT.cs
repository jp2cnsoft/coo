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
    public class MA_ACCOUNT : Seika.COO.DBA.DateBaseAccess
    {
        public MA_ACCOUNT(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        /// <summary>
        /// 取得服务ID
        /// </summary>
        public DataTable GetAccount(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT TOP 1 ");
            cmd.AppendLine(" 	    MA_ACCOUNTID ");
            cmd.AppendLine(" 	    ,BALANCE ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_ACCOUNT ");
            cmd.AppendFormat("  WHERE REGISTID = '{0}' ", register);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        public bool UpdateAccount(String MA_COMINFOid, String money)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" UPDATE  ");
            cmd.AppendLine("        MA_ACCOUNT ");
            cmd.AppendLine(" SET ");
            cmd.AppendFormat("        IN_MONEYTOTAL = IN_MONEYTOTAL + {0}", money);
            cmd.AppendFormat("       ,BALANCE = BALANCE + {0}", money);
            cmd.AppendFormat("       ,RESIDUALINVOICE = RESIDUALINVOICE + {0}", money);
            cmd.AppendLine(" WHERE ");
            cmd.AppendFormat("        REGISTID = '{0}'", MA_COMINFOid);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        public bool AddAccount(String MA_COMINFOid, String in_moneytotal, String out_moneytotal, String balance)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" MA_ACCOUNT ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       MA_ACCOUNTID");
            cmd.AppendLine("       ,REGISTID");
            cmd.AppendLine("       ,IN_MONEYTOTAL");
            cmd.AppendLine("       ,OUT_MONEYTOTAL");
            cmd.AppendLine("       ,BALANCE");
            cmd.AppendLine("       ,RESIDUALINVOICE)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}')",
               GetNextSeqNumber(), MA_COMINFOid, in_moneytotal, out_moneytotal,balance, balance);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 取得用户余额
        /// </summary>
        public DataTable GetBalance(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT TOP 1 ");
            cmd.AppendLine(" 	    MA_ACCOUNTID ");
            cmd.AppendLine(" 	    ,BALANCE ");
            cmd.AppendLine(" 	    ,OUT_MONEYTOTAL ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_ACCOUNT ");
            cmd.AppendFormat("  WHERE REGISTID = '{0}' ", register);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        public bool Updatebalance(String accountid,String outmoneytotal, String balance)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" UPDATE  ");
            cmd.AppendLine("        MA_ACCOUNT ");
            cmd.AppendLine(" SET ");
            cmd.AppendFormat("       BALANCE = {0}", balance);
            cmd.AppendFormat("       ,OUT_MONEYTOTAL = {0}", outmoneytotal);
            cmd.AppendLine(" WHERE ");
            cmd.AppendFormat("        MA_ACCOUNTID = '{0}'", accountid);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 删除用户帐户
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteAccount(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_ACCOUNT");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        
    }
}