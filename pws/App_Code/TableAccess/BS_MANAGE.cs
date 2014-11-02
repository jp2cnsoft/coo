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
    public class BS_MANAGE : Seika.COO.DBA.DateBaseAccess
    {
        public BS_MANAGE(DBConnect sql)
            : base(sql)
	    {
		  
	    }
       
        //获得经营范围列表
        public DataTable GetManageList(String registid,String lang)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("SELECT     a.MA_ORDERID, a.NAME");
            orgsql.AppendLine(" FROM         MA_CALLINGCLASS AS a ");
            orgsql.AppendLine(" LEFT OUTER JOIN");
            orgsql.AppendLine(" BS_MANAGE AS b ");
            orgsql.AppendLine(" ON a.MA_ORDERID = b.MA_PRODUCTCLASSID");
            orgsql.AppendLine(" WHERE     (b.MA_COMINFOID =");
            orgsql.AppendLine("(SELECT     MA_COMINFOID");
            orgsql.AppendLine(" FROM          MA_COMINFO");
            orgsql.AppendFormat(" WHERE      REGISTID = '{0}'))", registid);
            //orgsql.AppendFormat(" AND      LANG = '{0}'))", lang);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //删除经营范围
        public bool RemoveManageList(String registid, String lang)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("DELETE FROM BS_MANAGE ");
            orgsql.AppendLine("WHERE     (MA_COMINFOID = ");
            orgsql.AppendLine("(SELECT     MA_COMINFOID ");
            orgsql.AppendLine("FROM          MA_COMINFO ");
            orgsql.AppendFormat(" WHERE      REGISTID = '{0}')) ", registid);
            //orgsql.AppendFormat(" AND      LANG = '{0}')) ", lang);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //追加经营范围
        public bool AddManageList(String ma_id,String ma_cominfoid,String ma_productClassId,String path)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("INSERT INTO BS_MANAGE ");
            orgsql.AppendLine("(BS_MANAGEID, MA_COMINFOID, MA_PRODUCTCLASSID, PATH) ");
            orgsql.AppendFormat("VALUES     ({0}, '{1}', '{2}', '{3}') ", ma_id, ma_cominfoid, ma_productClassId, path);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

    }
}