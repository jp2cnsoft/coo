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

public partial class SimpleControl : System.Web.UI.UserControl
{

    private string m_RequestURL;

    // 设置请求WebService的URL
    public string RequestURL
    {
        get { return m_RequestURL; }
        set { m_RequestURL = value; }
    }

    public bool IsSelAddress() 
    {
        if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
        {
            if (null == CityId || "".Equals(CityId))
            {
                return false;
            }
            return true;
        }
        if (null == CityId || "".Equals(CityId))
        {
            return false;
        }
        return true;
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
    /// 返回选择国家编号
    /// </summary>
    /// <returns></returns>
    public string CountryId
    {
        set 
        {
            ViewState["CountryId"] = value;
        }
        get
        {
            return null == ViewState["CountryId"] ? "" : ViewState["CountryId"].ToString();
        }
    }

    /// <summary>
    /// 返回选择国家名称
    /// </summary>
    /// <returns></returns>
    public string CountryName
    {
        set 
        {
            ViewState["CountryName"] = value;
        }
        get
        {
            return null == ViewState["CountryName"] ? "" : ViewState["CountryName"].ToString();
        }
    }

    /// <summary>
    /// 返回选择省份的编号
    /// </summary>
    /// <returns></returns>
    public string ProvinceId
    {
        set 
        {
            ViewState["ProvinceId"] = value;
        }
        get
        {
            return null == ViewState["ProvinceId"] ? "" : ViewState["ProvinceId"].ToString();
        }
    }

    /// <summary>
    /// 返回选择省份的名称
    /// </summary>
    /// <returns></returns>
    public string ProvinceName
    {
        set
        {
            ViewState["ProvinceName"] = value;
        }
        get
        {
            return null == ViewState["ProvinceName"] ? "" : ViewState["ProvinceName"].ToString();
        }
    }

    /// <summary>
    /// 返回选择城市的编号
    /// </summary>
    /// <returns></returns>
    public string CityId
    {
        set
        {
            ViewState["CityId"] = value;
        }
        get
        {
            return null == ViewState["CityId"] ? "" : ViewState["CityId"].ToString();
        }
    }

    /// <summary>
    /// 返回选择城市的名称
    /// </summary>
    /// <returns></returns>
    public string CityName
    {
        set
        {
            ViewState["CityName"] = value;
        }
        get
        {
            return null == ViewState["CityName"] ? "" : ViewState["CityName"].ToString();
        }
    }

    /// <summary>
    /// 返回选择地区的名称
    /// </summary>
    /// <returns></returns>
    public string BoroughName
    {
        set
        {
            ViewState["BoroughName"] = value;
        }
        get
        {
            return null == ViewState["BoroughName"] ? "" : ViewState["BoroughName"].ToString();
        }
    }

    /// <summary>
    /// 返回选择地区的编号
    /// </summary>
    /// <returns></returns>
    public string BoroughId
    {
        set
        {
            ViewState["BoroughId"] = value;
        }
        get
        {
            return null == ViewState["BoroughId"] ? "" : ViewState["BoroughId"].ToString();
        }
    }

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

    // 国家类下的所有子类
    public DataTable ShowSelectItem;

    protected void Page_Load(object sender, EventArgs e)
    {

        // 接收表单
        SetAddress();

        if (Page.IsPostBack)
        {
            // 获得表单数据
            GetAddress();
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
            CountryId = dr[0].ToString();
            // 返回哪个经营类别的名称
            CountryName = dr["NAME"].ToString();

            // 获得产品类别
            ShowSelectItem = ci.GetGoodsKindByParentId(CountryId);
            RequestURL = "../../Api/CooImporter.asmx/FindGoodsKindList";
        }
        else if (ACTION_NAMES.ORG_CALLINGCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetCallingById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            CountryId = dr[0].ToString();
            // 返回哪个经营类别的名称
            CountryName = dr["NAME"].ToString();

            // 获得经营类别
            ShowSelectItem = ci.GetKindByParentId(CountryId);
            RequestURL = "../../Api/CooImporter.asmx/FindKindList";
        }
        else if (ACTION_NAMES.ORG_ADDRESSCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetAddressById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            CountryId = dr[0].ToString();
            // 返回哪个经营类别的名称
            CountryName = dr["NAME"].ToString();

            // 获得地区类别
            ShowSelectItem = ci.GetCityByParentId(CountryId);
            RequestURL = "../../Api/CooImporter.asmx/FindCityList";
        }
        else if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
        {

            // 返回指定父类下的所有子类
            DataTable dt = ci.GetPostById(Language);
            // 返回第一行
            DataRow dr = dt.Rows[0];
            // 返回哪个经营类别的编号
            CountryId = dr[0].ToString();
            // 返回哪个经营类别的名称
            CountryName = dr["NAME"].ToString();

            // 获得经营类别
            ShowSelectItem = ci.GetPostKindByParentId(CountryId);
            RequestURL = "../../Api/CooImporter.asmx/FindPostKindList";
        }
    }

    // 接收表单
    public void SetAddress()
    {
        if (null != Request["CountryId"] && !"".Equals(Request["CountryId"]))
        {
            CountryId = Request["CountryId"];
            CountryName = Request["CountryName"];
            if (null != Request["ProvinceId"] && !"".Equals(Request["ProvinceId"]))
            {
                ProvinceId = Request["ProvinceId"];
                ProvinceName = Request["ProvinceName"];
                if (null != Request["CityId"] && !"".Equals(Request["CityId"]))
                {
                    CityId = Request["CityId"];
                    CityName = Request["CityName"];
                    if (null != Request["BoroughId"] && !"".Equals(Request["BoroughId"]))
                    {
                        BoroughId = Request["BoroughId"];
                        BoroughName = Request["BoroughName"];
                    }
                }
            }
        }        
    }

    // 接收数据
    public void SetAddress(DataTable dt)
    {
        dt.Columns[0].ColumnName = "ID";
        dt.Columns[1].ColumnName = "NAME";

        if (1 == dt.Rows.Count)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                switch (x)
                {
                    case 0:
                        ProvinceId = dt.Rows[x]["ID"].ToString();
                        ProvinceName = dt.Rows[x]["NAME"].ToString();
                        break;
                }
            }
            Seika.COO.PageData.CooImporter ci = new Seika.COO.PageData.CooImporter();
            DataTable dt1 = null;
            // 设置请求WebService的请求路径
            if (ACTION_NAMES.ORG_PRODUCTCATEGORY.Equals(AnyCategory))
            {
 
            }
            else if (ACTION_NAMES.ORG_CALLINGCATEGORY.Equals(AnyCategory))
            {

            }
            else if (ACTION_NAMES.ORG_ADDRESSCATEGORY.Equals(AnyCategory))
            {

            }
            else if (ACTION_NAMES.ORG_POSTCATEGORY.Equals(AnyCategory))
            {
                dt1 = ci.GetPostKindByParentId(ci.GetPostById(ProvinceId).Rows[0]["CLASSTYPE"].ToString());
            }
            CountryId = dt1.Rows[0][0].ToString();
            CountryName = dt1.Rows[0][1].ToString();

        }
        else if (2 == dt.Rows.Count)
        {
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                switch (x)
                {
                    case 0:
                        ProvinceId = dt.Rows[x]["ID"].ToString();
                        ProvinceName = dt.Rows[x]["NAME"].ToString();
                        break;
                    case 1:
                        CityId = dt.Rows[x]["ID"].ToString();
                        CityName = dt.Rows[x]["NAME"].ToString();
                        break;
                }
            }
        }
        else
        {

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                switch (x)
                {
                    case 0:
                        CountryId = dt.Rows[x]["ID"].ToString();
                        CountryName = dt.Rows[x]["NAME"].ToString();
                        break;
                    case 1:
                        ProvinceId = dt.Rows[x]["ID"].ToString();
                        ProvinceName = dt.Rows[x]["NAME"].ToString();
                        break;
                    case 2:
                        CityId = dt.Rows[x]["ID"].ToString();
                        CityName = dt.Rows[x]["NAME"].ToString();
                        break;
                    case 3:
                        BoroughId = dt.Rows[x]["ID"].ToString();
                        BoroughName = dt.Rows[x]["NAME"].ToString();
                        break;
                }
            }
        }
    }

    // 获得表单数据
    public DataTable GetAddress()
    {
        DataTable dt = new DataTable();
        if (null != Request["CountryId"] && !"".Equals(Request["CountryId"]))
        {
            
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");  

            DataRow Country = dt.NewRow();
            Country["ID"] = Request["CountryId"];
            Country["NAME"] = Request["CountryName"];
            dt.Rows.Add(Country);

            if (null != Request["ProvinceId"] && !"".Equals(Request["ProvinceId"]))
            {
                DataRow Province = dt.NewRow();
                Province["ID"] = Request["ProvinceId"];
                Province["NAME"] = Request["ProvinceName"];
                dt.Rows.Add(Province);

                if (null != Request["CityId"] && !"".Equals(Request["CityId"]))
                {
                    DataRow City = dt.NewRow();
                    City["ID"] = Request["CityId"];
                    City["NAME"] = Request["CityName"];
                    dt.Rows.Add(City);
                    if (null != Request["BoroughId"] && !"".Equals(Request["BoroughId"]))
                    {
                        DataRow Borough = dt.NewRow();
                        Borough["ID"] = Request["BoroughId"];
                        Borough["NAME"] = Request["BoroughName"];
                        dt.Rows.Add(Borough);
                    }
                }
            }
        }

        return dt;
    }
}
