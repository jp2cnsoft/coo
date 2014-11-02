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
    public class BS_STRUCTUREDETAIL : Seika.COO.DBA.DateBaseAccess
    {
        public BS_STRUCTUREDETAIL(DBConnect sql)
            : base(sql)
	    {
		  
	    }
        //插入行业
        public bool InsertDetail(String id, String detail, String classmanageid,String cuserid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into BS_STRUCTUREDETAIL");
            orgsql.AppendLine("  (ID,DETAILID,MA_CLASSMANAGEID,CREATEUSERID,CREATEDATE");          
            orgsql.AppendLine("  values");
            orgsql.AppendFormat("  ('{0}','{1}','{2}','{3}',GetDate()", id, detail, classmanageid, cuserid);
            return sql.ExecuteStrQuery(orgsql.ToString());
          
        }

        //取得行业
        public DataTable GetDetail(String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" select * from BS_STRUCTUREDETAIL");
            orgsql.AppendFormat(" where  DETAILID = '{0}'", id);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}