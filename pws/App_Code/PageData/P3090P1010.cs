using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using Seika.COO.DBA.MA;
using Seika.COO.DBA.BS;
using Seika.Api;
using Seika.Db;
using System.Collections.Generic;

namespace Seika.COO.PageData
{
    /// <summary>
    /// 语言管理后台
    /// </summary>
    /// <remarks>
    /// 2008/09/17  于作伟
    /// </remarks>
    public class P3090P1010 : PageDataBase
    {
        public P3090P1010()
            : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            MA_LANGUAGE ma_language = new MA_LANGUAGE(conn);
            WebLang = ma_language.GetLangCominfo(RegisterId);

            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();

            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);

            //更新关闭状态
            if (State == null) return;
            for (int i = 0; i < State.Count; i++)
            {
                if (State[i].Count < 2) break;
                bs_weblang.UpdataCloseFlg(RegisterId, State[i][0].ToString(), State[i][1].ToString());

            }

            //更新默认语言为未默认语言
            bs_weblang.UpdataNoneDefaulFlg(RegisterId);

            //更新当前默认语言
            bs_weblang.UpdataDefaulFlg(RegisterId, DefaultLang);

            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 删除当前语言网站数据库信息
        /// </summary>
        /// <param name="lang"></param>
        public void Delete(String lang) 
        {
            DBConnect conn = this.GetDbConnect();

            //删除用户网站语言表相关数据
            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);
            bs_weblang.DeleteWebLang(RegisterId, lang);

            //删除产品搜索关系表
            BS_PRODUCT bs_product = new BS_PRODUCT(conn);
            bs_product.DelProdeucBs(RegisterId, lang);

            //删除产品相关信息
            MA_PRODUCT ma_product = new MA_PRODUCT(conn);
            ma_product.DeleteProduct(RegisterId, lang);

            //删除用户语言基础xml
            MA_USERXML ma_userxml = new MA_USERXML(conn);
            ma_userxml.DeleteUserXml(RegisterId, lang);

            //删除提交
            conn.Commit();
            conn.close();
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

        /// <summary>
        /// 取消更新 
        /// </summary>
        public override void Cancel()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 注册ID
        /// </summary>
        public String RegisterId
        {
            set;
            get;
        }

        /// <summary>
        /// 语言信息
        /// </summary>
        public DataTable WebLang
        {
            set;
            get;
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        public List<ArrayList> State
        {
            set;
            get;
        }

        /// <summary>
        /// 默认语言
        /// </summary>
        public String DefaultLang
        {
            set;
            get;
        }
    }
}