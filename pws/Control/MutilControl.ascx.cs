using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Seika.COO.Action;

public partial class MutilControl : System.Web.UI.UserControl
{
    private string m_RequestURL;

    private string m_kindIds;

    /// <summary>
    /// 选择项目父级节点集合
    /// </summary>
    public DataSet SelectTablePath
    {
        set { ViewState["SelectTablePath"] = value; }
        get
        {
            if (ViewState["SelectTablePath"] == null)
            {
                return null;
            }
            return (DataSet)ViewState["SelectTablePath"];
        }
    }

    public string KindIds
    {
        set
        {
            SetSelectTable(value);
            m_kindIds = Server.HtmlEncode(value);
        }
        get
        {
            return m_kindIds;
        }
    }

    // 设置请求WebService的URL
    public string RequestURL
    {
        get { return m_RequestURL; }
        set { m_RequestURL = value; }
    }

    public String AnyCategory
    {
        set
        {
            ViewState["AnyCategory"] = value;
        }
        get
        {
            return null != ViewState["AnyCategory"] ? ViewState["AnyCategory"].ToString() : "";
        }
    }

    public String AnyPostCategory
    {
        set
        {
            ViewState["AnyPostCategory"] = value;
        }
    }
    public String AnyIndustryCategory
    {
        set
        {
            ViewState["AnyIndustryCategory"] = value;
        }
    }

    /// <summary>
    /// 操作表名
    /// </summary>
    public String DbTableName
    {
        set { ViewState["dbTableName"] = value; }
        get
        {
            if (ViewState["dbTableName"] == null)
            {
                return null;
            }
            return (String)ViewState["dbTableName"];
        }
    }

    /// <summary>
    /// 处理action名称
    /// </summary>
    public String ActionName
    {
        set { ViewState["actionName"] = value; }
        get
        {
            if (ViewState["actionName"] == null)
            {
                return null;
            }
            return (String)ViewState["actionName"];
        }
    }

    private DataTable dtSelKind;

    public string GetKindId()
    {
        return Request["KindId"];
    }

    // 经营类别名称
    public string NAME;

    // 经营类别编号
    public string MA_ORDERID;

    // 经营类别类下的所有子类
    public DataTable ShowSelectItem;

