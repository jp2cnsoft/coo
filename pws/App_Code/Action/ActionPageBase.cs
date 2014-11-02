using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Data;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using Seika.COO.DBA.BS;
using System.Data.SqlClient;
using Seika.Util;
using Seika.Db;
using Seika.CooException;
using Seika.COO.Web.PG;
using Seika.Common.Net;

namespace Seika.COO.Action
{
    public abstract class ActionPageBase : ActionBase
    {

        public DataSetManage cmdt = null;
        public DataSet res = null;

        public MA_ZONECLASS ma_zone = null;
        public MA_CALLINGCLASS ma_calling = null;
        public MA_REGISTER ma_register = null;
        public MA_COMINFO ma_cominfo = null;
        public MA_PRODUCTCLASS ma_productclass = null;
        public MA_PRODUCT ma_product = null;
        public MA_POST ma_post = null;
        public MA_POSTCLASS ma_postclass = null;
        public MA_CODE ma_code = null;
        public ObjectStringTool m_objectStringTool = new ObjectStringTool();
        public SessionManager m_session = null;


        //下拉递归
        protected void GetList(DataSet ds, DataSetManage cmdt, MA_CLASSMANAGE ma_cm, String fid)
        {
            ds.Tables.Add(cmdt.GetCloneTable(ma_cm.GetOrgList(fid), fid));
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_CLASSMANAGEID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_CLASSMANAGEID"].ToString();
                GetList(ds, cmdt, ma_cm, fid);
            }   
        }

        protected void GetList(DataSet ds, DataSetManage cmdt, MA_CALLINGCLASS ma_calling, String fid)
        {
            ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgList(fid), fid));
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_ORDERID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_ORDERID"].ToString();
                GetList(ds, cmdt, ma_calling, fid);
            }        
        }

        protected void GetList(DataSet ds, DataSetManage cmdt, MA_PRODUCTCLASS ma_pro, String fid)
        {
            ds.Tables.Add(cmdt.GetCloneTable(ma_pro.GetOrgList(fid), fid));
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_PRODUCTCLASSID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_PRODUCTCLASSID"].ToString();
                GetList(ds, cmdt, ma_pro, fid);
            }        
        }

        protected void GetList(ref DataSet ds, DataSetManage cmdt, MA_ZONECLASS ma_zone, String fid)
        {
            ds.Tables.Add(ma_zone.GetOrgList(fid).Copy());
            ds.Tables[ds.Tables.Count - 1].TableName = fid;
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_ORDERID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_ORDERID"].ToString();
                GetList(ds, cmdt, ma_zone, fid);
            }       
        }

        protected void GetList(DataSet ds, DataSetManage cmdt, MA_ZONECLASS ma_zone, String fid)
        {
            ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(fid), fid));
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_ORDERID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_ORDERID"].ToString();
                GetList(ds, cmdt, ma_zone, fid);
            }
        }
        //postclass
        protected void GetList(DataSet ds, DataSetManage cmdt, MA_POSTCLASS ma_post, String fid)
        {
            ds.Tables.Add(cmdt.GetCloneTable(ma_post.GetOrgList(fid), fid));
            if (ds.Tables[fid].Rows.Count > 0 && ds.Tables[fid].Rows[0]["MA_ORDERID"] != null)
            {
                fid = ds.Tables[fid].Rows[0]["MA_ORDERID"].ToString();
                GetList(ds, cmdt, ma_post, fid);
            }
        }

        protected String StringToFilter(String str) 
        {
            return m_objectStringTool.StringToFilter(str);
        }

        protected void Resid2Name(String globalResName, ref DataTable dt)
        {
            ResManager rsMansge = new ResManager();
            int count = dt.Rows.Count;
            for (int i = count - 1;i>=0;i--)
            {
                String resid = dt.Rows[i]["RESID"].ToString();
                String name = rsMansge.GetGlobalRes(globalResName, resid);
                if(!String.IsNullOrEmpty(name))
                {
                     dt.Rows[i]["NAME"] = name;
                }
                else
                {
                    dt.Rows[i].Delete();
                }
            }
        }
    }
}