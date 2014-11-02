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
    public class MA_CALLINGCLASS : Seika.COO.DBA.DateBaseAccess
    {
        public MA_CALLINGCLASS(DBConnect sql)
            : base(sql)
	    {
		  
	    }
       
        //获得下拉
        public DataTable GetOrgList(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_CALLINGCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //获得内容
        public DataTable GetOrgContent(String classId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_ORDERID,CLASSTYPE,[NAME] from MA_CALLINGCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_ORDERID = '{0}')", classId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //是否是子节点    
        public bool IsSub(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_CALLINGCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0].Rows.Count == 0;
        }

        //获得下拉
        public DataTable GetCallingCategory(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select *, NAME,MA_ORDERID as CATEGORYID from MA_CALLINGCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}' )", type);
            // orgsql.AppendLine("  ORDER BY len(NAME) ");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得下拉
        public DataTable GetCallingType(String callingClassId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select CLASSTYPE from MA_CALLINGCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_ORDERID = '{0}')", callingClassId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //语言下全部行业列表
        public DataTable GetOrgList()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME],a.RESID,b.MA_LANGUAGEID");
            orgsql.AppendLine("                                      FROM MA_CALLINGCLASS a");
            orgsql.AppendLine("  INNER JOIN ");
            orgsql.AppendLine("                                            MA_LANGUAGE b ");
            orgsql.AppendLine("  ON  a.CLASSTYPE = b.MA_LANGUAGEID");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}