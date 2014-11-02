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
using Seika.COO.Web.PG;


namespace Seika.COO.Action
{
    /// <summary>
    /// 取得公司地址信息(到市级)
    /// </summary>
    public class OrgCominfoAddressCity : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet ds, String[] oparms)
        {
            DataSet cds = new DataSet();
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            cmdt = new DataSetManage();
            m_session = new SessionManager(Session);

            if (ds == null)
            {
                throw new SysException("ED00000020");
            }
            String registid = StringToFilter(ds.Tables[0].Rows[0]["REGISTID"].ToString());

            cds.Tables.Add(cmdt.GetCloneTable(ma_cominfo.GetComInfoAddressCity(registid, m_session.Page_UICultureID), "MA_COMINFO"));
            //返回结果集
            return cds;
        }
    }
}