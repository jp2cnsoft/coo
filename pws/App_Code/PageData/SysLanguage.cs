using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Seika.COO.DBA.MA;
using Seika.COO.Web.PG;
using Seika.Db;
using Seika.COO.DBA.SV;
using Seika.COO.DBA.DE;
using Seika.Api;
using Seika;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 语言数据取得
    /// </summary>
    /// <remarks>
    /// 2008/06/25  于作伟  新规作成
    /// </remarks>
    public class SysLanguage : PageDataBase
    {
        static Hashtable dts = new Hashtable();

        public SysLanguage()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            //语言一览数据不存在时
            if (!dts.ContainsKey("SysLanguage"))
            {
                //从数据库中取得语言一览数据
                DataTable dt = (new MA_LANGUAGE(conn)).GetLanguage();
                //保存数据
                if (!dts.ContainsKey("SysLanguage")) { dts.Add("SysLanguage", dt); } else { dts["SysLanguage"] = dt; }
            }

            conn.close();

        }

        /// <summary>
        /// 取得当前语言信息
        /// </summary>
        public void InitCurrLangInfo(String currSysLang)
        {
            DataTable dt;
            //语言一览数据不存在时
            if (!dts.ContainsKey("SysLanguage"))
            {
                DBConnect conn = this.GetDbConnect();
                //从数据库中取得语言一览数据
                dt = (new MA_LANGUAGE(conn)).GetLanguageInfo(currSysLang);
                conn.close();
            }
            //从本地缓存中读取
            else
            {
                //从缓存中读取
                DataTable SysLanguage = ((DataTable)dts["SysLanguage"]).Copy();
                //取得表中view
                DataView dv = SysLanguage.DefaultView;
                //过滤出当前值
                dv.RowFilter = "LANGSYS like '%" + currSysLang + "%'";
                dt = dv.ToTable();
            }

            if (dt != null || dt.Rows.Count < 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CurrLanguageId = row["MA_LANGUAGEID"].ToString();
                    CurrLanguageRes = row["LANGRES"].ToString();
                    CurrLanguageName = row["LANGNAME"].ToString();
                }
            }
            if (String.IsNullOrEmpty(CurrLanguageId) || String.IsNullOrEmpty(CurrLanguageId)) 
            {
                //默认语言ID
                CurrLanguageId = "CHINA";
                //默认语言资源
                CurrLanguageRes = "ZH-CN";
                //默认语言名称
                CurrLanguageName = "中文";
            }
        }

        /// <summary>
        /// 取得当前语言
        /// </summary>
        /// <returns></returns>
        public String GetLangers(String currLangersId)
        {
            DBConnect conn = this.GetDbConnect();

            //从数据库中取得语言
            DataTable dt = (new MA_LANGUAGE(conn)).GetLangres(currLangersId);
            conn.close();
            if (dt != null & dt.Rows.Count > 0)
            {
                return dt.Rows[0]["LANGRES"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 数据清空
        /// </summary>
        public override void Remove()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 取得数据
        /// </summary>
        public override void Load()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        //保存提交数据
        public override void Save()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 当前语言代码
        /// </summary>
        public String CurrLanguageRes
        {
            set 
            { 
                dts["LangRes"] = value; 
            }
            get
            {
                return (String)dts["LangRes"];
            }
        }

        /// <summary>
        /// 当前语言名称
        /// </summary>
        public String CurrLanguageName
        {
            set
            {
                dts["LangName"] = value;
            }
            get
            {
                return (String)dts["LangName"];
            }
        }

        /// <summary>
        /// 当前语言索引
        /// </summary>
        public String CurrLanguageId
        {
            set
            {
                dts["LangId"] = value;
            }
            get
            {
                return (String)dts["LangId"];
            }
        }

        /// <summary>
        /// 返回语言
        /// </summary>
        public DataTable Language
        {
            get
            {
                return (DataTable)dts["SysLanguage"];
            }
        }

    }
}
