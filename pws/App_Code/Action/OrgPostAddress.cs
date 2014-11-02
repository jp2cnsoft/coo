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
    /// 地域取得
    /// </summary>
    public class OrgPostAddress : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            res = new DataSet();
            cmdt = new DataSetManage();
            MA_POST ma_post = new MA_POST(sql);
            String jobId = cds.Tables["POSTADDRESS"].Rows[0]["JOBID"].ToString();

            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchJobCountry(jobId), "COUNTRY"));
            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchJobProvince(jobId), "PROVINCE"));
            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchJobCity(jobId), "CITY"));
            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchJobBorough(jobId), "BOROUGH"));
           
            //返回结果集
            return res;
        }
    }

}