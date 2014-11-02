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
    public class MA_ONLINETASK : Seika.COO.DBA.DateBaseAccess
    {
        public MA_ONLINETASK(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        //获得事务
        public DataTable GetOnLineTaskByTaskId(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select  * from MA_ONLINETASK");
            orgsql.AppendFormat("  where MA_ONLINE_TASKID = '{0}'", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //获得事务
        public DataTable GetOnLineTaskByMenuId(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select  * from MA_ONLINETASK");
            orgsql.AppendFormat("  where MENUID = '{0}'", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}