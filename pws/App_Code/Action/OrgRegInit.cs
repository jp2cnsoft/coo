using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
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

namespace Seika.COO.Action
{
    /// <summary>
    /// 组织地址取得
    /// </summary>
    public class OrgRegInit : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            //实例化一个数据集合
            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            //当前语言取得
            m_session = new SessionManager(Session);
            String currLang = m_session.Page_UICultureID;
            String registId = m_session.PageLogin_RegistId;

            MA_CODE ma_code = new MA_CODE(sql);
            MA_ZONECLASS ma_zone = new MA_ZONECLASS(sql);
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(sql);
            MA_PRODUCTCLASS ma_product = new MA_PRODUCTCLASS(sql);
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            //地址父ID
            String fid = String.Empty;
            BS_MANAGE bs_manage = new BS_MANAGE(sql);

            //取得所有下拉列表
            DataTable mdt = ma_code.GetMoney();
            Resid2Name("CodeManage", ref mdt);
            DataTable cdt = ma_code.GetOrgCharacter();
            Resid2Name("CodeManage", ref cdt);
            DataTable sdt = ma_code.GetOrgScale();
            Resid2Name("CodeManage", ref sdt);
            DataTable mmdt = ma_code.GetManageMode();
            Resid2Name("CodeManage", ref mmdt);
            DataTable ssdt = ma_code.GetStructueClass();
            Resid2Name("CodeManage", ref ssdt);

            ds.Tables.Add(cmdt.GetCloneTable(mdt, "BANKROLLUNIT"));
            ds.Tables.Add(cmdt.GetCloneTable(cdt, "CHARACTER"));
            ds.Tables.Add(cmdt.GetCloneTable(sdt, "EMPLOYEENUM"));
            ds.Tables.Add(cmdt.GetCloneTable(mmdt, "MANAGEMODE"));
            ds.Tables.Add(cmdt.GetCloneTable(ssdt, "STRUCTURECLASS"));

            ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetAddressContent(currLang), "ZONE"));
            if (ds.Tables["ZONE"].Rows.Count > 0 && ds.Tables["ZONE"].Rows[0]["MA_ORDERID"] != null)
            {
                ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(currLang), "ZONE_PROVINCE"));      
            }

            ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgContent(currLang), "CALLING"));
            if (ds.Tables["CALLING"].Rows.Count > 0 && ds.Tables["CALLING"].Rows[0]["MA_ORDERID"] != null)
            {
                ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgList(currLang), "CALLING_SUB"));     
            }


            ds.Tables.Add(cmdt.GetCloneTable(ma_cominfo.GetCountryId(registId), "COUNTRYID"));


            //返回结果集
            return ds;
        }

        

    }
}
