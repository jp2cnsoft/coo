<%@ WebService Language="C#" Class="Seika.COO.Api.CooImporter" %>

using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Data;
using Seika.COO.Action;
using Seika.Api;
using Seika;
using pd = Seika.COO.PageData;

/// <summary>
/// 找公司 找产品 找职位 帮助xml API
/// </summary>
/// <remarks>
/// 修正记录
/// -----------------------------------------------------------------
/// 2009/02/01      于 作伟     功能初设
/// </remarks>
namespace Seika.COO.Api
{
    [WebService(Namespace = "Seika.Coo.Api")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class CooImporter : System.Web.Services.WebService
    {

        public CooImporter()
        {
        }

        private static object _SyncRoot = new object();
        private static List<ApiImporterCollection> _apiList;
        private List<ApiImporterCollection> ApiList()
        {
            if (_apiList == null || _apiList.Count < 1 || _apiList[0].Count < 1)
            {
                //lock (_SyncRoot)
                //{
                    //if (_apiList == null)
                    //{
                        pd.CooImporter coo = new pd.CooImporter();
                        _apiList = coo.GetCommonList();
                //    }
                //}
            }

            return _apiList;
        }

        /// <summary>
        /// 行业
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetCalling(String language)
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 0)
            {
                DataView dv = _apiList[0].DaTable.DefaultView;
                dv.RowFilter = String.Format(" MA_LANGUAGEID = '{0}'", language);
                _apiList[0].DaTable = dv.ToTable();
                return _apiList[0];
            }
            return null;
        }
        
