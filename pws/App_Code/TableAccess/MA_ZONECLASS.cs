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
    public class MA_ZONECLASS : Seika.COO.DBA.DateBaseAccess
    {
        public MA_ZONECLASS(DBConnect sql)
            : base(sql)
	    {
		  
	    }
       
        //获得下拉
        public DataTable GetOrgList(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_ZONECLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            orgsql.AppendLine("  order by MA_ORDERID");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //是否是子节点    
        public bool IsSub(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_ZONECLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0].Rows.Count == 0;
        }

        //通过邮编获得地址
        public DataTable GetAddressbyPostCode(String post)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_ZONECLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( POSTALCODE = '{0}')", post);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //通过id获得地址
        public DataTable GetAddressContent(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_ZONECLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_ORDERID = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得下拉
        public DataTable GetAddressCategory(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select *, NAME,MA_ORDERID as CATEGORYID from MA_ZONECLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //语言下全部职位类别列表
        public DataTable GetOrgList()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME],a.RESID,b.MA_LANGUAGEID");
            orgsql.AppendLine("                                      FROM MA_ZONECLASS a");
            orgsql.AppendLine("  INNER JOIN ");
            orgsql.AppendLine("                                            MA_LANGUAGE b ");
            orgsql.AppendLine("  ON  a.CLASSTYPE = b.MA_LANGUAGEID");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}