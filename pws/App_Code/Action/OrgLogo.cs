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
    /// Summary description for OrgLogo
    /// </summary>
    public class OrgLogo : ActionPageBase
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
            String name = dr["LOGOURL"].ToString();
            String id = StringToFilter(dr["RIGISTID"].ToString());

            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);

            if (!ma_cominfo.UpdateCompanyLogo(name, id))
            {
                throw new SysException("ED00000020");
            }
            return cds;
        }
    }
}