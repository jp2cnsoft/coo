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
using Seika.COO.Action;


namespace Seika.COO.Action
{
    /// <summary>
    /// Summary description for OrgPassword
    /// </summary>
    public class OrgPassword : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            //取得xml字段值
            DataTable dt = cds.Tables["MA_COMINFO"];
            DataRow dr = dt.Rows[0];

            //公司ID            
            String registid = StringToFilter(dr["REGISTID"].ToString());
            String password = StringToFilter(dr["PASSWORD"].ToString());

            MA_REGISTER ma_register = new MA_REGISTER(sql);

            if (!ma_register.UpdateRegisterPassword(registid, password))
            {
                throw new SysException("ED00000020");
            }
            return cds;
        }
    }
}