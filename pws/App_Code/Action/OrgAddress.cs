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

namespace Seika.COO.Action
{
    /// <summary>
    /// 组织地址取得
    /// </summary>
    public class OrgAddress : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            if (cds == null)
            {
                throw new SysException("ED00000020");
            }
            String id;
            res = new DataSet();
            cmdt = new DataSetManage();
            ma_zone = new MA_ZONECLASS(sql);

            //无任何参数 
            if (oparms == null)
            {
                //取得地域下拉
                id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
                res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(id), "NAME"));
            }
            else
            {
                //带参数
                switch (oparms[0])
                {
                    //带默认值的全部取得
                    case "0":
                        //取得默认值
                        DataTable dt = cds.Tables["ADDRESS"];
                        //取得国家列表
                        res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList("ROOT"), "COUNTRY"));
                        //取得省市区列表
                        foreach (DataRow row in dt.Rows)
                        {
                            String provinceName = "PROVINCE";
                            String city = "CITY";
                            String borough = "BOROUGH";
                            res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(row["COUNTRY"].ToString()), provinceName));
                            res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(row["PROVINCE"].ToString()), city));
                            res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(row["CITY"].ToString()), borough));
                        }
                        break;
                    //条件取得
                    case "1":
                        id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
                        GetList(ref res, cmdt, ma_zone, id);
                        break;
                    //全部取得
                    case "2":
                        GetList(res, cmdt, ma_zone, null);
                        break;
                    //只取一级(包括条件地域信息)
                    case "3":
                        id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
                        res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetAddressContent(id), "NAME"));
                        res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(id), "ADDRESS"));
                        break;
                    case "4":
                        id = StringToFilter(cds.Tables[0].Rows[0][0].ToString());
                        res.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList(id), "ADDRESS"));
                        break;
                }
            }
            //返回结果集
            return res;
        }
    }
}
