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
    /// 招聘职位类别取得
    /// </summary>
    public class OrgPostSub : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            res = new DataSet();
            cmdt = new DataSetManage();
            ma_postclass = new MA_POSTCLASS(sql);
            String id = cds.Tables[0].Rows[0][0].ToString();
            DataTable dt = new DataTable("POST");
            dt.Columns.Add(new DataColumn("id"));
            DataRow dr = dt.NewRow();
            if (ma_postclass.IsSub(id))
            {
                dr["id"] = "0";
            }
            else
            {
                dr["id"] = "1";
            }
            dt.Rows.Add(dr);

            res.Tables.Add(cmdt.GetCloneTable(dt, "POST"));
            //返回结果集
            return res;
        }
    }
}