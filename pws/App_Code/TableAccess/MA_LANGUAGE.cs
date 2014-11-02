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
    public class MA_LANGUAGE : Seika.COO.DBA.DateBaseAccess
    {
        public MA_LANGUAGE(DBConnect sql)
            : base(sql)
	    {
	    }

        //获得语言列表
        public DataTable GetLanguage()
        {
            StringBuilder _cmd = new StringBuilder();
            _cmd.AppendLine("  SELECT ");
            _cmd.AppendLine("         MA_LANGUAGEID");
            _cmd.AppendLine("         ,LANGSYS");
            _cmd.AppendLine("         ,LANGRES");
            _cmd.AppendLine("         ,LANGNAME");
            _cmd.AppendLine("         ,SHOWFLG");
            _cmd.AppendLine("  FROM  MA_LANGUAGE");
            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }

        //获得语言列表
        public DataTable GetRegisterLanguage(String registerId)
        {
            StringBuilder _cmd = new StringBuilder();
            _cmd.AppendLine("  SELECT ");
            _cmd.AppendLine("         a.MA_LANGUAGEID");
            _cmd.AppendLine("         ,a.LANGSYS");
            _cmd.AppendLine("         ,a.LANGRES");
            _cmd.AppendLine("         ,a.LANGNAME");
            _cmd.AppendLine("         ,b.REGISTID");
            _cmd.AppendLine("  FROM  MA_LANGUAGE a");
            _cmd.AppendLine("  LEFT JOIN ");
            _cmd.AppendLine("  BS_WEBLANG b ");
            _cmd.AppendLine("  ON a.MA_LANGUAGEID = b.MA_LANGUAGEID ");
            _cmd.AppendFormat("  AND     (b.REGISTID = '{0}')", registerId);
            _cmd.AppendLine("  ORDER BY a.ORDERLANG ASC");
            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }

        //获得当前语言信息
        public DataTable GetLanguageInfo(String currSysLang)
        {
            StringBuilder _cmd = new StringBuilder();
            _cmd.AppendLine("  SELECT ");
            _cmd.AppendLine("         MA_LANGUAGEID");
            _cmd.AppendLine("         ,LANGSYS");
            _cmd.AppendLine("         ,LANGRES");
            _cmd.AppendLine("         ,LANGNAME");
            _cmd.AppendLine("  FROM  MA_LANGUAGE");
            _cmd.AppendLine("  WHERE ");
            _cmd.AppendFormat("  LANGSYS  LIKE '%{0}%' ", currSysLang);
            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }

        //获得当前语言
        public DataTable GetLangres(String currLanguageId)
        {
            StringBuilder _cmd = new StringBuilder();
            _cmd.AppendLine("  SELECT ");
            _cmd.AppendLine("         LANGRES");
            _cmd.AppendLine("  FROM MA_LANGUAGE");
            _cmd.AppendLine("  WHERE");
            _cmd.AppendFormat("      MA_LANGUAGEID = '{0}'", currLanguageId);
            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }

        //获得当前语言用户信息
        public DataTable GetLangCominfo(String registerId)
        {
            StringBuilder _cmd = new StringBuilder();

            _cmd.AppendLine("SELECT a.MA_LANGUAGEID ");
            _cmd.AppendLine("       ,a.LANGNAME ");
            _cmd.AppendLine("       ,a.LANGRES ");
            _cmd.AppendLine("       ,a.ORDERLANG ");
            _cmd.AppendLine("       ,a.ORDERLANGCN ");
            _cmd.AppendLine("       ,b.REGISTID ");
            _cmd.AppendLine("       ,b.MA_LANGUAGEID AS LANG ");
            _cmd.AppendLine("       ,b.MA_STYLEID ");
            _cmd.AppendLine("       ,b.DEFAULFLG ");
            _cmd.AppendLine("       ,b.OPENFLG ");
            _cmd.AppendLine("       ,c.XSLNAME ");
            _cmd.AppendLine("       ,c.CSSNAME ");
            _cmd.AppendLine("       ,datediff(day,getdate(),d.SERVICEENDDATE) as LIMITDATE ");
            _cmd.AppendLine("       ,e.COUNTRYID ");
            _cmd.AppendLine(" FROM MA_LANGUAGE a ");
            _cmd.AppendLine(" LEFT JOIN BS_WEBLANG b ");
            _cmd.AppendLine("       ON b.MA_LANGUAGEID=a.MA_LANGUAGEID ");
            _cmd.AppendFormat("     AND  b.REGISTID='{0}' ", registerId);
            _cmd.AppendLine(" LEFT JOIN MA_STYLE c ");
            _cmd.AppendLine("       ON b.MA_STYLEID=c.MA_STYLEID ");
            _cmd.AppendLine(" LEFT JOIN MA_REGISTER d ");
            _cmd.AppendLine("       ON d.REGISTID=b.REGISTID ");
            _cmd.AppendLine(" LEFT JOIN MA_COMINFO e ");
            _cmd.AppendFormat("       ON e.REGISTID='{0}' ", registerId);
            //_cmd.AppendLine("       ORDER BY a.ORDERLANG");

            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }

        //网站点击率查询获取语言列表
        public DataTable GetLang(String registerId, String lang)
        {
            StringBuilder _cmd = new StringBuilder();

            _cmd.AppendLine("SELECT a.MA_LANGUAGEID ");
            _cmd.AppendLine("       ,a.LANGNAME ");
            _cmd.AppendLine("       ,a.ORDERLANG ");
            _cmd.AppendLine("       ,a.ORDERLANGCN ");
            _cmd.AppendLine("       ,b.DEFAULFLG ");
            _cmd.AppendLine(" FROM MA_LANGUAGE a ");
            _cmd.AppendLine(" LEFT JOIN BS_WEBLANG b ");
            _cmd.AppendLine("       ON b.MA_LANGUAGEID=a.MA_LANGUAGEID ");
            _cmd.AppendFormat("     where b.REGISTID='{0}' ", registerId);
            if (lang == "china")
            {
                _cmd.AppendLine("     ORDER BY a.ORDERLANGCN ASC");
            }
            else if (lang == "japan")
            {
                _cmd.AppendLine("     ORDER BY a.ORDERLANG ASC");
            }

            return sql.ExecuteDataset(_cmd.ToString()).Tables[0];
        }
    }
}