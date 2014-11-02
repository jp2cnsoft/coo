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
    /// 邮件更新
    /// </summary>
    public class OrgEmailUpdate : ActionPageBase
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
            String email = StringToFilter(dr["EMAIL"].ToString());

            ma_register = new MA_REGISTER(sql);
            //更新邮件信息
            if (!ma_register.SetRegisterEmail(rigistId, email))
            {
                throw new SysException("ED00000020");
            }

            return cds;
        }
    }
}
