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
using System.Collections.Generic;
using System.Collections;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;
using Seika.Db;
using Seika.COO.DBA.MA;
using Seika.COO.PageData;

/// <summary>
/// 语言管理
/// </summary>
namespace Seika.COO.Web.PG
{
    public class LanguageManager : PageBase
    {
        SysLanguage lan;
        public LanguageManager()
        {
            lan = new SysLanguage();
        }

        /// <summary>
        /// 取得系统语言列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysLangList()
        {
            lan.Init();
            return lan.Language;
        }
        /// <summary>
        /// 取得当前系统语言
        /// </summary>
        /// <returns></returns>
        public String GetCurrLang(String currLang)
        {
            lan.InitCurrLangInfo(currLang);
            return lan.CurrLanguageRes;
        }

        /// <summary>
        /// 取得当前系统语言ID
        /// </summary>
        /// <returns></returns>
        public String GetCurrLangID()
        {
            return lan.CurrLanguageId;
        }

        /// <summary>
        /// 取得当前系统语言名称
        /// </summary>
        /// <returns></returns>
        public String GetCurrLangName()
        {
            return lan.CurrLanguageName;
        }

        /// <summary>
        /// 取得当前语言
        /// </summary>
        /// <returns></returns>
        public String GetCurrLangers(String currLangerId)
        {
            return lan.GetLangers(currLangerId);
        }

    }
}
