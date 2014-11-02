using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections;
using Seika.CooException;
using Seika.Util;

namespace Seika 
{
    public class DataViewReorder : DataView
    {
        /// <summary>
        /// 交换表行数据
        /// </summary>
        /// <param name="oldRow">原行</param>
        /// <param name="newRow">现行</param>
        /// <returns></returns>
        public void Swap(int oldRow, int newRow)
        {
            DataRow dr = this.ToTable().NewRow();
            dr.ItemArray = this.Table.Rows[oldRow].ItemArray;
            this.Table.Rows[oldRow].ItemArray = this.Table.Rows[newRow].ItemArray;
            this.Table.Rows[newRow].ItemArray = dr.ItemArray;  
        }
        /// <summary>
        /// 排列表行数据
        /// </summary>
        /// <param name="oldRow">原行</param>
        /// <param name="newRow">现行</param>
        public void Sort(int oldRow, int newRow) 
        {
            DataTable dt = this.ToTable();
            DataRow dr = dt.NewRow();
            dr.ItemArray = dt.Rows[oldRow].ItemArray;
            dt.Rows.InsertAt(dr, (oldRow < newRow) ? newRow + 1 : newRow);
            dt.Rows.RemoveAt((oldRow < newRow) ? oldRow : oldRow + 1);
            this.Table = dt;
        }

    }
}
