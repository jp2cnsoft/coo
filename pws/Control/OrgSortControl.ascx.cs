﻿using System; using System.Data; using System.Configuration; using System.Collections; using System.Web; using System.Web.Security; using System.Web.UI; using System.Web.UI.WebControls; using System.Web.UI.WebControls.WebParts; using System.Web.UI.HtmlControls;  public partial class Control_OrgSortControl : System.Web.UI.UserControl {     DataTable calling;     DataTable country;     int pageNum;      public DataTable Calling      {         set { calling = value; }     }      public DataTable Country     {         set { country = value; }     }      public int PageNum      {         set { pageNum = value; }     }      protected void Page_Load(object sender, EventArgs e)     {         if (!IsPostBack)         {             if (calling != null)             {                 dlistCalling.DataSource = calling;                 dlistCalling.DataBind();             }              if (country != null)             {                 dlistCountry.DataSource = country;                 dlistCountry.DataBind();             }              if (Session["CALLINGID"] != null)             {                 DataTable c = (DataTable)Session["CALLINGID"];                 callingTitleList.DataSource = c;                 callingTitleList.DataBind();             }              if (Session["COUNTRYID"] != null)             {                 DataTable c = (DataTable)Session["COUNTRYID"];                 countryTitleList.DataSource = c;                 countryTitleList.DataBind();             }         }     }      protected void lbtnAllCalling_Click(object sender, EventArgs e)     {         Session["CALLINGID"] = null;          String countryId = String.Empty;         if (Session["COUNTRYID"] != null)         {             countryId = GetNewClick((DataTable)Session["COUNTRYID"]);         }          Response.Redirect(Request.Path.ToString() + "?COUNTRYID=" + countryId);      }      protected void lbtnAllCountry_Click(object sender, EventArgs e)     {         Session["COUNTRYID"] = null;          String callingId = String.Empty;         if (Session["CALLINGID"] != null)         {             callingId = GetNewClick((DataTable)Session["CALLINGID"]);         }          Response.Redirect(Request.Path.ToString() + "?CALLINGID=" + callingId);     }      protected void dlistCalling_ItemDataBound(object sender, DataListItemEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             LinkButton lbtnCallingLink = (LinkButton)e.Item.FindControl("lbtnCallingLink");             Label lblCallingCount = (Label)e.Item.FindControl("lblCallingCount");              lbtnCallingLink.Text = lbtnCallingLink.Text.ToString() + "(" + lblCallingCount.Text.ToString() + ")";         }     }      protected void dlistCountry_ItemDataBound(object sender, DataListItemEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             LinkButton lbtnCountryLink = (LinkButton)e.Item.FindControl("lbtnCountryLink");             Label lblCountryCount = (Label)e.Item.FindControl("lblCountryCount");              lbtnCountryLink.Text = lbtnCountryLink.Text.ToString() + "(" + lblCountryCount.Text.ToString() + ")";         }     }      protected void dlistCalling_ItemCommand(object source, DataListCommandEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             HiddenField hidCallingID = (HiddenField)e.Item.FindControl("hidCallingID");             HiddenField hidCallingName = (HiddenField)e.Item.FindControl("hidCallingName");              DataTable c;             if (Session["CALLINGID"] != null)             {                 c = (DataTable)Session["CALLINGID"];             }             else              {                 c = new DataTable();                 c.Columns.Add("ID", typeof(string));                 c.Columns.Add("NAME", typeof(string));             }             DataRow dr = c.NewRow();             dr["ID"] = hidCallingID.Value.ToString();             dr["NAME"] = hidCallingName.Value.ToString();             c.Rows.Add(dr);             Session["CALLINGID"] = c;              String countryId = String.Empty;             if (Session["COUNTRYID"] != null)              {                 countryId = GetNewClick((DataTable)Session["COUNTRYID"]);             }              Response.Redirect(Request.Path.ToString() + "?CALLINGID=" + hidCallingID.Value.ToString() + "&COUNTRYID=" + countryId);         }     }      protected void dlistCountry_ItemCommand(object source, DataListCommandEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             HiddenField hidCountryID = (HiddenField)e.Item.FindControl("hidCountryID");             HiddenField hidCountryName = (HiddenField)e.Item.FindControl("hidCountryName");              DataTable c;             if (Session["COUNTRYID"] != null)             {                 c = (DataTable)Session["COUNTRYID"];             }             else             {                 c = new DataTable();                 c.Columns.Add("ID", typeof(string));                 c.Columns.Add("NAME", typeof(string));             }             DataRow dr = c.NewRow();             dr["ID"] = hidCountryID.Value.ToString();             dr["NAME"] = hidCountryName.Value.ToString();             c.Rows.Add(dr);              Session["COUNTRYID"] = c;              String callingId = String.Empty;             if (Session["CALLINGID"] != null)             {                 callingId = GetNewClick((DataTable)Session["CALLINGID"]);             }              Response.Redirect(Request.Path.ToString() + "?COUNTRYID=" + hidCountryID.Value.ToString() + "&CALLINGID=" + callingId);         }     }      protected void callingTitleList_ItemCommand(object source, DataListCommandEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             HiddenField hidCallingID = (HiddenField)e.Item.FindControl("hidCallingID");             DataTable c = (DataTable)Session["CALLINGID"];             c = RushIdData(c, hidCallingID.Value.ToString());             Session["CALLINGID"] = c;              String countryId = String.Empty;             if (Session["COUNTRYID"] != null)             {                 countryId = GetNewClick((DataTable)Session["COUNTRYID"]);             }              Response.Redirect(Request.Path.ToString() + "?CALLINGID=" + hidCallingID.Value.ToString() + "&COUNTRYID=" + countryId);          }     }      protected void countryTitleList_ItemCommand(object source, DataListCommandEventArgs e)     {         if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)         {             HiddenField hidCountryID = (HiddenField)e.Item.FindControl("hidCountryID");             DataTable c = (DataTable)Session["COUNTRYID"];             c = RushIdData(c, hidCountryID.Value.ToString());             Session["COUNTRYID"] = c;              String callingId = String.Empty;             if (Session["CALLINGID"] != null)             {                 callingId = GetNewClick((DataTable)Session["CALLINGID"]);             }              Response.Redirect(Request.Path.ToString() + "?COUNTRYID=" + hidCountryID.Value.ToString() + "&CALLINGID=" + callingId);          }     }      ////hashtable转datatable     //private DataTable Hash2Data(Hashtable h)      //{     //    DataTable d = new DataTable();     //    d.Columns.Add("ID", typeof(string));     //    d.Columns.Add("NAME", typeof(string));     //    foreach (DictionaryEntry de in h)      //    {     //        DataRow row = d.NewRow();     //        row["ID"] = de.Key.ToString();     //        row["NAME"] = de.Value.ToString();     //        d.Rows.Add(row);     //    }     //    return d;     //}      //清除同key值以后添加的数据     private DataTable RushIdData(DataTable h, String key)      {         DataTable temp = new DataTable();         temp.Columns.Add("ID", typeof(string));         temp.Columns.Add("NAME", typeof(string));          foreach (DataRow row in h.Rows)         {             DataRow dr = temp.NewRow();             dr["ID"] = row["ID"].ToString();             dr["NAME"] = row["NAME"].ToString();             temp.Rows.Add(dr);              if (row["ID"].ToString() == key)             {                 return temp;             }         }         return temp;     }      private String GetNewClick(DataTable dt)      {         String id = String.Empty;         if (dt.Rows.Count > 0)         {             id = dt.Rows[dt.Rows.Count - 1]["ID"].ToString();         }         return id;     }  } 