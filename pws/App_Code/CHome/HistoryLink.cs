using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using Seika;

/// <summary>
/// 导航链接构成
/// </summary>
namespace Seika.COO.Web.PG
{
    public class HistoryLink
    {
        public String Link { get; set; }
        public String Content { get; set; }
    }
    public class HistoryLinkCollection
    {
        private List<HistoryLink> _list = new List<HistoryLink>();

        public void Add(HistoryLink t)
        {
            _list.Add(t);
        }
        /// <summary>
        /// 初始化导航
        /// </summary>
        /// <param name="ds"></param>
        public void Init(DataSetManage ds)
        {
            if (ds.TableCount > 0 || ds.Get.Tables.Contains("mainitem"))
            {
                DataTable mdt = ds.Get.Tables["mainitem"];
                foreach (DataRow r in mdt.Rows)
                {
                    //公司简介
                    if (r["id"].ToString() == "0001")
                    {
                        //公司简介下级菜单
                        DataTable sdt = ds.Get.Tables["item"];
                        foreach (DataRow sr in sdt.Rows)
                        {
                            HistoryLink h = new HistoryLink();
                            h.Content = "<a href=\"../../Pages/" + r["managelink"].ToString() + "\">" + r["content"].ToString() + "</a> > " + sr["content"].ToString();
                            h.Link = sr["managelink"].ToString();
                            Add(h);
                        }
                    }
                    //其它菜单
                    else
                    {
                        HistoryLink h = new HistoryLink();
                        h.Content = r["content"].ToString();
                        h.Link = r["managelink"].ToString();
                        Add(h);
                    }
                }
            }
        }
        /// <summary>
        /// 取得Link内容
        /// </summary>
        /// <param name="pageLink"></param>
        /// <returns></returns>
        public String LoadLink(String pageLink)
        {
            foreach (HistoryLink h in _list)
            {
                if (!String.IsNullOrEmpty(h.Link) && pageLink.EndsWith(h.Link, StringComparison.OrdinalIgnoreCase))
                {
                    return h.Content;
                }
            }
            return null;
        }

    }
}