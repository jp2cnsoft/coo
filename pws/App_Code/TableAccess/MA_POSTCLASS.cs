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
    public class MA_POSTCLASS: Seika.COO.DBA.DateBaseAccess
    {
        public MA_POSTCLASS(DBConnect sql)
            : base(sql)
	    {
        
        }
        public DataTable GetPostClassAllCategory(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_ORDERID AS POSTID");
            orgsql.AppendLine("         ,NAME");
            orgsql.AppendLine("  from  MA_POSTCLASS");
            orgsql.AppendLine("  where ");
            orgsql.AppendFormat(" CLASSTYPE = '{0}'", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable GetPostClassCategory(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_ORDERID AS POSTID");
            orgsql.AppendLine("         ,NAME");
            orgsql.AppendLine("  from  MA_POSTCLASS");
            //orgsql.Appendline("  where ");
            //orgsql.AppendFormat(" CLASSTYPE = '{0}'", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得下拉
        public DataTable GetPostKindByParentId(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_ORDERID, NAME from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }


        //获得下拉
        public DataTable GetOrgList(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[type];
        }
        //获得当前内容
        public DataTable GetOrgContent(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_ORDERID AS CATEGORYID,NAME,CLASSTYPE from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_ORDERID = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable GetPostCategory(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select NAME, MA_ORDERID as CATEGORYID  from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //是否是子节点    
        public bool IsSub(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0].Rows.Count == 0;
        }

        //取得邮编列表
        public DataTable GetPostCD(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select  ClASSTYPE AS POSTCD, NAME from MA_POSTCLASS");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_ORDERID = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //语言下全部职位类别列表
        public DataTable GetOrgList()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME],a.RESID,b.MA_LANGUAGEID");
            orgsql.AppendLine("                                      FROM MA_POSTCLASS a");
            orgsql.AppendLine("  INNER JOIN ");
            orgsql.AppendLine("                                            MA_LANGUAGE b ");
            orgsql.AppendLine("  ON  a.CLASSTYPE = b.MA_LANGUAGEID");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}
