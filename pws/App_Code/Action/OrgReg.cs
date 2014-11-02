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
using Seika.COO.Action;
using Seika.COO.Web.PG;

/// <summary>
/// Summary description for OrgReg
/// </summary>
namespace Seika.COO.Action
{
    public class OrgReg : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            //取得xml字段值
            DataTable dt = cds.Tables["MA_COMINFO"];
            DataRow dr = dt.Rows[0];

            //公司ID
            String rigistId = StringToFilter(dr["RIGISTID"].ToString());
            //密码
            String passWord = StringToFilter(dr["PASSWORD"].ToString());
            //公司名
            String name = StringToFilter(dr["NAME"].ToString());
            //公司所在国
            String countryId = StringToFilter(dr["COUNTRYID"].ToString());
            //公司所在省
            String provinceId = StringToFilter(dr["PROVINCEID"].ToString());
            //公司所在市
            String cityId = StringToFilter(dr["CITYID"].ToString());
            //公司所在区
            String boroughId = StringToFilter(dr["BOROUGHID"].ToString());

            //其它经营
            String otherManage = StringToFilter(dr["OTHERMANAGE"].ToString());
            //联系人
            String linkMan = StringToFilter(dr["LINKMAN"].ToString());
            //电话号
            String phoneCountry = StringToFilter(dr["PHONE_COUNTRY"].ToString());
            String phoneZone = StringToFilter(dr["PHONE_ZONE"].ToString());
            String phoneCode = StringToFilter(dr["PHONE_CODE"].ToString());
            String phoneExt = StringToFilter(dr["PHONE_EXT"].ToString());
            //传真号
            String faxCountry = StringToFilter(dr["FAX_COUNTRY"].ToString());
            String faxZone = StringToFilter(dr["FAX_ZONE"].ToString());
            String faxCode = StringToFilter(dr["FAX_CODE"].ToString());
            String faxExt = StringToFilter(dr["FAX_EXT"].ToString());
            //移动电话
            String mobiletel = StringToFilter(dr["MOBILETEL"].ToString());
            //邮箱地址
            String email = StringToFilter(dr["EMAIL"].ToString());
            //认证标志
            String attestation = StringToFilter(dr["ATTESTATION"].ToString());
            //邮政编码
            String postcode = StringToFilter(dr["POSTCODE"].ToString());
            //详细地址
            String detailsAddress = StringToFilter(dr["DETAILSADDRESS"].ToString());

            ////取得当前系统语言
            //m_session = new SessionManager(Session);
            //String defLangId = m_session.Page_UICultureID;

            MA_REGISTER ma_register = new MA_REGISTER(sql);
            MA_COMINFO ma_cominfo = new MA_COMINFO(sql);
            BS_MANAGE bs_manage = new BS_MANAGE(sql);
            MA_ACCOUNT ma_account = new MA_ACCOUNT(sql);

            //如果公司ID重复抛错并返回
            if (ma_register.GetComRegistid(rigistId).Rows.Count > 0)
            {
                throw new AppException("ED01000120");
            }
            String ma_registerId = ma_register.GetNextSeqNumber();
            //追加帐户信息
            if (!ma_register.AddRegisterInfo(ma_registerId, rigistId , passWord, email))
            {
                throw new SysException("ED00000020");
            }

            //追加公司信息,如果错误抛错并返回
            if (!ma_cominfo.AddComInfo(ma_cominfo.GetNextSeqNumber(), ma_registerId, rigistId, name
                , countryId, provinceId, cityId, boroughId
                , otherManage, linkMan
                , phoneCountry, phoneZone, phoneCode, phoneExt
                , faxCountry, faxZone, faxCode, faxExt
                , mobiletel, attestation,"", postcode, detailsAddress))
            {
                throw new SysException("ED00000020");
            }
            
            //追加帐户
            if (!ma_account.AddAccount(rigistId,"0","0","0"))
            {
                throw new SysException("ED00000020");
            }

            //取得公司ID
            DataTable madt = ma_cominfo.GetComInfoId(rigistId);
            String maid = madt.Rows[0][0].ToString();
            
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

            if (!String.IsNullOrEmpty(maid))
                return cds;

            return null;
        }

        //取得默认样式
        private String GetDefaultStyleId(DataTable defStyleDt)
        {
            foreach (DataRow row in defStyleDt.Rows)
            {
                return row["MA_STYLEID"].ToString();
            }
            return "";
        }

        private String GetCallingClassPath(DBConnect sql, String callingClassId)
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

        private void callingClass(ref String path, MA_CALLINGCLASS ma_callingClass, String callingClassId)
        {
            DataTable dt = ma_callingClass.GetCallingType(callingClassId);
            foreach (DataRow row in dt.Rows)
            {
                path = "/" + row["CLASSTYPE"].ToString() + path;
                callingClass(ref path, ma_callingClass, row["CLASSTYPE"].ToString());
            }
        }
    }
}
