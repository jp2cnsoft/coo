using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.COO.DBA.MA;
using System.IO;
using System.Data.SqlClient;
using Seika.Db;
using Seika.Util;
using Seika.CooException;
using Seika.COO.DBA.BS;
using Seika.COO.Web.PG;
using Seika.Common.Net;

namespace Seika.COO.Action
{
    public class ActionServer : System.Web.UI.Page
    {
        private ResManager res = new ResManager();
        String actionName = String.Empty;
        ExceptionMess errMess;

        /// <summary>
        /// 抛错异常集合
        /// </summary>
        public ExceptionMess ErrMess 
        {
            get { return errMess; }
            set { errMess = value; }
        }

        public ActionServer(String actionName)
        {
            this.actionName = actionName;
        }

        public DataSet ServerStart(String actionName, DataSet cds, String[] oparms)
        {
            this.actionName = actionName;
            return ServerStart(cds, oparms);
        }

        public DataSet ServerStart(String actionName, DataSet cds)
        {
            this.actionName = actionName;
            return ServerStart(cds, null);
        }

        public DataSet ServerStart(DataSet cds)
        {
            return ServerStart(cds, null);
        }

        public DataSet ServerStart(DataSet cds, String[] oparms)
        {
            if (String.IsNullOrEmpty(actionName))
            {
                throw new SysException("ED00000020");
            }
            
            DataSet rds = null;
            DBConnect sql = new DBConnect(ActionBase.strconn);
            
            try
            {
                //打开数据库
                sql.open();

                Type act = Type.GetType(ActionBase.actionPath + "." + actionName);

                Object actObj = Activator.CreateInstance(act);

                ActionBase ab = (ActionBase)actObj;

                rds = ab.Run(sql, cds, oparms);

                //提交事务
                sql.Commit();

                return rds;
            }
            catch (AppException es)
            {
                if (((AppException)es).AppMessage != "ED01000160") 
                {
                    sql.Rollback();
                }
                ExceptionWorking(((AppException)es).AppMessage);
            }
            catch (SysException es)
            {
                SessionManager sessionManager = SessionManager.GetSessionManager(Session);
                sessionManager.SystemExceptionId = es;
                throw es;
                //sql.Rollback();
                //ExceptionWorking(((SysException)es).SysMessage);
            }
            //捕获系统异常
            catch (System.Exception es)
            {
                sql.Rollback();
                throw es;
            }
            finally
            {
                //关库
                sql.close();
            }
            return null;
        }

        private void ExceptionWorking(String _mess)
        {
            if (errMess == null) return;

            String mess = res.GetGlobalResMess(_mess);

            for (int i = 0; i < errMess.GetData.Count; i++)
            {
                Label col = (Label)errMess.GetControl[i];
                ExceptionControls cols = (ExceptionControls)errMess.GetControls[i];
                StringBuilder _sb = new StringBuilder();
                for (int j = 0; j < errMess.GetData[i].Count; j++)
                {
                    String err = errMess.GetData[i].GetAppException[j].AppMessage.ToString();
                    //设置出错控件
                    for (int k = 0; k < errMess.GetControls[i].Count; k++)
                    {
                        Control kc = (Control)errMess.GetControls[i].Get[k];
                        String controlName = kc.GetType().Name.ToString();

                        switch (controlName)
                        {
                            case "TextBox":
                                ((TextBox)kc).BackColor = CodeSymbol.color_err;
                                break;
                            case "DropDownList":
                                ((DropDownList)kc).BackColor = CodeSymbol.color_err;
                                break;
                        }
                    }
                    _sb.Append(mess);
                    _sb.Append("</br>");
                }
                col.Text = _sb.ToString();
                col.ForeColor = CodeSymbol.color_err;
                break;
            }
        }
    }

