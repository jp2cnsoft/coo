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
using System.IO;
using System.Xml;
using System.Text;
using Seika.COO.Web.PG;
using Seika;
using Seika.COO.Util;
using Seika.CooException;
using Seika.ServicesCol;
using Seika.COO.Action;
using System.Drawing;

public partial class Control_Category : System.Web.UI.UserControl
{
    public String AnyCategory
    {
        set 
        {
            ViewState["AnyCategory"] = value;
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
    public String Language
    {
        set
        {
            ViewState["Language"] = value;
        }
        get { if (ViewState["Language"] == null)
                return "";
            return ViewState["Language"].ToString();
        }
    }
    String currLangId = String.Empty;
    String currLangName = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void InitCalling()
    {
        InitLang();
        BindCallingAddress(currLangId, ViewState["AnyCategory"].ToString(),"Category",callingTitleList, rptCalling);
    }

    public void InitAddress()
    {
        InitLang();
        BindCallingAddress(currLangId, ACTION_NAMES.ORG_ADDRESS, "Address",countryTitleList,rptCountry);
    }

    public void InitPost()
    {
        InitLang();
        BindCallingAddress(currLangId, ViewState["AnyPostCategory"].ToString(), "Post", postTitleList, rptPost);
    }

    public void InitIndustry()
    {
        InitLang();
        BindCallingAddress(currLangId, ViewState["AnyIndustryCategory"].ToString(), "Industry", industryTitleList, rptIndustry);
    }

    private void InitLang() 
    {
        if (String.IsNullOrEmpty(Language))
        {
            SessionManager session = new SessionManager(Session);
            currLangId = session.Page_UICultureID;
        }
        else 
        {
            currLangId = Language;
        }
    }

    protected void lbtnAllCalling_Click(object sender, EventArgs e)
    {
        BindCallingAddress("ROOT", ViewState["AnyCategory"].ToString(), rptCalling);
        DataTable dt = ((DataTable)ViewState["Category"]).Clone();
        ViewState["Category"] = dt;
        BindCallingAddress("Category", callingTitleList);
    }

    protected void lbtnAllCountry_Click(object sender, EventArgs e)
    {
        BindCallingAddress("ROOT", ACTION_NAMES.ORG_ADDRESS, rptCountry);
        DataTable dt = ((DataTable)ViewState["Address"]).Clone();
        ViewState["Address"] = dt;
        BindCallingAddress("Address", countryTitleList);
    }

    protected void lbtnAllPost_Click(object sender, EventArgs e)
    {
        BindCallingAddress("ROOT", ViewState["AnyPostCategory"].ToString(), rptPost);
        DataTable dt = ((DataTable)ViewState["Post"]).Clone();
        ViewState["Post"] = dt;
        BindCallingAddress("Post", postTitleList);
    }

    protected void lbtnAllIndustry_Click(object sender, EventArgs e)
    {
        BindCallingAddress("ROOT", ViewState["AnyIndustryCategory"].ToString(), rptIndustry);
        DataTable dt = ((DataTable)ViewState["Industry"]).Clone();
        ViewState["Industry"] = dt;
        BindCallingAddress("Industry", industryTitleList);
    }

    //行业头绑定命令,用于头显示字符
    protected void callingTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblCallingLine")).Visible = true; }
    }
    //地域头绑定命令
    protected void countryTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblAddressLine")).Visible = true; }
    }
    //职位头绑定命令
    protected void postTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblPostLine")).Visible = true; }
    }
    protected void industryTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblIndustryLine")).Visible = true; }
    }

    protected void rptCalling_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyCategory"].ToString(), rptCalling);
        AddVDataSet("CATEGORYID", "Category", name, id);
        BindCallingAddress("Category", callingTitleList);
    }
    
    /// <summary>
    /// 选择地区2009-05-04(需要，修改)
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rptCountry_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        // 获得地区编号
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;

        // BindCallingAddress
        BindCallingAddress(id, ACTION_NAMES.ORG_ADDRESS, rptCountry);

        // 向ViewState添加，结构为DataTable
        AddVDataSet("MA_ORDERID", "Address", name, id);

        // 重新绑定
        BindCallingAddress("Address", countryTitleList);
    }

    protected void rptPost_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyPostCategory"].ToString(), rptPost);
        AddVDataSet("CATEGORYID", "Post", name, id);
        BindCallingAddress("Post", postTitleList);
    }

    protected void rptIndustry_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyIndustryCategory"].ToString(), rptIndustry);
        AddVDataSet("MA_ORDERID", "Industry", name, id);
        BindCallingAddress("Industry", industryTitleList);
    }

    protected void callingTitleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyCategory"].ToString(), rptCalling);
        RemoveVDataSet("CATEGORYID", "Category", id);
        BindCallingAddress("Category", callingTitleList);
    }

    protected void countryTitleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ACTION_NAMES.ORG_ADDRESS, rptCountry);
        RemoveVDataSet("MA_ORDERID", "Address", id);
        BindCallingAddress("Address", countryTitleList);
    }

    protected void postTitleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyPostCategory"].ToString(), rptPost);
        RemoveVDataSet("CATEGORYID", "Post", id);
        BindCallingAddress("Post", postTitleList);
    }

    protected void industryTitleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        String id = ((LinkButton)e.CommandSource).CommandName;
        String name = ((LinkButton)e.CommandSource).Text;
        BindCallingAddress(id, ViewState["AnyIndustryCategory"].ToString(), rptIndustry);
        RemoveVDataSet("MA_ORDERID", "Industry", id);
        BindCallingAddress("Industry", industryTitleList);
    }

    private void AddVDataSet(String mark, String id, String name, String mid)
    {
        DataTable dt;
        if (ViewState[id] == null)
        {
            dt = new DataTable();
            dt.Columns.Add(mark);
            dt.Columns.Add("NAME");
        }
        else 
        {
            dt = (DataTable)ViewState[id]; 
        }
        DataRow dr = dt.NewRow();
        dr[mark] = mid;
        dr["NAME"] = name;
        dt.Rows.Add(dr);
        ViewState[id] = dt;
    }

    private void RemoveVDataSet(String mark, String id, String mid) 
    {
        DataTable dt = ((DataTable)ViewState[id]).Clone();
        int i;
        for (i = 0; i < ((DataTable)ViewState[id]).Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[mark] = ((DataTable)ViewState[id]).Rows[i][mark];
            dr["NAME"] = ((DataTable)ViewState[id]).Rows[i]["NAME"];
            dt.Rows.Add(dr);
            if (((DataTable)ViewState[id]).Rows[i][mark].ToString() == mid)
            {
                break;
            }
        }
        ViewState[id] = dt;
    }
    private void BindCallingAddress(String id, String act,String vsList,Repeater dlTitle, Repeater dl)
    {
        ActionServer actser = new ActionServer(act);
        DataSetManage dataSetManage = new DataSetManage(act);
        dataSetManage.NewRow();
        dataSetManage.AddColumnAndValue("id", id);
        dataSetManage.RowBind();
        DataSet ds = actser.ServerStart(dataSetManage.Get, new String[] { "3" });
        dlTitle.DataSource = ViewState[vsList] = ds.Tables[0];
        dlTitle.DataBind();
        dl.DataSource = ds.Tables[1];
        dl.DataBind();
    }

    private void BindCallingAddress(String id, String act, Repeater dl)
    {
        ActionServer actser = new ActionServer(act);
        DataSetManage dataSetManage = new DataSetManage(act);
        dataSetManage.NewRow();
        dataSetManage.AddColumnAndValue("id", id);
        dataSetManage.RowBind();
        DataSet ds = actser.ServerStart(dataSetManage.Get, new String[] { "3" });
        dl.DataSource = ds.Tables[1];
        dl.DataBind();
    }

    private void BindCallingAddress(String id, Repeater dl)
    {
        dl.DataSource = (DataTable)ViewState[id];
        dl.DataBind();
    }

    public DataTable GetCalling()
    {
        return (DataTable)ViewState["Category"];
    }

    public DataTable GetPost()
    {
        return (DataTable)ViewState["Post"];
    }

    public DataTable GetAddress()
    {
        return (DataTable)ViewState["Address"];
    }

    public DataTable GetIndustry()
    {
        return (DataTable)ViewState["Industry"];
    }

    public void SetCallingAddressName(String cname,String aname)
    {
        lbtnAllCalling.Text = cname;
        lbtnAllCountry.Text = aname;
    }

    public void SetPostCallingAddressName(String pname,String cname,String aname)
    {
        lbtnAllPost.Text = pname;
        lbtnAllCalling.Text = cname;
        lbtnAllCountry.Text = aname;
    }
    public void SetIndustryPostCallingAddressName(String pname, String cname, String aname, String iname)
    {
        lbtnAllPost.Text = pname;
        lbtnAllCalling.Text = cname;
        lbtnAllCountry.Text = aname;
        lbtnAllIndustry.Text = iname;
    }

    /// <summary>
    /// 类型隐藏
    /// </summary>
    /// <param name="Visible"></param>
    public void SetPanelTypeVisible(bool Visible)
    {
        PanType.Visible = Visible;
    }

    /// <summary>
    /// 线隐藏
    /// </summary>
    /// <param name="Visible"></param>
    public void SetPanelLineVisible(bool Visible)
    {
        PanLine.Visible = Visible;
    }

    /// <summary>
    /// 地址隐藏
    /// </summary>
    /// <param name="Visible"></param>
    public void SetPanelAddressVisible(bool Visible)
    {
        PanAddress.Visible = Visible;
    }

    /// <summary>
    /// 职位显示
    /// </summary>
    /// <param name="Visible"></param>
    public void SetPanelPostVisible(bool Visible)
    {
        PanPost.Visible = Visible;
    }
    //行业显示
    public void SetPanelIndustryVisible(bool Visible)
    {
        panIndustry.Visible = Visible;
    }

    /// <summary>
    /// 类型列数
    /// </summary>
    /// <param name="count"></param>
    public void SetcCallingColumns(int count)
    {
        //rptCalling.RepeatColumns = count;
    }


    public void SetCallingLineVisible(bool Visible)
    {
        for (int i = 0; i < rptCalling.Items.Count; i++)
        {
            Panel panCallingline = (Panel)rptCalling.Items[i].FindControl("Pancallinglin");
            panCallingline.Visible = Visible;
        }
    }

    public void InitUpdateCalling(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            BindCallingAddress(dt.Rows[i]["id"].ToString(), ACTION_NAMES.ORG_PRODUCTCATEGORY, rptCalling);
            AddVDataSet("CATEGORYID", "Category", dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString());
            BindCallingAddress("Category", callingTitleList);
        }
    }

    public void InitUpdatePost(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            BindCallingAddress(dt.Rows[i]["id"].ToString(), ACTION_NAMES.ORG_POSTCATEGORY, rptPost);
            AddVDataSet("CATEGORYID", "Post", dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString());
            BindCallingAddress("Post", postTitleList);
        }
    }

    public void InitUpdateIndustry(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            BindCallingAddress(dt.Rows[i]["id"].ToString(), ACTION_NAMES.ORG_CALLINGCATEGORY, rptIndustry);
            AddVDataSet("CATEGORYID", "Industry", dt.Rows[i]["name"].ToString(), dt.Rows[i]["id"].ToString());
            BindCallingAddress("Industry", industryTitleList);
        }
    }

    public void InitUpdateAddress(DataTable dt)
    {
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    BindCallingAddress(dt.Rows[i]["MA_ORDERID"].ToString(), ACTION_NAMES.ORG_ADDRESSCATEGORY, rptCountry);
        //    AddVDataSet("CATEGORYID", "Category", dt.Rows[i]["NAME"].ToString(), dt.Rows[i]["MA_ORDERID"].ToString());
        //    BindCallingAddress("Category", countryTitleList);
        //}
        //取得表中结尾非空值并删除表中空记录
        String last = String.Empty;
        int count = dt.Rows.Count;
        for (int i = count - 1; i >= 0; i--) 
        {
            if (String.IsNullOrEmpty(dt.Rows[i][0].ToString()))
            {
                dt.Rows[i].Delete();
            }
            else 
            {
                last = dt.Rows[i][0].ToString();
                break;
            }
        }
        //如果表中没有任何值则初期化
        if (String.IsNullOrEmpty(last))
        {
            InitAddress();
        }
        else
        {
            //绑定头
            countryTitleList.DataSource = ViewState["Address"] = dt;
            countryTitleList.DataBind();
            //绑定内容
            BindCallingAddress(last, ACTION_NAMES.ORG_ADDRESS, rptCountry);
        }
    }

    /// <summary>
    /// 检查选择地址是否符合条件
    /// </summary>
    /// <returns></returns>
    public bool IsSelAddress() 
    {
        DataTable address = (DataTable)ViewState["Address"];
        int count = address.Rows.Count;
        if (count < 1) return false;

        String cateoryid = address.Rows[count - 1][0].ToString();
        ActionServer act= new ActionServer(ACTION_NAMES.ORG_ADDRESSCATEGORY);
        DataSet ds = GetSelectDs(cateoryid);
        DataSet ddlDs = act.ServerStart(ds);
        if (ddlDs == null) return false;

        DataTable dtx = ddlDs.Tables[1];
        if (dtx.Rows.Count > 0) return false;
        return true;
    }

    // 组成dataset单值
    private DataSet GetSelectDs(String selectValue)
    {
        DataSetManage dsm = new DataSetManage("SELECTTABLE");
        dsm.NewRow();
        dsm.AddColumnAndValue("SELECTLIST", selectValue);
        dsm.RowBind();
        return dsm.Get;
    }
    protected void rptCountry_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblCallingLine")).Visible = true; }
    }
    protected void rptCalling_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblCallingLine")).Visible = true; }
    }
    protected void rptPost_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblPostLine")).Visible = true; }
    }
    protected void rptIndustry_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblIndustryLine")).Visible = true; }
    }
    
    
    
    
    
}
