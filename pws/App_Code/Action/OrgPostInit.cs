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
    public class OrgPostInit : ActionPageBase
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
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
           
            //钱币类型
            DataTable mdt = ma_code.GetMoney();
            Resid2Name("CodeManage", ref mdt);
            //性别
            DataTable sexdt = ma_code.GetSex();
            Resid2Name("CodeManage", ref sexdt);
            //学历
            DataTable sldt = ma_code.GetSchoollevel();
            Resid2Name("CodeManage", ref sldt);
            //工作性质
            DataTable wkdt = ma_code.GetWorkkind();
            Resid2Name("CodeManage", ref wkdt);
            //年龄
            DataTable agedt = ma_code.GetAge();
            Resid2Name("CodeManage", ref agedt);
            //发布日期 
            DataTable datedt = ma_code.GetIssueDate();
            Resid2Name("CodeManage", ref datedt);
            //工作经验
            DataTable expdt = ma_code.GetExperiene();
            Resid2Name("Codemanage", ref expdt);

            ds.Tables.Add(cmdt.GetCloneTable(mdt, "BANKROLLUNIT"));
            ds.Tables.Add(cmdt.GetCloneTable(sexdt, "SEXCLASS"));
            ds.Tables.Add(cmdt.GetCloneTable(sldt, "SCHOOLLEVEL"));
            ds.Tables.Add(cmdt.GetCloneTable(wkdt, "WORKKIND"));
            ds.Tables.Add(cmdt.GetCloneTable(agedt, "AGE"));
            ds.Tables.Add(cmdt.GetCloneTable(datedt, "ISSUEDATE"));
            ds.Tables.Add(cmdt.GetCloneTable(expdt, "EXPERIENCE"));

            ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetAddressContent(currLang), "ZONE"));
            if (ds.Tables["ZONE"].Rows.Count > 0 && ds.Tables["ZONE"].Rows[0]["MA_ORDERID"] != null)
            {
                ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(currLang), "ZONE_PROVINCE"));
            }

            ds.Tables.Add(cmdt.GetCloneTable(ma_cominfo.GetCountryId(registId), "COUNTRYID"));

            //返回结果集
            return ds;
        }

    }
}