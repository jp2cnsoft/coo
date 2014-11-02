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
    public class MA_VIEWCOUNT : Seika.COO.DBA.DateBaseAccess
    {
        public MA_VIEWCOUNT(DBConnect sql)
            : base(sql)
        {
        }

        //取得访问统计信息
        public DataTable GetViewCount(String registId, String pageId, String viewDate, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select *");
            orgsql.AppendLine("  from MA_VIEWCOUNT a");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     a.PAGEID = '{0}'", pageId);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     a.VIEWDATE = '{0}'", viewDate);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     a.LANG = '{0}'", lang);
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       isnull(a.DELFLG,'') <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //取得最新访问统计数据
        public DataTable GetViewCountNum(String registId, String pageId, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select SUM(VIEWNUM) COUNT");
            orgsql.AppendLine("  from MA_VIEWCOUNT a");
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("     a.REGISTID = '{0}'", registId);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     a.PAGEID = '{0}'", pageId);
            orgsql.AppendLine("  and");
            orgsql.AppendFormat("     a.LANG = '{0}'", lang);
            orgsql.AppendLine("  and");
            orgsql.AppendLine("       isnull(a.DELFLG,'') <> 1");
            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        //更新访问统计信息
        public bool UpdateViewCount(String ma_viewCountId, int viewNum)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  update MA_VIEWCOUNT");
            orgsql.AppendFormat("set VIEWNUM = {0}", viewNum);
            orgsql.AppendLine("  where");
            orgsql.AppendFormat("    MA_VIEWCOUNTID = '{0}'", ma_viewCountId);
            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        //追加访问统计信息
        public bool AddViewCount(String ma_viewCountId, String registId, String pageId, int viewNum, String viewDate, String lang)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  insert into MA_VIEWCOUNT");
            orgsql.AppendLine("       (MA_VIEWCOUNTID");
            orgsql.AppendLine("       ,REGISTID");
            orgsql.AppendLine("       ,PAGEID");
            orgsql.AppendLine("       ,VIEWNUM");
            orgsql.AppendLine("       ,VIEWDATE");
            orgsql.AppendLine("       ,LANG)");
            orgsql.AppendLine("  values ");
            orgsql.AppendFormat("   ( '{0}','{1}','{2}',{3},'{4}','{5}')",
                 ma_viewCountId, registId, pageId, viewNum, viewDate, lang);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }

        /// <summary>
        /// 取得产品点击率
        /// </summary>
        /// <param name="registId">注册ID</param>
        /// <param name="lang">语言</param>
        /// <param name="date">日期</param>
        /// <param name="dateType">日期类型</param>
        public DataTable GetProdCount(String registId, String lang, String date, String dateType)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select PAGEID AS PAGEID,");
            orgsql.AppendLine("  sum(a.C1) AS C1,");
            orgsql.AppendLine("  sum(a.C2) AS C2,");
            orgsql.AppendLine("  sum(a.C3) AS C3,");
            orgsql.AppendLine("  sum(a.C4) AS C4,");
            orgsql.AppendLine("  sum(a.C5) AS C5,");
            orgsql.AppendLine("  sum(a.C6) AS C6,");
            orgsql.AppendLine("  sum(a.C7) AS C7,");
            orgsql.AppendLine("  sum(a.C8) AS C8,");
            orgsql.AppendLine("  sum(a.C9) AS C9,");
            orgsql.AppendLine("  sum(a.C10) AS C10,");
            orgsql.AppendLine("  sum(a.C11) AS C11,");
            orgsql.AppendLine("  sum(a.C12) AS C12,");
            orgsql.AppendLine("  sum(a.C13) AS C13,");
            orgsql.AppendLine("  sum(a.C14) AS C14,");
            orgsql.AppendLine("  sum(a.C15) AS C15");
            orgsql.AppendLine("FROM ");
            orgsql.AppendLine("  (    ");
            //第1列
            orgsql.AppendLine("    SELECT PAGEID, SUM(ISNULL(VIEWNUM,0)) C1, 0 C2, 0 C3, 0 C4, 0 C5, 0 C6, 0 C7, 0 C8, 0 C9, 0 C10");
            orgsql.AppendLine("    , 0 C11, 0 C12, 0 C13, 0 C14, 0 C15");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-15,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-14,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第2列
            orgsql.AppendLine("    SELECT PAGEID, 0 , SUM(ISNULL(VIEWNUM,0)), 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-14,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-13,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第3列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-13,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-12,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第4列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-12,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-11,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第5列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-11,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-10,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第6列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-10,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-9,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第7列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-9,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-8,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第8列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-8,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-7,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第9列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-7,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-6,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第10列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-6,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-5,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第11列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-5,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-4,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第12列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-4,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-3,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第13列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-3,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-2,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第14列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-2,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-1,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("  UNION ");
            //第15列
            orgsql.AppendLine("    SELECT PAGEID, 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),convert(datetime,'{0}'),112) ", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),convert(datetime,'{0}'),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-1,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),convert(datetime,'{0}'),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),convert(datetime,'{0}'),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY PAGEID     ");
            orgsql.AppendLine("       ) AS a");
            orgsql.AppendLine("    group by PAGEID");

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }


        /// <summary>
        /// 取得模块点击率
        /// </summary>
        /// <param name="registId">注册ID</param>
        /// <param name="lang">语言</param>
        /// <param name="date">日期</param>
        /// <param name="dateType">日期类型</param>
        public DataTable GetModCount(String registId, String lang, String date, String dateType)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("  select PAGEID AS PAGEID,");
            orgsql.AppendLine("  sum(a.C1) AS C1,");
            orgsql.AppendLine("  sum(a.C2) AS C2,");
            orgsql.AppendLine("  sum(a.C3) AS C3,");
            orgsql.AppendLine("  sum(a.C4) AS C4,");
            orgsql.AppendLine("  sum(a.C5) AS C5,");
            orgsql.AppendLine("  sum(a.C6) AS C6,");
            orgsql.AppendLine("  sum(a.C7) AS C7,");
            orgsql.AppendLine("  sum(a.C8) AS C8,");
            orgsql.AppendLine("  sum(a.C9) AS C9,");
            orgsql.AppendLine("  sum(a.C10) AS C10,");
            orgsql.AppendLine("  sum(a.C11) AS C11,");
            orgsql.AppendLine("  sum(a.C12) AS C12,");
            orgsql.AppendLine("  sum(a.C13) AS C13,");
            orgsql.AppendLine("  sum(a.C14) AS C14,");
            orgsql.AppendLine("  sum(a.C15) AS C15");
            orgsql.AppendLine("FROM ");
            orgsql.AppendLine("  (    ");
            //第1列
            orgsql.AppendLine("    SELECT left(PAGEID,5) pageid, SUM(ISNULL(VIEWNUM,0)) C1, 0 C2, 0 C3, 0 C4, 0 C5, 0 C6, 0 C7, 0 C8, 0 C9, 0 C10");
            orgsql.AppendLine("    , 0 C11, 0 C12, 0 C13, 0 C14, 0 C15");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-15,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-14,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-14,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第2列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , SUM(ISNULL(VIEWNUM,0)), 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-14,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-13,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-13,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第3列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-13,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-12,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-12,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第4列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-12,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-11,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-11,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第5列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-11,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-10,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-10,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第6列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-10,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-9,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-9,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第7列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-9,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-8,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-8,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第8列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-8,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-7,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-7,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第9列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-7,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-6,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-6,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第10列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-6,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-5,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-5,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第11列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-5,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-4,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-4,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第12列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-4,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-3,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-3,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第13列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-3,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-2,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-2,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第14列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) , 0 ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),dateadd(day,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),dateadd(month,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-2,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),dateadd(week,-1,convert(datetime,'{0}')),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),dateadd(year,-1,convert(datetime,'{0}')),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("  UNION ");
            //第15列
            orgsql.AppendLine("    SELECT left(PAGEID,5), 0 , 0, 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ");
            orgsql.AppendLine("    , 0 , 0 , 0 , 0 , SUM(ISNULL(VIEWNUM,0)) ");
            orgsql.AppendLine("    FROM MA_VIEWCOUNT");
            orgsql.AppendLine("    WHERE");
            if (date != null)
            {
                if (dateType == ViewDateType.DAY)
                {
                    orgsql.AppendFormat("   VIEWDATE=convert(varchar(8),convert(datetime,'{0}'),112) ", date);
                }
                else if (dateType == ViewDateType.MONTH)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,6) = convert(varchar(6),convert(datetime,'{0}'),112)", date);
                }
                else if (dateType == ViewDateType.WEEK)
                {
                    orgsql.AppendFormat("   viewdate between convert(varchar(8),dateadd(week,-1,convert(datetime,'{0}')+1),112)", date);
                    orgsql.AppendFormat("   and convert(varchar(8),convert(datetime,'{0}'),112)", date);
                }
                else if (dateType == ViewDateType.YEAR)
                {
                    orgsql.AppendFormat("   left(VIEWDATE,4) = convert(varchar(4),convert(datetime,'{0}'),112)", date);
                }
            }
            if (registId != null)
            {
                orgsql.AppendFormat("   AND REGISTID='{0}'", registId);
            }
            if (lang != null)
            {
                orgsql.AppendFormat("   AND LANG='{0}' ", lang);
            }
            orgsql.AppendLine("         AND isnull(DELFLG,'') <> 1");
            //orgsql.AppendLine("         AND left(PAGEID,10)='P3050P0030'");
            orgsql.AppendLine("             GROUP BY left(PAGEID,5)     ");
            orgsql.AppendLine("       ) AS a");
            orgsql.AppendLine("    group by a.pageid");

            return sql.ExecuteDataset(orgsql.ToString()).Tables[0];
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public bool DeleteViewCount(String registerId)
        {
            StringBuilder orgsql = new StringBuilder();
            orgsql.AppendLine("   DELETE");
            orgsql.AppendLine("        FROM MA_VIEWCOUNT");
            orgsql.AppendLine("   WHERE");
            orgsql.AppendFormat("       REGISTID = '{0}'", registerId);

            return sql.ExecuteStrQuery(orgsql.ToString());
        }
    }
}