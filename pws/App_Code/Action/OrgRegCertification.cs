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
   
    public class OrgRegCertification : ActionPageBase
    {

        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            String id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
            
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);

            DataTable dt = ma_cominfo.GetCompanyAllInfo(id);
            cmdt = new DataSetManage();
            
            if (dt==null)
            {
                throw new SysException("ED00000020");
            }
                       
            DataSet dsc = new DataSet();
            dsc.Tables.Add(cmdt.GetCloneTable(dt, "MA_COMINFO"));

            return dsc;

        }


      
    
    
    
    }
}

