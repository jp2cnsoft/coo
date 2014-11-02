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
    /// Summary description for OrgPostDel
    /// </summary>
    public class OrgPostDel : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            String id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());

            MA_POST ma_post = new MA_POST(sql);

            if (!ma_post.DelJOB(id))
            {
                throw new SysException("ED00000020");
            }
            return cds;
        }
    }
}