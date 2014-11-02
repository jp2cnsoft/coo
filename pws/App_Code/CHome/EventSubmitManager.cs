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
using System.Web.SessionState;
/// <summary>
/// 提交事件事务管理
/// </summary>
namespace Seika.COO.Web.PG
{
    public class EventSubmitManager
    {
        SessionManager sessionMag = null;

        public EventSubmitManager(SessionManager sessionMag)
        {
            this.sessionMag = sessionMag;
        }

        public bool IsSyoRiFlg
        {
            get
            {
                bool pagSubFlg = false;
                if (sessionMag.PageSubmitSta == false)
                {
                    sessionMag.PageSubmitSta = pagSubFlg;
                    return pagSubFlg;
                }
                pagSubFlg = (bool)sessionMag.PageSubmitSta;
                return pagSubFlg;
            }
        }

        /// <summary>
        /// 业务处理中标记设置
        /// </summary>
        public void SetSyoRiFlg()
        {
            sessionMag.PageSubmitSta = true;
        }

        /// <summary>
        /// 业务处理中标记清空
        /// </summary>
        public void ClearSyoRiFlg()
        {
            sessionMag.PageSubmitSta = false;
        }
    }
}