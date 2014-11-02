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
    /// 组织经营类别取得
    /// </summary>
    public class OrgPost : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            m_session = new SessionManager(Session);
            res = new DataSet();
            cmdt = new DataSetManage();
            MA_POST ma_post = new MA_POST(sql);
            DataRow dr = cds.Tables[0].Rows[0];
            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchPostCount(
                            StringToFilter(dr["NAME"].ToString()),
                            dr["MA_POSTCLASSID"].ToString(),
                            dr["COUNTRYID"].ToString(),
                            dr["PROVINCEID"].ToString(),
                            dr["CITY"].ToString(),
                            dr["BOROGUHID"].ToString(),

                            dr["SCHOOLLEVEL"].ToString(),
                            dr["SEX"].ToString(),
                            dr["AGE"].ToString(),
                            dr["WORKKIND"].ToString(),
                            dr["EXPERIENCE"].ToString(),
                            dr["ISSUEDATE"].ToString(),
                            m_session.Page_UICultureID
                            ), "POSTCOUNT"));
            res.Tables.Add(cmdt.GetCloneTable(ma_post.SearchPost(
                            StringToFilter(dr["NAME"].ToString()),
                            dr["MA_POSTCLASSID"].ToString(),
                            dr["COUNTRYID"].ToString(),
                            dr["PROVINCEID"].ToString(),
                            dr["CITY"].ToString(),
                            dr["BOROGUHID"].ToString(),

                            dr["SCHOOLLEVEL"].ToString(),
                            dr["SEX"].ToString(),
                            dr["AGE"].ToString(),
                            dr["WORKKIND"].ToString(),
                            dr["EXPERIENCE"].ToString(),
                            dr["ISSUEDATE"].ToString(),
                            m_session.Page_UICultureID,
                            Convert.ToInt16(dr["STARTIDX"].ToString()), Convert.ToInt16(dr["COUNT"].ToString())), "POST"));
            //返回结果集
            return res;
        }
    }
}