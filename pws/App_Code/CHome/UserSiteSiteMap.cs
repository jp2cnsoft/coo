using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections;
using Seika;
using Seika.Common.Net;
using System.Xml;
using Seika.COO.Web.PG;

/// <summary>
/// 网站地图定义
/// </summary>
/// <remarks>
/// 2008/10/17  于作伟  新规作成
namespace Seika.COO.Web.PG
{
    public class UserSiteSiteMap
    {
        ResManager m_rsMansge = new ResManager();

        XmlDocMangContent XmlDocList
        {
            get;
            set;
        }
        String UserId
        {
            get;
            set;
        }

        String Language
        {
            get;
            set;
        }

        String RootNode
        {
            get;
            set;
        }

        PageXmlSymbol m_pageXmlSym = new PageXmlSymbol();

        public UserSiteSiteMap(String userId, String language, String rootNode,XmlDocMangContent xmlDocList)
        {
            UserId = userId;
            Language = language;
            XmlDocList = xmlDocList;
            RootNode = rootNode;
        }

        public MemoryStream GetStream()
        {
            XmlDocManage xmlDocMang = new XmlDocManage();
            xmlDocMang.SAttribute = "linkid";
            Navi(xmlDocMang);
            Comsubmenu(xmlDocMang);
            P3040P0010(xmlDocMang);
            P3050P0010(xmlDocMang);
            return xmlDocMang.GetXmlDocStream();
        }

        public XmlDocManage GetXmlDoc()
        {
            XmlDocManage xmlDocMang = new XmlDocManage();
            xmlDocMang.SAttribute = "linkid";
            Navi(xmlDocMang);
            Comsubmenu(xmlDocMang);
            P3040P0010(xmlDocMang);
            P3050P0010(xmlDocMang);
            return xmlDocMang;
        }

        public void Navi(XmlDocManage xmlDocMang)
        {
            DataSetManage dsMang = new DataSetManage(XmlDocList[m_pageXmlSym.Menu]);
            DataTable dtnavi = dsMang.Get.Tables["mainitem"];

            DataTable dtmap = new DataTable("map");
            dtmap.Columns.Add("linkid");
            dtmap.Columns.Add("linkname");

            if (dtnavi != null)
            {
                foreach (DataRow row in dtnavi.Rows)
                {
                    if (row["id"].ToString() != "manage" && row["menushow"].ToString() == "1")
                    {
                        DataRow drmap = dtmap.NewRow();
                        drmap["linkid"] = row["url"].ToString();
                        drmap["linkname"] = row["content"].ToString();
                        dtmap.Rows.Add(drmap);
                    }
                }
            }
            ArrayList tPropert = new ArrayList();
            tPropert.Add("linkid");
            tPropert.Add("linkname");
            xmlDocMang.UpdateLocalXml("chxml/"+ RootNode, dtmap, tPropert, true, null);

        }

        public void P3040P0010(XmlDocManage xmlDocMang)
        {
            String xpath = "chxml/" + RootNode + "/map[@linkid='P3040P0010_000_0']";
            if (xmlDocMang.GetXmlNode(xpath) == null)
                return;
            DataSetManage dsMang = new DataSetManage(XmlDocList[m_pageXmlSym.P3040P0900]);
            DataTable dtp = dsMang.Get.Tables["name"];

            DataTable dtmap = new DataTable("map");

            dtmap.Columns.Add("linkid");
            dtmap.Columns.Add("linkname");

            string linkid = "P3040P0010_000_0";
            DataRow drmap = dtmap.NewRow();
            drmap["linkid"] = linkid.ToString();
            drmap["linkname"] = m_rsMansge.GetGlobalPageResource("ALLNEWS");
            dtmap.Rows.Add(drmap);

            if (dtp != null)
            {
                foreach (DataRow row in dtp.Rows)
                {
                    DataRow dr = dtmap.NewRow();
                    string rLinkid = "P3040P0010_" + row["typeid"].ToString() + "_0";
                    dr["linkid"] = rLinkid.ToString();
                    dr["linkname"] = row["con"].ToString();
                    dtmap.Rows.Add(dr);
                }
            }
            ArrayList tPropert = new ArrayList();
            tPropert.Add("linkid");
            tPropert.Add("linkname");
            xmlDocMang.UpdateLocalXml(xpath, dtmap, tPropert, true, null);
        }

        public void Comsubmenu(XmlDocManage xmlDocMang)
        {
            String xpath = "chxml/"+RootNode+"/map[@linkid='P3010P0310']";
            if (xmlDocMang.GetXmlNode(xpath) == null)
                return;
            DataSetManage dsMang = new DataSetManage(XmlDocList[m_pageXmlSym.Menu]);
            DataTable dtp = dsMang.Get.Tables["item"];

            DataTable dtmap = new DataTable("map");

            dtmap.Columns.Add("linkid");
            dtmap.Columns.Add("linkname");

            if (dtp != null)
            {
                foreach (DataRow row in dtp.Rows)
                {
                    if (row["menushow"].ToString() == "1")
                    {
                        DataRow dr = dtmap.NewRow();
                        dr["linkid"] = row["url"].ToString();
                        dr["linkname"] = row["content"].ToString();
                        dtmap.Rows.Add(dr);
                    }
                }
            }
            else
            {
                string linkid = "P3010P0310";
                DataRow drmap = dtmap.NewRow();
                drmap["linkid"] = linkid.ToString();
                drmap["linkname"] = m_rsMansge.GetGlobalPageResource("COMMEMO");
                dtmap.Rows.Add(drmap);
            }

            ArrayList tPropert = new ArrayList();
            tPropert.Add("linkid");
            tPropert.Add("linkname");
            xmlDocMang.UpdateLocalXml(xpath, dtmap, tPropert, true, null);

        }

        public void P3050P0010(XmlDocManage xmlDocMang)
        {
            String xpath = "chxml/"+RootNode+"/map[@linkid='P3050P0010_000_0']";
            if (xmlDocMang.GetXmlNode(xpath) == null)
                return;
            DataSetManage dsMang = new DataSetManage(XmlDocList[m_pageXmlSym.P3050P0900]);
            DataTable dtp = dsMang.Get.Tables["name"];

            DataTable dtmap = new DataTable("map");
            dtmap.Columns.Add("linkid");
            dtmap.Columns.Add("linkname");

            string linkid = "P3050P0010_000_0";
            DataRow drmap = dtmap.NewRow();
            drmap["linkid"] = linkid.ToString();
            drmap["linkname"] = m_rsMansge.GetGlobalPageResource("ALLPRODUCT");
            dtmap.Rows.Add(drmap);

            if (dtp != null)
            {
                foreach (DataRow row in dtp.Rows)
                {
                    string rLinkid = "P3050P0010" + "_" + row["typeid"].ToString() + "_0";
                    DataRow dr = dtmap.NewRow();

                    dr["linkid"] = rLinkid.ToString();
                    dr["linkname"] = row["con"].ToString();
                    dtmap.Rows.Add(dr);
                }
            }
            ArrayList tPropert = new ArrayList();
            tPropert.Add("linkid");
            tPropert.Add("linkname");
            xmlDocMang.UpdateLocalXml(xpath, dtmap, tPropert, true, null);

        }
    }
}