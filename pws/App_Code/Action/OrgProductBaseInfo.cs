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

/// <summary>
/// Summary description for OrgProductBaseInfo
/// </summary>
namespace Seika.COO.Action
{
    public class OrgProductBaseInfo : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {

            if (cds == null)
            {
                throw new SysException("ED00000020");
            }

            //取得xml字段值
            DataTable dt = cds.Tables["PRODUCT"];
            DataRow dr = dt.Rows[0];

            String status = dr["STATUS"].ToString();

            DataSet dsc = new DataSet("PRODUCT");
            switch (status)
            {
                case "add":
                    dsc = AddProduct(dt, sql);
                    break;
                case "update":
                    dsc = UpdateProduct(dt, sql);
                    break;
            }

            return dsc;
        }


        public DataSet AddProduct(DataTable dt, DBConnect sql)
        {
            DataRow dr = dt.Rows[0];
            //产品ID
            String ma_productid = StringToFilter(dr["ID"].ToString());
            //公司ID
            String rigistId = StringToFilter(dr["REGISTID"].ToString());
            String name = StringToFilter(dr["NAME"].ToString());
            String explain = StringToFilter(dr["EXPLAIN"].ToString());
            String imgname = StringToFilter(dr["IMGNAME"].ToString());
            String price = StringToFilter(dr["PRICE"].ToString());
            String priceunitid = StringToFilter(dr["PRICEUNITID"].ToString());
            String publishdate = StringToFilter(dr["PUBLISHDATE"].ToString());
            String pubcom = StringToFilter(dr["PUBCOM"].ToString());
            String countryId = StringToFilter(dr["COUNTRYID"].ToString());
            String provinceId = StringToFilter(dr["PROVINCEID"].ToString());
            String cityId = StringToFilter(dr["CITYID"].ToString());
            String boroughId = StringToFilter(dr["BOROUGHID"].ToString());
            String lang = StringToFilter(dr["LANG"].ToString());
            //判断产品分类是否写入数据库
            bool boolProdClass = Convert.ToBoolean(StringToFilter(dr["PRODCLASS"].ToString()));

            MA_PRODUCT ma_product = new MA_PRODUCT(sql);
            BS_MANAGE bs_manage = new BS_MANAGE(sql);
            BS_PRODUCT bs_product = new BS_PRODUCT(sql);

            //追加公司信息,如果错误抛错并返回
            if (!ma_product.AddProductBaseInfo(ma_productid, rigistId, name, explain,
                imgname, price, priceunitid, publishdate, 
                pubcom,countryId, provinceId, cityId, boroughId, lang))
            {
                throw new SysException("ED00000020");
            }

            if (boolProdClass == true)
            {
                //产品分类ID
                String ma_productclassid = dr["MA_PRODUCTCLASSID"].ToString();
                //产品分类父路径
                String path = dr["PATH"].ToString();

                if (!bs_product.AddProductBs(bs_manage.GetNextSeqNumber(), ma_productid, ma_productclassid, path))
                {
                    throw new SysException("ED00000020");
                }
            }

            
            DataTable dtp = new DataTable("PRODUCT");
            dtp.Columns.Add(new DataColumn("id"));
            DataRow drs = dtp.NewRow();
            drs["id"] = ma_productid;
            dtp.Rows.Add(drs);
            DataSet dsc = new DataSet();
            dsc.Tables.Add(dtp);
            return dsc;

        }


        public DataSet UpdateProduct(DataTable dt, DBConnect sql)
        {
            DataRow dr = dt.Rows[0];

            String strProductId = dr["ID"].ToString();
            //公司ID
            String rigistId = StringToFilter(dr["REGISTID"].ToString());
            String name = StringToFilter(dr["NAME"].ToString());
            String explain = StringToFilter(dr["EXPLAIN"].ToString());
            String imgname = StringToFilter(dr["IMGNAME"].ToString());
            String price = StringToFilter(dr["PRICE"].ToString());
            String priceunitid = StringToFilter(dr["PRICEUNITID"].ToString());
            String publishdate = StringToFilter(dr["PUBLISHDATE"].ToString());
            String pubcom = StringToFilter(dr["PUBCOM"].ToString());
            String countryId = StringToFilter(dr["COUNTRYID"].ToString());
            String provinceId = StringToFilter(dr["PROVINCEID"].ToString());
            String cityId = StringToFilter(dr["CITYID"].ToString());
            String boroughId = StringToFilter(dr["BOROUGHID"].ToString());
            String lang = StringToFilter(dr["LANG"].ToString());
            //判断产品分类是否写入数据库
            bool boolProdClass = Convert.ToBoolean(StringToFilter(dr["PRODCLASS"].ToString()));

            MA_PRODUCT ma_product = new MA_PRODUCT(sql);
            BS_PRODUCT bs_product = new BS_PRODUCT(sql);

            if (!ma_product.UpdateProductBaseInfo(strProductId, rigistId, name, explain,
                imgname, price, priceunitid, publishdate,
                pubcom,countryId, provinceId, cityId, boroughId, lang))
            {
                throw new SysException("ED00000020");
            }

            if (boolProdClass == true)
            {
                //产品分类ID
                String ma_productclassid = dr["MA_PRODUCTCLASSID"].ToString();
                //产品分类父路径
                String path = dr["PATH"].ToString();

                if (!bs_product.UpdateProductBs(strProductId, ma_productclassid, path))
                {
                    throw new SysException("ED00000020");
                }
            }

            DataTable dtp = new DataTable("PRODUCT");
            dtp.Columns.Add(new DataColumn("id"));
            DataRow drs = dtp.NewRow();
            drs["id"] = strProductId;
            dtp.Rows.Add(drs);
            DataSet dsc = new DataSet();
            dsc.Tables.Add(dtp);

            return dsc;

        }
    }
}
