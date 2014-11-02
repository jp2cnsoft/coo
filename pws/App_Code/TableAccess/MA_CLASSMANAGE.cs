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
    public class MA_CLASSMANAGE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_CLASSMANAGE(DBConnect sql)
            : base(sql)
	    {
		  
	    }
       
        //获得下拉
        public DataTable GetOrgList(String type)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_CLASSMANAGEID,NAME from MA_CLASSMANAGE");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", type);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //是否是子节点    
        public bool IsSub(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select * from MA_CLASSMANAGE");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( CLASSTYPE = '{0}')", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0].Rows.Count == 0;
        }
    }
}