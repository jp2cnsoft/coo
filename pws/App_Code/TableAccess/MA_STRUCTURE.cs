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
    public class MA_STRUCTURE : Seika.COO.DBA.DateBaseAccess 
    {
        public MA_STRUCTURE(DBConnect sql): base(sql)
        {        
        }
        //插入组织注册信息
        public bool InsertOrgIfo(String name,String pronounce,String bankroll,String bankrollunit,String deputy_famiy,
                                String deputy_name,String funddate,String licencecode,String charactter,String managmode,String employeenum,
                                String country,String postcode,String provice,String city,String borough,String address, String phone_country,
                                String phone_zone,String phone_code,String Phone_ext,String fax_country,String fax_zone,String fax_code,
                                String fax_ext,String homepage,String mail_address,String mail_ext,String logoid,String id,String calling,ref String ma_id)
        {
            StringBuilder orgsql = new StringBuilder();
            ma_id = GetNextSeqNumber();
            orgsql.AppendLine("  insert into MA_STRUCTURE");
            orgsql.AppendLine("  (MA_STRUCTUREID,NAME,PRONOUNCE,BANKROLL,BANKROLLUNIT,DEPUTY_FAMILY,");
            orgsql.AppendLine("  DEPUTY_NAME,FOUNDDATE,LICENCECODE,CHARACTER,MANAGEMODE,EMPLOYEENUM,COUNTRY,");
            orgsql.AppendLine("  POSTCODE,PROVINCE,CITY,BOROUGH,ADDRESS,PHONE_COUNTRY,PHONE_ZONE,");
            orgsql.AppendLine("  PHONE_CODE,PHONE_EXT,FAX_COUNTRY,FAX_ZONE,FAX_CODE,FAX_EXT,HOMEPAGE,");
            orgsql.AppendLine("  MAIL_ADDRESS,MAIL_EXT,LOGOID, DELETEFLG,UPDATENO,CREATEUSERID,CREATEDATE,CALLING)");
            orgsql.AppendLine("  values");
            orgsql.AppendFormat("  ('{0}','{1}','{2}','{3}','{4}','{5}',",ma_id, name, pronounce, bankroll, bankrollunit,deputy_famiy);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}','{5}',", deputy_name, funddate, licencecode, charactter, managmode,employeenum);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}',", country, postcode, provice, city, borough);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}',", address, phone_country, phone_zone, phone_code, Phone_ext);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}',", fax_country, fax_zone, fax_code, fax_ext, homepage);
            orgsql.AppendFormat("  '{0}','{1}','{2}','{3}','{4}',", mail_address, mail_ext, logoid, 1, 1);
            orgsql.AppendFormat("  '{0}',GetDate(),'{1}')", id, calling);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }
        //验证组织名是否重复
        public bool CheckOrgName(String name)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" select * from MA_STRUCTURE");
            orgsql.AppendFormat(" where  NAME = '{0}'", name);
            DataSet ds = sql.ExecuteDataset(orgsql.ToString());
            return ds.Tables[0].Rows.Count > 0;
        }
        //验证组织执照是否重复
        public bool CheckOrgLicence(String lic)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" select * from MA_STRUCTURE");
            orgsql.AppendFormat(" where  LICENCECODE = '{0}'", lic);
            DataSet ds = sql.ExecuteDataset(orgsql.ToString());
            return ds.Tables[0].Rows.Count > 0;
        }

        //取得公司一览
        public DataTable GetOrgSum(String key,String add, String pro)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.MA_STRUCTUREID,a.NAME,a.BANKROLL,b.NAME as BANKROLLUNIT,a.DEPUTY_FAMILY,a.DEPUTY_NAME,");
            orgsql.AppendLine("  a.FOUNDDATE ,d.NAME as COUNTRY,e.NAME as PROVINCE,f.NAME as CITY,a.BOROUGH,a.ADDRESS,g.NAME as CHARACTER,");
            orgsql.AppendLine("  a.LOGOID");            
            orgsql.AppendLine("  from MA_STRUCTURE a");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE b");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.BANKROLLUNIT = b.MA_CODEID");          
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CLASSMANAGE d");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.COUNTRY = d.MA_CLASSMANAGEID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CLASSMANAGE e");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.PROVINCE = e.MA_CLASSMANAGEID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CLASSMANAGE f");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.CITY = f.MA_CLASSMANAGEID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE g");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.CHARACTER = g.MA_CODEID");
            orgsql.AppendLine("  where");
            if (key != "")
            {
                orgsql.AppendFormat("  a.NAME like '%{0}%' and ", key);
            }
            if (pro != "")
            {
                orgsql.AppendFormat("  a.CALLING in ('{0}') and", pro);
            }

            if (add != "" )
            {
                orgsql.AppendFormat(" (a.COUNTRY = '{0}' or a.PROVINCE = '{1}' or a.CITY = '{2}') and",add, add, add);
            }
            orgsql.AppendLine("  a.MA_STRUCTUREID != ''");
            orgsql.AppendLine("  order by  a.MA_STRUCTUREID desc");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得公司信息
        public DataTable GetUpdateOrg(String orgId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select a.MA_STRUCTUREID,a.NAME,a.BANKROLL,b.NAME as BANKROLLUNIT,a.DEPUTY_FAMILY,a.DEPUTY_NAME,");
            orgsql.AppendLine("  a.FOUNDDATE ,d.NAME as COUNTRY,e.NAME as PROVINCE,f.NAME as CITY,a.BOROUGH,a.ADDRESS,g.NAME as CHARACTER,");
            orgsql.AppendLine("  a.LOGOID");
            orgsql.AppendLine("  from MA_STRUCTURE");
            orgsql.AppendLine("  where");
            orgsql.AppendLine("     MA_STRUCTUREID != ''");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
        public DataTable GetMa_structure(String MA_STRUCTUREID)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" SELECT a.MA_STRUCTUREID,a.PRONOUNCE, a.NAME, a.BANKROLL,a.COUNTRY,a.PROVINCE ,a.CITY,a.BOROUGH, b.NAME AS BANKROLLUNIT, a.DEPUTY_FAMILY, a.DEPUTY_NAME, a.FOUNDDATE, ");
            orgsql.AppendLine("  d.NAME AS COUNTRY, e.NAME AS PROVINCE, f.NAME AS CITY, J.NAME AS BOROUGH,k.NAME AS EMPLOYEENUM,a.ADDRESS, g.NAME AS CHARACTER,");
            orgsql.AppendLine("  I.NAME AS MANAGEMODE, a.POSTCODE, h.USER_FAMILY, h.USER_NAME, h.PHONE_COUNTRY, h.PHONE_ZONE, h.PHONE_CODE, h.PHONE_EXT, ");
            orgsql.AppendLine("h.FAX_COUNTRY, h.FAX_ZONE, h.FAX_CODE, h.FAX_EXT, h.MAIL_ADDRESS, h.MAIL_EXT,h.homepage,h.phone_mobile");            
            orgsql.AppendLine("  from MA_STRUCTURE a");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE b");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.BANKROLLUNIT = b.MA_CODEID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS  d");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.COUNTRY = d.MA_ORDERID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS  e");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.PROVINCE = e.MA_ORDERID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS  f");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.CITY = f.MA_ORDERID");           
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE g");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.CHARACTER = g.MA_CODEID");            
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        ma_contact h");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.MA_STRUCTUREID = h.id");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE  I");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.MANAGEMODE = I.MA_CODEID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_ZONECLASS  J");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.BOROUGH = J.MA_ORDERID");
            orgsql.AppendLine("   LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE k");
            orgsql.AppendLine("   ON ");
            orgsql.AppendLine("        a.EMPLOYEENUM  = k.MA_CODEID");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     MA_STRUCTUREID = '{0}'", MA_STRUCTUREID);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }
    }
}