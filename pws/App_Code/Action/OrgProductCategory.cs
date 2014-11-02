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
    /// 组织经营类别取得
    /// </summary>
    public class OrgProductCategory : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }          
            res = new DataSet();
            cmdt = new DataSetManage();
            ma_productclass = new MA_PRODUCTCLASS(sql);     
            String id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
            res.Tables.Add(cmdt.GetCloneTable(ma_productclass.GetOrgContent(id), "PRODUCT_ROOT"));
            res.Tables.Add(cmdt.GetCloneTable(ma_productclass.GetProductCategory(id), "PRODUCT"));
            //返回结果集
            return res;
        }
    }
}
