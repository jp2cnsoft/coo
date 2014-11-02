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
    public class MA_SERVICE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_SERVICE(DBConnect sql)
            : base(sql)
	    {}

        /// <summary>
        /// 取得服务信息一览
        /// </summary>
        public DataTable GetServiceList(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    a.MA_SERVICEID ");
            cmd.AppendLine(" 	    ,a.TYPE ");
            cmd.AppendLine(" 	    ,a.SERVICENAME  ");
            cmd.AppendLine(" 	    ,a.README  ");
            cmd.AppendLine(" 	    ,a.CHARGE  ");
            cmd.AppendLine(" 	    ,a.SERVICEUNIT  ");
            cmd.AppendLine(" 	    ,b.NAME AS  SERVICEUNITNAME  ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_SERVICE a");
            cmd.AppendLine("    LEFT OUTER JOIN");
            cmd.AppendLine("    MA_CODE b");
            cmd.AppendLine("    ON");
            cmd.AppendLine("        a.SERVICEUNIT = b.MA_CODEID");
            cmd.AppendLine("    LEFT OUTER JOIN");
            cmd.AppendLine("    MA_COMINFO c");
            cmd.AppendLine("    ON");
            cmd.AppendLine("        c.COUNTRYID = a.COUNTRYID");
            cmd.AppendLine("    WHERE a.DELFLG IS NULL");
            cmd.AppendFormat("  and c.REGISTID = '{0}'", registId);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 按条件取得服务信息
        /// </summary>
        public DataTable GetServiceDetail(String serviceId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    a.MA_SERVICEID ");
            cmd.AppendLine(" 	    ,a.TYPE ");
            cmd.AppendLine(" 	    ,a.SERVICENAME  ");
            cmd.AppendLine(" 	    ,a.README  ");
            cmd.AppendLine(" 	    ,a.CHARGE  ");
            cmd.AppendLine(" 	    ,a.SERVICEUNIT  ");
            cmd.AppendLine(" 	    ,a.EXTENDTYPE  ");
            cmd.AppendLine(" 	    ,b.NAME AS  SERVICEUNITNAME  ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_SERVICE a");
            cmd.AppendLine("    LEFT OUTER JOIN");
            cmd.AppendLine("    MA_CODE b");
            cmd.AppendLine("    ON");
            cmd.AppendLine("        a.SERVICEUNIT = b.MA_CODEID");
            cmd.AppendLine("    WHERE a.DELFLG IS NULL");
            cmd.AppendFormat("  AND a.MA_SERVICEID = '{0}'",serviceId);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 按条件取得我的服务信息
        /// </summary>
        public DataTable GetServiceMy(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT b.MA_SERVICEID");
            cmd.AppendLine(" 	    ,b.BS_SERVICEID ");
            cmd.AppendLine(" 	    ,b.SERVICENUM ");
            cmd.AppendLine(" 	    ,b.STARTDATE ");
            cmd.AppendLine(" 	    ,b.ENDDATE ");
            cmd.AppendLine(" 	    ,b.REMARK ");
            cmd.AppendLine(" 	    ,b.STATE ");
            cmd.AppendLine(" 	    ,c.TYPE  ");
            cmd.AppendLine(" 	    ,c.CHARGE  ");
            cmd.AppendLine(" 	    ,c.SERVICENAME  ");
            cmd.AppendLine(" 	    ,d.NAME  ");
            cmd.AppendLine("        ,c.SENDBACK");
            //cmd.AppendLine("        ,a.MA_ACCOUNTID");
            //cmd.AppendLine("        ,a.IN_MONEYTOTAL");
            cmd.AppendLine(" 	FROM    MA_ACCOUNT    a  ");
            cmd.AppendLine("    INNER JOIN   BS_SERVICE    b");
            cmd.AppendLine("        ON   a.MA_ACCOUNTID = b.MA_ACCOUNTID");
            cmd.AppendLine("    LEFT JOIN     MA_SERVICE    c");
            cmd.AppendLine("        ON   b.MA_SERVICEID = c.MA_SERVICEID");
            cmd.AppendLine("    LEFT JOIN   MA_CODE       d");
            cmd.AppendLine("        ON   c.SERVICEUNIT = d.MA_CODEID");
            cmd.AppendLine("    WHERE");
            cmd.AppendFormat("      REGISTID = '{0}'", registId);
            cmd.AppendLine("    ORDER BY b.BS_SERVICEID DESC");


            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 按条件取得我的服务信息
        /// </summary>
        public DataTable GetServiceMyState(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    c.STATE  ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_COMINFO    a");
            cmd.AppendLine("    LEFT JOIN ");
            cmd.AppendLine("        MA_ACCOUNT    b");
            cmd.AppendLine("    ON");
            cmd.AppendLine("        a.MA_COMINFOID = b.MA_COMINFOID");
            cmd.AppendLine("        LEFT JOIN");
            cmd.AppendLine("        BS_SERVICE    c");
            cmd.AppendLine("    ON");
            cmd.AppendLine("        b.MA_ACCOUNTID = c.MA_ACCOUNTID");
            cmd.AppendLine("    WHERE");
            cmd.AppendFormat("      REGISTID = '{0}'", registId);


            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        public bool Startservice(String serviceid, String startdate,String enddate)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" UPDATE  ");
            cmd.AppendLine("        BS_SERVICE ");
            cmd.AppendLine(" SET ");
            cmd.AppendFormat("       STATE = 1");
            cmd.AppendFormat("       ,STARTDATE = {0}", startdate);
            cmd.AppendFormat("       ,ENDDATE = {0}", enddate);
            cmd.AppendLine(" WHERE ");
            cmd.AppendFormat("        BS_SERVICEID = '{0}'", serviceid);

            return sql.ExecuteStrQuery(cmd.ToString());
        }

        /// <summary>
        /// 按域名后缀取得购买信息
        /// </summary>
        /// <param name="domainExt"></param>
        /// <returns></returns>
        public DataTable GetServiceByDomain(String domainExt) 
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT a.MA_SERVICEID");
            cmd.AppendLine(" 	      ,a.TYPE ");
            cmd.AppendLine(" 	      ,a.SERVICENAME ");
            cmd.AppendLine(" 	      ,a.README ");
            cmd.AppendLine(" 	      ,a.CHARGE ");
            cmd.AppendLine(" 	      ,c.NAME AS SERVICEUNIT ");
            cmd.AppendLine(" 	      ,a.EXTENDTYPE ");
            cmd.AppendLine(" 	FROM              MA_SERVICE    a  ");
            cmd.AppendLine("    LEFT OUTER JOIN   MA_CODE       b  ");
            cmd.AppendLine("                 ON   a.EXTENDTYPE = b.LTYPE");
            cmd.AppendLine("    LEFT OUTER JOIN   MA_CODE       c  ");
            cmd.AppendLine("                 ON   a.SERVICEUNIT = c.MA_CODEID");
            cmd.AppendLine("    WHERE");
            cmd.AppendFormat("      b.NAME = '{0}'", domainExt);
            cmd.AppendLine("    AND");
            cmd.AppendLine("      a.DELFLG IS NULL");


            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得服务价格
        /// </summary>
        public DataTable GetServiceCharge(String type, String lang)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    CHARGE  ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_SERVICE  ");
            cmd.AppendLine("    WHERE");
            cmd.AppendFormat("      TYPE = '{0}'", type);
            cmd.AppendFormat(" AND COUNTRYID='{0}'",lang);


            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
    }
}