    public abstract class ACTION_NAMES
    {
        /// <summary>激活码检测</summary>
        public const String ORG_ACTIVATION = "OrgActivation";
        /// <summary>公司地址列表</summary>
        public const String ORG_COMINFO_ADDRESS = "OrgCominfoAddress";
        /// <summary>公司地址列表(到市级)</summary>
        public const String ORG_COMINFO_ADDRESS_CITY = "OrgCominfoAddressCity";
        /// <summary>取得地址列表</summary>
        public const String ORG_ADDRESS = "OrgAddress";
        /// <summary>检测用户是否认证</summary>
        public const String ORG_ATTESTATION = "OrgAttestation";
        /// <summary>取得行业分类信息</summary>
        public const String ORG_CALLING = "OrgCalling";
        /// <summary>取得行业分类信息</summary>
        public const String ORG_CALLINGCATEGORY = "OrgCallingCategory";
        /// <summary>取得地址信息</summary>
        public const String ORG_ADDRESSCATEGORY = "OrgAddressCategory";
        /// <summary>取得公司信息</summary>
        public const String ORG_COMPANY = "OrgCompany";
        /// <summary>检测用户是否被激活过</summary>
        public const String ORG_DELETEFLG = "OrgDeleteflg";
        /// <summary>组织信息取得</summary>
        public const String ORG_INFO = "OrgInfo";
        /// <summary>组织地址取得</summary>
        public const String ORG_LIST = "OrgList";
        /// <summary>组织登陆</summary>
        public const String ORG_LOGIN = "OrgLogin";
        /// <summary>修改Logo路径</summary>
        public const String ORG_LOGO = "OrgLogo";
        /// <summary>修改密码</summary>
        public const String ORG_PASSWORD = "OrgPassword";
        /// <summary>公司产品查询</summary>
        public const String ORG_PRODUCT = "OrgProduct";
        /// <summary>公司产品查询</summary>
        public const String ORG_PRODUCTCATEGORY = "OrgProductCategory";
        /// <summary>公司产品查询</summary>
        public const String ORG_PRODUCTDEL = "OrgProductDel";
        /// <summary>公司产品查询</summary>
        public const String ORG_PRODUCTSUB = "OrgProductSub";

        /// <summary>招聘职位查询</summary>
        public const String ORG_POSTCATEGORY = "OrgPostCategory";
        /// <summary>职位删除</summary>
        public const String ORG_POSTDEL = "OrgPostDel";
        /// <summary>职位查询</summary>
        public const String ORG_POSTSUB = "OrgPostSub";
        /// <summary>职位查询</summary>
        public const String ORG_POST = "OrgPost";
        /// <summary>地域查询</summary>
        public const String ORG_POSTADDRESS = "OrgPostAddress";
        /// <summary>职位下拉查询</summary>
        public const String ORG_POSTINIT = "OrgPostInit";
        /// <summary>职位分类信息添加修改</summary>
        public const String ORG_POSTBASEINFO = "OrgPostBaseInfo";

        /// <summary>追加经营范围</summary>
        public const String ORG_REG = "OrgReg";
        /// <summary>取得公司信息</summary>
        public const String ORG_REGCERTIFICATION = "OrgRegCertification";
        /// <summary>组织地址取得</summary>
        public const String ORG_REGINIT = "OrgRegInit";
        /// <summary>注册帐号的取得</summary>
        public const String ORG_REGISTID = "OrgRegistid";
        /// <summary>更新公司注册信息</summary>
        public const String ORG_UPDATAREGCERTIFICATION = "OrgUpdataRegCertification";
        /// <summary>组织更新</summary>
        public const String ORG_UPDATE = "OrgUpdate";
        /// <summary>逻辑删除激活用户</summary>
        public const String ORG_UPDATADELETEFLG = "OrgUpdataDeleteflg";
        /// <summary>产品分类信息添加修改</summary>
        public const String ORG_PRODUCTBASEINFO = "OrgProductBaseInfo";
        /// <summary>CODE取得</summary>
        public const String ORG_CODE = "OrgCode";
        /// <summary>更新EMAIL</summary>
        public const String ORG_EMAILUPDATE = "OrgEmailUpdate";
        /// <summary>更新为激活状态并返回密码</summary>
        public const String ORG_UPDATEACTIVE = "OrgUpdateActive";

        /// <summary>主页企业/产品/职位菜单取得</summary>
        public const String ORG_SUBMENU = "OrgSubMenu";
        
    }
}
