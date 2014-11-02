using System;
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
    public class MA_COMINFO : Seika.COO.DBA.DateBaseAccess
    {
        public MA_COMINFO(DBConnect sql)
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
            orgsql.AppendLine("  from  MA_COMINFO a");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       a.DELFLG IS NULL");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        //取得公司信息
        public DataTable GetComInfo(String registId,String lang)
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
            orgsql.AppendLine("         ,a.LOGOURL");
            orgsql.AppendLine("         ,a.OTHERMANAGE");
            orgsql.AppendLine("         ,a.ARTIFICIALPERSON");
            orgsql.AppendLine("         ,a.LINKMAN");
            orgsql.AppendLine("         ,a.MOBILETEL");
            orgsql.AppendLine("         ,a.EXPLAIN");

            orgsql.AppendLine("         ,f.EMAIL");
            orgsql.AppendLine("         ,a.PHONE_COUNTRY");
            orgsql.AppendLine("         ,a.PHONE_ZONE");
            orgsql.AppendLine("         ,a.PHONE_CODE");
            orgsql.AppendLine("         ,a.PHONE_EXT");
            orgsql.AppendLine("         ,a.FAX_COUNTRY");
            orgsql.AppendLine("         ,a.FAX_ZONE");
            orgsql.AppendLine("         ,a.FAX_CODE");
            orgsql.AppendLine("         ,a.FAX_EXT");

            orgsql.AppendLine("         ,a.POSTCODE");
            orgsql.AppendLine("         ,a.ADDRESS");

            orgsql.AppendLine("  from  MA_COMINFO a");
            orgsql.AppendLine("  left outer join MA_ZONECLASS b");
            orgsql.AppendLine("  on a.COUNTRYID = b.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS c");
            orgsql.AppendLine("  on a.PROVINCEID = c.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS d");
            orgsql.AppendLine("  on a.CITYID = d.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS e");
            orgsql.AppendLine("  on a.BOROUGHID = e.MA_ORDERID");

            orgsql.AppendLine("  left outer join MA_REGISTER f");
            orgsql.AppendLine("  on a.MA_REGISTERID = f.MA_REGISTERID");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       isnull(a.DELFLG,'') <> 1");
            //orgsql.AppendLine("  and");
            //orgsql.AppendFormat("     a.LANG = '{0}'", lang);
            //orgsql.AppendLine("  or");
            //orgsql.AppendLine("       a.DELFLG IS NULL");
            //orgsql.AppendLine("  and");
            //orgsql.AppendLine("       a.DELFLG <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司信息
        public DataTable GetComTitle(String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select top 10 a.NAME ,a.REGISTID");
            orgsql.AppendLine("    ,CASE WHEN n.HTTP_SV_DOMAIN IS NULL THEN n.HTTP_SV_IP ELSE n.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine("  from MA_COMINFO a");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER m ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       m.REGISTID = a.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER n ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      n.SV_SERVERID = m.SV_SERVERID ");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER h");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.MA_REGISTERID = a.MA_REGISTERID");
            orgsql.AppendLine("  LEFT OUTER JOIN");
		    orgsql.AppendLine("         BS_WEBLANG i");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = i.REGISTID ");
            orgsql.AppendLine("  WHERE a.MA_COMINFOID <> ''");
            orgsql.AppendLine("  AND   a.DELFLG IS NULL");
            orgsql.AppendLine("  AND   a.SAVEFLG IS NULL");
            orgsql.AppendLine("  AND   a.TOPORDER IS NOT NULL");
            orgsql.AppendLine("  AND datediff(day,getdate(),h.SERVICEENDDATE) > 0");
            orgsql.AppendFormat("  AND   a.COUNTRYID = '{0}'", langId);
            orgsql.AppendFormat("  AND   i.MA_LANGUAGEID = '{0}'", langId);
            orgsql.AppendLine(" ORDER BY a.TOPORDER DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司信息ID
        public DataTable GetComInfoId(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select MA_COMINFOID from MA_COMINFO");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //更新公司信息
        public bool UpdateComInfo(String registId, String registName,  String countryId, String provinceId, String cityId, String boroughId,
             String artificialperson, String explain, String otherManage, String linkMan, String mobiletel, String phone_1, String phone_2,
            String phone_3, String phone_4, String fax_1, String fax_2, String fax_3, String fax_4, String postcode, String address)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMINFO");
            //orgsql.AppendFormat("set CAPITAL = '{0}'", capital);
            //orgsql.AppendLine("  ,");
            //orgsql.AppendFormat("    CAPITALUNIT = '{0}'", capitalunit);
            //orgsql.AppendLine("  ,");
            orgsql.AppendFormat("set OTHERMANAGE = '{0}'", otherManage);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    COUNTRYID = '{0}'", countryId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PROVINCEID = '{0}'", provinceId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    CITYID = '{0}'", cityId);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    BOROUGHID = '{0}'", boroughId);

            orgsql.AppendLine("  ,");
            //orgsql.AppendFormat("    CHARACTER = '{0}'", characterId);
            //orgsql.AppendLine("  ,");
            //orgsql.AppendFormat("    MANAGEMODE = '{0}'", managemodeId);
            //orgsql.AppendLine("  ,");
            //orgsql.AppendFormat("    REGISTDAY = '{0}'", registday);
            //orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ARTIFICIALPERSON = '{0}'", artificialperson);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    EXPLAIN = '{0}'", explain);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    LINKMAN = '{0}'", linkMan);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    MOBILETEL = '{0}'", mobiletel);
            orgsql.AppendLine("  ,");
            //orgsql.AppendLine("    SAVEFLG = NULL ");
            //orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    NAME = '{0}'", registName);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PHONE_COUNTRY = '{0}'", phone_1);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PHONE_ZONE = '{0}'", phone_2);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PHONE_CODE = '{0}'", phone_3);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    PHONE_EXT = '{0}'", phone_4);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    FAX_COUNTRY = '{0}'", fax_1);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    FAX_ZONE = '{0}'", fax_2);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    FAX_CODE = '{0}'", fax_3);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    FAX_EXT = '{0}'", fax_4);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    POSTCODE = '{0}'", postcode);
            orgsql.AppendLine("  ,");
            orgsql.AppendFormat("    ADDRESS = '{0}'", address);

            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            //orgsql.AppendFormat(" and    Lang = '{0}'", lang);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public bool AddComInfo(String cominfoid, String ma_registerId,  String rigistId,  String name
                ,  String countryId,  String provinceId,  String cityId,  String boroughId
                ,  String otherManage,  String linkMan
                ,  String phoneCountry,  String phoneZone,  String phoneCode,  String phoneExt
                ,  String faxCountry,  String faxZone,  String faxCode,  String faxExt
                ,  String mobiletel, String attestation,String saveFlg ,String postcode, String detailsAddress)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_COMINFO");
            orgsql.AppendLine("       (MA_COMINFOID");
            orgsql.AppendLine("       ,MA_REGISTERID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,NAME");

            orgsql.AppendLine("       ,COUNTRYID");
            orgsql.AppendLine("       ,PROVINCEID");
            orgsql.AppendLine("       ,CITYID");
            orgsql.AppendLine("       ,BOROUGHID");

            orgsql.AppendLine("       ,OTHERMANAGE");
            orgsql.AppendLine("       ,LINKMAN");

            orgsql.AppendLine("       ,PHONE_COUNTRY");
            orgsql.AppendLine("       ,PHONE_ZONE");
            orgsql.AppendLine("       ,PHONE_CODE");
            orgsql.AppendLine("       ,PHONE_EXT");

            orgsql.AppendLine("       ,FAX_COUNTRY");
            orgsql.AppendLine("       ,FAX_ZONE");
            orgsql.AppendLine("       ,FAX_CODE");
            orgsql.AppendLine("       ,FAX_EXT");

            orgsql.AppendLine("       ,MOBILETEL");
            orgsql.AppendLine("       ,ATTESTATION");

            orgsql.AppendLine("       ,SAVEFLG");
            orgsql.AppendLine("       ,POSTCODE");
            orgsql.AppendLine("       ,ADDRESS");
            orgsql.AppendLine("       ,CREATEDATE)");
           
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20},'{21}','{22}',GetDate())",
                 cominfoid,  ma_registerId,   rigistId,   name
                ,   countryId,   provinceId,   cityId,   boroughId
                ,   otherManage,   linkMan
                ,   phoneCountry,   phoneZone,   phoneCode,   phoneExt
                ,   faxCountry,   faxZone,   faxCode,   faxExt
                ,   mobiletel,  attestation, String.IsNullOrEmpty(saveFlg) ? "NULL" : "'"+saveFlg+"'", postcode, detailsAddress);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public DataTable SearchCompanyCount(String name, String calling, String country,
                                String province, String city, String borough,String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT ");
            orgsql.AppendLine("         COUNT(DISTINCT(a.MA_COMINFOID)) AS ROWNUM");
            orgsql.AppendLine("  FROM MA_COMINFO a");
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
            orgsql.AppendLine("        a.MA_COMINFOID = f.MA_COMINFOID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        f.MA_PRODUCTCLASSID = g.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER h");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.MA_REGISTERID = a.MA_REGISTERID");
            orgsql.AppendLine("  LEFT OUTER JOIN");
		    orgsql.AppendLine("        BS_WEBLANG i");
            orgsql.AppendLine("  ON	   a.REGISTID = i.REGISTID");

            orgsql.AppendLine("  WHERE a.MA_COMINFOID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" AND a.NAME like '%{0}%'  ", name);
            }
            if (calling != "")
            {
                orgsql.AppendFormat(" AND (f.MA_PRODUCTCLASSID = '{0}' OR isnull(f.PATH,'') like '%{0}%')  ", calling);
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
            if (langId != "")
            {
                orgsql.AppendFormat(" AND a.COUNTRYID = '{0}'  ",langId);
            }
            orgsql.AppendLine(" AND a.DELFLG IS NULL  ");
            orgsql.AppendLine(" AND a.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),h.SERVICEENDDATE) > 0");
            orgsql.AppendLine(" AND isnull(h.WAITFLG,'') <> '1'  ");
            orgsql.AppendFormat("   AND   i.MA_LANGUAGEID = '{0}' ", langId);
            //orgsql.AppendFormat(" AND a.LANG = '{0}' ", langId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchCompany(String name, String calling, String country,
                                        String province, String city, String borough,
                                        String langId,int startIdx,int count)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" SELECT  ");
	        orgsql.AppendLine("     i.MA_COMINFOID ");
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
            orgsql.AppendLine("    ,i.OTHERMANAGE");
            orgsql.AppendLine("    ,CASE WHEN n.HTTP_SV_DOMAIN IS NULL THEN n.HTTP_SV_IP ELSE n.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" ( ");
            orgsql.AppendLine(" SELECT * ");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" (");
            orgsql.AppendLine("  SELECT ");
            orgsql.AppendLine("         a.MA_COMINFOID");
            orgsql.AppendLine("        ,a.REGISTID");
            orgsql.AppendLine("        ,a.NAME");
            orgsql.AppendLine("        ,a.ATTESTATION");
            orgsql.AppendLine("        ,a.CHARACTER");
            orgsql.AppendLine("        ,a.MANAGEMODE");
            orgsql.AppendLine("        ,a.CREATEDATE");
            orgsql.AppendLine("        ,a.LOGOURL");
            orgsql.AppendLine("        ,a.CAPITAL");
            orgsql.AppendLine("        ,a.EXPLAIN");
            orgsql.AppendLine("        ,a.TOPORDER");
            orgsql.AppendLine("    ,a.OTHERMANAGE");
            orgsql.AppendLine("        ,o.NAME CAPITALUNIT");
            orgsql.AppendLine("        ,CONVERT(varchar(12), CONVERT(datetime , a.REGISTDAY, 112), 23) as REGISTDAY");
            orgsql.AppendLine("        ,RTRIM(b.NAME)+' '+RTRIM(c.NAME)+' '+RTRIM(d.NAME)+' '+RTRIM(e.NAME) as ADDRESS");
            orgsql.AppendLine("        ,ROW_NUMBER() OVER(ORDER BY TOPORDER DESC,a.CREATEDATE DESC) AS ROWNUM");
            orgsql.AppendLine("        ,a.DELFLG  ");
            orgsql.AppendLine("        ,a.SAVEFLG  ");
            orgsql.AppendLine("  FROM MA_COMINFO a");
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
            orgsql.AppendLine("        a.MA_COMINFOID = f.MA_COMINFOID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        f.MA_PRODUCTCLASSID = g.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE o");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        o.MA_CODEID = a.CAPITALUNIT");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER z");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        z.MA_REGISTERID = a.MA_REGISTERID");
            orgsql.AppendLine("  LEFT OUTER JOIN");
		    orgsql.AppendLine("        BS_WEBLANG k");
            orgsql.AppendLine("  ON	  a.REGISTID = k.REGISTID");
            orgsql.AppendLine("  WHERE a.MA_COMINFOID <> ''");
            orgsql.AppendLine("  AND   a.DELFLG IS NULL");
            orgsql.AppendLine("  AND   a.SAVEFLG IS NULL");
            orgsql.AppendLine(" AND datediff(day,getdate(),z.SERVICEENDDATE) > 0");
            orgsql.AppendLine(" AND isnull(z.WAITFLG,'') <> '1'  ");
            //orgsql.AppendFormat("  AND   a.LANG = '{0}'", langId);
            if (name != "")
            {
                orgsql.AppendFormat(" AND a.NAME like '%{0}%'  ", name);
            }
            if (calling != "")
            {
                orgsql.AppendFormat(" AND (f.MA_PRODUCTCLASSID = '{0}' OR isnull(f.PATH,'') like '%{0}%')  ", calling);
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
            if (langId != "")
            {
                orgsql.AppendFormat(" AND a.COUNTRYID = '{0}'  ", langId);
            }
            orgsql.AppendFormat("   AND   k.MA_LANGUAGEID = '{0}'", langId);

            

            orgsql.AppendLine("GROUP BY");
            orgsql.AppendLine("        a.TOPORDER");
	        orgsql.AppendLine("        ,a.MA_COMINFOID");
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
            orgsql.AppendLine("    ,a.OTHERMANAGE");
            orgsql.AppendLine(" ) AS h");
            orgsql.AppendFormat(" WHERE ROWNUM BETWEEN {0} AND {1}", startIdx, (startIdx + count - 1));
            orgsql.AppendLine(") AS i");
            orgsql.AppendLine("LEFT OUTER JOIN ");
            orgsql.AppendLine("       BS_MANAGE j");
            orgsql.AppendLine(" ON ");
            orgsql.AppendLine("       i.MA_COMINFOID = j.MA_COMINFOID");
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

            orgsql.AppendLine("  order by TOPORDER DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        ////取得公司样式
        //public DataTable GetComInfoStyle(String registId, String lang)
        //{
        //    StringBuilder orgsql = new StringBuilder();
        //    orgsql.AppendLine("  SELECT a.MA_COMINFOID");
        //    orgsql.AppendLine("         ,b.XSLNAME");
        //    orgsql.AppendLine("         ,b.CSSNAME");
        //    orgsql.AppendLine("  FROM MA_COMINFO a");
        //    orgsql.AppendLine("  LEFT OUTER JOIN MA_STYLE b");
        //    orgsql.AppendLine("  ON");
        //    orgsql.AppendLine("       a.MA_STYLEID = b.MA_STYLEID");
        //    orgsql.AppendFormat("WHERE  a.REGISTID = '{0}'", registId);
        //    //orgsql.AppendLine("  AND");
        //    //orgsql.AppendLine("       DELFLG IS NULL");
        //    orgsql.AppendFormat(" AND   a.LANG = '{0}'", lang);

        //    return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        //}

        public DataTable GetCompanyAllInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select *");
            orgsql.AppendLine("  from  MA_COMINFO");
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
            orgsql.AppendLine("  update MA_COMINFO");
            orgsql.AppendFormat(" set LINKMAN = '{0}'", linkman);
            orgsql.AppendFormat("    ,MOBILETEL = '{0}'", mobiletile);
            orgsql.AppendFormat("    ,CHARTERURL= '{0}'", charter);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //更新公司网站
        public bool UpdataComInfoOpenflg(String registId,String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMINFO");
            orgsql.AppendLine(" set OPENFLG = '1'");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    REGISTID = '{0}'", registId);
            orgsql.AppendFormat("   and LANG = '{0}'", lang);
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
        //    orgsql.AppendLine("  update MA_COMINFO");
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
            orgsql.AppendLine("  update MA_COMINFO");
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
        public bool UpdateCompanyPassword(String registid, String password)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_COMINFO");
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
            orgsql.AppendLine("  UPDATE MA_COMINFO");
            orgsql.AppendLine("  SET");
            orgsql.AppendFormat("  ATTESTATION = '{0}'", vipSta);
            orgsql.AppendLine("  WHERE");
            orgsql.AppendFormat("  REGISTID = '{0}'", registid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public DataTable GetComAttestation(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select Attestation from MA_COMINFO");
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
            orgsql.AppendLine("  select Registid from MA_COMINFO");
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


        public bool AddInsertdb(String cominfoid, String registId, String passWord, String name, String question,
               String answer, String countryId, String provinceId, String cityId, String boroughId,
               String charter, String linkman, String mobiletel, String attestation, String organId,
               String character, String managemode, String email, String registday, String artifictialperson)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_COMINFO");
            orgsql.AppendLine("       (MA_COMINFOID");
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
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',GetDate())", cominfoid, registId, passWord, name, question, answer, countryId, provinceId, cityId, boroughId, charter, linkman, mobiletel, attestation, organId, character, managemode, email, registday, artifictialperson);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        /// <summary>
        /// 降序取得公司一览数据
        /// </summary>
        /// <param name="startIdx">取得开始行号</param>
        /// <param name="count">取得件数</param>
        /// <returns></returns>
        public DataTable GetCompanyList(int startIdx, int count,String langId)
        {
            StringBuilder cmd = new StringBuilder();

            cmd.AppendLine(" SELECT * ");
            cmd.AppendLine(" FROM ");
            cmd.AppendLine(" (");
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	     a.MA_COMINFOID");
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
            cmd.AppendLine("        ,ROW_NUMBER() OVER(ORDER BY a.MA_COMINFOID DESC) AS ROWNUM");
            cmd.AppendLine("        ,a.DELFLG");
            cmd.AppendLine("        ,a.LANG");
            cmd.AppendLine("    FROM ");
            cmd.AppendLine("        MA_COMINFO a");
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
            //cmd.AppendLine("             MA_REGISTER j");
            //cmd.AppendLine(" 	    ON ");
            //cmd.AppendLine("             j.MA_REGISTERID = a.MA_REGISTERID");
            //cmd.AppendLine(" 	    LEFT OUTER JOIN ");
            //cmd.AppendLine("             MA_CALLINGCLASS h");
            //cmd.AppendLine(" 	    ON ");
            //cmd.AppendLine("             h.MA_ORDERID = a.SV_SERVERID");
            cmd.AppendLine(" WHERE a.DELFLG IS NULL");
            cmd.AppendLine(" AND a.SAVEFLG IS NULL");
            cmd.AppendLine(" ) AS t");
            cmd.AppendLine(" WHERE ROWNUM BETWEEN {0} AND {1}");
            cmd.AppendFormat(" AND LANG = '{0}'", langId);
            cmd.AppendLine(" ORDER BY MA_COMINFOID DESC");

            String sqlcmd = String.Format(cmd.ToString(), startIdx, (startIdx + count - 1));
            return sql.ExecuteDataset (sqlcmd).Tables[0];
        }

        /// <summary>
        /// 修改公司登录密码
        /// </summary>
        public bool SetCompanyPassWord(String registId, String newpassword)
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
        /// 取得问题类型信息
        /// </summary>
        public DataTable GetStyle(String ltype)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine("    SELECT ");
            cmd.AppendLine(" 	    MA_CODEID");
            cmd.AppendLine(" 	   ,RESID");
            cmd.AppendLine(" 	   ,NAME");
            cmd.AppendLine("    FROM");
            cmd.AppendLine("        MA_CODE ");
            cmd.AppendLine(" WHERE");
            cmd.AppendFormat(" LTYPE  = '{0}'", ltype);

            return sql.ExecuteDataset(cmd.ToString()).Tables[0];
        }
        public bool AddCusInfo(String ma_custommailid, String typeid, String title, String depict, String name, String email, String state, String img1, String img2)
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
                ma_custommailid,typeid, title, depict,img1,img2, name, email, state);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //取得公司信息地址信息
        public DataTable GetComInfoAddress(String registId, String lang)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.COUNTRYID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            orgsql.AppendLine("   UNION ALL");

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.PROVINCEID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            orgsql.AppendLine("   UNION ALL");

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.CITYID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            orgsql.AppendLine("   UNION ALL");

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.BOROUGHID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            return sql.ExecuteDataset(String.Format(orgsql.ToString(), registId, lang)).Tables[0];
        }

        //取得公司信息地址信息
        public DataTable GetComInfoAddressCity(String registId, String lang)
        {
            StringBuilder orgsql = new StringBuilder();

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.COUNTRYID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            orgsql.AppendLine("   UNION ALL");

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.PROVINCEID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            orgsql.AppendLine("   UNION ALL");

            orgsql.AppendLine("  SELECT a.MA_ORDERID,a.[NAME] ");
            orgsql.AppendLine("         FROM MA_ZONECLASS a ");
            orgsql.AppendLine("         LEFT OUTER JOIN ");
            orgsql.AppendLine("              MA_COMINFO b ");
            orgsql.AppendLine("         ON ");
            orgsql.AppendLine("              a.MA_ORDERID=b.CITYID");
            orgsql.AppendLine("   WHERE b.REGISTID='{0}'");
            orgsql.AppendLine("         AND b.LANG='{1}' ");
            orgsql.AppendLine("         AND DELFLG IS NULL");
            orgsql.AppendLine("         AND SAVEFLG IS NULL");

            return sql.ExecuteDataset(String.Format(orgsql.ToString(), registId, lang)).Tables[0];
        }


        //取得企业信息
        public DataTable GetEnterprisesInfo(String registId, String name, String phone, String email, String linkMan,
                                            String artificialPerson, String regBeginDay, String regEndDay, String expBeginDay,
                                            String expEndDay, String servicesDate,String servicesMonth, String countryId, 
                                            String provinceId, String cityId, String boroughId,String callingId, String langId,
                                            String payState)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT a.REGISTID ");  //会员ID
            orgsql.AppendLine("         ,a.MA_COMINFOID");
            orgsql.AppendLine("         ,f.PASSWORD");  //登陆密码
            orgsql.AppendLine("         ,a.NAME");      //企业名
            orgsql.AppendLine("         ,a.ARTIFICIALPERSON");      //公司法人
            orgsql.AppendLine("         ,a.LINKMAN");   //联系人
            //orgsql.AppendLine("         ,a.MOBILETEL");  
            //电话
            orgsql.AppendLine("         ,RTRIM(a.PHONE_COUNTRY)+' '+RTRIM(a.PHONE_ZONE)+' '+RTRIM(a.PHONE_CODE)+' '+RTRIM(a.PHONE_EXT) AS PHONE");  
            orgsql.AppendLine("         ,f.EMAIL");     //信箱
            orgsql.AppendLine("         ,j.NAME AS CALLINGNAME");  //行业
            orgsql.AppendLine("         ,a.REGISTDAY");   //注册日
            orgsql.AppendLine("         ,convert(char(8),f.SUCCEEDDATETIME,112) AS SUCCEEDDATETIME");  //最后登录时间
            orgsql.AppendLine("         ,i.STARTDATE");   //服务开始日
            orgsql.AppendLine("         ,i.ENDDATE");     //服务到期日
            orgsql.AppendLine("         ,datediff(yy,i.STARTDATE,i.ENDDATE) AS SERVICESDATE");  //期间
            //地域
            orgsql.AppendLine("         ,RTRIM(b.NAME)+' '+RTRIM(c.NAME)+' '+RTRIM(d.NAME)+' '+RTRIM(e.NAME) AS ADDRESS");  

            orgsql.AppendLine("  FROM MA_COMINFO a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("         MA_ZONECLASS b");
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
            orgsql.AppendLine("        MA_REGISTER f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ACCOUNT h");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_SERVICE i");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.MA_ACCOUNTID = i.MA_ACCOUNTID");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = h.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS j");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_MANAGE g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        g.MA_PRODUCTCLASSID = j.MA_ORDERID");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_COMINFOID = g.MA_COMINFOID");
           
            orgsql.AppendLine("  WHERE a.MA_COMINFOID <> ''");
            //会员ID
            if (registId != "")
            {
                orgsql.AppendFormat(" and a.REGISTID like '%{0}%'  ", registId);
            }
            //公司名
            if (name != "")
            {
                orgsql.AppendFormat(" and a.NAME like '%{0}%'  ", name);
            }
            //电话 
            if (phone != "")
            {
                orgsql.AppendFormat(" and (a.PHONE_COUNTRY like '%{0}%' or a.PHONE_ZONE like '%{1}%' or a.PHONE_CODE like '%{2}%' or a.PHONE_EXT like '%{3}%')", phone, phone, phone, phone);
            }
            //信箱
            if (email != "")
            {
                orgsql.AppendFormat(" and f.EMAIL like '%{0}%'  ", email);
            }
            //联系人
            if (linkMan != "")
            {
                orgsql.AppendFormat(" and a.LINKMAN like '%{0}%'  ", linkMan);
            }
            //公司法人
            if (artificialPerson != "")
            {
                orgsql.AppendFormat(" and a.ARTIFICIALPERSON like '%{0}%'  ", artificialPerson);
            }
            //注册日
            if (regBeginDay != "" && regEndDay != "")
            {
                orgsql.AppendFormat(" and (a.REGISTDAY between '{0}' and '{1}')", regBeginDay, regEndDay);
            }
            else if (regBeginDay != "")
            {
                orgsql.AppendFormat(" and a.REGISTDAY = '{0}'", regBeginDay);
            }
            else if (regEndDay != "")
            {
                orgsql.AppendFormat(" and a.REGISTDAY = '{0}'", regEndDay);
            }
            //过期日
            if (expBeginDay != "" && expEndDay != "")
            {
                orgsql.AppendFormat(" and (i.ENDDATE between '{0}' and '{1}')", expBeginDay, expEndDay);
            }
            else if (expBeginDay != "")
            {
                orgsql.AppendFormat(" and i.ENDDATE = '{0}'", expBeginDay);
            }
            else if (expEndDay != "")
            {
                orgsql.AppendFormat(" and i.ENDDATE = '{0}'", expEndDay);
            }
            //服务期间
            if (servicesDate != "")
            {
                orgsql.AppendFormat(" and datediff(yy,i.STARTDATE,i.ENDDATE) = '{0}'", servicesDate);
            }
            //剩余服务月数
            if (servicesMonth != "")
            {
                orgsql.AppendFormat(" and datediff(mm,getdate(),i.ENDDATE) = '{0}'", servicesMonth);
            }
            //地域
            if (countryId != "")
            {
                orgsql.AppendFormat(" and a.COUNTRYID = '{0}'", countryId);
            }
            if (provinceId != "")
            {
                orgsql.AppendFormat(" and a.PROVINCEID = '{0}'", provinceId);
            }
            if (cityId != "")
            {
                orgsql.AppendFormat(" and a.CITYID = '{0}'", cityId);
            }
            if (boroughId != "")
            {
                orgsql.AppendFormat(" and a.BOROUGHID = '{0}'", boroughId);
            }
            //行业
            if (callingId != "")
            {
                orgsql.AppendFormat(" and g.MA_PRODUCTCLASSID = '{0}'", callingId);
            }
            //付费状态
            if (payState == "0")
            {
                orgsql.AppendFormat(" and i.STATE = '1'");
            }
            else if (payState == "1")
            {
                orgsql.AppendFormat(" and h.BALANCE > 0");
            }
            else if (payState == "2")
            {
                orgsql.AppendFormat(" and i.STATE = '0'");
            }
            else if (payState == "01")
            {
                orgsql.AppendFormat(" and (i.STATE = '1' or h.BALANCE > 0)");
            }
            else if (payState == "02")
            {
                orgsql.AppendFormat(" and (i.STATE = '1' or i.STATE = '0')");
            }
            else if (payState == "12")
            {
                orgsql.AppendFormat(" and (h.BALANCE > 0 or i.STATE = '0')");
            }
            else if (payState == "012")
            {
                orgsql.AppendFormat(" and (i.STATE = '1' or h.BALANCE > 0 or i.STATE = '0')");
            }
            orgsql.AppendFormat(" and a.COUNTRYID = '{0}'  ", langId);
            //orgsql.AppendFormat(" and a.LANG = '{0}'  ", langId);
            //orgsql.AppendLine(" ORDER BY a.MA_COMINFOID DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //网站激活取得公司信息
        public DataTable GetCompanyInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.NAME");
            orgsql.AppendLine("         ,a.EXPLAIN ");
            orgsql.AppendLine("         ,a.PHONE_ZONE ");
            orgsql.AppendLine("         ,a.PHONE_CODE ");
            orgsql.AppendLine("         ,a.OTHERMANAGE");
            orgsql.AppendLine("         ,a.POSTCODE");
            orgsql.AppendLine("         ,a.ADDRESS AS DETAILSADDRESS");
            orgsql.AppendLine("         ,f.EMAIL ");
            orgsql.AppendLine("         ,j.NAME AS CALLINGNAME");
            orgsql.AppendLine("         ,RTRIM(b.NAME)+RTRIM(c.NAME)+RTRIM(d.NAME)+RTRIM(e.NAME) AS ADDRESS");
            
            orgsql.AppendLine("  from  MA_COMINFO a");
            orgsql.AppendLine("  left outer join MA_ZONECLASS b");
            orgsql.AppendLine("  on a.COUNTRYID = b.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS c");
            orgsql.AppendLine("  on a.PROVINCEID = c.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS d");
            orgsql.AppendLine("  on a.CITYID = d.MA_ORDERID");
            orgsql.AppendLine("  left outer join MA_ZONECLASS e");
            orgsql.AppendLine("  on a.BOROUGHID = e.MA_ORDERID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CALLINGCLASS j");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_MANAGE g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        g.MA_PRODUCTCLASSID = j.MA_ORDERID");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_COMINFOID = g.MA_COMINFOID");

            orgsql.AppendLine("  left outer join MA_REGISTER f");
            orgsql.AppendLine("  on a.MA_REGISTERID = f.MA_REGISTERID");

            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       isnull(a.DELFLG,'') <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable GetCountryId(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.COUNTRYID AS LANG");
            orgsql.AppendLine("  from  MA_COMINFO a");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司注册日期
        public DataTable GetRegistDate(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select CONVERT(VARCHAR(10),CREATEDATE,23) AS CREATEDATE from MA_COMINFO");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //申请发票取得公司信息
        public DataTable GetRecCompanyInfo(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.NAME");
            orgsql.AppendLine("         ,a.LINKMAN ");
            orgsql.AppendLine("         ,a.POSTCODE");
            orgsql.AppendLine("         ,a.ADDRESS AS DETAILSADDRESS");
            orgsql.AppendLine("         ,RTRIM(b.NAME)+RTRIM(c.NAME)+RTRIM(d.NAME)+RTRIM(e.NAME) AS ADDRESS");

            orgsql.AppendLine("  from  MA_COMINFO a");
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
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       isnull(a.DELFLG,'') <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司ICP备案证书号
        public DataTable GetCertCode(String registId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select CERTCODE,CERTNAME from MA_COMINFO");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     REGISTID = '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 取得ICP备案证书
        /// </summary>
        /// <param name="certCode"></param>
        /// <returns></returns>
        public DataTable GetCertCodeCont(String registId,String certCode)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select CERTCODE from MA_COMINFO");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     CERTCODE = '{0}'", certCode);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     REGISTID <> '{0}'", registId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 修改公司ICP备案证书号
        /// </summary>
        public bool SetCertCode(String registId, String certcode, String certname)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("    UPDATE MA_COMINFO");
            orgsql.AppendLine("    SET");
            orgsql.AppendFormat(" CERTCODE = '{0}'", certcode);
            orgsql.AppendFormat(" ,CERTNAME = '{0}'", certname);
            orgsql.AppendLine(" WHERE");
            orgsql.AppendFormat(" REGISTID = '{0}'", registId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteCominfo(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_COMINFO");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}