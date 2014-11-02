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
    public class MA_COMPANY : Seika.COO.DBA.DateBaseAccess
    {
        public MA_COMPANY(DBConnect sql)
            : base(sql)
        {
        }
        //取得公司信息
        public DataTable GetComInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.NAME");
            orgsql.AppendLine("         ,a.COUNTRYID ");
            orgsql.AppendLine("         ,a.PROVINCEID");
            orgsql.AppendLine("         ,a.CITYID");
            orgsql.AppendLine("         ,a.BOROUGHID");
            orgsql.AppendLine("         ,b.NAME as COUNTRYNAME");
            orgsql.AppendLine("         ,c.NAME as PROVINCENAME");
            orgsql.AppendLine("         ,d.NAME as CITYNAME");
            orgsql.AppendLine("         ,e.NAME as BOROUGHNAME");
            orgsql.AppendLine("  from  MA_COMPANY a");
            orgsql.AppendLine("  left outer join MA_ZONECLASS b");
            orgsql.AppendLine("  on a.COUNTRYID = b.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS c");
            orgsql.AppendLine("  on a.PROVINCEID = c.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS d");
            orgsql.AppendLine("  on a.CITYID = d.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS e");
            orgsql.AppendLine("  on a.BOROUGHID = e.MA_ORDERID");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  or");
            orgsql.AppendLine("       a.DELFLG IS NULL");
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       a.DELFLG <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司信息ID
        public DataTable GetComInfoId(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_COMPANYID from MA_COMPANY");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //更新公司信息
        public bool UpdateComInfo(String registId, String registName, String capital, String capitalunit, String countryId, String provinceId, String cityId, String boroughId,
            String characterId, String managemodeId, String registday, String artificialperson, String explain, String organ)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendFormat("set CAPITAL = '{0}'", capital);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    CAPITALUNIT = '{0}'", capitalunit);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    COUNTRYID = '{0}'", countryId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PROVINCEID = '{0}'", provinceId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    CITYID = '{0}'", cityId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    BOROUGHID = '{0}'", boroughId);

            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    CHARACTER = '{0}'", characterId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    MANAGEMODE = '{0}'", managemodeId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    REGISTDAY = '{0}'", registday);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ARTIFICIALPERSON = '{0}'", artificialperson);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    EXPLAIN = '{0}'", explain);
            //orgsql.AppendLine("  ,");
            //orgsql.AppendFormat("    ORGAN = '{0}'", organ);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    NAME = '{0}'", registName);

            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        public bool AddComInfo(String companyid, String registId, String passWord, String name, String capital,String capitalunit,
            String countryId, String provinceId, String cityId, String boroughId, 
            String charter, String linkman, String mobiletel, String attestation, String organId,
            String character, String managemode, String email, String registday, String styleId, 
            String artifictialperson, String explain,String delflg)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_COMPANY");
            orgsql.AppendLine("       (MA_COMPANYID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,PASSWORD");
            orgsql.AppendLine("       ,NAME");
            orgsql.AppendLine("       ,CAPITAL");
            orgsql.AppendLine("       ,CAPITALUNIT");
            orgsql.AppendLine("       ,COUNTRYID");
            orgsql.AppendLine("       ,PROVINCEID");
            orgsql.AppendLine("       ,CITYID");
            orgsql.AppendLine("       ,BOROUGHID");
            orgsql.AppendLine("       ,CHARTER");
            orgsql.AppendLine("       ,LINKMAN");
            orgsql.AppendLine("       ,MOBILETEL");
            orgsql.AppendLine("       ,ATTESTATION");
            orgsql.AppendLine("       ,ORGAN");
            orgsql.AppendLine("       ,CHARACTER");
            orgsql.AppendLine("       ,MANAGEMODE");
            orgsql.AppendLine("       ,EMAIL");
            orgsql.AppendLine("       ,REGISTDAY");
            orgsql.AppendLine("       ,ARTIFICIALPERSON");
            orgsql.AppendLine("       ,EXPLAIN");
            orgsql.AppendLine("       ,MA_STYLEID");
            orgsql.AppendLine("       ,DELFLG");
            //orgsql.AppendLine("       ,LOGOURL");
            orgsql.AppendLine("       ,CREATEDATE)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',GetDate())",
                companyid, registId, passWord, name, capital
                , capitalunit, countryId, provinceId, cityId
                , boroughId, charter, linkman, mobiletel, attestation
                , organId, character, managemode, email, registday
                , artifictialperson,explain, styleId, delflg);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public DataTable SearchCompanyCount(String name, String calling, String country,
                                String province, String city, String borough)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT ");
            orgsql.AppendLine("         COUNT(DISTINCT(a.MA_COMPANYID)) AS ROWNUM");
            orgsql.AppendLine("  FROM MA_COMPANY a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS b");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.COUNTRYID = b.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS c");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.PROVINCEID = c.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS d");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.CITYID = d.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS e");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.BOROUGHID = e.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_MANAGE f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_COMPANYID = f.MA_COMPANYID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        f.MA_PRODUCTCLASSID = g.MA_ORDERID");
            orgsql.AppendLine("  WHERE a.MA_COMPANYID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" AND a.NAME like '%{0}%'  ", name);
            }
            if (calling != "")
            {
                orgsql.AppendFormat(" AND (f.MA_PRODUCTCLASSID = '{0}' OR isnull(f.PATH,'') like '%{0}')  ", calling);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" AND a.COUNTRYID = '{0}'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" AND a.PROVINCEID = '{0}'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" AND a.CITYID = '{0}'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" AND a.BOROUGHID = '{0}'  ", borough);
            }
            orgsql.AppendLine(" AND a.DELFLG IS NULL  ");
            orgsql.AppendLine(" AND a.SAVEFLG IS NULL  ");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchCompany(String name, String calling, String country,
                                        String province, String city, String borough,int startIdx,int count)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" SELECT  ");
	        orgsql.AppendLine("     i.MA_COMPANYID ");
	        orgsql.AppendLine("    ,i.REGISTID ");
	        orgsql.AppendLine("    ,i.NAME ");
	        orgsql.AppendLine("    ,i.ATTESTATION ");
	        orgsql.AppendLine("    ,i.CHARACTER ");
	        orgsql.AppendLine("    ,i.MANAGEMODE ");
	        orgsql.AppendLine("    ,i.CREATEDATE ");
            orgsql.AppendLine("    ,i.LOGOURL ");
            orgsql.AppendLine("    ,i.CAPITAL ");
            orgsql.AppendLine("    ,i.CAPITALUNIT ");
            orgsql.AppendLine("    ,i.EXPLAIN ");
	        orgsql.AppendLine("    ,i.REGISTDAY ");
	        orgsql.AppendLine("    ,i.ADDRESS ");
            orgsql.AppendLine("    ,k.NAME CALLING");
	        orgsql.AppendLine("    ,i.ROWNUM ");
            orgsql.AppendLine("    ,CASE WHEN n.HTTP_SV_DOMAIN IS NULL THEN n.HTTP_SV_IP ELSE n.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" ( ");
            orgsql.AppendLine(" SELECT * ");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" (");
            orgsql.AppendLine("  SELECT ");
            orgsql.AppendLine("         a.MA_COMPANYID");
            orgsql.AppendLine("        ,a.REGISTID");
            orgsql.AppendLine("        ,a.NAME");
            orgsql.AppendLine("        ,a.ATTESTATION");
            orgsql.AppendLine("        ,a.CHARACTER");
            orgsql.AppendLine("        ,a.MANAGEMODE");
            orgsql.AppendLine("        ,a.CREATEDATE");
            orgsql.AppendLine("        ,a.LOGOURL");
            orgsql.AppendLine("        ,a.CAPITAL");
            orgsql.AppendLine("        ,a.EXPLAIN");
            orgsql.AppendLine("        ,o.NAME CAPITALUNIT");
            orgsql.AppendLine("        ,CONVERT(varchar(12), CONVERT(datetime , a.REGISTDAY, 112), 23) as REGISTDAY");
            orgsql.AppendLine("        ,RTRIM(b.NAME)+' '+RTRIM(c.NAME)+' '+RTRIM(d.NAME)+' '+RTRIM(e.NAME) as ADDRESS");
            orgsql.AppendLine("        ,ROW_NUMBER() OVER(ORDER BY a.CREATEDATE DESC) AS ROWNUM");
            orgsql.AppendLine("        ,a.DELFLG  ");
            orgsql.AppendLine("        ,a.SAVEFLG  ");
            orgsql.AppendLine("  FROM MA_COMPANY a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS b");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.COUNTRYID = b.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS c");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.PROVINCEID = c.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS d");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.CITYID = d.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS e");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.BOROUGHID = e.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_MANAGE f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_COMPANYID = f.MA_COMPANYID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        f.MA_PRODUCTCLASSID = g.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE o");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        o.MA_CODEID = a.CAPITALUNIT");
            orgsql.AppendLine("  WHERE a.MA_COMPANYID <> ''");
            orgsql.AppendLine("  AND   a.DELFLG IS NULL");
            orgsql.AppendLine("  AND   a.SAVEFLG IS NULL");
            if (name != "")
            {
                orgsql.AppendFormat(" AND a.NAME like '%{0}%'  ", name);
            }
            if (calling != "")
            {
                orgsql.AppendFormat(" AND (f.MA_PRODUCTCLASSID = '{0}' OR isnull(f.PATH,'') like '%{0}')  ", calling);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" AND a.COUNTRYID = '{0}'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" AND a.PROVINCEID = '{0}'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" AND a.CITYID = '{0}'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" AND a.BOROUGHID = '{0}'  ", borough);
            }
            orgsql.AppendLine("GROUP BY");
	        orgsql.AppendLine("        a.MA_COMPANYID");
	        orgsql.AppendLine("       ,a.REGISTID");
	        orgsql.AppendLine("       ,a.NAME");
	        orgsql.AppendLine("       ,a.ATTESTATION");
	        orgsql.AppendLine("       ,a.CHARACTER");
	        orgsql.AppendLine("       ,a.MANAGEMODE");
	        orgsql.AppendLine("       ,a.CREATEDATE");
            orgsql.AppendLine("       ,a.LOGOURL");
            orgsql.AppendLine("       ,a.CAPITAL");
            orgsql.AppendLine("       ,a.EXPLAIN");
            orgsql.AppendLine("       ,o.NAME");
	        orgsql.AppendLine("       ,REGISTDAY");
	        orgsql.AppendLine("       ,b.NAME");
	        orgsql.AppendLine("       ,c.NAME");
	        orgsql.AppendLine("       ,d.NAME");
            orgsql.AppendLine("       ,e.NAME");
            orgsql.AppendLine("       ,a.DELFLG");
            orgsql.AppendLine("       ,a.SAVEFLG");
            orgsql.AppendLine(" ) AS h");
            orgsql.AppendFormat(" WHERE ROWNUM BETWEEN {0} AND {1}", startIdx, (startIdx + count - 1));
            orgsql.AppendLine(") AS i");
            orgsql.AppendLine("LEFT OUTER JOIN ");
            orgsql.AppendLine("       BS_MANAGE j");
            orgsql.AppendLine(" ON ");
            orgsql.AppendLine("       i.MA_COMPANYID = j.MA_COMPANYID");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       MA_CALLINGCLASS k");
            orgsql.AppendLine(" ON ");
            orgsql.AppendLine("       j.MA_PRODUCTCLASSID = k.MA_ORDERID");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER m ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       m.REGISTID = i.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER n ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      n.SV_SERVERID = m.SV_SERVERID ");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司信息
        public DataTable GetComInfo(String registId, String password)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.MA_COMPANYID");
            orgsql.AppendLine("         ,b.XSLNAME");
            orgsql.AppendLine("         ,b.CSSNAME");
            orgsql.AppendLine("  FROM MA_COMPANY a");
            orgsql.AppendLine("  LEFT OUTER JOIN MA_STYLE b");
            orgsql.AppendLine("  ON");
            orgsql.AppendLine("       a.MA_STYLEID = b.MA_STYLEID");
            orgsql.AppendFormat("WHERE a.REGISTID = '{0}'", registId);
            //orgsql.AppendLine("  AND");
            //orgsql.AppendLine("       DELFLG IS NULL");
            orgsql.AppendFormat(" AND   CAST(a.PASSWORD AS varbinary) = CAST('{0}' AS varbinary)", password);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //更新公司信息
        public bool UpdateComInfo(String registId, String ip)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendFormat("set SUCCEEDIP = '{0}'", ip);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    SUCCEEDDATETIME = getdate()");
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ERRNUM = 0");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新公司信息
        public bool UpdateLoginErrComInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendFormat("set ERRDATETIME = getdate()");
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ERRNUM = ERRNUM + 1");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新公司信息
        public bool UpdateNullErrComInfo(String registId)
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

        //更新公司错误信息
        public DataTable GetErrComInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_COMPANYID,ERRDATETIME,ERRNUM from MA_COMPANY");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        public DataTable GetCompanyAllInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select *");
            orgsql.AppendLine("  from  MA_COMPANY");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            orgsql.AppendLine("  or");
            orgsql.AppendLine("       DELFLG IS NULL");
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       DELFLG <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 更新公司注册信息
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="linkman"></param>
        /// <param name="mobiletile"></param>
        /// <param name="charter"></param>
        /// <returns></returns>
        public bool UpdataComInfo(String registId, String linkman, String mobiletile, String charter)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendFormat(" set LINKMAN = '{0}'", linkman);
            orgsql.AppendFormat("    ,MOBILETEL = '{0}'", mobiletile);
            orgsql.AppendFormat("    ,CHARTERURL= '{0}'", charter);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        ///// <summary>
        ///// 保存公司风格
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool UpdateCompanyStyle(String name, String id)
        //{
        //    StringBuilder orgsql = new StringBuilder();
        //    orgsql.AppendLine("  update MA_COMPANY");
        //    orgsql.AppendLine("  set");
        //    orgsql.AppendFormat("  STYLECODE = '{0}'", name);
        //    orgsql.AppendLine("  where");
        //    orgsql.AppendFormat("  REGISTID = '{0}'", id);

        //    return sql.ExecuteStrQuery(orgsql.ToString());
        //}


        /// <summary>
        /// 保存公司徽标
        /// </summary>
        /// <param name="logo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateCompanyLogo(String logo, String id)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendLine("  set");
            orgsql.AppendFormat("  LOGOURL = '{0}'", logo);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  REGISTID = '{0}'", id);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 取得公司注册帐号与邮箱
        /// </summary>
        /// <param name="registid"></param>
        /// <returns></returns>
        public DataTable GetComRegistid(String registid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select REGISTID,EMAIL,PASSWORD from MA_COMPANY");
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
        public bool UpdateCompanyPassword(String registid, String password)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMPANY");
            orgsql.AppendLine("  set");
            orgsql.AppendFormat("  PASSWORD = '{0}'", password);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  REGISTID = '{0}'", registid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 更新VIP状态
        /// </summary>
        /// <param name="registid"></param>
        /// <param name="vipSta"></param>
        /// <returns></returns>
        public bool UpdateServiceVip(String registid,String vipSta)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  UPDATE MA_COMPANY");
            orgsql.AppendLine("  SET");
            orgsql.AppendFormat("  ATTESTATION = '{0}'", vipSta);
            orgsql.AppendLine("  WHERE");
            orgsql.AppendFormat("  REGISTID = '{0}'", registid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public DataTable GetComAttestation(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select Attestation from MA_COMPANY");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("REGISTID = '{0}'", registId);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        public DataTable GetComActivationcode(String ACTIVATIONCODE)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_STRUCTUREID,ACTIVATIONCODE from MA_STRUCTURE_1");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("ACTIVATIONCODE = '{0}'", ACTIVATIONCODE);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable GetComDeleteflg(String MA_STRUCTUREID)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select Deleteflg from MA_STRUCTURE");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat(" MA_STRUCTUREID = '{0}'", MA_STRUCTUREID);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        public DataTable GetRegistid(String Registid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select Registid from ma_company");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("Registid = '{0}'", Registid);

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //更新激活信息
        public bool UpdataDeleteflg(String MA_STRUCTUREID)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_STRUCTURE");
            orgsql.AppendFormat("set DELETEFLG = '1'");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    MA_STRUCTUREID = '{0}'", MA_STRUCTUREID);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        public bool AddInsertdb(String companyid, String registId, String passWord, String name, String question,
               String answer, String countryId, String provinceId, String cityId, String boroughId,
               String charter, String linkman, String mobiletel, String attestation, String organId,
               String character, String managemode, String email, String registday, String artifictialperson)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_COMPANY");
            orgsql.AppendLine("       (MA_COMPANYID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,PASSWORD");
            orgsql.AppendLine("       ,NAME");
            orgsql.AppendLine("       ,CLEWQUESTION");
            orgsql.AppendLine("       ,CLEWANSWER");
            orgsql.AppendLine("       ,COUNTRYID");
            orgsql.AppendLine("       ,PROVINCEID");
            orgsql.AppendLine("       ,CITYID");
            orgsql.AppendLine("       ,BOROUGHID");
            orgsql.AppendLine("       ,CHARTER");
            orgsql.AppendLine("       ,LINKMAN");
            orgsql.AppendLine("       ,MOBILETEL");
            orgsql.AppendLine("       ,ATTESTATION");
            orgsql.AppendLine("       ,ORGAN");
            orgsql.AppendLine("       ,CHARACTER");
            orgsql.AppendLine("       ,MANAGEMODE");
            orgsql.AppendLine("       ,EMAIL");
            orgsql.AppendLine("       ,REGISTDAY");
            orgsql.AppendLine("       ,ARTIFICIALPERSON");
            orgsql.AppendLine("       ,CREATEDATE)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',GetDate())", companyid, registId, passWord, name, question, answer, countryId, provinceId, cityId, boroughId, charter, linkman, mobiletel, attestation, organId, character, managemode, email, registday, artifictialperson);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        /// <summary>
        /// 降序取得公司一览数据
        /// </summary>
        /// <param name="startIdx">取得开始行号</param>
        /// <param name="count">取得件数</param>
        /// <returns></returns>
        public DataTable GetCompanyList(int startIdx, int count)
        {
            StringBuilder cmd = new StringBuilder();

            cmd.AppendLine(" SELECT * ");
            cmd.AppendLine(" FROM ");
            cmd.AppendLine(" (");
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	     a.MA_COMPANYID");
            cmd.AppendLine(" 	    ,a.REGISTID");
            cmd.AppendLine(" 	    ,a.NAME");
            cmd.AppendLine(" 	    ,a.ATTESTATION");
            cmd.AppendLine(" 	    ,a.CHARACTER");
            cmd.AppendLine(" 	    ,a.MANAGEMODE");
            cmd.AppendLine(" 	    ,a.EXPLAIN");
            cmd.AppendLine(" 	    ,h.NAME AS PROVINCE");
            cmd.AppendLine(" 	    ,i.NAME AS CITY");
            cmd.AppendLine(" 	    ,CONVERT(VARCHAR(10),A.CREATEDATE,120) AS CREATEDATE");
            cmd.AppendLine(" 	    ,CONVERT(VARCHAR(12), CONVERT(DATETIME , a.REGISTDAY, 112), 23) AS REGISTDAY");
            cmd.AppendLine(" 	    ,RTRIM(b.NAME) + ' ' + RTRIM(c.NAME) + ' ' + RTRIM(d.NAME) + ' ' + RTRIM(e.NAME) AS ADDRESS");
            cmd.AppendLine(" 	    ,f.SV_SERVERID");
            cmd.AppendLine(" 	    ,CASE WHEN g.HTTP_SV_DOMAIN IS NULL THEN g.HTTP_SV_IP ELSE g.HTTP_SV_DOMAIN END AS SERVERHOST");
            cmd.AppendLine("        ,ROW_NUMBER() OVER(ORDER BY a.MA_COMPANYID DESC) AS ROWNUM");
            cmd.AppendLine("        ,a.DELFLG");
            cmd.AppendLine("    FROM ");
            cmd.AppendLine("        MA_COMPANY a");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine(" 	        MA_ZONECLASS b");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine(" 	        a.COUNTRYID = b.MA_ORDERID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             MA_ZONECLASS c");
            cmd.AppendLine(" 	    ON");
            cmd.AppendLine("             a.PROVINCEID = c.MA_ORDERID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             MA_ZONECLASS d");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             a.CITYID = d.MA_ORDERID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             MA_ZONECLASS e");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             a.BOROUGHID = e.MA_ORDERID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             SV_BSSERVER f");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             a.REGISTID = f.REGISTID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             SV_SERVER g");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             f.SV_SERVERID = g.SV_SERVERID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             MA_ZONECLASS h");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             h.MA_ORDERID = a.PROVINCEID");
            cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            cmd.AppendLine("             MA_ZONECLASS i");
            cmd.AppendLine(" 	    ON ");
            cmd.AppendLine("             i.MA_ORDERID = a.CITYID");
            //cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            //cmd.AppendLine("             MA_CALLINGCLASS h");
            //cmd.AppendLine(" 	    ON ");
            //cmd.AppendLine("             h.MA_ORDERID = a.SV_SERVERID");
            cmd.AppendLine(" WHERE a.DELFLG IS NULL");
            cmd.AppendLine(" AND a.SAVEFLG IS NULL");
            cmd.AppendLine(" ) AS t");
            cmd.AppendLine(" WHERE ROWNUM BETWEEN {0} AND {1}");
            cmd.AppendLine(" ORDER BY MA_COMPANYID DESC");

            String sqlcmd = String.Format(cmd.ToString(), startIdx, (startIdx + count - 1));
            return sql.ExecuteDataset (sqlcmd).Tables[0];
        }
        /// <summary>
        /// 取得公司登录密码
        /// </summary>
        public DataTable GetCompanyPassWord(String registId)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    PASSWORD ");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_COMPANY ");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" REGISTID = '{0}'", registId);
            cmd.AppendLine(" AND SAVEFLG IS NULL");

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
            //.Rows[0]["PASSWORD"]
        }
        /// <summary>
        /// 修改公司登录密码
        /// </summary>
        public bool SetCompanyPassWord(String registId, String newpassword)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("    UPDATE MA_COMPANY");
            orgsql.AppendLine("    SET");
            orgsql.AppendFormat(" PASSWORD = '{0}'", newpassword);
            orgsql.AppendLine(" WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
         /// <summary>
        /// 取得问题类型信息
        /// </summary>
        public DataTable GetStyle(String ltype)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    MA_CODEID");
            cmd.AppendLine(" 	   ,NAME");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_CODE ");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" LTYPE  = '{0}'", ltype);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        public bool AddCusInfo(String MA_CUSTOMMAILID, String typeid, String title, String depict, String name, String email, String state, String img1, String img2)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_CUSTOMMAIL");
            orgsql.AppendLine("       (MA_CUSTOMMAILID");
            orgsql.AppendLine("       ,TYPEID");
            orgsql.AppendLine("       ,TITLE");
            orgsql.AppendLine("       ,DETAIL");
            orgsql.AppendLine("       ,IMG1");
            orgsql.AppendLine("       ,IMG2");
            orgsql.AppendLine("       ,RENAME");
            orgsql.AppendLine("       ,REMAIL");
            orgsql.AppendLine("       ,STATE");
            orgsql.AppendLine("       ,CREATEDATE)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',GetDate())",
                MA_CUSTOMMAILID,typeid, title, depict,img1,img2, name, email, state);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }

}