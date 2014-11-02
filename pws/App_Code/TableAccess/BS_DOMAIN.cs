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
    public class BS_DOMAIN : Seika.COO.DBA.DateBaseAccess
    {
        public BS_DOMAIN(DBConnect sql)
            : base(sql)
	    {
            
	    }
       
        //获得用户域名列表
        public DataTable GetDomainList(String registid)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("SELECT     BS_DOMAINID,WEBDOMAIN ");
            orgsql.AppendLine("    FROM   BS_DOMAIN ");
            orgsql.AppendFormat(" WHERE     REGISTID = '{0}'", registid);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //删除用户域名列表
        public bool RemoveDomainList(String registid)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("DELETE FROM BS_DOMAIN ");
            orgsql.AppendFormat(" WHERE      REGISTID = '{0}' ", registid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //追加经营范围
        public bool AddDomain(String bs_id, String webDomain, String registId)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("INSERT INTO BS_DOMAIN ");
            orgsql.AppendLine("(BS_DOMAINID, WEBDOMAIN, REGISTID) ");
            orgsql.AppendFormat("VALUES     ('{0}', '{1}', '{2}') ", bs_id, webDomain, registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
        /// <summary>
        /// 取得所有域名列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDomainList()
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("SELECT BS_DOMAINID, WEBDOMAIN, REGISTID ");
            orgsql.AppendLine("FROM   BS_DOMAIN");

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得当前域名
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public DataTable GetDomain(String registId,String domainName)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("SELECT BS_DOMAINID ");
            orgsql.AppendLine("FROM   BS_DOMAIN");
            orgsql.AppendFormat("WHERE  WEBDOMAIN = '{0}'",domainName);
            orgsql.AppendFormat("AND  REGISTID <> '{0}'", registId);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}