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
/// <summary>
/// Summary description for DropdownlistControl
/// </summary>
namespace Seika.ServicesCol
{
    public class DropdownlistControl : DropDownList
    {
        public DropdownlistControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //重写方法
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string optgroup;

            ArrayList optOptionGroups = new ArrayList();

            foreach (ListItem item in this.Items)
            {
                if (item.Attributes["optgroup"] == null)
                {
                    RenderListItem(item, writer);
                }
                else
                {
                    optgroup = item.Attributes["optgroup"];

                    if (optOptionGroups.Contains(optgroup))
                    {
                        RenderListItem(item, writer);
                    }
                    else
                    {
                        if (optOptionGroups.Count > 0)
                        {
                            optgroupEndTag(writer);
                        }
                        optgroupBeginTag(optgroup, writer);
                        optOptionGroups.Add(optgroup);
                    }
                }
            }

            if (optOptionGroups.Count > 0)
            {
                optgroupEndTag(writer);
            }
        }


        //option 简单添加style
        private void RenderListItem(ListItem item, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("option");
            writer.WriteAttribute("value", item.Value, true);
            writer.WriteAttribute("style", "color:#7395c1", true);
            if (item.Selected)
            {
                writer.WriteAttribute("selected", "selected", false);
            }
            foreach (string key in item.Attributes.Keys)
            {
                writer.WriteAttribute(key, item.Attributes[key]);
            }
            writer.Write(HtmlTextWriter.TagRightChar);
            HttpUtility.HtmlEncode(item.Text, writer);
            writer.WriteEndTag("option");
            writer.WriteLine();
        }

        //option 添加optgroup
        private void optgroupBeginTag(string name, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("optgroup");
            writer.WriteAttribute("label", name);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteLine();
        }

        private void optgroupEndTag(HtmlTextWriter writer)
        {
            writer.WriteEndTag("optgroup");
            writer.WriteLine();
        }

        //调用（根据DropDownList 无限级分级一文修改这个构造函数）
        //public NewDropDownList()
        //{
        //    ListItem li1 = new ListItem();
        //    li1.Attributes.Add("optgroup", "dsfasf");
        //    Items.Add(li1);

        //    ListItem li2 = new ListItem();
        //    Items.Add(li2);

        //    ListItem li3 = new ListItem();
        //    Items.Add(li3);
        //}

    }
}