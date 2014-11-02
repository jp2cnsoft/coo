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

    public class OrgUpdataRegCertification : ActionPageBase
    {

        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            String id = cds.Tables[0].Rows[0][0].ToString();
            String linkman=StringToFilter(cds.Tables[0].Rows[0][1].ToString());
            String mobiletel = StringToFilter(cds.Tables[0].Rows[0][2].ToString());
            String charterurl = StringToFilter(cds.Tables[0].Rows[0][3].ToString());

            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            if (!ma_cominfo.UpdataComInfo(id, linkman, mobiletel, charterurl))
            {
                throw new SysException("ED00000020");
            }

            return cds;

        }






    }
}