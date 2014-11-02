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
    public class MA_POST : Seika.COO.DBA.DateBaseAccess
    {
        public MA_POST(DBConnect sql)
            : base(sql)
        {
            
        }

        public bool AddPostBaseInfo(String ma_jobId, String name, String rigistId, String ma_classtype, String ma_orderid, int number, String countryId,
              String provinceId,String cityId,String boroughId,String country,String province,String city,String borough,String workkindId,String effectdate,String wage,
               String schoollevelId, String sexId, String ageBegin, String ageEnd, String experience, String officedepict, String officerequest, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_JOB");
            orgsql.AppendLine("       (MA_JOBID");
            orgsql.AppendLine("       ,JOBNAME");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,POSTTYPECD");
            orgsql.AppendLine("       ,POSTCD");
            orgsql.AppendLine("       ,NUMBER");
            orgsql.AppendLine("       ,STATECD_1");
            orgsql.AppendLine("       ,STATECD_2");
            orgsql.AppendLine("       ,STATECD_3");
            orgsql.AppendLine("       ,STATECD_4");
            orgsql.AppendLine("       ,STATERESCD_1");
            orgsql.AppendLine("       ,STATERESCD_2");
            orgsql.AppendLine("       ,STATERESCD_3");
            orgsql.AppendLine("       ,STATERESCD_4");
            orgsql.AppendLine("       ,WORKKINDCD");
            orgsql.AppendLine("       ,EFFECTDATE");
            orgsql.AppendLine("       ,WAGE");
            orgsql.AppendLine("       ,SCHOOLLEVELCD");
            orgsql.AppendLine("       ,SEXCD");
            orgsql.AppendLine("       ,AGE_BEGIN");
            orgsql.AppendLine("       ,AGE_END");
            orgsql.AppendLine("       ,EXPERIENCE");
            orgsql.AppendLine("       ,OFFICEDEPICT");
            orgsql.AppendLine("       ,OFFICEREQUEST");
            orgsql.AppendLine("       ,LANG");
            orgsql.AppendLine("       ,CREATEDATE");
            orgsql.AppendLine("       ,UPDATEDATE)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}',getdate(),getdate())",
                ma_jobId, name, rigistId, ma_classtype, ma_orderid, number, countryId,
                provinceId, cityId, boroughId, country, province, city, borough, workkindId, effectdate, wage,
                schoollevelId, sexId, ageBegin, ageEnd, experience, officedepict, officerequest, lang);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        public bool UpdatePostBaseInfo(String ma_jobId, String name, String rigistId, String ma_classtype, String ma_orderid, int number, String countryId,
              String provinceId, String cityId, String boroughId, String country, String province, String city, String borough, String workkindId, String effectdate, String wage,
               String schoollevelId, String sexId, String ageBegin, String ageEnd, String experience, String officedepict, String officerequest, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_JOB");
            orgsql.AppendLine("     set");
            orgsql.AppendFormat("        JOBNAME = '{0}'", name);
            orgsql.AppendFormat("       ,REGISTID= '{0}'", rigistId);
            orgsql.AppendFormat("       ,POSTTYPECD= '{0}'", ma_classtype);
            orgsql.AppendFormat("       ,POSTCD= '{0}'", ma_orderid);
            orgsql.AppendFormat("       ,NUMBER= {0}", number);
            orgsql.AppendFormat("       ,STATECD_1= '{0}'", countryId);
            orgsql.AppendFormat("       ,STATECD_2= '{0}'", provinceId);
            orgsql.AppendFormat("       ,STATECD_3= '{0}'", cityId);
            orgsql.AppendFormat("       ,STATECD_4= '{0}'", boroughId);
            orgsql.AppendFormat("       ,STATERESCD_1= '{0}'", country);
            orgsql.AppendFormat("       ,STATERESCD_2= '{0}'", province);
            orgsql.AppendFormat("       ,STATERESCD_3= '{0}'", city);
            orgsql.AppendFormat("       ,STATERESCD_4= '{0}'", borough);
            orgsql.AppendFormat("       ,WORKKINDCD= '{0}'", workkindId);
            orgsql.AppendFormat("       ,EFFECTDATE= '{0}'", effectdate);
            orgsql.AppendFormat("       ,WAGE= '{0}'", wage);
            orgsql.AppendFormat("       ,SCHOOLLEVELCD= '{0}'", schoollevelId);
            orgsql.AppendFormat("       ,SEXCD= '{0}'", sexId);
            orgsql.AppendFormat("       ,AGE_BEGIN= '{0}'", ageBegin);
            orgsql.AppendFormat("       ,AGE_END= '{0}'", ageEnd);
            orgsql.AppendFormat("       ,EXPERIENCE= '{0}'", experience);
            orgsql.AppendFormat("       ,OFFICEDEPICT= '{0}'", officedepict);
            orgsql.AppendFormat("       ,OFFICEREQUEST= '{0}'", officerequest);
            orgsql.AppendFormat("       ,LANG= '{0}'", lang);
            orgsql.AppendLine("         ,CREATEDATE= getdate()");
            orgsql.AppendLine("         ,UPDATEDATE= getdate()");
            orgsql.AppendFormat("       where MA_JOBID = '{0}'", ma_jobId);


            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public bool DelJOB(String ma_jobid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_JOB");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       MA_JOBID = '{0}'", ma_jobid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public DataTable SearchPostCount(String name, String post, String country,
                                       String province, String city, String borough,
            String schoollevel, String sex, String age, String workkind, String experience, String issuedate, String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT  ");
            orgsql.AppendLine("         COUNT(a.MA_JOBID) AS ROWNUM ");
            orgsql.AppendLine("  FROM MA_JOB a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("         MA_ZONECLASS b");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_1 = b.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS c");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_2 = c.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS d");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_3 = d.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS e");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_4 = e.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER h");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.REGISTID = a.REGISTID");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_POSTCLASS k");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.POSTCD = k.MA_ORDERID");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_POSTCLASS l");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.POSTTYPECD = l.MA_ORDERID");

            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_CODE g");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.PRICEUNITID = g.MA_CODEID");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        BS_PROD h");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_PRODUCTCLASS j");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        h.MA_PRODUCTCLASSID = j.MA_PRODUCTCLASSID");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.MA_PRODID = h.MA_PRODID");
            orgsql.AppendLine("  WHERE a.MA_JOBID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" and a.JOBNAME like '%{0}%'  ", name);
            }
            //if (product != "")
            //{
            //    orgsql.AppendFormat(" and (h.MA_PRODUCTCLASSID = '{0}' or h.PATH  like '%{0}%')  ", product);
            //}
            if (post != "")
            {
                orgsql.AppendFormat(" and (a.POSTCD like '%{0}%'  or a.POSTTYPECD like '%{1}%')", post, post);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" and a.STATECD_1 like '%{0}%'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" and a.STATECD_2 like '%{0}%'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" and a.STATECD_3 like '%{0}%'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" and a.STATECD_4 like '%{0}%'  ", borough);
            }

            if (schoollevel != "")
            {
                orgsql.AppendFormat(" and CONVERT(int, SUBSTRING(a.SCHOOLLEVELCD,5,15)) >= CONVERT(int, '{0}')  ", schoollevel);
            }
            if (sex != "")
            {
                orgsql.AppendFormat(" and a.SEXCD = '{0}'  ", sex);
            }
            if (age != "")
            {
                orgsql.AppendFormat(" and CONVERT(int, a.AGE_BEGIN) <= CONVERT(int, '{0}')  ", age);
            }
            if (workkind != "")
            {
                orgsql.AppendFormat(" and a.WORKKINDCD like '%{0}%'  ", workkind);
            }
            if (experience != "")
            {
                orgsql.AppendFormat(" and (CONVERT(int, a.EXPERIENCE) >= CONVERT(int, '{0}')  OR  CONVERT(int, a.EXPERIENCE) = 0 )", experience);
            }
            if (issuedate != "")
            {
                orgsql.AppendFormat(" and (select DATEDIFF(dd,a.CREATEDATE,getdate()))  {0} ", issuedate);
            }
            if (langId != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", langId);
            }
            orgsql.Append(" and f.DELFLG IS NULL  ");
            orgsql.Append(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),h.SERVICEENDDATE) > 0");
            orgsql.AppendFormat(" and a.LANG = '{0}'  ", langId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchPost(String name, String post, String country,
                                       String province, String city, String borough,
            String schoollevel, String sex, String age, String workkind, String experience, String issuedate,
                                       String langId, int startIdx, int count)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" SELECT * ");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" (");
            orgsql.AppendLine("  SELECT a.MA_JOBID ");
            orgsql.AppendLine("         ,a.REGISTID");
            orgsql.AppendLine("         ,a.NUMBER");
            orgsql.AppendLine("         ,a.JOBNAME");
            orgsql.AppendLine("         ,a.STATECD_3");
            orgsql.AppendLine("         ,a.STATERESCD_3 AS ADDRESS");
            orgsql.AppendLine("         ,a.WORKKINDCD");
            orgsql.AppendLine("         ,g.NAME AS SCHOOLLEVELCD");
            orgsql.AppendLine("         ,h.NAME AS SEXCD");
            orgsql.AppendLine("         ,a.EXPERIENCE");
            orgsql.AppendLine("         ,a.AGE_BEGIN");
            orgsql.AppendLine("         ,a.AGE_END");
            //orgsql.AppendLine("         ,a.WORKKINDCD");
            //orgsql.AppendLine("         ,a.EFFECTDATE");
            //orgsql.AppendLine("         ,a.WAGE");
            //orgsql.AppendLine("         ,a.SEXCD");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,a.JOBNAME");
            //orgsql.AppendLine("         ,g.NAME");
            //orgsql.AppendLine("         ,a.EXPLAIN");
            //orgsql.AppendLine("         ,g.NAME as PRICEUNIT");
            orgsql.AppendLine("         ,CONVERT(varchar(12), CONVERT(datetime , a.CREATEDATE, 112), 23) AS CREATEDATE");
            //orgsql.AppendLine("         ,CONVERT(varchar(12), CONVERT(datetime , a.PUBLISHDATE, 112), 23) AS PUBLISHDATE");
            //orgsql.AppendLine("         ,RTRIM(b.NAME)+' '+RTRIM(c.NAME)+' '+RTRIM(d.NAME)+' '+RTRIM(e.NAME) AS ADDRESS");
            //orgsql.AppendLine("         ,a.IMGNAME");
            orgsql.AppendLine("         ,f.NAME AS COMPANY");
            orgsql.AppendLine("         ,CASE WHEN l.HTTP_SV_DOMAIN IS NULL THEN l.HTTP_SV_IP ELSE l.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine("         ,ROW_NUMBER() OVER(ORDER BY a.MA_JOBID DESC) AS ROWNUM");
            orgsql.AppendLine("  FROM MA_JOB a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("         MA_ZONECLASS b");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_1 = b.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS c");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_2 = c.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS d");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_3 = d.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS e");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.STATECD_4 = e.MA_ORDERID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER z");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        z.REGISTID = a.REGISTID");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_POSTCLASS g");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.POSTCD = g.MA_ORDERID");
            //orgsql.AppendLine("        MA_CODE h");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.POSTCD = g.MA_ORDERID");
            //orgsql.AppendLine("        MA_POSTCLASS h");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.POSTTYPECD = g.CLASSTYPE");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_CODE g");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.PRICEUNITID = g.MA_CODEID");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        BS_PROD h");
            //orgsql.AppendLine("  LEFT OUTER JOIN ");
            //orgsql.AppendLine("        MA_PRODUCTCLASS j");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        h.MA_PRODUCTCLASSID = j.MA_PRODUCTCLASSID");
            //orgsql.AppendLine("  ON ");
            //orgsql.AppendLine("        a.MA_PRODID = h.MA_PRODID");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       MA_CODE g ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       g.MA_CODEID = a.SCHOOLLEVELCD ");

            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       MA_CODE h ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       h.MA_CODEID = a.SEXCD ");

            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER k ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       k.REGISTID = a.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER l ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      l.SV_SERVERID = k.SV_SERVERID ");
            orgsql.AppendLine("  WHERE a.MA_JOBID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" and a.JOBNAME like '%{0}%'  ", name);
            }
            if (post != "")
            {
                orgsql.AppendFormat(" and (a.POSTCD like '%{0}%'  or a.POSTTYPECD like '%{1}%')", post, post);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" and a.STATECD_1 like '%{0}%'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" and a.STATECD_2 like '%{0}%'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" and a.STATECD_3 like '%{0}%'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" and a.STATECD_4 like '%{0}%'  ", borough);
            }

            if (schoollevel != "")
            {
                orgsql.AppendFormat(" and CONVERT(int, SUBSTRING(a.SCHOOLLEVELCD,5,15)) >= CONVERT(int, '{0}')  ", schoollevel);
            }
            if (sex != "")
            {
                orgsql.AppendFormat(" and a.SEXCD = '{0}'  ", sex);
            }
            if (age != "")
            {
                orgsql.AppendFormat(" and CONVERT(int, a.AGE_BEGIN) <= CONVERT(int, '{0}')   ", age);
            }
            if (workkind != "")
            {
                orgsql.AppendFormat(" and a.WORKKINDCD like '%{0}%'  ", workkind);
            }
            if (experience != "")
            {
                orgsql.AppendFormat(" and (CONVERT(int, a.EXPERIENCE) >= CONVERT(int, '{0}') OR CONVERT(int, a.EXPERIENCE) = 0) ", experience);
            }
            if (issuedate != "")
            {
                orgsql.AppendFormat(" and (select DATEDIFF(dd,a.CREATEDATE,getdate()))  {0} ", issuedate);
            }
            if (langId != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", langId);
            }
            orgsql.AppendLine(" and f.DELFLG IS NULL  ");
            orgsql.AppendLine(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),z.SERVICEENDDATE) > 0");
            orgsql.AppendFormat(" and a.LANG = '{0}'  ", langId);
            orgsql.AppendLine(" ) AS k");
            orgsql.AppendFormat(" WHERE ROWNUM BETWEEN {0} AND {1}", startIdx, (startIdx + count - 1));
            //orgsql.AppendLine(" ORDER BY MA_JOBID DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }


        public DataTable SearchJobCountry(String jobId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select STATECD_1 AS MA_ORDERID,STATERESCD_1 AS NAME from MA_JOB");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_JOBID = '{0}')", jobId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchJobProvince(String jobId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select STATECD_2 AS MA_ORDERID,STATERESCD_2 AS NAME from MA_JOB");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_JOBID = '{0}')", jobId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchJobCity(String jobId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select STATECD_3 AS MA_ORDERID,STATERESCD_3 AS NAME from MA_JOB");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_JOBID = '{0}')", jobId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchJobBorough(String jobId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select STATECD_4 AS MA_ORDERID,STATERESCD_4 AS NAME from MA_JOB");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("  ( MA_JOBID = '{0}')", jobId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable GetJobTitle(String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select f.NAME ,f.REGISTID, a.MA_JOBID");
            orgsql.AppendLine("    ,CASE WHEN n.HTTP_SV_DOMAIN IS NULL THEN n.HTTP_SV_IP ELSE n.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine("  from MA_JOB a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER m ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       m.REGISTID = a.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER n ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      n.SV_SERVERID = m.SV_SERVERID ");
            orgsql.AppendLine("  WHERE a.MA_JOBID <> ''");
            orgsql.AppendLine(" and f.DELFLG IS NULL  ");
            orgsql.AppendLine(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendFormat(" and f.LANG = '{0}'  ", langId);
            orgsql.AppendLine(" ORDER BY MA_JOBID DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 删除招聘表
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteJob(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_JOB");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}