    // 语言
    public String Language
    {
        set
        {
            ViewState["Language"] = value;
        }
        get
        {
            if (ViewState["Language"] == null)
                return "";
            return ViewState["Language"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (null != Request["KindId"] && !"".Equals(Request["KindId"]))
        {
            KindIds = Request["KindId"];
        }
 
        //ShowSelectItem = ci.GetKindByParentId(MA_ORDERID);
        if (Page.IsPostBack)
        {
            GetSelectTable();
        }

        // 实例数据访问层对旬
        Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();

        // 设置请求WebService的请求路径
        if (ACTION_NAMES.ORG_PRODUCTCATEGORY.Equals(AnyCategory))
        {
            // 返回指定父类下的所有子类
            DataTable dt = ci.GetProdById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            MA_ORDERID = dr["MA_ORDERID"].ToString();
            // 返回哪个经营类别的名称
            NAME = dr["NAME"].ToString();

            // 获得产品类别
            ShowSelectItem = ci.GetGoodsKindByParentId(MA_ORDERID);
            RequestURL = "../../Api/CooImporter.asmx/FindGoodsKindList";
        }
        else if (ACTION_NAMES.ORG_CALLINGCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetCallingById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            MA_ORDERID = dr["MA_ORDERID"].ToString();
            // 返回哪个经营类别的名称
            NAME = dr["NAME"].ToString();

            // 获得经营类别
            ShowSelectItem = ci.GetKindByParentId(MA_ORDERID);
            RequestURL = "../../Api/CooImporter.asmx/FindKindList";
        }
        else if (ACTION_NAMES.ORG_ADDRESSCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetAddressById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            MA_ORDERID = dr["MA_ORDERID"].ToString();
            // 返回哪个经营类别的名称
            NAME = dr["NAME"].ToString();

            // 获得地区类别
            ShowSelectItem = ci.GetCityByParentId(MA_ORDERID);
            RequestURL = "../../Api/CooImporter.asmx/FindCityList";
        }
        else if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetProdById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            MA_ORDERID = dr["MA_ORDERID"].ToString();
            // 返回哪个经营类别的名称
            NAME = dr["NAME"].ToString();

            // 获得经营类别
            ShowSelectItem = ci.GetPostKindByParentId(MA_ORDERID);
            RequestURL = "../../Api/CooImporter.asmx/FindPostKindList";
        }
    }

    public void SetSelectTable(string kindIs)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("NAME");
        string[] kinds = kindIs.Split('@');
        foreach (string k in kinds)
        {
            if (null != k && !"".Equals(k) && 2 == k.Split('=').Length)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = k.Split('=')[0];
                dr["NAME"] = k.Split('=')[1];
                dt.Rows.Add(dr);
            }
        }
        this.dtSelKind = dt;
    }

    /// <summary>
    /// 返回多地区时使用
    /// </summary>
    /// <returns></returns>
    public DataTable GetSelectTables()
    {
        Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
        DataTable dt = GetSelectTable();
        if (!dt.Columns.Contains("IDS"))
        {
            dt.Columns.Add("IDS");
        }
        foreach (DataRow dr in dt.Rows)
        {
            string parentId = dr["ID"].ToString();
            string IDS = "";
            while (true)
            {
                DataRow pdr = null;
                // 设置请求WebService的请求路径
                if (ACTION_NAMES.ORG_PRODUCTCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetProdById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_CALLINGCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetCallingById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_ADDRESSCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetAddressById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetPostById(parentId).Rows[0];
                }

                parentId = pdr["CLASSTYPE"].ToString();
                IDS = pdr[0].ToString() + "," + IDS;
                if ("ROOT".Equals(pdr["CLASSTYPE"]))
                {
                    break;
                }
            }
            dr["IDS"] = IDS;
        }
        

        return dt;
    }

    public void SetSelectTable(DataTable selTable)
    {
        Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
        selTable.Columns[0].ColumnName = "ID";
        this.dtSelKind = selTable;
        string kindIds = "";
        foreach (DataRow dr in selTable.Rows)
        {
            string parentId = dr["ID"].ToString();

            if ("".Equals(parentId))
            {
                break;
            }

            string NAME = "";
            while (true)
            {
                DataRow pdr = null;
                // 设置请求WebService的请求路径
                if (ACTION_NAMES.ORG_PRODUCTCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetProdById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_CALLINGCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetCallingById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_ADDRESSCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetAddressById(parentId).Rows[0];
                }
                else if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
                {
                    pdr = ci.GetPostById(parentId).Rows[0];
                }
                
                parentId = pdr["CLASSTYPE"].ToString();

                if (NAME.Length > 0)
                {
                    NAME = "&gt;" + NAME;
                }

                NAME = pdr["NAME"].ToString() + NAME;
                if ("ROOT".Equals(pdr["CLASSTYPE"]))
                {
                    break;
                }
            }

            dr["NAME"] = NAME;

            kindIds += dr["ID"] + "=" + dr["NAME"] + "@";
        }
        KindIds = kindIds;
    }


    //public void SetSelectTable(DataTable selTable) 
    //{
    //    selTable.Columns[0].ColumnName = "ID";
    //    this.dtSelKind = selTable;
    //    string kindIds = "";
    //    foreach (DataRow dr in selTable.Rows)
    //    {
    //        kindIds += dr["ID"] + "=" + dr["NAME"] + "@";
    //    }
    //    KindIds = kindIds;
    //}

    public DataTable GetSelectTable()
    {
        if (null == dtSelKind && null != GetKindId() && !"".Equals(GetKindId()))
        {
            SetSelectTable(GetKindId());
        }
        return dtSelKind;
    }
}
