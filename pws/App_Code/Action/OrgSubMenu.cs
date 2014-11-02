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

    public class OrgSubMenu : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            m_session = new SessionManager(Session);

            ma_cominfo = new MA_COMINFO(sql);
            DataTable mdt = ma_cominfo.GetComTitle(m_session.Page_UICultureID);
            foreach(DataRow row in mdt.Rows)
            {
                String name = row["NAME"].ToString();
                if(name.Length >= 14)
                {
                    row["NAME"] = name.Substring(0,14) + "...";
                }
            }
            ds.Tables.Add(cmdt.GetCloneTable(mdt, "COMTITLE"));

            ma_product = new MA_PRODUCT(sql);
            DataTable pdt = ma_product.GetProTitle(m_session.Page_UICultureID);
            foreach (DataRow row in pdt.Rows)
            {
                String name = row["NAME"].ToString();
                if (name.Length >= 14)
                {
                    row["NAME"] = name.Substring(0, 14) + "...";
                }
            }
            ds.Tables.Add(cmdt.GetCloneTable(pdt, "PROTITLE"));

            //ma_post = new MA_POST(sql);
            //DataTable jdt = ma_post.GetJobTitle(m_session.Page_UICultureID);
            //foreach (DataRow row in jdt.Rows)
            //{
            //    String name = row["NAME"].ToString();
            //    if (name.Length >= 14)
            //    {
            //        row["NAME"] = name.Substring(0, 14) + "...";
            //    }
            //}
            //ds.Tables.Add(cmdt.GetCloneTable(jdt, "JOBTITLE"));

            return ds;
        }
    }
}