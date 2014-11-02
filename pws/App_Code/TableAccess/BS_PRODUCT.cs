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
    public class BS_PRODUCT : Seika.COO.DBA.DateBaseAccess
    {
        public BS_PRODUCT(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        
        public bool AddProductBs(String bs_prodid,String ma_productid,String ma_productclassid,String path)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into BS_PROD");
            orgsql.AppendLine("       (BS_PRODID");
            orgsql.AppendLine("       ,MA_PRODID");
            orgsql.AppendLine("       ,MA_PRODUCTCLASSID");
            orgsql.AppendLine("       ,PATH)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}')", bs_prodid, ma_productid, ma_productclassid, path);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        public bool UpdateProductBs(String ma_productid, String ma_productclassid, String path)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update BS_PROD");
            orgsql.AppendLine("       set");
            orgsql.AppendFormat("     MA_PRODUCTCLASSID= '{0}'", ma_productclassid);
            orgsql.AppendFormat("    ,PATH= '{0}'", path);
            orgsql.AppendFormat("     where MA_PRODID = '{0}'", ma_productid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public bool DelProdeucBs(String ma_productid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM BS_PROD");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       MA_PRODID = '{0}'", ma_productid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public bool DelProdeucBs(String registerId,String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM BS_PROD");
            orgsql.AppendLine("   WHERE EXISTS");
            orgsql.AppendLine("   (");
            orgsql.AppendLine("         SELECT b.MA_PRODID");
            orgsql.AppendLine("             FROM MA_PROD b");
            orgsql.AppendLine("         WHERE");
            orgsql.AppendFormat("              b.REGISTID = '{0}'", registerId);
            orgsql.AppendLine("         AND");
            orgsql.AppendFormat("              b.LANG = '{0}'", lang);
            orgsql.AppendLine("         AND");
            orgsql.AppendLine("                BS_PROD.MA_PRODID = b.MA_PRODID");
            orgsql.AppendLine("   )");

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}