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
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.COO.Web.PG;
using Seika.CooException;

public partial class Control_GroupControl : ControlBase
{
    string[] seach = null;

    public DataTable Group
    {
        set
        {
            ViewState["group"] = value;
            ViewState["bakgp"] = value;
        }
        get { return (DataTable)ViewState["group"]; }
    }

    public String DataName
    {
        set { ViewState["dtname"] = value; }
        get { return (String)ViewState["dtname"]; }
    }

    public String StrName
    {
        set { ViewState["strName"] = value; }
        get { return (String)ViewState["strName"]; }
    }

    public bool NewLineState
    {
        set { ViewState["newLineState"] = value; }
        get { return (bool)ViewState["newLineState"]; }
    }

    public String AddButtonText 
    {
        set { ViewState["addButtonText"] = value; }
        get { return (String)ViewState["addButtonText"]; }
    }
    
    public string[] Seach
    {
        set { seach = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(AddButtonText)) 
        {
            btnAddGroup.Text = AddButtonText;
        }
    }

    /// <summary>
    /// 初期化控件
    /// </summary>
    public void InitControl()
    {
        
        //绑定详细列表
        //绑定详细列表
        chklstGroup.DataSource = (DataTable)ViewState["group"];
        chklstGroup.DataTextField = "con";
        chklstGroup.DataValueField = "typeid";
        chklstGroup.DataBind();

        //ViewState["bakgp"] = m_dsManage.GetCloneTable((DataTable)ViewState["group"], ((DataTable)ViewState["group"]).TableName.Trim());
        if (seach != null)
        {
            SeachTable(seach);
        }
        if (ViewState["group"] == null)
        {
            lblNullGroup.Text = (String)ViewState["strName"];
        }
    }

    /// <summary>
    /// 显示分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        String groupid = "";