        /// <summary>
        /// 产品类别
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetProductType(String language)
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 1)
            {
                DataView dv = _apiList[1].DaTable.DefaultView;
                dv.RowFilter = String.Format(" MA_LANGUAGEID = '{0}'", language);
                _apiList[1].DaTable = dv.ToTable();
                return _apiList[1];
            }
            return null;
        }
        
        /// <summary>
        /// 职位类别
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetPostType(String language)
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 2)
            {
                DataView dv = _apiList[2].DaTable.DefaultView;
                dv.RowFilter = String.Format(" MA_LANGUAGEID = '{0}'", language);
                _apiList[2].DaTable = dv.ToTable();
                return _apiList[2];
            }
            return null;
        }
        /// <summary>
        /// 职位CODE
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetPostCode()
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 3)
            {
                return _apiList[3];
            }
            return null;
        }
        /// <summary>
        /// 地址
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetAddress(String language)
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 4)
            {
                DataView dv = _apiList[4].DaTable.DefaultView;
                dv.RowFilter = String.Format(" MA_LANGUAGEID = '{0}'", language);
                _apiList[4].DaTable = dv.ToTable();
                return _apiList[4];
            }
            return null;
        }
        
        /// <summary>
        /// 帮助xml
        /// </summary>
        /// <param name="language"></param>
        /// <param name="xmlName"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetWebSiteXml(String language,String xmlName)
        {
            ApiList();
            if (_apiList != null && _apiList.Count > 5)
            {
                DataView dv = _apiList[5].DaTable.DefaultView;
                dv.RowFilter = String.Format(" LANG = '{0}' and XMLNAME = '{1}'", language,xmlName);
                DataTable dt = dv.ToTable();
                foreach (DataRow r in dt.Rows) 
                {
                    _apiList[5].StrCode = r["XMLCON"].ToString();
                    break;
                }
                return _apiList[5];
            }
            return null;
        }
        /// <summary>
        /// 取得推荐公司
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetTopCompany(String language)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetTopCompany(language);
        }
        /// <summary>
        /// 取得推荐产品
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetTopProduct(String language)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetTopProduct(language);
        }
        /// <summary>
        /// 取得网站语言
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection GetWebSiteLanguage(String languageCode)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetLanguage(languageCode);
        }
        
        /// <summary>
        /// 选择地址
        /// </summary>
        /// <param name="language"></param>
        /// <param name="fsub"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchAddress(String fsub)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchAddressList(fsub);
        }

        /// <summary>
        /// 选择公司行业
        /// </summary>
        /// <param name="language"></param>
        /// <param name="fsub"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchCalling(String fsub)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchCompanyCallingList(fsub);
        }
        
        /// <summary>
        /// 选择产品类别
        /// </summary>
        /// <param name="language"></param>
        /// <param name="fsub"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchProductType(String fsub)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchProductCallingList(fsub);
        }

        /// <summary>
        /// 选择职业类别
        /// </summary>
        /// <param name="language"></param>
        /// <param name="fsub"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchPostType(String fsub)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchPostCallingList(fsub);
        }
        
        /// <summary>
        /// 公司搜索
        /// </summary>
        /// <param name="language"></param>
        /// <param name="comName"></param>
        /// <param name="comCalling"></param>
        /// <param name="comCountry"></param>
        /// <param name="comProvince"></param>
        /// <param name="comCity"></param>
        /// <param name="comBorough"></param>
        /// <param name="startIdx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchCompany(String language, String comName, String comCalling, String comCountry, String comProvince
            , String comCity, String comBorough, int startIdx, int count)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchCompanyList(language, comName, comCalling, comCountry, comProvince
            , comCity, comBorough, startIdx, count);
        }

        /// <summary>
        /// 产品搜索
        /// </summary>
        /// <param name="language"></param>
        /// <param name="comName"></param>
        /// <param name="comCalling"></param>
        /// <param name="comCountry"></param>
        /// <param name="comProvince"></param>
        /// <param name="comCity"></param>
        /// <param name="comBorough"></param>
        /// <param name="startIdx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchProduct(String language, String proName, String proCalling, String proCountry, String proProvince,
                                  String proCity, String proBorough, int startIdx, int count)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchProductList( language,  proName,  proCalling,  proCountry,  proProvince,
                                   proCity,  proBorough,  startIdx,  count);
        }

        /// <summary>
        /// 工作搜索
        /// </summary>
        /// <param name="language"></param>
        /// <param name="comName"></param>
        /// <param name="comCalling"></param>
        /// <param name="comCountry"></param>
        /// <param name="comProvince"></param>
        /// <param name="comCity"></param>
        /// <param name="comBorough"></param>
        /// <param name="startIdx"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public ApiImporterCollection SearchWork(String language, String wkName, String wkCalling, String wkCountry, String wkProvince,
                                  String wkCity, String wkBorough, String wkSchoolLevel, String wkSex, String wkAge,
                                  String wkKind, String wkExperience, String wkCreateDate, int startIdx, int count)
        {
            Seika.COO.PageData.CooImporter cooImp = new Seika.COO.PageData.CooImporter();
            return cooImp.GetSearchPostList(language, wkName, wkCalling, wkCountry, wkProvince,
                                   wkCity,  wkBorough,  wkSchoolLevel,  wkSex,  wkAge,
                                   wkKind,  wkExperience,  wkCreateDate,  startIdx,  count);
        }


        /// <summary>
        /// 通过父类编号返回些父类下的所有子类，客户端接收JSON数据结构对象数组
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回结果集</returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public ArrayList FindCityList(string parentId)
        {
            Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
            DataTable dt = ci.GetCityByParentId(parentId);
            ArrayList userList = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                MAZONECLASS mz = new MAZONECLASS();
                mz.MA_ORDERID = dr["MA_ORDERID"].ToString();
                mz.NAME = dr["NAME"].ToString();
                userList.Add(mz);
            }
            return userList;
        }

        /// <summary>
        /// 通过父类编号返回些父类下的所有子类，客户端接收JSON数据结构对象数组
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回结果集</returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public ArrayList FindKindList(string parentId)
        {
            Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
            DataTable dt = ci.GetKindByParentId(parentId);
            ArrayList userList = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                MAZONECLASS mz = new MAZONECLASS();
                mz.MA_ORDERID = dr["MA_ORDERID"].ToString();
                mz.NAME = dr["NAME"].ToString();
                userList.Add(mz);
            }
            return userList;
        }

        /// <summary>
        /// 通过父类编号返回些父类下的所有子类，客户端接收JSON数据结构对象数组
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回结果集</returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public ArrayList FindGoodsKindList(string parentId)
        {
            Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
            DataTable dt = ci.GetGoodsKindByParentId(parentId);
            ArrayList userList = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                MAZONECLASS mz = new MAZONECLASS();
                mz.MA_ORDERID = dr["MA_ORDERID"].ToString();
                mz.NAME = dr["NAME"].ToString();
                userList.Add(mz);
            }
            return userList;
        }

        /// <summary>
        /// 通过父类编号返回些父类下的所有子类，客户端接收JSON数据结构对象数组
        /// </summary>
        /// <param name="parentId">父类编号</param>
        /// <returns>返回结果集</returns>
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public ArrayList FindPostKindList(string parentId)
        {
            Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
            DataTable dt = ci.GetPostKindByParentId(parentId);
            ArrayList userList = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                MAZONECLASS mz = new MAZONECLASS();
                mz.MA_ORDERID = dr["MA_ORDERID"].ToString();
                mz.NAME = dr["NAME"].ToString();
                userList.Add(mz);
            }
            return userList;
        }

        // 用于返回类别的实体类
        public class MAZONECLASS
        {
            public string MA_ORDERID;
            public string NAME;
        }
    }

}