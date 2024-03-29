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
    /// 数据库xml操作
    /// </summary>
    /// <remarks>
    /// 2008/12/24  于作伟  新规作成
    /// </remarks>
    public class UserSiteXmlManagerData : PageDataBase
    {

        public UserSiteXmlManagerData()
            : base()
        {}

        /// <summary>
        /// 初期处理
        /// </summary>
        public override void Init(){}

        /// <summary>
        /// 插入基础Xml到用户表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="tradeName"></param>
        /// <param name="havingFlg"></param>
        public void InsertCommonUserXml(String registId,String language,String tradeName,String havingFlg)
        {
            DBConnect conn = this.GetDbConnect();
            MA_USERXML userXml = (new MA_USERXML(conn));
            userXml.DeleteBasicUserXml(registId, language);
            //conn.Commit();
            //conn.open();
            userXml.InsertBasicUserXml(registId, language, tradeName, havingFlg);
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 取得基础xml列表
        /// </summary>
        /// <param name="language"></param>
        /// <param name="havingFlg"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public DataTable GetBasicXmlList(String language,String havingFlg, String[] userXmlId)
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = new DataTable();
            dt.Columns.Add("XMLCON");
            dt.Columns.Add("XMLNAME");
            foreach (String s in userXmlId)
            {
                DataTable dtr = (new MA_BASICXML(conn)).GetBasicXmlList(language, havingFlg, s);
                foreach (DataRow r in dtr.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["XMLCON"] = r["XMLCON"].ToString();
                    dr["XMLNAME"] = r["XMLNAME"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            conn.close();
            return dt;
        }
        /// <summary>
        /// 取得基础行业xml列表
        /// </summary>
        /// <param name="language"></param>
        /// <param name="partitionflg"></param>
        /// <param name="userXmlId"></param>
        /// <returns></returns>
        public DataTable GetBasicXmlListPart(String language, String partitionflg, String[] userXmlId)
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = new DataTable();
            dt.Columns.Add("XMLCON");
            dt.Columns.Add("XMLNAME");
            foreach (String s in userXmlId)
            {
                DataTable dtr = (new MA_BASICXML(conn)).GetBasicXmlListPart(language, partitionflg, s);
                foreach (DataRow r in dtr.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["XMLCON"] = r["XMLCON"].ToString();
                    dr["XMLNAME"] = r["XMLNAME"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            conn.close();
            return dt;
        }

        /// <summary>
        /// 取得用户xml列表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="userXmlId"></param>
        public DataTable GetUserXmlList(String registId, String language, String[] userXmlId)
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = new DataTable();
            dt.Columns.Add("XMLCON");
            dt.Columns.Add("MA_USERXMLID");
            foreach (String s in userXmlId)
            {
                DataTable dtr = (new MA_USERXML(conn)).GetUserXml(registId, language, s);
                foreach (DataRow r in dtr.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["XMLCON"] = r["XMLCON"].ToString();
                    dr["MA_USERXMLID"] = r["MA_USERXMLID"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            conn.close();
            return dt;
        }

        /// <summary>
        /// 保存用户xml列表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <param name="dt"></param>
        public void SaveUserXmlList(String registId, String language, DataTable dt)
        {
            DBConnect conn = this.GetDbConnect();
            foreach (DataRow dr in dt.Rows)
            {
                String xmlCon = dr["XMLCON"].ToString();
                String xmlId = dr["MA_USERXMLID"].ToString();
               (new MA_USERXML(conn)).UpdateUserXml(registId, language, xmlId, xmlCon);
            }
            conn.Commit();
            conn.close();
        }

        /// <summary>
        /// 取得全部用户xml列表
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        public List<DataTable> GetUserXmlList(String registId, String language, String havingFlg)
        {
            List<DataTable> _list = new List<DataTable>();
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new MA_USERXML(conn)).GetUserXmlList(registId, language);
            DataTable dtb = (new MA_BASICXML(conn)).GetBasicXmlList(language,havingFlg);
            _list.Add(dt);
            _list.Add(dtb);
            conn.close();
            return _list;
        }
        /// <summary>
        /// 取得用户xml数据SEO关键字
        /// </summary>
        /// <param name="registId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public DataTable GetUserXmlListSeoKeyword(String registId, String language) 
        {
            DBConnect conn = this.GetDbConnect();
            DataTable dt = (new MA_SEOKEYWORD(conn)).GetSeoKeyWord(registId, language);
            conn.close();
            return dt;
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
