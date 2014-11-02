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
using Seika.ServicesCol;
using Seika.COO.Action;

public partial class Control_SelectChkControl : System.Web.UI.UserControl
{
    /// <summary>
    /// 选择内容
    /// </summary>
    public DataTable SelectTable
    {
        set { ViewState["SelectTable"] = value; }
        get
        {
            if (ViewState["SelectTable"] == null)
            {
                return null;
            }
            return (DataTable)ViewState["SelectTable"];
        }
    }
    /// <summary>
    /// 初期化选择
    /// </summary>
    public DataTable SelectInitTable
    {
        set { ViewState["SelectInitTable"] = value; }
        get
        {
            if (ViewState["SelectInitTable"] == null)
            {
                return null;
            }
            return (DataTable)ViewState["SelectInitTable"];
        }
    }
    /// <summary>
    /// 选择导行
    /// </summary>
    public DataTable SelectNavi
    {
        set { ViewState["SelectNavi"] = value; }
        get
        {
            if (ViewState["SelectNavi"] == null)
            {
                return null;
            }
            return (DataTable)ViewState["SelectNavi"];
        }
    }
    /// <summary>
    /// 默认选择ID表
    /// </summary>
    public DataTable ChkSelectId
    {
        set { ViewState["selectId"] = value; }
        get
        {
            if (ViewState["selectId"] == null)
            {
                return null;
            }
            return (DataTable)ViewState["selectId"];
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

    /// <summary>
    /// 取得选择check项级别
    /// </summary>
    public int SelectTableLevel
    {
        set { ViewState["SelectTableLevel"] = value; }
        get
        {
            if (ViewState["SelectTableLevel"] == null)
            {
                return 0;
            }
            return (int)ViewState["SelectTableLevel"];
        }
    }

    /// <summary>
    /// 取得选择项目
    /// </summary>
    public DataTable GetSelectTable
    {
        get
        {
            return GetSelectTableChecked();
        }
    }
    /// <summary>
    /// 取得选择集合
    /// </summary>
    public DataSet GetSelectTablePath
    {
        get 
        { 
            GetSelectTablePathChecked();
            return SelectTablePath;
        }
    }

    //public Control GetCheckListSelect 
    //{
    //    get { return chklstSelectChk; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            InitPage();
        }
    }

    public void InitPage() 
    {
        //选择项初期化
        if (SelectTable != null)
        {
            chklstSelectChk.DataSource = SelectTable;
        }
        else 
        {
            DataTable ma_Class = new DataTable(DbTableName);
            ma_Class.Columns.Add("MA_ORDERID");
            ma_Class.Columns.Add("NAME");
            chklstSelectChk.DataSource = SelectTable = ma_Class;
        }
        chklstSelectChk.DataTextField = "NAME";
        chklstSelectChk.DataValueField = "MA_ORDERID";
        chklstSelectChk.DataBind();

        if (ChkSelectId != null)
        {
            rptSelect.DataSource = ChkSelectId;
            rptSelect.DataBind();
        }
        ChkLstSelect(chklstSelectChk);
        //GetSubSelect("ROOT");

        //内容初期化
        rptSelect.DataSource = SelectInitTable;
        rptSelect.DataBind();
        rptSelect.Visible = true;
        
        //选择路径初期化 
        selectTitleList.DataSource = SelectNavi;
        selectTitleList.DataBind();

        btnSelect.Visible = false;
    }

    protected void lbtnAllSelect_Click(object sender, EventArgs e)
    {
        SelectNavi = null;
        GetSubSelect("ROOT");
        btnSelect.Visible = false;
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (SelectTable != null)
        {
            DataTable dt = SelectTable;
            ArrayList lstState = ChkLstState();
            if (ViewState["pid"] != null && ViewState["pname"] != null)
            {
                //不存在记录则添加该值，并添加该值路径表
                if (!CheckTableSame(dt, ViewState["pname"].ToString()))
                {
                    DataRow row = dt.NewRow();
                    row["MA_ORDERID"] = ViewState["pid"].ToString();
                    row["NAME"] = ViewState["pname"].ToString();
                    dt.Rows.Add(row);

                    //父级导航
                    if (SelectTablePath == null)
                        //建立新集合
                        SelectTablePath = new DataSet();
                    DataSetManage cmdt = new DataSetManage();
                    //添加表
                    SelectTablePath.Tables.Add(cmdt.GetCloneTable(SelectNavi, (new ObjectStringTool()).GetNextSeqNumber()));
                }

                chklstSelectChk.DataSource = dt;
                chklstSelectChk.DataBind();

                btnSelect.Visible = false;

                ChkLstSelect(lstState, chklstSelectChk);
                chklstSelectChk.Items[chklstSelectChk.Items.Count - 1].Selected = true;

            }
        }
    }

