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
    /// 激活组织并取得密码
    /// </summary>
    public class OrgUpdateActive : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            //取得xml字段值
            DataTable dt = cds.Tables["MA_REGISTER"];
            DataRow dr = dt.Rows[0];

            String rigistId = StringToFilter(dr["RIGISTID"].ToString());

            ma_register = new MA_REGISTER(sql);
            String servicesDate = DateTime.Now.AddDays(CodeSymbol.m_servicesDate).ToString();
            //更新激活状态
            if (!ma_register.SetRegisterWait(rigistId, servicesDate))
            {
                return cds;
            }

            //取得密码
            ma_cominfo = new MA_COMINFO(sql);
            cmdt = new DataSetManage();
            cds.Tables.Add(cmdt.GetCloneTable(ma_cominfo.GetComRegistid(rigistId), "MA_REGISTER_MAIL"));

            return cds;
        }

    }
}
