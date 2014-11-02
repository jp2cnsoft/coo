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
/// Summary description for OrgPostBaseInfo
/// </summary>

namespace Seika.COO.Action
{
    public class OrgPostBaseInfo : ActionPageBase
    {
        public override DataSet Run(DBConnect sql, DataSet cds, String[] oparms)
        {

            if (cds == null)
            {
                throw new SysException("ED00000020");
            }

            //取得xml字段值
            DataTable dt = cds.Tables["POSTDB"];
            DataRow dr = dt.Rows[0];

            String status = dr["STATUS"].ToString();

            DataSet dsc = new DataSet("POSTDB");
            switch (status)
            {
                case "add":
                    dsc = AddPost(dt, sql);
                    break;
                case "update":
                    dsc = UpdatePost(dt, sql);
                    break;
            }

            return dsc;
        }


        public DataSet AddPost(DataTable dt, DBConnect sql)
        {
            DataRow dr = dt.Rows[0];
            //职位ID
            String ma_jobId = StringToFilter(dr["ID"].ToString());
            //公司ID
            String rigistId = StringToFilter(dr["REGISTID"].ToString());
            //姓名
            String name = StringToFilter(dr["NAME"].ToString());
            //招聘人数
            int number = Convert.ToInt32(StringToFilter(dr["NUMBER"].ToString()));
            //学历
            String schoollevelId = StringToFilter(dr["SCHOOLLEVELCD"].ToString());
            //性别
            String sexId = StringToFilter(dr["SEXCD"].ToString());
            //年龄
            String ageBegin = StringToFilter(dr["AGE_BEGIN"].ToString());
            String ageEnd = StringToFilter(dr["AGE_END"].ToString());
            //工作经验
            String experience = StringToFilter(dr["EXPERIENCE"].ToString());
            //工作性质
            String workkindId = StringToFilter(dr["WORKKINDCD"].ToString());
            //职位月薪
            String wage = StringToFilter(dr["WAGE"].ToString());
            //职位描述
            String officedepict = StringToFilter(dr["OFFICEDEPICT"].ToString());
            //职位要求
            String officerequest = StringToFilter(dr["OFFICEREQUEST"].ToString());
            //语言
            String lang = StringToFilter(dr["LANG"].ToString());
            //创建时间
            String createdate = StringToFilter(dr["CREATEDATE"].ToString());
            //更新时间
            String updatedate = StringToFilter(dr["UPDATEDATE"].ToString());
            //有效日期
            String effectdate = StringToFilter(dr["EFFECTDATE"].ToString());
            //地址
            String countryId = StringToFilter(dr["STATECD_1"].ToString());
            String provinceId = StringToFilter(dr["STATECD_2"].ToString());
            String cityId = StringToFilter(dr["STATECD_3"].ToString());
            String boroughId = StringToFilter(dr["STATECD_4"].ToString());

            String country = StringToFilter(dr["STATERESCD_1"].ToString());
            String province = StringToFilter(dr["STATERESCD_2"].ToString());
            String city = StringToFilter(dr["STATERESCD_3"].ToString());
            String borough = StringToFilter(dr["STATERESCD_4"].ToString());

            //职位ID
            String ma_orderid = dr["MA_ORDERID"].ToString();
            //职位类型
            String ma_classtype = dr["CLASSTYPE"].ToString();

            MA_POST ma_post = new MA_POST(sql);

            //追加公司信息,如果错误抛错并返回
            if (!ma_post.AddPostBaseInfo(ma_jobId, name, rigistId, ma_classtype, ma_orderid, number, countryId,
                provinceId, cityId, boroughId, country, province, city, borough, workkindId, effectdate, wage,
                schoollevelId, sexId, ageBegin, ageEnd, experience, officedepict, officerequest, lang))
            {
                throw new SysException("ED00000020");
            }

            
            DataTable dtp = new DataTable("POST");
            dtp.Columns.Add(new DataColumn("id"));
            DataRow drs = dtp.NewRow();
            drs["id"] = ma_jobId;
            dtp.Rows.Add(drs);
            DataSet dsc = new DataSet();
            dsc.Tables.Add(dtp);
            return dsc;

        }


        public DataSet UpdatePost(DataTable dt, DBConnect sql)
        {
            DataRow dr = dt.Rows[0];

            //职位ID
            String ma_jobId = StringToFilter(dr["ID"].ToString());
            //公司ID
            String rigistId = StringToFilter(dr["REGISTID"].ToString());
            //姓名
            String name = StringToFilter(dr["NAME"].ToString());
            //招聘人数
            int number = Convert.ToInt32(StringToFilter(dr["NUMBER"].ToString()));
            //学历
            String schoollevelId = StringToFilter(dr["SCHOOLLEVELCD"].ToString());
            //性别
            String sexId = StringToFilter(dr["SEXCD"].ToString());
            //年龄
            String ageBegin = StringToFilter(dr["AGE_BEGIN"].ToString());
            String ageEnd = StringToFilter(dr["AGE_END"].ToString());
            //工作经验
            String experience = StringToFilter(dr["EXPERIENCE"].ToString());
            //工作性质
            String workkindId = StringToFilter(dr["WORKKINDCD"].ToString());
            //职位月薪
            String wage = StringToFilter(dr["WAGE"].ToString());
            //职位描述
            String officedepict = StringToFilter(dr["OFFICEDEPICT"].ToString());
            //职位要求
            String officerequest = StringToFilter(dr["OFFICEREQUEST"].ToString());
            //语言
            String lang = StringToFilter(dr["LANG"].ToString());
            //创建时间
            String createdate = StringToFilter(dr["CREATEDATE"].ToString());
            //更新时间
            String updatedate = StringToFilter(dr["UPDATEDATE"].ToString());
            //有效日期
            String effectdate = StringToFilter(dr["EFFECTDATE"].ToString());
            //地址
            String countryId = StringToFilter(dr["STATECD_1"].ToString());
            String provinceId = StringToFilter(dr["STATECD_2"].ToString());
            String cityId = StringToFilter(dr["STATECD_3"].ToString());
            String boroughId = StringToFilter(dr["STATECD_4"].ToString());

            String country = StringToFilter(dr["STATERESCD_1"].ToString());
            String province = StringToFilter(dr["STATERESCD_2"].ToString());
            String city = StringToFilter(dr["STATERESCD_3"].ToString());
            String borough = StringToFilter(dr["STATERESCD_4"].ToString());

            //职位ID
            String ma_orderid = dr["MA_ORDERID"].ToString();
            //职位类型
            String ma_classtype = dr["CLASSTYPE"].ToString();

            MA_POST ma_post = new MA_POST(sql);

            if (!ma_post.UpdatePostBaseInfo(ma_jobId, name, rigistId, ma_classtype, ma_orderid, number, countryId,
                provinceId, cityId, boroughId, country, province, city, borough, workkindId, effectdate, wage,
                schoollevelId, sexId, ageBegin, ageEnd, experience, officedepict, officerequest, lang))
            {
                throw new SysException("ED00000020");
            }

            DataTable dtp = new DataTable("POST");
            dtp.Columns.Add(new DataColumn("id"));
            DataRow drs = dtp.NewRow();
            drs["id"] = ma_jobId;
            dtp.Rows.Add(drs);
            DataSet dsc = new DataSet();
            dsc.Tables.Add(dtp);

            return dsc;

        }
    
    }
}