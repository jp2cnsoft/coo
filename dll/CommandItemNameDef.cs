using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seika.Transform.Command
{
    /// <summary>
    /// 参数种别定义
    /// </summary>
    public static class CommandItemNameDef
    {
        //命令ID
        public const String CommandId = "CMDID";
        //文件名
        public const String FilePath = "filepath";
        //返信必须
        public const String ReceiveNeed = "receiveneed";
        //风格ID
        public const String StyleId = "styleid";
        //分组NO
        public const String GroupNo = "groundno";
        //COO CD(服务器CD)
        public const String CooServerName = "CooServerName";
        //对象类别（个人/公司）
        public const String SubjectType = "subjecttype";
        //对象ID
        public const String SubjectId = "subjectid";
        //语言
        public const String Language = "language";
        //文件操作类别
        public const String StyleType = "operatetype";
        //操作源数据集名
        public const String SourceFileName = "sfilename";
        //生成文所需要XSL名
        public const String XSLFileName = "sxsl";
        //生成文件类型（stand,blue）
        public const String OutputFileType = "optype";
        //生成目标文件名
        public const String OutputFileName = "dfilename";
        //生成文件类别
        public const String GroupCatagory = "groupcatagory";
        //图片路径
        public const String ImagePath = "imgpath";
        //前页链接状态
        public const String PrevPage = "pervpage";
        //后面链接状态
        public const String NextPage = "nextpage";
        //删除标记
        public const String DeleteFlg = "deleteflg";
        //命令级别
        public const String CommandLevel = "operatelevel";
        //转换用XSL文件ID
        public const String TransXslId = "transxslid";
        //风格ID指定
        public const String TransStyleId = "transstyleid";
        //用户ID
        public const String UserId = "userid";
        //用户密码
        public const String Passwd = "passwd";
    }
}