        for (int i = 0; i < chklstGroup.Items.Count; i++)
        {   
            if (chklstGroup.Items[i].Selected)
            {
                groupid += chklstGroup.Items[i].Value.Trim() + ",";
            }
        }
        ViewState["Seach"] = groupid;
        pnlAddGroup.Visible = true;
        UnSetControlColor(txtNewGroup);
        lblErrorGroup.Text = "";
    }

    /// <summary>
    /// 取消分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGroupCancel_Click(object sender, EventArgs e)
    {
        pnlAddGroup.Visible = false;
        txtNewGroup.Text = "";
        UnSetControlColor(txtNewGroup);
    }

    /// <summary>
    /// 添加分组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CheckInput();
                    
            DataTable dt =  (DataTable)ViewState["group"];
            String dtname = "";
            if (dt == null)
            {
                dtname = ViewState["dtname"].ToString();
                AddCon(null, dtname);
            }
            else
            {
                lblNullGroup.Visible = false;
                dtname = dt.TableName;
                AddCon(m_dsManage.GetCloneTable(dt, dtname), dtname);
            }
            String seach = Convert.ToString(ViewState["Seach"]);
            SeachTable(seach.Split(','));
            lblNullGroup.Visible = false;
               
            pnlAddGroup.Visible = false;
        }
        catch (ExceptionMess em)
        {
            SendAppMsg(em);
        }
        
    }

    private void AddCon(DataTable dt,String dtname)
    {
        if (dt == null)
        {
            dt = new DataTable(dtname);
            dt.Columns.Add(new DataColumn("id"));
            dt.Columns.Add(new DataColumn("typeid"));
            dt.Columns.Add(new DataColumn("con"));
            dt.Columns.Add(new DataColumn("maxpage"));
        }

        int id = MaxId(dt, "id");

        DataRow dr = dt.NewRow();
        dr["id"]      = m_objStrTool.SetStringZero(id.ToString(), 3);
        dr["typeid"]  = System.DateTime.Now.ToString("yyMMddHHmmssfff");
        dr["con"]     = m_objStrTool.StringToFilter(txtNewGroup.Text.Trim());
        dr["maxpage"] = "0";
        dt.Rows.Add(dr);
        chklstGroup.DataSource = dt;
        chklstGroup.DataTextField = "con";
        chklstGroup.DataValueField = "typeid";
        chklstGroup.DataBind();

        txtNewGroup.Text = "";

        ViewState["group"] = dt;
    }

    /// <summary>
    /// 获得最大ID
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public int MaxId(DataTable dt, String id)
    {
        int maxId = 0;
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int count = Convert.ToInt32(dt.Rows[i][id]);
                if (maxId < Convert.ToInt32(dt.Rows[i][id]))
                {
                    maxId = Convert.ToInt32(dt.Rows[i][id]);
                }
            }
        }
        return maxId + 1;
    }

    /// <summary>
    ///  获得选择项
    /// </summary>
    /// <param name="dtname"></param>
    /// <returns></returns>
    public DataTable GetSeachGroup()
    {
        //设置新类别状态
        NewLineState = false;
        DataTable dt_group = (DataTable)ViewState["bakgp"];

        DataTable dtGroup = new DataTable("name");

        dtGroup.Columns.Add(new DataColumn("id"));
        dtGroup.Columns.Add(new DataColumn("typeid"));
        dtGroup.Columns.Add(new DataColumn("con"));
        dtGroup.Columns.Add(new DataColumn("maxpage"));
        dtGroup.Columns.Add(new DataColumn("status"));

        //ArrayList al = new ArrayList();
        int num = 0;
        String id = "";
        if (dt_group != null)
        {
            for (int i = 0; i < chklstGroup.Items.Count; i++)
            {
                if (chklstGroup.Items[i].Selected)
                {
                    bool blid = false;

                    DataRow dr = dtGroup.NewRow();
                    for (int k = 0; k < dt_group.Rows.Count; k++)
                    {
                        if (chklstGroup.Items[i].Value.Trim() == dt_group.Rows[k]["typeid"].ToString())
                        {
                            id = dt_group.Rows[k]["id"].ToString();
                            break;
                        }
                    }
                    dr["typeid"] = chklstGroup.Items[i].Value.Trim();
                    dr["maxpage"] = "0";
                    dr["con"] = chklstGroup.Items[i].Text.Trim();
                    for (int j = 0; j < dt_group.Rows.Count; j++)
                    {
                        if (chklstGroup.Items[i].Value.ToString() == dt_group.Rows[j]["typeid"].ToString())
                        {
                            blid = true;
                            break;
                        }
                    }
                    if (blid == false)
                    {
                        int inid = MaxId(dt_group, "id") + num;
                        dr["id"] = m_objStrTool.SetStringZero(inid.ToString(), 3);
                        dr["status"] = "N";
                        NewLineState = true;
                        num++;
                    }
                    else
                    {
                        dr["id"] = id;
                    }
                    dtGroup.Rows.Add(dr);
                }
            }
        }
        else
        {
            for (int i = 0; i < chklstGroup.Items.Count; i++)
            {
                if (chklstGroup.Items[i].Selected)
                {
                    DataRow dr = dtGroup.NewRow();
                    dr["typeid"] = chklstGroup.Items[i].Value.Trim();
                    dr["maxpage"] = "0";
                    dr["con"] = chklstGroup.Items[i].Text.Trim();
                    int inid = MaxId(dt_group, "id") + num;
                    dr["id"] = m_objStrTool.SetStringZero(inid.ToString(), 3);
                    dr["status"] = "N";
                    num++;
                    dtGroup.Rows.Add(dr);
                    NewLineState = true;
                }
            }
        }

        return dtGroup;
    }

    /// <summary>
    /// 初期化选中项
    /// </summary>
    /// <param name="seach"></param>
    public void SeachTable(string[] seach)
    {
        for (int i = 0; i < seach.Length; i++)
        {
            for (int j = 0; j < chklstGroup.Items.Count; j++)
            {
                if (seach[i].ToString() == chklstGroup.Items[j].Value.Trim())
                {
                    chklstGroup.Items[j].Selected = true;
                }
            }
        }

    }

    /// <summary>
    /// 验证
    /// </summary>
    private void CheckInput()
    {
        m_dsManage = new DataSetManage();
        m_epList = new ExceptionMess();
        UnSetControlColor(txtNewGroup);
        if (!String.IsNullOrEmpty(txtNewGroup.Text.Trim()))
        {
            if (txtNewGroup.Text.Trim().Length < 2 && txtNewGroup.Text.Trim().Length > 20)
            {
                SetControlColor(txtNewGroup);
                lblErrorGroup.Text = "";
                lblNullGroup.Visible = false;
            }
            else
            {
                if (txtNewGroup.Text.Trim().Length > 20)
                {
                    SetControlColor(txtNewGroup);
                    m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000050"), new String[] { GetResource("R00090.Text"), "2-20" })), lblErrorGroup);
                    throw m_epList;
                }
                if (txtNewGroup.Text.Trim().Length < 2)
                {
                    SetControlColor(txtNewGroup);
                    m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000051"), new String[] { GetResource("R00090.Text"), "2-20" })), lblErrorGroup);
                    throw m_epList;
                }
            }
            
            DataTable dt =  (DataTable)ViewState["group"];
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (txtNewGroup.Text.Trim() == dt.Rows[i]["con"].ToString())
                    {
                        SetControlColor(txtNewGroup);
                        m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000220"), new String[] { GetResource("R00090.Text") })), lblErrorGroup);
                        throw m_epList;
                    }
                }
            }
        }
        else
        {
            lblErrorGroup.Text = "";
            SetControlColor(txtNewGroup);
            m_epList.Add(new ExceptionData(new AppException(m_rsMansge.GetGlobalResMess("ED00000040"), new String[] { GetResource("R00090.Text") })), lblErrorGroup);
            throw m_epList;
        }

    }
           
    
}
