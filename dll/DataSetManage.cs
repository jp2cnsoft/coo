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
    public class DataSetManage
    {
        DataSet ds;
        DataRow dr;
        int index = 0;
        String xmlLocalPath = String.Empty;
        XmlDocManage xml;

        /// <summary>
        /// 取得表的数量
        /// </summary>
        public int TableCount
        {
            get { return ds.Tables.Count; }
        }

        /// <summary>
        /// 取得数据集
        /// </summary>
        public DataSet Get
        {
            get { return ds; }
        }

        /// <summary>
        /// 数据集路径
        /// </summary>
        public String XmlLocalPath
        {
            set { xmlLocalPath = value; }
        }

        /// <summary>
        /// XML文档
        /// </summary>
        public XmlDocManage Xml 
        {
            get { return xml; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataSetManage()
        {
            ds = new DataSet();
        }

        /// <summary>
        /// 带默认表构造
        /// </summary>
        /// <param name="tableName"></param>
        public DataSetManage(String tableName)
        {
            ds = new DataSet();
            ds.Tables.Add(new DataTable(tableName));
        }

        /// <summary>
        /// 带默认表构造
        /// </summary>
        /// <param name="tableName"></param>
        public DataSetManage(XmlDocManage xmlDocMang)
        {
            ds = new DataSet();
            XmlTextReader xtreader = xmlDocMang.ToXmlReader();
            if (xtreader != null)
            {
                ds.ReadXml((XmlReader)xtreader);
            }
        }

        /// <summary>
        /// 添加一张空表
        /// </summary>
        public void AddTable(String tableName)
        {
            ds.Tables.Add(new DataTable(tableName));
            index++;
        }

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="dt"></param>
        public void AddTable(DataTable dt) 
        {
            ds.Tables.Add(dt);
            index++;
        }

        /// <summary>
        /// 新建表行
        /// </summary>
        /// <param name="index">表格索引</param>
        public void NewRow()
        {
            dr = ds.Tables[index].NewRow();
        }

        /// <summary>
        /// 添加列(默认字段为string型)
        /// </summary>
        /// <param name="field"></param>
        public void AddColumns(String field) 
        {
            ds.Tables[index].Columns.Add(new DataColumn(field, typeof(string)));
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="field"></param>
        /// <param name="dataType"></param>
        public void AddColumns(String field, Type dataType)
        {
            ds.Tables[index].Columns.Add(new DataColumn(field, dataType));
        }

        /// <summary>
        /// 添加行数据
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">字段值</param>
        public void Add(String field,Object value)
        {
            dr[field] = value;
        }

        /// <summary>
        /// 添加单行数据(自动建立列)
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">字段值</param>
        public void AddColumnAndValue(String field, Object value)
        {
            ds.Tables[index].Columns.Add(new DataColumn(field, typeof(string)));
            dr[field] = value;
        }

        /// <summary>
        /// 添加单行数据(自动建立列)
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">字段值</param>
        public void AddColumnAndValue(String field, Type dataType,Object value)
        {
            ds.Tables[index].Columns.Add(new DataColumn(field, dataType));
            dr[field] = value;
        }

        /// <summary>
        /// 更新行数据
        /// </summary>
        public void RowBind()
        {
            ds.Tables[index].Rows.Add(dr);
        }

        /// <summary>
        /// 读取本地XML转成DataSet
        /// </summary>
        /// <param name="xmlPath">DataSet名称</param>
        /// <param name="xmlPath">xml路径</param>
        public bool ReadLocalXml2DataSet(String DataSetName,String xmlLocalPath) 
        {
            try
            {
                //判断文件是否存在
                if (!System.IO.File.Exists(xmlLocalPath))
                {
                    Log.WriteError(xmlLocalPath);
                    throw new SysException("ED00000410");
                }
                //类变量覆值
                this.xmlLocalPath = xmlLocalPath;
                //建立DataSet
                ds = new DataSet(DataSetName);
                //读取XML并转换成DataSet
                ds.ReadXml(xmlLocalPath);
                //调入到xml对象
                xml = new XmlDocManage(xmlLocalPath);
            }
            catch(SysException e) 
            {
                Log.WriteError(xmlLocalPath);
                throw new SysException("ED00000410", e);
            }
            return true;
        }

        /// <summary>
        /// 写入本地dataset到xml
        /// </summary>
        public void WriteLocalDataSet2Xml()
        {
            ds.WriteXml(xmlLocalPath,XmlWriteMode.IgnoreSchema);
        }

        /// <summary>
        /// 数据流dataset到xml
        /// </summary>
        public void WriteLocalDataSet2XmlStream()
        {
            MemoryStream stream = new MemoryStream();
            ds.WriteXml(stream);
            stream.Seek(0, SeekOrigin.Begin);
            xml = new XmlDocManage(stream);
        }

        /// <summary>
        /// 数据流dataset到xml
        /// </summary>
        public void WriteXmlStream(ref XmlDocManage xmldoc)
        {
            MemoryStream stream = new MemoryStream();
            ds.WriteXml(stream);
            stream.Seek(0, SeekOrigin.Begin);
            xmldoc = new XmlDocManage(stream);
        }

        /// <summary>
        /// DataSet写XML流
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public String DataSetWXml(DataSet ds)
        {
            String m_str = String.Empty;
            //创建一个内存流，未分配内存空间
            MemoryStream content = new MemoryStream();
            //创建读取内存的对象
            StreamReader sr = new StreamReader(content);
            try
            {
                //将DATASET以XML格式写到内存缓冲区里
                ds.WriteXml(content, XmlWriteMode.IgnoreSchema);
                //重置缓冲区大小
                content.Capacity = Convert.ToInt32(content.Length);
                //将流指针移至最前
                content.Seek(0, SeekOrigin.Begin);
                //读取缓冲区到字符串
                m_str = sr.ReadToEnd();
               
            }
            catch (SysException e)
            {
                throw new SysException("ED00000370", e);
            }
            finally
            {
                //关闭流
                sr.Close();
            }
            return m_str;        
        }

        /// <summary>
        /// DataSet读XML流
        /// </summary>
        /// <param name="m_str"></param>
        /// <returns></returns>
        public DataSet DataSetRXml(String m_str)
        {
            //创建一个内存流，未分配内存空间
            MemoryStream content = new MemoryStream();
            //创建写入内存的对象
            StreamWriter sw = new StreamWriter(content);
            //创建一个DataSet用于还原XML
            DataSet ds = new DataSet();
            try
            {
                //将字符串写入缓冲区
                sw.Write(m_str);
                //保存
                sw.Flush();
                //将流指针移至最前
                content.Seek(0, SeekOrigin.Begin);
                //DataSet读取缓冲区中的XML
                ds.ReadXml(content);
            }
            catch (SysException e)
            {
                throw new SysException("ED00000380", e);
            }
            finally
            {   //清理缓冲区
                sw.Close();
            }
            return ds;
        }

        /// <summary>
        /// 删除表行数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool DeleteTableLine(DataTable dt) 
        {
            try
            {
                int i = 0;
                //取得列数
                int columc = dt.Columns.Count;

                //遍历行
                while (i < dt.Rows.Count)
                {
                    //按条件删除行数据
                    if (dt.Rows[i][columc - 1].ToString() == "1")
                    {
                        //删除该行
                        dt.Rows.RemoveAt(i);
                        //更新
                        dt.AcceptChanges();
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch (SysException e)
            {
                throw new SysException("ED00000390", e);
            }

            return true;
        }

        /// <summary>
        /// 带列克隆表
        /// </summary>
        /// <param name="dt">原表</param>
        /// <param name="tablename">新表名</param>
        /// <param name="dcc">追加的新列</param>
        /// <returns></returns>
        public DataTable GetCloneTable(DataTable dt, String tablename, DataColumn[] dcc)
        {
            //构造新表
            DataTable cdt = new DataTable(tablename);

            //建立新表列
            foreach (DataColumn dc in dt.Columns)
            {   //新表列是否存在
                if (!cdt.Columns.Contains(dc.ColumnName))
                {   //添加新列
                    cdt.Columns.Add(dc.ColumnName);
                }
            }
            //建立追加列
            foreach (DataColumn dc in dcc)
            {   //新表列是否存在
                if (!cdt.Columns.Contains(dc.ColumnName))
                {
                    //添加新列
                    cdt.Columns.Add(dc.ColumnName);
                }
            }
            //导入行数据
            foreach (DataRow dr in dt.Rows)
            {   //整行导入
                cdt.ImportRow(dr);
            }
            return cdt;
        }

        /// <summary>
        /// 完全复制一个DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataTable GetCloneTable(DataTable dt, String tablename)
        {
            //克隆新表
            DataTable cdt = new DataTable();
            if (dt != null)
            {
                cdt = dt.Clone();
                //新表表名
                cdt.TableName = tablename;
                //导入行数据
                foreach (DataRow dr in dt.Rows)
                {
                    //整行导入
                    cdt.ImportRow(dr);
                }
            }
            return cdt;
        }

        /// <summary>
        /// 复制一个DataTable连续几行
        /// </summary>
        /// <param name="dt">原表</param>
        /// <param name="tablename">新表名</param>
        /// <param name="start">复制开始行</param>
        /// <param name="len">复制行数</param>
        /// <param name="sum">原表总行数</param>
        /// <returns></returns>
        public DataTable GetCloneTable(DataTable dt, String tablename, int start, int len, ref int sum)
        {
            //克隆新表
            DataTable cdt = dt.Clone();
            //新表表名
            cdt.TableName = tablename;
            //原表行数
            int count = dt.Rows.Count;
            //原表总行数
            sum = count;
            if (start < count)
            {
                //设置导入行数
                int loop = 0;
                if ((start + len) <= count)
                {
                    loop = len;
                }
                else
                {
                    loop = count - start;
                }
                //导入行数据
                for (int i = 0; i < loop; i++)
                {
                    //整行导入
                    cdt.ImportRow(dt.Rows[i + start]);
                }
            }
            return cdt;
        }
    }
}
