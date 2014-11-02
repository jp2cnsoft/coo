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
    /// 组织更新
    /// </summary>
    public class OrgUpdate : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            //取得当前系统语言
            m_session = new SessionManager(Session);
            String defLangId = m_session.Page_UICultureID;
            //取得xml字段值
            DataTable dt = cds.Tables["MA_COMINFO"];
            DataRow dr = dt.Rows[0];

            String rigistId = StringToFilter(dr["RIGISTID"].ToString());
            String rigistName = StringToFilter(dr["NAME"].ToString());
            //注册资金
            //String capital = StringToFilter(dr["CAPITAL"].ToString());
            //资金单位
            //String capitalunit = dr["CAPITALUNIT"].ToString();
            String countryId = dr["COUNTRYID"].ToString();
            String provinceId = dr["PROVINCEID"].ToString();
            String cityId = dr["CITYID"].ToString();
            String boroughId = dr["BOROUGHID"].ToString();

            //String characterId = dr["CHARACTERID"].ToString();
            //String managemodeId = dr["MANAGEMODEID"].ToString();
            //String logourl = dr["LOGOURL"].ToString();
            //String logourl = "";
            //String registday = dr["REGISTDAY"].ToString();
            String artificialperson = StringToFilter(dr["ARTIFICIALPERSON"].ToString());
            String explain = StringToFilter(dr["EXPLAIN"].ToString());
            String otherManage = StringToFilter(dr["OTHERMANAGE"].ToString());
            String linkMan = StringToFilter(dr["LINKMAN"].ToString());
            String mobiletel = StringToFilter(dr["MOBILETEL"].ToString());
            //String organ = StringToFilter(dr["ORGAN"].ToString());

            String phone_1 = StringToFilter(dr["PHONE_COUNTRY"].ToString());
            String phone_2 = StringToFilter(dr["PHONE_ZONE"].ToString());
            String phone_3 = StringToFilter(dr["PHONE_CODE"].ToString());
            String phone_4 = StringToFilter(dr["PHONE_EXT"].ToString());
            String fax_1 = StringToFilter(dr["FAX_COUNTRY"].ToString());
            String fax_2 = StringToFilter(dr["FAX_ZONE"].ToString());
            String fax_3 = StringToFilter(dr["FAX_CODE"].ToString());
            String fax_4 = StringToFilter(dr["FAX_EXT"].ToString());
            String email = StringToFilter(dr["EMAIL"].ToString());

            String postcode = StringToFilter(dr["POSTCODE"].ToString());
            String address = StringToFilter(dr["ADDRESS"].ToString());

            ma_cominfo = new MA_COMINFO(sql);
            BS_MANAGE bs_manage = new BS_MANAGE(sql);
            //更新组织注册信息
            //if (!ma_cominfo.UpdateComInfo(rigistId, rigistName, capital, capitalunit, countryId, provinceId, cityId, boroughId, characterId, managemodeId,
            //     registday, artificialperson, explain,organ,defLangId))
            if (!ma_cominfo.UpdateComInfo(rigistId, rigistName, countryId, provinceId, cityId, boroughId
                     , artificialperson, explain, otherManage, linkMan, mobiletel, phone_1, phone_2, phone_3
                     ,phone_4, fax_1, fax_2, fax_3, fax_4, postcode, address))
            {
                throw new SysException("ED00000020");
            }

            //更新EMAIL
            ma_register = new MA_REGISTER(sql);
            if (!ma_register.SetEmail(rigistId, email))
            {
                throw new SysException("ED00000020");
            }

            //取得公司ID
            DataTable madt = ma_cominfo.GetComInfoId(rigistId);
            String maid = madt.Rows[0][0].ToString();

            //删除经营范围
            bs_manage.RemoveManageList(rigistId, defLangId);
            //追加经营范围
            DataTable dtc = cds.Tables["MA_CALLINGCLASS"];
            int numIndex = 0;
            foreach (DataRow row in dtc.Rows)
            {
                String ma_id = bs_manage.GetNextSeqNumber() + numIndex.ToString();
                String path = GetCallingClassPath(sql, row["ID"].ToString());
                if (!bs_manage.AddManageList(ma_id, maid, row["ID"].ToString(), path))
                {
                    throw new SysException("ED00000020");
                }
                numIndex++;
            }

            return cds;
        }

        private String GetCallingClassPath(DBConnect sql,String callingClassId) 
        {
            MA_CALLINGCLASS ma_callingClass = new MA_CALLINGCLASS(sql);
            String path = String.Empty;
            callingClass(ref path, ma_callingClass, callingClassId);
            if (path.Length > 0)
            {
                path = path.Remove(0, 1);
            }
            return path;
        }

        private void callingClass(ref String path, MA_CALLINGCLASS ma_callingClass,String callingClassId) 
        {
            DataTable dt = ma_callingClass.GetCallingType(callingClassId);
            foreach (DataRow row in dt.Rows) 
            {
                path = "/" + row["CLASSTYPE"].ToString() + path;
                callingClass(ref path,ma_callingClass, row["CLASSTYPE"].ToString());
            }
        }
    }
}
