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
using Seika.COO.Web.PG;

public partial class Control_SaveSuccess : ControlBase
{
    public String Remark
    {
        set { ViewState["Remark"] = value; }
        get
        {
            if (ViewState["Remark"] == null)
            {
                return GetResource("SAVESUCCESS");
            }
            return (String)ViewState["Remark"];
        }
    }
    //String remark = String.Empty;
    //public String Remark
    //{
    //    set { remark = value; }
    //    get { return remark; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        lblRemark.Text = Remark;
    }
}