    //列表框选择状态
    private ArrayList ChkLstState() 
    {
        ArrayList lst = new ArrayList();
        int items = chklstSelectChk.Items.Count;
        for (int i = 0; i < items; i++) 
        {
            if (chklstSelectChk.Items[i].Selected) 
            {
                lst.Add(chklstSelectChk.Items[i].Value.ToString());
            }
        }
        return lst;
    }

    //选择列表
    private void ChkLstSelect(ArrayList lstState, CheckBoxList checkBoxList)
    {
        int count = checkBoxList.Items.Count;
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < lstState.Count; j++)
            {
                if (lstState[j].ToString() == checkBoxList.Items[i].Value.ToString())
                {
                    checkBoxList.Items[i].Selected = true;
                    break;
                }
            }
        }
    }

    //选择列表
    private void ChkLstSelect(CheckBoxList checkBoxList)
    {
        int count = checkBoxList.Items.Count;
        for (int i = 0; i < count; i++)
        {
            checkBoxList.Items[i].Selected = true;
        }
    }

    //头绑定命令
    protected void selectTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblSelectLink")).Visible = true; }
    }

    protected void selectTitleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            HiddenField hfSelectID = (HiddenField)e.Item.FindControl("hfSelectID");
            DataTable c = SelectNavi;
            c = RushIdData(c, hfSelectID.Value.ToString());
            ChkSelectId = SelectNavi =c;

            selectTitleList.DataSource = c;
            selectTitleList.DataBind();

            //取得下级列表
            GetSubSelect(hfSelectID.Value.ToString());
        }
    }

    private void GetSubSelect(String selectId)
    {
        ActionServer actSelect = new ActionServer(ActionName);
        DataSet ddlDs = actSelect.ServerStart(GetSelectDs(selectId));
        if (ddlDs != null)
        {
            DataTable dt = ddlDs.Tables[0];

            //子表存在记录不显示添加按钮
            if (dt.Rows.Count > 0)
            {
                //全部
                if (selectId == "ROOT")
                {
                    rptSelect.DataSource = dt;
                    rptSelect.DataBind();
                    rptSelect.Visible = true;

                    selectTitleList.DataSource = ChkSelectId = null;
                    selectTitleList.DataBind();
                }
                //子内容
                else
                {
                    rptSelect.DataSource = dt;
                    rptSelect.DataBind();
                    rptSelect.Visible = true;
                    btnSelect.Visible = false;
                }
                //存在子记录且取得的记录级别与规定在此级别下相同则显示添加按钮
                if (SelectTableLevel <= ((DataTable)selectTitleList.DataSource).Rows.Count) btnSelect.Visible = true;
            }//否则显示添加按钮
            else
            {
                rptSelect.Visible = false;
                btnSelect.Visible = true;
            }
        }
    }
   
    // 选择经营项目
    protected void rptSelect_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            HiddenField hfSelectID = (HiddenField)e.Item.FindControl("hfSelectID");
            HiddenField hfSelectText = (HiddenField)e.Item.FindControl("hfSelectText");

            DataTable c;
            if (SelectNavi != null)
            {
                c = SelectNavi;
            }
            else
            {
                c = new DataTable();
                c.Columns.Add("MA_ORDERID", typeof(string));
                c.Columns.Add("NAME", typeof(string));
            }
            DataRow dr = c.NewRow();
            dr["MA_ORDERID"] = ViewState["pid"] = hfSelectID.Value.ToString();
            dr["NAME"] = ViewState["pname"] = hfSelectText.Value.ToString();
            c.Rows.Add(dr);
            SelectNavi = c;

            selectTitleList.DataSource = c;
            selectTitleList.DataBind();

            //取得下级列表
            GetSubSelect(hfSelectID.Value.ToString());

        }
    }
    protected void rptSelect_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ObjectStringTool ost = new ObjectStringTool();
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            //截取字符
            LinkButton lbtnSelectLink = (LinkButton)e.Item.FindControl("lbtnSelectLink");
            lbtnSelectLink.ToolTip = lbtnSelectLink.Text.ToString();
            //lbtnSelectLink.Text = ost.FormatLongString(lbtnSelectLink.Text.ToString(), 7);
        }
        if (e.Item.ItemIndex != 0) { ((Label)e.Item.FindControl("lblCallingLine")).Visible = true; }
    }

    //取得选择的项目
    private DataTable GetSelectTableChecked()
    {
        //新建表用于返回当前选择的项目存储
        DataTable sdt = new DataTable();
        //选择取的表记录为空则返回空表
        if (SelectTable == null) return sdt;
        //原始表记录copy到新表中
        sdt = SelectTable.Copy();
        //项目数
        int dtc = sdt.Rows.Count;
        //check list数
        int count = chklstSelectChk.Items.Count;

        for (int i = dtc - 1; i >= 0; i--)
        {   //表中记录
            DataRow row = sdt.Rows[i];
            //状态
            bool state = false;
            //check list遍历
            for (int j = 0; j < count; j++)
            {   //有选择的值
                if (chklstSelectChk.Items[j].Selected)
                {   //当前值存在于表中
                    if (row[0].ToString() == chklstSelectChk.Items[j].Value.ToString())
                    {
                        //改变为存在状态
                        state = true;
                        //跳出当前循环体
                        break;
                    }
                }
            }
            //check list 中不存在
            if (!state)
                //删除记录
                sdt.Rows.Remove(row);
        }
        return sdt;
    }

    //取得选择的父级集合
    private void GetSelectTablePathChecked() 
    {
        if (SelectTablePath == null) return;
        //集合数
        int dtc = SelectTablePath.Tables.Count;
        //check list数
        int count = chklstSelectChk.Items.Count;

        for (int i = dtc - 1; i >= 0 ;i--)
        {   //集合中的表
            DataTable dt = SelectTablePath.Tables[i];
            //初期化当前状态
            bool state = false;
            //遍历当前表
            for (int ri = dt.Rows.Count - 1; ri >= 0;ri-- )
            {
                String rid = dt.Rows[ri][0].ToString();
                if (!String.IsNullOrEmpty(rid))
                {
                    //check list遍历
                    for (int j = 0; j < count; j++)
                    {   //有选择的值
                        if (chklstSelectChk.Items[j].Selected)
                        {   //当前值存在于表中
                            if (rid == chklstSelectChk.Items[j].Value.ToString())
                            {
                                //改变为存在状态
                                state = true;
                                break;
                            }
                        }
                    }   
                    break;
                }
            }
            //check list 中不存在
            if (!state)
                //删除该表
                SelectTablePath.Tables.Remove(dt);
        }
    }

    //清除同key值以后添加的数据
    private DataTable RushIdData(DataTable h, String key)
    {
        DataTable temp = new DataTable();
        temp.Columns.Add("MA_ORDERID", typeof(string));
        temp.Columns.Add("NAME", typeof(string));

        foreach (DataRow row in h.Rows)
        {
            DataRow dr = temp.NewRow();
            dr["MA_ORDERID"] = row["MA_ORDERID"].ToString();
            dr["NAME"] = row["NAME"].ToString();
            temp.Rows.Add(dr);

            if (row["MA_ORDERID"].ToString() == key)
            {
                return temp;
            }
        }
        return temp;
    }

    /// <summary>
    /// 组成dataset单值
    /// </summary>
    /// <param name="selectValue"></param>
    /// <returns></returns>
    private DataSet GetSelectDs(String selectValue)
    {
        DataSetManage dsm = new DataSetManage("SELECTTABLE");
        dsm.NewRow();
        dsm.AddColumnAndValue("SELECTLIST", selectValue);
        dsm.RowBind();
        return dsm.Get;
    }

    //验证值是否存在
    private bool CheckTableSame(DataTable dt,String str) 
    {
        foreach (DataRow r in dt.Rows) 
        {
            if (r["NAME"].ToString() == str) return true;
        }
        return false;
    }

}
