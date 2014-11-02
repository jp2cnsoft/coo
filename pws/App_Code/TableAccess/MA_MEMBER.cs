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
    public class MA_MEMBER : Seika.COO.DBA.DateBaseAccess
    {
        public MA_MEMBER(DBConnect sql)
            : base(sql)
        {
            
        }

        /// <summary>
        /// 取得消费金额
        /// </summary>
        public DataTable GetMemberBalance(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ISNULL(SUM(PAYMONEY),0) AS MEMBERBALANCE");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("         MA_MEMBER ");
            cmd.AppendLine("  WHERE ");
            cmd.AppendFormat("  REGISTID = '{0}' ", register);
            cmd.AppendLine("  AND STATEFLG = '1' ");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }


        /// <summary>
        /// 会员服务信息
        /// </summary>
        public DataTable GetServices(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT CONVERT(VARCHAR(10),CONVERT(DATETIME,BEGINDATE),111) BEGINDATE");
            cmd.AppendLine("    ,CONVERT(VARCHAR(10),CONVERT(DATETIME,ENDDATE),111) ENDDATE");
            cmd.AppendLine("    ,CONVERT(VARCHAR(10),PAYDATE,111) PAYDATE");
            cmd.AppendLine("    ,MA_MEMBERID");
            cmd.AppendLine("    ,NUMBER");
            cmd.AppendLine("    ,PAYMONEY");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_MEMBER ");
            cmd.AppendFormat("  WHERE  REGISTID = '{0}' ", register);
            cmd.AppendLine("        AND STATEFLG = '1' ");
            cmd.AppendLine("        ORDER BY ENDDATE DESC ");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 会员服务是否已付款
        /// </summary>
        public DataTable GetSTATEFLG(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT STATEFLG");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_MEMBER ");
            cmd.AppendFormat("  WHERE  REGISTID = '{0}' ", register);
            cmd.AppendLine("        AND STATEFLG = '0' ");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 会员服务支付成功，更新状态
        /// </summary>
        public bool UpdateMember(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" UPDATE  ");
            cmd.AppendLine("        MA_MEMBER ");
            cmd.AppendLine(" SET ");
            cmd.AppendLine("      STATEFLG = '1'");
            cmd.AppendLine(" WHERE ");
            cmd.AppendFormat("        REGISTID = '{0}'", registId);
            cmd.AppendLine("    AND STATEFLG='0'  ");

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 删除未付费用户记录
        /// </summary>
        public bool DeleteMember(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" DELETE  ");
            cmd.AppendLine("        MA_MEMBER ");
            cmd.AppendLine(" WHERE ");
            cmd.AppendFormat("        REGISTID = '{0}'", registId);
            cmd.AppendLine("    AND STATEFLG='0'  ");

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        //添加会员记录
        public bool InsertMember(String id, String registId, String begindate, String number, String paymoney, String stateflg, String modeflg)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_MEMBER");
            orgsql.AppendLine("       (MA_MEMBERID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,BEGINDATE");
            orgsql.AppendLine("       ,ENDDATE");
            orgsql.AppendLine("       ,NUMBER");
            orgsql.AppendLine("       ,PAYDATE");
            orgsql.AppendLine("       ,PAYMONEY");
            orgsql.AppendLine("       ,STATEFLG");
            orgsql.AppendLine("       ,MODEFLG)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}',CONVERT(VARCHAR(8),CONVERT(DATETIME,'{2}'),112),CONVERT(VARCHAR(8),DATEADD(DAY,-1,DATEADD(YEAR,{3},CONVERT(DATETIME,'{4}'))),112),'{5}',getdate(),'{6}','{7}','{8}')",
                 id, registId, begindate, number, begindate, number, paymoney, stateflg, modeflg);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 获得服务到期日
        /// </summary>
        public DataTable GetServiceEndDate(String register)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT CONVERT(DATETIME,ENDDATE) AS ENDDATE");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_MEMBER ");
            cmd.AppendFormat("  WHERE  REGISTID = '{0}' ", register);
            cmd.AppendLine("        AND STATEFLG = '0' ");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 删除用户服务表
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DelMember(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_MEMBER");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}