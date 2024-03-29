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
    public class OrgUpdataDeleteflg : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            String MA_STRUCTUREID = StringToFilter(cds.Tables[0].Rows[0][0].ToString());              
                MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
               
                if (!ma_cominfo.UpdataDeleteflg(MA_STRUCTUREID))
                {
                    throw new SysException("ED00000020");
                }
             return cds;
           
        }
    }
}






