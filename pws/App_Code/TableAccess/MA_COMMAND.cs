﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Seika;
using Seika.COO.Util;
using Seika.Db;

namespace Seika.COO.DBA.MA
{
    public class MA_COMMAND : Seika.COO.DBA.DateBaseAccess
    {
        public MA_COMMAND(DBConnect sql)
            : base(sql)
        {
        }

        /// <summary>
        /// 追加命令
        /// </summary>
        /// <param name="ma_commandid">命令ID</param>
        /// <param name="groupNo">组ID</param>
        /// <param name="subjectType">服务器编号</param>
        /// <param name="subjectType">对象类型</param>
        /// <param name="subjectId">对象ID</param>
        /// <param name="subjectId">语言</param>
        /// <param name="operateType">操作类别</param>
        /// <param name="sFilename">源文件名</param>
        /// <param name="sXsl">源XSL名</param>
        /// <param name="opType">转换类别</param>
        /// <param name="cStyle">样式名称</param>
        /// <param name="dFilename">目标文件名</param>
        /// <param name="groupCatagory">生成HTML类别</param>
        /// <param name="groupCatagory">图片路径</param>
        /// <param name="pervPage">上页状态</param>
        /// <param name="nextPage">下页状态</param>
        /// <returns></returns>
        public bool AddCommand(String ma_commandid, String groupNo, String cooCode, String subjectType, 
               String subjectId, String lang,String operateType,String sFilename, String sXsl,
               String opType, String cStyle, String cssName, String dFilename, String groupCatagory,
               String imgPath, String pervPage, String nextPage, String operateLevel)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.AppendLine(" INSERT INTO ");
            cmd.AppendLine(" MA_COMMAND ");
            cmd.AppendLine("       (");
            cmd.AppendLine("       GROUPNO");
            cmd.AppendLine("       ,COOCODE");
            cmd.AppendLine("       ,SUBJECTTYPE");
            cmd.AppendLine("       ,SUBJECTID");
            cmd.AppendLine("       ,LANG");
            cmd.AppendLine("       ,OPERATETYPE");
            cmd.AppendLine("       ,SFILENAME");
            cmd.AppendLine("       ,SXSL");
            cmd.AppendLine("       ,OPTYPE");
            cmd.AppendLine("       ,CSTYLE");
            cmd.AppendLine("       ,CSSNAME");
            cmd.AppendLine("       ,DFILENAME");
            cmd.AppendLine("       ,GROUPCATAGORY");
            cmd.AppendLine("       ,IMGPATH");
            cmd.AppendLine("       ,PERVPAGE");
            cmd.AppendLine("       ,NEXTPAGE");
            cmd.AppendLine("       ,OPERATELEVEL");
            cmd.AppendLine("       ,CREATEDATE)");
            cmd.AppendLine(" VALUES ");
            cmd.AppendFormat("   ( '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',GetDate())",
                groupNo, cooCode,subjectType, subjectId, lang,operateType,
                sFilename, sXsl, opType, cStyle, cssName, dFilename, groupCatagory,
                imgPath, pervPage, nextPage, operateLevel);

            return sql.ExecuteStrQuery(cmd.ToString());
        }
    }
}