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
using System.Collections.Generic;

namespace Seika.COO.PageData
{
    /// <summary>
    /// COO入口Api数据库操作部分
    /// </summary>
    /// <remarks>
    /// 2008/02/03  于作伟  新规作成
    /// </remarks>
    public class CooImporter : PageDataBase
    {
        static Hashtable dts = new Hashtable();
        String xmlNewsPath = String.Empty;

        public CooImporter()
            : base()
        {
        }

        public ApiImporterCollection GetLanguage(String languageCode)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DataTable dt;
            //语言一览数据不存在时
            if (!dts.ContainsKey("SysLanguage"))
            {
                DBConnect conn = this.GetDbConnect();
                //从数据库中取得语言一览数据
                dt = (new MA_LANGUAGE(conn)).GetLanguageInfo(languageCode);
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
                dv.RowFilter = "LANGSYS like '%" + languageCode + "%'";
                dt = dv.ToTable();
            }
            foreach (DataRow r in dt.Rows) 
            {
                ac.StrCode = r["MA_LANGUAGEID"].ToString();
                break;
            }
            return ac;
        }

        /// <summary>
        /// 取得基本信息一览
        /// </summary>
        /// <returns></returns>
        public List<ApiImporterCollection> GetCommonList()
        {
            List<ApiImporterCollection> acList = new List<ApiImporterCollection>();
            DBConnect conn = this.GetDbConnect();


            {
                //行业
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_CALLINGCLASS ma_callingclass = new MA_CALLINGCLASS(conn);
                ac.DaTable = ma_callingclass.GetOrgList();
                acList.Add(ac);
            }

            {
                //产品类别
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_PRODUCTCLASS ma_productclass = new MA_PRODUCTCLASS(conn);
                ac.DaTable = ma_productclass.GetOrgList();
                acList.Add(ac);
            }

            {
                //职位类别
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_POSTCLASS ma_postclass = new MA_POSTCLASS(conn);
                ac.DaTable = ma_postclass.GetOrgList();
                acList.Add(ac);
            }

            {
                DataSetManage cmdt = new DataSetManage();
                DataSet ds = new DataSet();
                //职位CODE
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_CODE ma_code = new MA_CODE(conn);
                //钱币类型
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetMoney(), "BANKROLLUNIT"));
                //性别
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetSex(), "SEXCLASS"));
                //学历
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetSchoollevel(), "SCHOOLLEVEL"));
                //工作性质
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetWorkkind(), "WORKKIND"));
                //年龄
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetAge(), "AGE"));
                //发布日期 
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetIssueDate(), "ISSUEDATE"));
                //工作经验
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetExperiene(), "EXPERIENCE"));
                ac.DaTableColl = ds;
                acList.Add(ac);
            }

            {
                //地址
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_ZONECLASS ma_zoneclass = new MA_ZONECLASS(conn);
                ac.DaTable = ma_zoneclass.GetOrgList();
                acList.Add(ac);
            }

            {
                //帮助
                ApiImporterCollection ac = new ApiImporterCollection();
                MA_BASICXML ma_basicxml = new MA_BASICXML(conn);
                ac.DaTable = ma_basicxml.GetBasicWebSiteList(UserSiteXmlManager.xmlWebSiteType);
                acList.Add(ac);
            }

            return acList;
        }

        

        /// <summary>
        /// 取得地址
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchAddressList(String fsub)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            MA_ZONECLASS ma_zone = new MA_ZONECLASS(conn);

            DataTable croot = new DataTable();
            croot.Columns.Add("MA_ORDERID");
            croot.Columns.Add("CLASSTYPE");
            croot.Columns.Add("NAME");
            GetAddressFSub(ma_zone, ref croot, fsub);
            ds.Tables.Add(cmdt.GetCloneTable(croot, "ADDRESS_ROOT"));
            ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(fsub), "ADDRESS"));

            ac.DaTableColl = ds;

            return ac;
        }

        //取得公司行业导航
        private void GetAddressFSub(MA_ZONECLASS ma_zoneclass, ref DataTable ddt, String fsub)
        {
            DataTable sdt = ma_zoneclass.GetAddressContent(fsub);
            foreach (DataRow r in sdt.Rows)
            {
                if (!String.IsNullOrEmpty(r["MA_ORDERID"].ToString()))
                {
                    DataRow dr = ddt.NewRow();
                    dr["MA_ORDERID"] = r["MA_ORDERID"].ToString();
                    dr["CLASSTYPE"] = r["CLASSTYPE"].ToString();
                    dr["NAME"] = r["NAME"].ToString();
                    ddt.Rows.InsertAt(dr, 0);
                    GetAddressFSub(ma_zoneclass, ref ddt, r["CLASSTYPE"].ToString());
                }
            }
        }

        /// <summary>
        /// 取得公司行业
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchCompanyCallingList(String fsub)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(conn);
            DataTable croot = new DataTable();
            croot.Columns.Add("MA_ORDERID");
            croot.Columns.Add("CLASSTYPE");
            croot.Columns.Add("NAME");
            GetCompanyCallingFSub(ma_calling, ref croot, fsub);
            ds.Tables.Add(cmdt.GetCloneTable(croot, "CALLING_ROOT"));
            ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetCallingCategory(fsub), "CALLING"));

            ac.DaTableColl = ds;

            return ac;
        }

        //取得公司行业导航
        private void GetCompanyCallingFSub(MA_CALLINGCLASS ma_calling, ref DataTable ddt, String fsub) 
        {
            DataTable sdt = ma_calling.GetOrgContent(fsub);
            foreach (DataRow r in sdt.Rows) 
            {
                if (!String.IsNullOrEmpty(r["MA_ORDERID"].ToString())) 
                {
                    DataRow dr = ddt.NewRow();
                    dr["MA_ORDERID"] = r["MA_ORDERID"].ToString();
                    dr["CLASSTYPE"] = r["CLASSTYPE"].ToString();
                    dr["NAME"] = r["NAME"].ToString();
                    ddt.Rows.InsertAt(dr, 0);
                    GetCompanyCallingFSub(ma_calling, ref ddt, r["CLASSTYPE"].ToString());
                }
            }
        }
        /// <summary>
        /// 取得推荐公司
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public ApiImporterCollection GetTopCompany(String language)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable pdt = ma_cominfo.GetComTitle(language);
            foreach (DataRow row in pdt.Rows)
            {
                String name = row["NAME"].ToString();
                if (name.Length >= 14)
                {
                    row["NAME"] = name.Substring(0, 14) + "...";
                }
            }
            ac.DaTable = pdt;
            return ac;

        }
        /// <summary>
        /// 取得推荐产品
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetTopProduct(String language) 
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            MA_PRODUCT ma_product = new MA_PRODUCT(conn);
            DataTable pdt = ma_product.GetProTitle(language);
            foreach (DataRow row in pdt.Rows)
            {
                String name = row["NAME"].ToString();
                if (name.Length >= 14)
                {
                    row["NAME"] = name.Substring(0, 14) + "...";
                }
            }
            ac.DaTable = pdt;
            return ac;
        }

        /// <summary>
        /// 取得产品类别
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchProductCallingList(String fsub)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            MA_PRODUCTCLASS ma_productclass = new MA_PRODUCTCLASS(conn);
            //ds.Tables.Add(cmdt.GetCloneTable(ma_productclass.GetOrgContent(fsub), "PRODUCT_ROOT"));
            DataTable pcroot = new DataTable();
            pcroot.Columns.Add("CATEGORYID");
            pcroot.Columns.Add("CLASSTYPE");
            pcroot.Columns.Add("NAME");
            GetProductCallingFSub(ma_productclass, ref pcroot, fsub);
            ds.Tables.Add(cmdt.GetCloneTable(pcroot, "PRODUCT_ROOT"));
            ds.Tables.Add(cmdt.GetCloneTable(ma_productclass.GetProductCategory(fsub), "PRODUCT"));

            ac.DaTableColl = ds;

            return ac;
        }
        /// 取得产品类别导航
        private void GetProductCallingFSub(MA_PRODUCTCLASS ma_productclass, ref DataTable ddt, String fsub)
        {
            DataTable sdt = ma_productclass.GetOrgContent(fsub);
            foreach (DataRow r in sdt.Rows)
            {
                if (!String.IsNullOrEmpty(r["CATEGORYID"].ToString()))
                {
                    DataRow dr = ddt.NewRow();
                    dr["CATEGORYID"] = r["CATEGORYID"].ToString();
                    dr["CLASSTYPE"] = r["CLASSTYPE"].ToString();
                    dr["NAME"] = r["NAME"].ToString();
                    ddt.Rows.InsertAt(dr, 0);
                    GetProductCallingFSub(ma_productclass, ref ddt, r["CLASSTYPE"].ToString());
                }
            }
        }

        /// <summary>
        /// 取得职业类别
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchPostCallingList(String fsub)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            MA_POSTCLASS ma_postclass = new MA_POSTCLASS(conn);
            //ds.Tables.Add(cmdt.GetCloneTable(ma_postclass.GetOrgContent(fsub), "POST_ROOT"));
            DataTable proot = new DataTable();
            proot.Columns.Add("CATEGORYID");
            proot.Columns.Add("CLASSTYPE");
            proot.Columns.Add("NAME");
            GetPostCallingFSub(ma_postclass, ref proot, fsub);
            ds.Tables.Add(cmdt.GetCloneTable(proot, "POST_ROOT"));
            ds.Tables.Add(cmdt.GetCloneTable(ma_postclass.GetPostCategory(fsub), "POST"));

            ac.DaTableColl = ds;

            return ac;
        }
        // 取得职业类别导航
        private void GetPostCallingFSub(MA_POSTCLASS ma_postclass, ref DataTable ddt, String fsub)
        {
            DataTable sdt = ma_postclass.GetOrgContent(fsub);
            foreach (DataRow r in sdt.Rows)
            {
                if (!String.IsNullOrEmpty(r["CATEGORYID"].ToString()))
                {
                    DataRow dr = ddt.NewRow();
                    dr["CATEGORYID"] = r["CATEGORYID"].ToString();
                    dr["CLASSTYPE"] = r["CLASSTYPE"].ToString();
                    dr["NAME"] = r["NAME"].ToString();
                    ddt.Rows.InsertAt(dr, 0);
                    GetPostCallingFSub(ma_postclass, ref ddt, r["CLASSTYPE"].ToString());
                }
            }
        }

        /// <summary>
        /// 取得公司一览
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchCompanyList(String language, String comName, String comCalling, String comCountry, String comProvince
        ,String comCity,String comBorough,int startIdx,int count) 
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            MA_COMINFO ma_cominfo = new MA_COMINFO(conn);
            DataTable dc = ma_cominfo.SearchCompanyCount(
                            StringToFilter(comName)
                            , StringToFilter((comCalling.ToLower() == comCountry.ToLower()) ? "" : comCalling)
                            , StringToFilter(comCountry)
                            , StringToFilter(comProvince)
                            , StringToFilter(comCity)
                            , StringToFilter(comBorough)
                            , language
                            );
            if (dc.Rows.Count > 0)
                ac.Count = Convert.ToInt16(dc.Rows[0][0].ToString());

            DataTable da = ma_cominfo.SearchCompany(
                            StringToFilter(comName)
                            , StringToFilter((comCalling.ToLower() == comCountry.ToLower()) ? "" : comCalling)
                            , StringToFilter(comCountry)
                            , StringToFilter(comProvince)
                            , StringToFilter(comCity)
                            , StringToFilter(comBorough)
                            , language
                            , startIdx
                            , count);

            ac.DaTable = da;

            return ac;
        }

        /// <summary>
        /// 取得产品一览
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchProductList(String language, String proName, String proCalling, String proCountry, String proProvince,
                                  String proCity, String proBorough, int startIdx, int count)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            MA_PRODUCT ma_product = new MA_PRODUCT(conn);
            DataTable dc = ma_product.SearchProductCount(
                            StringToFilter(proName)
                            , StringToFilter((proCalling.ToLower() == language.ToLower()) ? "" : proCalling)
                            , StringToFilter(proCountry)
                            , StringToFilter(proProvince)
                            , StringToFilter(proCity)
                            , StringToFilter(proBorough)
                            , language
                            );
            if (dc.Rows.Count > 0)
                ac.Count = Convert.ToInt16(dc.Rows[0][0].ToString());

            DataTable da = ma_product.SearchProduct(
                            StringToFilter(proName)
                            , StringToFilter((proCalling.ToLower() == language.ToLower()) ? "" : proCalling)
                            , StringToFilter(proCountry)
                            , StringToFilter(proProvince)
                            , StringToFilter(proCity)
                            , StringToFilter(proBorough)
                            , language
                            , startIdx
                            , count);

            ac.DaTable = da;

            return ac;
        }

        /// <summary>
        /// 取得招聘一览
        /// </summary>
        /// <returns></returns>
        public ApiImporterCollection GetSearchPostList(String language, String wkName, String wkCalling, String wkCountry, String wkProvince,
                                  String wkCity, String wkBorough, String wkSchoolLevel, String wkSex, String wkAge,
                                  String wkKind, String wkExperience, String wkCreateDate, int startIdx, int count)
        {
            ApiImporterCollection ac = new ApiImporterCollection();
            DBConnect conn = this.GetDbConnect();

            MA_POST ma_post = new MA_POST(conn);
            DataTable dc = ma_post.SearchPostCount(
                            StringToFilter(wkName)
                            , (wkCalling.ToLower() == wkCountry.ToLower()) ? "" : wkCalling
                            , wkCountry
                            , wkProvince
                            , wkCity
                            , wkBorough
                            , wkSchoolLevel
                            , wkSex
                            , wkAge
                            , wkKind
                            , wkExperience
                            , wkCreateDate
                            , language
                            );
            if (dc.Rows.Count > 0)
                ac.Count = Convert.ToInt16(dc.Rows[0][0].ToString());

            DataTable da = ma_post.SearchPost(
                            StringToFilter(wkName)
                            , (wkCalling.ToLower() == wkCountry.ToLower()) ? "" : wkCalling
                            , wkCountry
                            , wkProvince
                            , wkCity
                            , wkBorough
                            , wkSchoolLevel
                            , wkSex
                            , wkAge
                            , wkKind
                            , wkExperience
                            , wkCreateDate
                            , language
                            , startIdx
                            , count);

            ac.DaTable = da;

            return ac;
        }


        /// <summary>
        /// 返回指定父类别下的所有子类别
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回所有子类</returns>
        public DataTable GetCityByParentId(String parentId)
        {
            MA_ZONECLASS ma_zone = new MA_ZONECLASS(this.GetDbConnect());
            return ma_zone.GetAddressCategory(parentId);
        }

        /// <summary>
        /// 按指定ID号返回类别
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public DataTable GetCityById(string id)
        {
            MA_ZONECLASS ma_zone = new MA_ZONECLASS(this.GetDbConnect());
            return ma_zone.GetAddressContent(id);
        }


        /// <summary>
        /// 返回指定父类别下的所有子类别
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回所有子类</returns>
        public DataTable GetKindByParentId(String parentId)
        {
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(this.GetDbConnect());
            return ma_calling.GetCallingCategory(parentId);
        }

        /// <summary>
        /// 返回指定父类别下的所有子类别（产品类别）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetGoodsKindByParentId(String parentId)
        {
            MA_PRODUCTCLASS ma_prod = new MA_PRODUCTCLASS(this.GetDbConnect());
            return ma_prod.GetGoodsKindListByParentId(parentId);
        }

        /// <summary>
        /// 返回指定父类别下的所有子类别（产品类别）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetPostKindByParentId(String parentId)
        {
            MA_POSTCLASS ma_prod = new MA_POSTCLASS(this.GetDbConnect());
            return ma_prod.GetPostKindByParentId(parentId);
        }

        /// <summary>
        /// 按指定ID号返回类别
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public DataTable GetPostById(string id)
        {
            MA_POSTCLASS ma_calling = new MA_POSTCLASS(this.GetDbConnect());
            return ma_calling.GetOrgContent(id);
        }

        /// <summary>
        /// 按指定ID号返回类别
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public DataTable GetCallingById(string id)
        {
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(this.GetDbConnect());
            return ma_calling.GetOrgContent(id);
        }

        /// <summary>
        /// 按指定ID号返回类别
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public DataTable GetProdById(string id)
        {
            MA_PRODUCTCLASS ma_calling = new MA_PRODUCTCLASS(this.GetDbConnect());
            return ma_calling.GetOrgContent(id);
        }

        /// <summary>
        /// 按指定ID号返回类别
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public DataTable GetAddressById(string id)
        {
            MA_ZONECLASS ma_calling = new MA_ZONECLASS(this.GetDbConnect());
            return ma_calling.GetAddressContent(id);
        }

        

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init()
        {
            throw new System.Exception("The method or operation is not implemented.");

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

    }
}
