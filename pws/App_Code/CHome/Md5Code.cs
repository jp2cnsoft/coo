using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Web.Security;

/// <summary>
/// Md5加密函数
/// </summary>
namespace Seika.COO.Web.PG
{
    public class Md5Code
    {
        public String GetMd5Code(String str) 
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
    }
}