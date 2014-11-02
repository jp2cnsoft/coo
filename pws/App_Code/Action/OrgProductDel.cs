using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using System.IO;
using System.Data.SqlClient;
using Seika.Db;
using Seika.Util;
using Seika.CooException;
using Seika.COO.DBA.BS;


namespace Seika.COO.Action
{
    /// <summary>
    /// Summary description for OrgProductDel
    /// </summary>
    public class OrgProductDel : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            String id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());

            MA_PRODUCT ma_product = new MA_PRODUCT(sql);
            BS_PRODUCT bs_product = new BS_PRODUCT(sql);
            if (!ma_product.DelProduct(id))
            {
                throw new SysException("ED00000020");
            }
            bs_product.DelProdeucBs(id);
            //if (!bs_product.DelProdeucBs(id))
            //{
            //    throw new SysException("ED00000020");
            //}
            return cds;
        }
    }
}
