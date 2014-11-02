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
    public class OrgList : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {
            //实例化一个数据集合
            DataSet ds = new DataSet();
            DataSetManage cmdt = new DataSetManage();
            //地址父ID
            String fid = String.Empty;

            MA_CODE ma_code = new MA_CODE(sql);
            MA_ZONECLASS ma_zone = new MA_ZONECLASS(sql);
            MA_CALLINGCLASS ma_calling = new MA_CALLINGCLASS(sql);
            MA_PRODUCTCLASS ma_product = new MA_PRODUCTCLASS(sql);
            //页面初始化
            if (oparms[0] == "0")
            {
                //取得所有下拉列表                      
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetMoney(), "BANKROLLUNIT"));
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetOrgCharacter(), "CHARACTER"));
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetOrgScale(), "EMPLOYEENUM"));
                ds.Tables.Add(cmdt.GetCloneTable(ma_code.GetManageMode(), "MANAGEMODE"));

                ds.Tables.Add(cmdt.GetCloneTable(ma_zone.GetOrgList("ROOT"), "COUNTRY"));
                if (ds.Tables["COUNTRY"].Rows.Count > 0 && ds.Tables["COUNTRY"].Rows[0]["MA_ORDERID"] != null)
                {
                    fid = ds.Tables["COUNTRY"].Rows[0]["MA_ORDERID"].ToString();
                    switch (oparms[1])
                    {
                        //更新页面初始化
                        case "0":
                            GetList(ds, cmdt, ma_zone, fid);
                            break;
                        case "1":
                            break;
                    }
                }

                ds.Tables.Add(cmdt.GetCloneTable(ma_calling.GetOrgList("ROOT"), "CALLING"));
                if (ds.Tables["CALLING"].Rows.Count > 0 && ds.Tables["CALLING"].Rows[0]["MA_ORDERID"] != null)
                {
                    fid = ds.Tables["CALLING"].Rows[0]["MA_ORDERID"].ToString();
                    GetList(ds, cmdt, ma_calling, fid);
                }

                ds.Tables.Add(cmdt.GetCloneTable(ma_product.GetOrgList("ROOT"), "PRODUCT"));
                if (ds.Tables["PRODUCT"].Rows.Count > 0 && ds.Tables["PRODUCT"].Rows[0]["MA_PRODUCTCLASSID"] != null)
                {
                    fid = ds.Tables["PRODUCT"].Rows[0]["MA_PRODUCTCLASSID"].ToString();
                    GetList(ds, cmdt, ma_product, fid);
                }

            }
            //取得MA_CLASSMANAGEID所有节点（不包含子节点）
            else
            {
                if (cds == null)
                {
                    throw new SysException("ED00000020");
                }

                String m_str = cds.Tables[0].Rows[0][0].ToString();

                if (oparms[0] == "1")
                {
                    DataColumn ac = new DataColumn("ISSUB");
                    DataTable dt = null;
                    switch (oparms[1])
                    {
                        case "1":
                            dt = cmdt.GetCloneTable(ma_zone.GetOrgList(m_str), m_str);
                            dt.Columns.Add(ac);
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (ma_zone.IsSub(dr["MA_ORDERID"].ToString()))
                                {
                                    dr["ISSUB"] = 1;
                                }
                                else
                                {
                                    dr["ISSUB"] = 0;
                                }
                            }
                            break;
                        case "2":
                            dt = cmdt.GetCloneTable(ma_calling.GetOrgList(m_str), m_str);
                            dt.Columns.Add(ac);
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (ma_calling.IsSub(dr["MA_ORDERID"].ToString()))
                                {
                                    dr["ISSUB"] = 1;
                                }
                                else
                                {
                                    dr["ISSUB"] = 0;
                                }
                            }
                            break;
                        case "3":
                            dt = cmdt.GetCloneTable(ma_product.GetOrgList(m_str), m_str);
                            dt.Columns.Add(ac);
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (ma_product.IsSub(dr["MA_PRODUCTCLASSID"].ToString()))
                                {
                                    dr["ISSUB"] = 1;
                                }
                                else
                                {
                                    dr["ISSUB"] = 0;
                                }
                            }
                            break;
                    }
                    ds.Tables.Add(dt);

                }
                //通过邮编取地址
                else if (oparms[0] == "2")
                {

                    DataTable pdt = cmdt.GetCloneTable(ma_zone.GetAddressbyPostCode(m_str), m_str);

                    if (pdt.Rows.Count > 0 && pdt.Rows[0]["CLASSTYPE"] != null)
                    {
                        pdt = GetAddressById(pdt, pdt.Rows[0]["CLASSTYPE"].ToString(), ma_zone, cmdt);
                    }

                    if (pdt.Rows.Count > 0 && pdt.Rows[0]["MA_ORDERID"] != null)
                    {
                        pdt = GetAddressByType(pdt, pdt.Rows[0]["MA_ORDERID"].ToString(), ma_zone, cmdt);
                    }
                    ds.Tables.Add(pdt);

                    if (pdt.Rows.Count > 0 && pdt.Rows[0]["MA_ORDERID"] != null)
                    {
                        GetList(ds, cmdt, ma_zone, pdt.Rows[0]["MA_ORDERID"].ToString());
                    }
                }
                //取得MA_CLASSMANAGEID所有节点（包含子节点）
                else
                {
                    switch (oparms[1])
                    {
                        case "1":
                            GetList(ds, cmdt, ma_zone, m_str);
                            break;
                        case "2":
                            GetList(ds, cmdt, ma_calling, m_str);
                            break;
                        case "3":
                            GetList(ds, cmdt, ma_product, m_str);
                            break;
                    }
                }
            }
            //返回结果集
            return ds;
        }

        //通过邮编获得地址向上找
        private DataTable GetAddressById(DataTable dt, String id, MA_ZONECLASS ma_zone, DataSetManage cmdt)
        {
            DataTable pdt = cmdt.GetCloneTable(ma_zone.GetAddressContent(id), id);
            if (pdt.Rows.Count > 0 && pdt.Rows[0]["MA_ORDERID"] != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    pdt.Rows.Add(dr);
                }
                return GetAddressById(pdt, pdt.Rows[0]["MA_ORDERID"].ToString(), ma_zone, cmdt);
            }
            else
            {
                return dt;
            }
        }

        //通过邮编获得地址向下找
        private DataTable GetAddressByType(DataTable dt, String type, MA_ZONECLASS ma_zone, DataSetManage cmdt)
        {
            DataTable pdt = cmdt.GetCloneTable(ma_zone.GetOrgList(type), type);
            if (pdt.Rows.Count > 0 && pdt.Rows[0]["CLASSTYPE"] != null)
            {
                dt.Rows.Add(pdt.Rows[0]);
                return GetAddressByType(dt, pdt.Rows[0]["CLASSTYPE"].ToString(), ma_zone, cmdt);
            }
            else
            {
                return dt;
            }
        }
    }
}
