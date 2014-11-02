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
    /// 取得工作信息
    /// </summary>
    public class OrgRecruiter : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            //if (cds == null)
            //{
            //    throw new SysException("ED00000020");
            //}
            //m_session = new SessionManager(Session);
            //res = new DataSet();
            //cmdt = new DataSetManage();
            //ma_job = new MA_TAKEJOB(sql);
            //switch(oparms[0])
            //{
            //    case "ADD": return Add(cds);
            //        break;
            //    case "UPDATE": return Update(cds);
            //        break;
            //    case "DEL": ma_job.DelJob(cds.Tables[0].Columns["MA_JOBID"]);

            //}

            //  res.Tables.Add(cmdt.GetCloneTable(ma_product.SearchJobCount(
            //StringToFilter(dr["NAME"].ToString()),
            //dr["MA_PRODUCTCLASSID"].ToString(),
            //dr["COUNTRYID"].ToString(),
            //dr["PROVINCEID"].ToString(),
            //dr["CITY"].ToString(),
            //dr["BOROGUHID"].ToString(),
            //m_session.Page_UICultureID
            //                ), "TAKEJOBCOUNT"));
            //res.Tables.Add(cmdt.GetCloneTable(ma_product.SearchJob(
            //StringToFilter(dr["NAME"].ToString()),
            //dr["MA_PRODUCTCLASSID"].ToString(),
            //dr["COUNTRYID"].ToString(),
            //dr["PROVINCEID"].ToString(),
            //dr["CITY"].ToString(),
            //dr["BOROGUHID"].ToString(),
            //m_session.Page_UICultureID,
            //Convert.ToInt16(dr["STARTIDX"].ToString()), Convert.ToInt16(dr["COUNT"].ToString())),
            //                  ),"TAKEJOB"));
            //返回结果集
            return res;
        }
        /////
        ///// 添加数据
        /////
        //private void Add(DataTable dt)
        //{
        //    if (dt.TableName.Trim().Equals("MA_JOB"))
        //    {
        //        string jobid = ma_job.GetNextSeqNumber();
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            ma_job.AddJob(jobid, r["JOBNAME"].ToString(), r["REGISTID"].ToString(), r["POSTTYPECD"].ToString(), r["POSTCD"].ToString(), r["NUMBER"].ToString(), r["STATECD_1"].ToString(), r["STATECD_2"].ToString(),
        //               r["STATECD_3"].ToString(), r["STATECD_4"].ToString(), r["STATERESCD_1"].ToString(), r["STATERESCD_2"].ToString(), r["STATERESCD_3"].ToString(), r["STATERESCD_4"].ToString(),
        //               r["WORKKINDCD"].ToString(), r["EFFECTDATE"].ToString(), r["WAGE"].ToString(), r["SCHOOLLEVELCD"].ToString(), r["SEXCD"].ToString(), r["AGE_BEGIN"].ToString(), r["AGE_END"].ToString(), r["EXPERIENCE"].ToString(),
        //               r["OFFICEDEPICT"].ToString(), r["OFFICEREQUEST"].ToString(), r["LANG"].ToString(), r["CREATEDATE"].ToString(), r["UPDATEDATE"].ToString());
        //        }
        //    }
        //}
        ////other ADD
        //private void AddOther( String jobname, String registid, String posttypecd,
        //    String postcd, String number, String statecd_1, String statecd_2, String statecd_3, String statecd_4, String staterescd_1, String staterescd_2,
        //    String staterescd_3, String staterescd_4, String workkindcd, String effectdate, String wage, String schoollevelcd, String sexcd, String age_begin,
        //    String age_end, String experience, String officedepict, String officerequest, String lang, String createdate, String updatedate)
        //{
        //    string ma_jobid = ma_job.GetNextSeqNumber();
        //    ma_job.AddJob(ma_jobid, jobname, registid, posttypecd, postcd, number, statecd_1, statecd_2,
        //            statecd_3, statecd_4, staterescd_1, staterescd_2, staterescd_3, staterescd_4,
        //            workkindcd, effectdate, wage, schoollevelcd, sexcd, age_begin, age_end, experience,
        //            officedepict, officerequest, lang, createdate, updatedate);
        //}
        ///// 
        /////更新
        /////
        //private Boolean Update(String ma_jobid, String jobname, String registid, String posttypecd,
        //    String postcd, String number, String statecd_1, String statecd_2, String statecd_3, String statecd_4, String staterescd_1, String staterescd_2,
        //    String staterescd_3, String staterescd_4, String workkindcd, String effectdate, String wage, String schoollevelcd, String sexcd, String age_begin,
        //    String age_end, String experience, String officedepict, String officerequest, String lang, String createdate, String updatedate)
        //{
        //    return ma_job.UpdateJob(ma_jobid, jobname, registid, posttypecd,
        //    postcd, number, statecd_1, statecd_2, statecd_3, statecd_4, staterescd_1, staterescd_2,
        //    staterescd_3, staterescd_4, workkindcd, effectdate, wage, schoollevelcd, sexcd, age_begin,
        //    age_end, experience, officedepict, officerequest, lang, createdate, updatedate);
        //}
        ////other
        //private Boolean UpdateOther(DataTable dt)
        //{
        //    if (dt.TableName.Trim().Equals("MA_JOB"))
        //    {
               
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            return ma_job.Update(r["MA_JOBID"].ToString(), r["JOBNAME"].ToString(), r["REGISTID"].ToString(), r["POSTTYPECD"].ToString(), r["POSTCD"].ToString(), r["NUMBER"].ToString(), r["STATECD_1"].ToString(), r["STATECD_2"].ToString(),
        //               r["STATECD_3"].ToString(), r["STATECD_4"].ToString(), r["STATERESCD_1"].ToString(), r["STATERESCD_2"].ToString(), r["STATERESCD_3"].ToString(), r["STATERESCD_4"].ToString(),
        //               r["WORKKINDCD"].ToString(), r["EFFECTDATE"].ToString(), r["WAGE"].ToString(), r["SCHOOLLEVELCD"].ToString(), r["SEXCD"].ToString(), r["AGE_BEGIN"].ToString(), r["AGE_END"].ToString(), r["EXPERIENCE"].ToString(),
        //               r["OFFICEDEPICT"].ToString(), r["OFFICEREQUEST"].ToString(), r["LANG"].ToString(), r["CREATEDATE"].ToString(), r["UPDATEDATE"].ToString());
        //        }
        //    }
        //}

    }
}