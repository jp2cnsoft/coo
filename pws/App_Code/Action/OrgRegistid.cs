using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Seika.Db;
using Seika.Util;
using Seika.CooException;
using Seika.COO.DBA.MA;
using System.Web.Services;
using System.Web.Services.Protocols;
using Seika;
using Seika.COO.Util;
using System.IO;
using System.Data.SqlClient;
using Seika.COO.DBA.BS;


namespace Seika.COO.Action
{
    /// <summary>
    /// 注册帐号的取得
    /// </summary>
    public class OrgRegistid : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet ds, String[] oparms)
        {

            if (ds == null)
            {
                throw new SysException("ED00000020");
            }
            String registid = StringToFilter(ds.Tables[0].Rows[0]["REGISTID"].ToString());
            //返回的ds
            DataSet cds = new DataSet();
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            cmdt = new DataSetManage();
            DataTable dt = ma_cominfo.GetComRegistid(registid);
            if (dt.Rows.Count < 1)
            {
                throw new AppException("ED01000290");
            }
            cds.Tables.Add(cmdt.GetCloneTable(dt, "MA_COMINFO"));
            //返回结果集
            return cds;
        }
    }
}