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
    public class MA_PRODUCT : Seika.COO.DBA.DateBaseAccess
    {
        public MA_PRODUCT(DBConnect sql)
            : base(sql)
	    {
		  
	    }

        public DataTable SearchProductCount(String name, String product, String country,
                                       String province, String city, String borough,String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  SELECT  ");
            orgsql.AppendLine("         COUNT(a.MA_PRODID) AS ROWNUM ");
            orgsql.AppendLine("  FROM MA_PROD a");

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
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER z");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        z.REGISTID = a.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.PRICEUNITID = g.MA_CODEID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_PROD h");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_PRODUCTCLASS j");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.MA_PRODUCTCLASSID = j.MA_PRODUCTCLASSID");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_PRODID = h.MA_PRODID");
            orgsql.AppendLine("  WHERE a.MA_PRODID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" and a.NAME like '%{0}%'  ", name);
            }
            if (product != "")
            {
                orgsql.AppendFormat(" and (h.MA_PRODUCTCLASSID = '{0}' or h.PATH  like '%{0}%')  ", product);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" and f.PROVINCEID = '{0}'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" and f.CITYID = '{0}'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" and f.BOROUGHID = '{0}'  ", borough);
            }
            if (langId != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", langId);
            }
            orgsql.Append(" and f.DELFLG IS NULL  ");
            orgsql.Append(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),z.SERVICEENDDATE) > 0");
            orgsql.AppendFormat(" and a.LANG = '{0}'  ",langId);
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public DataTable SearchProduct(String name, String product, String country,
                                       String province, String city, String borough,
                                       String langId, int startIdx, int count)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine(" SELECT * ");
            orgsql.AppendLine(" FROM ");
            orgsql.AppendLine(" (");
            orgsql.AppendLine("  SELECT a.MA_PRODID ");
            orgsql.AppendLine("         ,a.REGISTID");
            orgsql.AppendLine("         ,a.NAME");
            orgsql.AppendLine("         ,a.PRICE");
            orgsql.AppendLine("         ,a.PUBCOM");
            orgsql.AppendLine("         ,a.EXPLAIN");
            orgsql.AppendLine("         ,a.TOPORDER");
            orgsql.AppendLine("         ,g.NAME as PRICEUNIT");
            orgsql.AppendLine("         ,CONVERT(varchar(12), CONVERT(datetime , a.PUBLISHDATE, 112), 23) AS PUBLISHDATE");
            orgsql.AppendLine("         ,RTRIM(b.NAME)+' '+RTRIM(c.NAME)+' '+RTRIM(d.NAME)+' '+RTRIM(e.NAME) AS ADDRESS");
            orgsql.AppendLine("         ,a.IMGNAME");
            orgsql.AppendLine("         ,f.NAME AS COMPANY");
            orgsql.AppendLine("         ,CASE WHEN l.HTTP_SV_DOMAIN IS NULL THEN l.HTTP_SV_IP ELSE l.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine("         ,ROW_NUMBER() OVER(ORDER BY a.MA_PRODID DESC) AS ROWNUM");
            orgsql.AppendLine("  FROM MA_PROD a");
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
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER z");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        z.REGISTID = a.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_CODE g");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.PRICEUNITID = g.MA_CODEID");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        BS_PROD h");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_PRODUCTCLASS j");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.MA_PRODUCTCLASSID = j.MA_PRODUCTCLASSID");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.MA_PRODID = h.MA_PRODID");
            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER k ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       k.REGISTID = a.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER l ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      l.SV_SERVERID = k.SV_SERVERID ");
            orgsql.AppendLine("  WHERE a.MA_PRODID <> ''");
            if (name != "")
            {
                orgsql.AppendFormat(" and a.NAME like '%{0}%'  ", name);
            }
            if (product != "")
            {
                orgsql.AppendFormat(" and (h.MA_PRODUCTCLASSID = '{0}' or h.PATH  like '%{0}%')  ", product);
            }
            if (country != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", country);
            }
            if (province != "")
            {
                orgsql.AppendFormat(" and f.PROVINCEID = '{0}'  ", province);
            }
            if (city != "")
            {
                orgsql.AppendFormat(" and f.CITYID = '{0}'  ", city);
            }
            if (borough != "")
            {
                orgsql.AppendFormat(" and f.BOROUGHID = '{0}'  ", borough);
            }
            if (langId != "")
            {
                orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", langId);
            }
            orgsql.AppendLine(" and f.DELFLG IS NULL  ");
            orgsql.AppendLine(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),z.SERVICEENDDATE) > 0");
            orgsql.AppendFormat(" and a.LANG = '{0}'  ",langId);
            orgsql.AppendLine(" ) AS k");
            orgsql.AppendFormat(" WHERE ROWNUM BETWEEN {0} AND {1}", startIdx, (startIdx + count - 1));
            orgsql.AppendLine(" ORDER BY TOPORDER DESC,MA_PRODID DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        public bool AddProductBaseInfo(String ma_productid, String rigistId, String name, String explain, 
            String imgname, String price, String priceunitid, String publishdate,
            String pubcom, String countryId, String provinceId, String cityId, String boroughId, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_PROD");
            orgsql.AppendLine("       (MA_PRODID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,NAME");
            orgsql.AppendLine("       ,EXPLAIN");
            orgsql.AppendLine("       ,IMGNAME");
            orgsql.AppendLine("       ,PRICE");
            orgsql.AppendLine("       ,PRICEUNITID");
            orgsql.AppendLine("       ,PUBLISHDATE");
            orgsql.AppendLine("       ,PUBCOM");
            orgsql.AppendLine("       ,COUNTRYID");
            orgsql.AppendLine("       ,PROVINCEID");
            orgsql.AppendLine("       ,CITYID");
            orgsql.AppendLine("       ,BOROUGHID");
            orgsql.AppendLine("       ,LANG)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                ma_productid, rigistId, name, explain,
                imgname, price, priceunitid, publishdate,
                pubcom,countryId, provinceId, cityId, boroughId,lang);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }


        public bool UpdateProductBaseInfo(String ma_productid, String rigistId, String name, String explain, 
            String imgname, String price, String priceunitid, String publishdate,
            String pubcom,String countryId, String provinceId, String cityId, String boroughId, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_PROD");
            orgsql.AppendLine("     set");
            orgsql.AppendFormat("        REGISTID = '{0}'",rigistId);
            orgsql.AppendFormat("       ,NAME= '{0}'",name);
            orgsql.AppendFormat("       ,EXPLAIN= '{0}'", explain);
            orgsql.AppendFormat("       ,IMGNAME= '{0}'",imgname);
            orgsql.AppendFormat("       ,PRICE= '{0}'",price);
            orgsql.AppendFormat("       ,PRICEUNITID= '{0}'",priceunitid);
            orgsql.AppendFormat("       ,PUBLISHDATE= '{0}'",publishdate);
            orgsql.AppendFormat("       ,PUBCOM= '{0}'", pubcom);
            orgsql.AppendFormat("       ,COUNTRYID= '{0}'",countryId);
            orgsql.AppendFormat("       ,PROVINCEID= '{0}'",provinceId);
            orgsql.AppendFormat("       ,CITYID= '{0}'",cityId);
            orgsql.AppendFormat("       ,BOROUGHID= '{0}'", boroughId);
            orgsql.AppendFormat("       ,LANG= '{0}'", lang);
            orgsql.AppendFormat("       where MA_PRODID = '{0}'", ma_productid);


            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        public bool DelProduct(String ma_productid)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_PROD");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       MA_PRODID = '{0}'", ma_productid);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 降序取得产品一览数据
        /// </summary>
        /// <param name="startIdx">取得开始行号</param>
        /// <param name="count">取得件数</param>
        /// <returns></returns>
        public DataTable GetProductList(int startIdx, int count)
        {
            StringBuilder cmd = new StringBuilder();

            cmd.AppendLine(" SELECT *");
            cmd.AppendLine(" FROM ");
            cmd.AppendLine(" (");
            cmd.AppendLine("    SELECT");
            cmd.AppendLine("          a.MA_PRODID");
            cmd.AppendLine("         ,a.REGISTID");
            cmd.AppendLine("         ,a.NAME");
            cmd.AppendLine("         ,a.IMGNAME");
            cmd.AppendLine("         ,a.PRICE");
            cmd.AppendLine("         ,a.PRICEUNITID");
            cmd.AppendLine("         ,CONVERT(VARCHAR(12), CONVERT(DATETIME , a.PUBLISHDATE, 112), 23) PUBLISHDATE");
            cmd.AppendLine("         ,a.COUNTRYID");
            cmd.AppendLine("         ,a.PROVINCEID");
            cmd.AppendLine("         ,a.CITYID");
            cmd.AppendLine("         ,a.BOROUGHID");
            cmd.AppendLine(" 	     ,b.SV_SERVERID");
            cmd.AppendLine(" 	     ,CASE WHEN c.HTTP_SV_DOMAIN IS NULL THEN c.HTTP_SV_IP ELSE c.HTTP_SV_DOMAIN END AS SERVERHOST");
            cmd.AppendLine("         ,ROW_NUMBER() OVER(ORDER BY a.MA_PRODID DESC) AS ROWNUM");
            cmd.AppendLine("    FROM MA_PROD a");
            cmd.AppendLine("    LEFT OUTER JOIN ");
            cmd.AppendLine("         SV_BSSERVER b");
            cmd.AppendLine(" 	ON ");
            cmd.AppendLine("         a.REGISTID = b.REGISTID");
            cmd.AppendLine(" 	LEFT OUTER JOIN ");
            cmd.AppendLine("         SV_SERVER c");
            cmd.AppendLine(" 	ON ");
            cmd.AppendLine("         b.SV_SERVERID = c.SV_SERVERID"); 
            cmd.AppendLine(" ) AS t");
            cmd.AppendLine(" WHERE ROWNUM BETWEEN {0} AND {1}");
            cmd.AppendLine(" ORDER BY MA_PRODID DESC");

            String sqlcmd = String.Format(cmd.ToString(), startIdx, (startIdx + count - 1));
            return sql.ExecuteDataset(sqlcmd).Tables[0];
        }

        public DataTable GetProTitle(String langId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select top 10 a.MA_PRODID ,a.NAME ,a.REGISTID");
            orgsql.AppendLine("    ,CASE WHEN n.HTTP_SV_DOMAIN IS NULL THEN n.HTTP_SV_IP ELSE n.HTTP_SV_DOMAIN END AS SERVERHOST");
            orgsql.AppendLine("  from MA_PROD a");
            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_COMINFO f");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        a.REGISTID = f.REGISTID");

            orgsql.AppendLine("  LEFT OUTER JOIN ");
            orgsql.AppendLine("        MA_REGISTER h");
            orgsql.AppendLine("  ON ");
            orgsql.AppendLine("        h.REGISTID = a.REGISTID");

            orgsql.AppendLine(" LEFT OUTER JOIN ");
            orgsql.AppendLine("       SV_BSSERVER m ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("       m.REGISTID = a.REGISTID ");
            orgsql.AppendLine("LEFT OUTER JOIN  ");
            orgsql.AppendLine("      SV_SERVER n ");
            orgsql.AppendLine("ON  ");
            orgsql.AppendLine("      n.SV_SERVERID = m.SV_SERVERID ");
            orgsql.AppendLine("  WHERE a.MA_PRODID <> ''");
            orgsql.AppendLine(" and f.DELFLG IS NULL  ");
            orgsql.AppendLine(" and f.SAVEFLG IS NULL  ");
            orgsql.AppendLine(" and a.TOPORDER IS NOT NULL  ");
            orgsql.AppendLine(" AND datediff(day,getdate(),h.SERVICEENDDATE) > 0");
            orgsql.AppendFormat(" and a.LANG = '{0}'  ", langId);
            orgsql.AppendFormat(" and f.COUNTRYID = '{0}'  ", langId);
            orgsql.AppendLine(" ORDER BY a.TOPORDER DESC");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public bool DeleteProduct(String registerId,String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_PROD");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);
            orgsql.AppendLine("   AND");
            orgsql.AppendFormat("       LANG = '{0}'", lang);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 删除产品表
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteProd(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_PROD");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}