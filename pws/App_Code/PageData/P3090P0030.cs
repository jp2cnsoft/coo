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
using Seika.Api;
using Seika.Db;
using System.Collections.Generic;

namespace Seika.COO.PageData
{
    /// <summary>
    /// ICP证书上传
    /// </summary>
    /// <remarks>
    /// 2009/01/16  李博
    /// </remarks>
    public class P3090P0030 : PageDataBase
    {
        public P3090P0030()
            : base()
        {
            
        }

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable dtCompany = ma_cominfo.GetCertCode(RegisterId);
            if (dtCompany != null && dtCompany.Rows.Count > 0)
            {
                CertCode = dtCompany.Rows[0]["CERTCODE"].ToString();
                CertName = dtCompany.Rows[0]["CERTNAME"].ToString();
            }

            BS_WEBLANG bs_weblang = new BS_WEBLANG(conn);
            Lang = bs_weblang.GetLang(RegisterId);

            conn.close();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public override void Save()
        {
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            ma_cominfo.SetCertCode(RegisterId, CertCode, CertName);

            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 检测cert号是否重复
        /// </summary>
        /// <param name="certCode"></param>
        /// <returns></returns>
        public bool CheckCertCode(String registId,String certCode) 
        {
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable dt = ma_cominfo.GetCertCodeCont(registId,certCode);

            conn.close();

            if (dt.Rows.Count > 0)
                return true;
            return false;
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
        public String WebLang
        {
            set;
            get;
        }

        /// <summary>
        /// 证书号
        /// </summary>
        public String CertCode
        {
            set;
            get;
        }

        /// <summary>
        /// 证书名
        /// </summary>
        public String CertName
        {
            set;
            get;
        }

        /// <summary>
        /// 激活语言表
        /// </summary>
        public DataTable Lang
        {
            set;
            get;
        }
    }
}