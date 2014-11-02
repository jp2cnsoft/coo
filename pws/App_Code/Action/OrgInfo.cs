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
using Seika.COO.Web.PG;

namespace Seika.COO.Action
{
    /// <summary>
    /// 组织信息取得
    /// </summary>
    public class OrgInfo : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            String registId = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
            //返回的ds
            String MA_STRUCTUREID = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            BS_MANAGE bs_manage = new BS_MANAGE(sql);
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(sql);
            MA_STRUCTURE ma_structure = new MA_STRUCTURE(sql);

            DataTable dt = ma_cominfo.GetComInfo(registId,"");
            if (dt.Rows.Count > 0)
            {
                //取得当前系统语言
                String lang = dt.Rows[0]["COUNTRYID"].ToString();
                ds.Tables.Add(cmdt.GetCloneTable(dt, "MA_COMINFO"));
                ds.Tables.Add(cmdt.GetCloneTable(bs_manage.GetManageList(registId, lang), "MA_CALLINGCLASS"));
                ds.Tables.Add(cmdt.GetCloneTable(ma_structure.GetMa_structure(MA_STRUCTUREID), "MA_STRUCTURE"));

                ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgContent(lang), "CALLING"));
                if (ds.Tables["CALLING"].Rows.Count > 0 && ds.Tables["CALLING"].Rows[0]["MA_ORDERID"] != null)
                {
                    ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgList(lang), "CALLING_SUB"));
                }
            }
            //返回结果集
            return ds;
        }
    }
}
