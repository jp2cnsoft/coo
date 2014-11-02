using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Seika.ServicesCol
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:SDropDownList runat=server></{0}:SButton>")]
    public class SDropDownList : DropDownList
    {
        //重写方法
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string optgroup;

            ArrayList optOptionGroups = new ArrayList();

            foreach (ListItem item in this.Items)
            {
                if (item.Value.ToString() != "optgroup")
                {
                    RenderListItem(item, writer);
                }
                else
                {
                    optgroup = item.Text.ToString();

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
    }
}
