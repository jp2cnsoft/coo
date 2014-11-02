
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
    /// Summary description for OrgATTSTATION
    /// </summary>
    public class OrgDeleteflg : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {

            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            String Deleteflg = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
            //返回的ds
            DataSet cs = new DataSet();
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            cmdt = new DataSetManage();
            cs.Tables.Add(cmdt.GetCloneTable(ma_cominfo.GetComDeleteflg(Deleteflg), "MA_STRUCTURE"));

            //返回结果集
            return cs;

        }
    }
}