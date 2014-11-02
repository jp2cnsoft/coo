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
using Seika.CooException;
using Seika.COO.Action;
using System.IO;
using Seika;
using Seika.Db;
using Seika.COO.DBA.MA;

/// <summary>
/// Summary description for LoginManager
/// </summary>
namespace Seika.COO.Web.PG
{
    public class ServiceBuy : PageBase
    {
        DBConnect conn = null;
        
        public ServiceBuy() 
        {
            conn = DBConnectPool.GetConnect();
            conn.open();
        }

        public void ServiceVip() 
        {
            UpdateShowSta("1");
            UpdateXmlShowSta("1");
        }

        private void UpdateShowSta(String showSta)
        {
            (new MA_COMPANY(conn)).UpdateServiceVip(m_registId, showSta);
            conn.Commit();
        }

        private void UpdateXmlShowSta(String showSta) 
        {
            m_dsManage = new DataSetManage();
            //读取XML内容转为DataSet
            m_dsManage.ReadLocalXml2DataSet("chxml", m_xmlPath);
            //获得xml概述节点       
            DataTable dtHeader = m_dsManage.Get.Tables["header"];
            foreach (DataRow row in dtHeader.Rows) 
            {
                row["authentication"] = showSta;
            }
            m_dsManage.WriteLocalDataSet2Xml();
        }

        public void Close()
        {
            conn.close();
        }
    }
}