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


namespace Seika.Api
{
    /// <summary>
    /// Api调用数据状态
    /// </summary>
    public class ReloadDataFlag
    {
        static bool publish_homepage_news = false;
        static bool publish_homepage_company = false;
        static bool publish_homepage_companylst = false;
        static bool publish_homepage_product = false;
        static bool publish_homepage_productlst = false;
        static bool publish_homepage_joblst_init = false;
        static bool publish_homepage_joblst = false;
        static bool publish_homepage_postclass = false;

        /// <summary>
        /// 新闻状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_NEWS
        {
            get { return publish_homepage_news; }
            set { publish_homepage_news = value; }
        }
        /// <summary>
        /// 公司状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_COMPANY
        {
            get { return publish_homepage_company; }
            set { publish_homepage_company = value; }
        }
        /// <summary>
        /// 公司一览状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_COMPANYLST
        {
            get { return publish_homepage_companylst; }
            set { publish_homepage_companylst = value; }
        }
        /// <summary>
        /// 产品状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_PRODUCT
        {
            get { return publish_homepage_product; }
            set { publish_homepage_product = value; }
        }
        /// <summary>
        /// 产品一览状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_PRODUCTLST
        {
            get { return publish_homepage_productlst; }
            set { publish_homepage_productlst = value; }
        }
        /// <summary>
        /// 招聘一览初期化
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_JOBLST_INIT
        {
            get { return publish_homepage_joblst_init; }
            set { publish_homepage_joblst_init = value; }
        }
        /// <summary>
        /// 招聘一览状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_JOBLST
        {
            get { return publish_homepage_joblst; }
            set { publish_homepage_joblst = value; }
        }
        /// <summary>
        /// 类别状态
        /// </summary>
        public static bool PUBLISH_HOMEPAGE_POSTCLASS
        {
            get { return publish_homepage_postclass; }
            set { publish_homepage_postclass = value; }
        }
    }
}
