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
    /// 生成全部静态页处理
    /// </summary>
    /// <remarks>
    /// 2008/08/01  于作伟  新规作成
    /// </remarks>
    public class P3000P9999 : PageDataBase
    {
        public P3000P9999() : base()
        {
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            //取得全部用户信息
            RegisterList = (new MA_REGISTER(conn)).GetRegisterList();

            conn.close();

        }

        public void GetAddress() 
        {
            DBConnect conn = this.GetDbConnect();

            AddressName = new String[4];
            //取得地址名
            DataTable ad0 = (new MA_ZONECLASS(conn)).GetAddressContent(AddressId[0]);
            if (ad0.Rows.Count > 0)
                AddressName[0] = ad0.Rows[0]["NAME"].ToString();
            DataTable ad1 = (new MA_ZONECLASS(conn)).GetAddressContent(AddressId[1]);
            if (ad1.Rows.Count > 0)
                AddressName[1] = ad1.Rows[0]["NAME"].ToString();
            DataTable ad2 = (new MA_ZONECLASS(conn)).GetAddressContent(AddressId[2]);
            if (ad2.Rows.Count > 0)
                AddressName[2] = ad2.Rows[0]["NAME"].ToString();
            DataTable ad3 = (new MA_ZONECLASS(conn)).GetAddressContent(AddressId[3]);
            if (ad3.Rows.Count > 0)
                AddressName[3] = ad3.Rows[0]["NAME"].ToString();

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
        /// 全部公司信息列表
        /// </summary>
        public DataTable RegisterList
        {
            set;
            get;
        }

        /// <summary>
        /// 地址ID
        /// </summary>
        public String[] AddressId
        {
            get;
            set;
        }

        /// <summary>
        /// 地址名
        /// </summary>
        public String[] AddressName
        {
            get;
            set;
        }
    }
}
