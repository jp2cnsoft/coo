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
    /// 组织登陆
    /// </summary>
    public class OrgLogin : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new AppException("ED00000020");
            }
            DataSetManage cmdt = new DataSetManage();
            String registId = StringToFilter(cds.Tables[0].Rows[0]["registId"].ToString());
            String password = StringToFilter(cds.Tables[0].Rows[0]["password"].ToString());
            String ip = cds.Tables[0].Rows[0]["ip"].ToString();
            //取得当前系统语
            m_session = new SessionManager(Session);
            //String lang = m_session.Page_UICultureID;

            ma_register = new MA_REGISTER(sql);
            //判断限制
            CheckErrNum(registId);
            DataTable regInfo = ma_register.GetRegInfo(registId, password);

            //判断用户名密码是否正确
            //失败
            if (regInfo.Rows.Count < 1)
            {
                ma_register.UpdateLoginErrComInfo(registId);
                sql.Commit();
                throw new AppException("ED01000160");
            }
            //成功
            else 
            {
                cds.Tables.Add(cmdt.GetCloneTable(regInfo, "REGINFO"));
                ma_register.UpdateComInfo(registId, ip);
                //ma_register = new MA_REGISTER(sql);
                //DataTable comInfo = ma_register.GetRegisterStyle(registId);
                //cds.Tables.Add(cmdt.GetCloneTable(comInfo, "STYLEINFO"));
            }
            //返回结果集
            return cds;
        }

        private void CheckErrNum(String registId) 
        {
            BaseFunction baseFunction = new BaseFunction();
            DataTable edt = ma_register.GetErrRegInfo(registId);
            if (edt.Rows.Count > 0)
            {
                String errDateTime = edt.Rows[0]["ERRDATETIME"].ToString();
                String errNum = edt.Rows[0]["ERRNUM"].ToString();
                if (!String.IsNullOrEmpty(errDateTime) && !String.IsNullOrEmpty(errNum) && Convert.ToInt16(errNum) >= 3)
                {
                    double minutes = 60;
                    double limitMinutes = GetMinutesLimit(Convert.ToDateTime(errDateTime),DateTime.Now);
                    if (limitMinutes <= minutes)
                    {
                        throw new AppException("ED01000180");
                    }
                    else 
                    {
                        ma_register.UpdateNullErrRegInfo(registId);
                    }
                }
            }
            else 
            {
                throw new AppException("ED01000170");
            }
        }

        private double GetMinutesLimit(DateTime otime,DateTime ntime) 
        {
            TimeSpan ts = ntime - otime;
            return ts.TotalMinutes;
        }
    }
}
