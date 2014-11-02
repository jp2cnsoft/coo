using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Seika.Api
{
    /// <summary>
    /// api对象
    /// </summary>
    public class ApiImporterCollection
    {
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 数据内容表
        /// </summary>
        public DataTable DaTable { get; set; }
        /// <summary>
        /// 数据内容集合
        /// </summary>
        public DataSet DaTableColl { get; set; }
        /// <summary>
        /// 字符串类型数据串
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public String StrCode { get; set; }
    }
}
