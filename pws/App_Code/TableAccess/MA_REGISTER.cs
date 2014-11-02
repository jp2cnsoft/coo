﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.Db;

namespace Seika.COO.DBA.MA
{
    public class MA_REGISTER : Seika.COO.DBA.DateBaseAccess
    {
        public MA_REGISTER(DBConnect sql) : base(sql)
        {
        }

        //取得登陆信息
        public DataTable GetRegInfo(String registId, String password)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_REGISTERID,a.WAITFLG,b.LANG,c.LANGRES");
            orgsql.AppendLine("  FROM MA_REGISTER a");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_COMINFO b");
            orgsql.AppendLine("  ON a.MA_REGISTERID = b.MA_REGISTERID ");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_LANGUAGE c");
            orgsql.AppendLine("  ON c.MA_LANGUAGEID = b.LANG ");
            orgsql.AppendFormat(" WHERE a.REGISTID = '{0}'", registId);
            orgsql.AppendFormat(" AND   CAST(a.PASSWORD AS varbinary) = CAST('{0}' AS varbinary)", password);
            //orgsql.AppendLine(" AND   b.SAVEFLG IS NULL");
            //orgsql.AppendLine(" AND   a.WAITFLG IS NULL");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }


        //更新登陆信息
        public bool UpdateComInfo(String registId, String ip)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_REGISTER");
            orgsql.AppendFormat("set SUCCEEDIP = '{0}'", ip);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    SUCCEEDDATETIME = getdate()");
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ERRNUM = 0");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新登陆信息
        public bool UpdateLoginErrComInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_REGISTER");
            orgsql.AppendFormat("set ERRDATETIME = getdate()");
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ERRNUM = ERRNUM + 1");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新登陆错误信息
        public DataTable GetErrRegInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_REGISTERID,ERRDATETIME,ERRNUM from MA_REGISTER");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            //orgsql.AppendLine("  and");
            //orgsql.AppendLine("     WAITFLG IS NULL");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 登陆信息注册
        /// </summary>
        /// <param name="registerid">注册ID</param>
        /// <param name="registId">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="email">EMAIL</param>
        /// <returns></returns>
        public bool AddRegisterInfo(String registerid, String registId, String passWord, String email)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_REGISTER");
            orgsql.AppendLine("       (MA_REGISTERID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,PASSWORD");
            orgsql.AppendLine("       ,WAITDATETIME");
            orgsql.AppendLine("       ,EMAIL");
            orgsql.AppendLine("       ,WAITFLG)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}',GETDATE(),'{3}','1')",
                registerid, registId, passWord,email);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
        /// <summary>
        /// 取得登陆注册帐号与邮箱
        /// </summary>
        /// <param name="registid"></param>
        /// <returns></returns>
        public DataTable GetComRegistid(String registid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select REGISTID,EMAIL,PASSWORD from MA_REGISTER");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("REGISTID = '{0}'", registid);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="registid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateRegisterPassword(String registid, String password)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_REGISTER");
            orgsql.AppendLine("  set");
            orgsql.AppendFormat("  PASSWORD = '{0}'", password);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  REGISTID = '{0}'", registid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新登陆信息
        public bool UpdateNullErrRegInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendFormat("set ERRDATETIME = NULL");
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ERRNUM = 0");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 取得登陆登录密码
        /// </summary>
        public DataTable GetRegisterPassWord(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    PASSWORD ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_REGISTER ");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" REGISTID = '{0}'", registId);
            //cmd.AppendLine(" AND SAVEFLG IS NULL");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        /// <summary>
        /// 修改登陆登录密码
        /// </summary>
        public bool SetRegisterPassWord(String registId, String newpassword)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("    UPDATE MA_REGISTER");
            orgsql.AppendLine("    SET");
            orgsql.AppendFormat(" PASSWORD = '{0}'", newpassword);
            orgsql.AppendLine(" WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
        /// <summary>
        /// 取得公司样式
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>
        public DataTable GetRegisterStyle(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_REGISTERID");
            orgsql.AppendLine("         ,a.EMAIL");
            orgsql.AppendLine("         ,b.XSLNAME");
            orgsql.AppendLine("         ,b.CSSNAME");
            orgsql.AppendLine("  FROM MA_REGISTER a");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_STYLE b");
            orgsql.AppendLine("  ON");
            orgsql.AppendLine("       a.MA_STYLEID = b.MA_STYLEID");
            orgsql.AppendFormat("WHERE  a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  AND");
            orgsql.AppendLine("       a.DELFLG IS NULL");

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得全部公司不同语言全部数据
        /// </summary>
        /// <param name="registId"></param>
        /// <returns></returns>
        public DataTable GetRegisterList()
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_REGISTERID");
            orgsql.AppendLine("         ,a.REGISTID");
            orgsql.AppendLine("         ,b.XSLNAME");
            orgsql.AppendLine("         ,b.CSSNAME");
            orgsql.AppendLine("         ,c.LANG");
            orgsql.AppendLine("  FROM MA_REGISTER a");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_STYLE b");
            orgsql.AppendLine("  ON");
            orgsql.AppendLine("       a.MA_STYLEID = b.MA_STYLEID");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_COMINFO c");
            orgsql.AppendLine("  ON");
            orgsql.AppendLine("       a.MA_REGISTERID = c.MA_REGISTERID");
            orgsql.AppendLine("WHERE  ");
            orgsql.AppendLine("       c.SAVEFLG IS NULL");

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 修改邮件
        /// </summary>
        public bool SetRegisterEmail(String registId, String email)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("    UPDATE MA_REGISTER");
            orgsql.AppendLine("    SET");
            orgsql.AppendFormat(" EMAIL = '{0}'", email);
            orgsql.AppendLine(" WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 修改注册激活状态
        /// </summary>
        public bool SetRegisterWait(String registId, String servicesDate)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" UPDATE MA_REGISTER");
            orgsql.AppendLine("     SET");
            orgsql.AppendLine(" WAITFLG = NULL");
            orgsql.AppendFormat(" ,SERVICEENDDATE = '{0}'", servicesDate);
            orgsql.AppendLine("     WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);
            orgsql.AppendLine("     AND");
            orgsql.AppendLine(" WAITFLG = '1'");

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 取得服务到期时间
        /// </summary>
        public DataTable GetServicesEndDate(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select SERVICEENDDATE from MA_REGISTER");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 更新电子邮箱
        /// </summary>
        public bool SetEmail(String registId, String email)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" UPDATE MA_REGISTER");
            orgsql.AppendLine("     SET");
            orgsql.AppendFormat(" EMAIL = '{0}'", email);
            orgsql.AppendLine("     WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 更新会员服务到期日
        /// </summary>
        public bool SetServiceDate(String registId,String serviceDate)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" UPDATE MA_REGISTER");
            orgsql.AppendLine("     SET");
            orgsql.AppendFormat(" SERVICEENDDATE = '{0}'", serviceDate);
            orgsql.AppendLine("     WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 删除注册信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteRegister(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_REGISTER");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}