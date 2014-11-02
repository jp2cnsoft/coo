using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Seika.COO.PageData;
using Seika.Transform.Command.Client;
using Seika.Common.Compress;

namespace Seika.COO.Web.PG
{
    /// <summary>
    /// 用户操作类
    /// </summary>
    public class UserOperate
    {
        P9010P0010 pageData = new P9010P0010();

        /// <summary>
        /// 删除FTP用户目录及数据库数据
        /// </summary>
        /// <param name="name">用户名</param>
        public void DeleteUser(String userid)
        {
            if (userid != "")
            {
                //删除FTP用户目录
                CommandSender cs = new CommandSender();
                cs.DeleteUserSite(userid);
 
                //删除数据库
                pageData.Remove(userid);
            }
        }

        /// <summary>
        /// 创建压缩文件
        /// </summary>
        /// <param name="filename">压缩文件名</param>
        /// <param name="directory">数据源目录</param>
        public void CreateZip(string filename, string directory)
        {
            //如果文件没有找到，则报错    
            //if (!System.IO.File.Exists(directory))
            //{
            //    throw new System.IO.FileNotFoundException("The specified file " + directory + " could not be found. Zipping aborderd");
            //}
            //else
            //{
                ZipCompress zipCompress = new ZipCompress();
                zipCompress.Zip(filename, directory);
            //}
        }
    }
}