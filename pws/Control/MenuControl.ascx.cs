using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Seika;
using Seika.COO.Util;
using Seika.COO.Web.PG;
using Seika.CooException;

public partial class Control_MenuControl : ControlBase
{
    public XmlDocManage XmlDataSource
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        repMenu.ItemDataBound += new RepeaterItemEventHandler(repMenu_ItemDataBound);
        if (!IsPostBack)
        {
            DataSetManage ds = new DataSetManage(XmlDataSource);
            if (ds != null && ds.Get.Tables.Contains("item")) 
            {
                DataTable dt = ds.Get.Tables["item"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "manageshow='1'";
                repMenu.DataSource = dv.ToTable();
                repMenu.DataBind();
            }
        }
    }

    protected void repMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Request.FilePath.EndsWith(((HiddenField)e.Item.FindControl("hfdLink")).Value.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            ((Image)e.Item.FindControl("imgHead")).ImageUrl = "~/Images/book02.png";
        }
    }
}
