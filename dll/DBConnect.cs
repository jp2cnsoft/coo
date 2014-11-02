using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Seika.CooException;

namespace Seika.Db
{
    public class DBConnect
    {
        private SqlConnection sqlConn = null;
        //声明一个sql事务对象
        private SqlTransaction transaction = null;
        //声明一个sql命令对象
        private SqlCommand sqlComm = null;
        //声明一个连接串
        private String con = String.Empty;

        public DBConnect()
        {
            //创建数据库连接对象
            sqlConn = new SqlConnection(con);
            sqlComm = new SqlCommand();
        }

        public DBConnect(String conn)
        {
            //创建数据库连接对象
            sqlConn = new SqlConnection(conn);
            sqlComm = new SqlCommand();
            con = conn;
        }
        //打开数据库
        public bool open ()
        {
            try
            {
                sqlConn.Open();
                //开始一个处理事务
                transaction = sqlConn.BeginTransaction();
                //sql命令对象事务为当前事务
                sqlComm.Transaction = transaction;
                //sql命令对像连接为当前连接
                sqlComm.Connection = sqlConn;

                return true;
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000010", e);
            }
        }

        //关闭数据库
        public bool close()
        {
            try
            {
                sqlConn.Close();
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000290", e);
            }
            return true;
        }

        //执行普通的SQL
        public bool ExecuteStrQuery(String m_str)
        {
            try
            {
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = m_str;
                return (sqlComm.ExecuteNonQuery() > 0);
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000300", e);
            }  
        }
        //执行插入图片的SQL
        public bool ExecuteImgQuery(String m_str,String img,byte [] m_data)
        {
            try
            {
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = m_str;
                //添加图片参数
                sqlComm.Parameters.Add(img, SqlDbType.Image);
                //给参数赋值
                sqlComm.Parameters[img].Value = m_data;
                return (sqlComm.ExecuteNonQuery() > 0);
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000310", e);
            }  
        }
        //事务回滚
        public void Rollback()
        {
            try
            {
                transaction.Rollback();
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000320", e);
            }  
        }
        //设置连接字符串
        public void SetConnectionString(String m_str)
        {
            con = m_str;
        }

        //重连数据库
        public bool RconncetDB()
        {
            try
            {
                sqlConn.Close();
                //创建数据库连接对象
                sqlConn = new SqlConnection(con);
                sqlComm = new SqlCommand();
                return true;
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000330", e);
            }
        }
        //读取数据库数据到DataSet
        public DataSet ExecuteDataset(String m_str)
        {
            DataSet ds = new DataSet();
            try
            {
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = m_str;
                SqlDataAdapter sda = new SqlDataAdapter(sqlComm);
                sda.Fill(ds);
                return ds;
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000340", e);
            }
        }
        //读取数据库数据到SqlDataReader
        public SqlDataReader ExecuteReader(String m_str)
        {
            SqlDataReader sdr = null;
            try
            {
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = m_str;
                sdr = sqlComm.ExecuteReader();
                return sdr;
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000350", e);
            }
        }
        //提交事务
        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch (SqlException e)
            {
                throw new SysException("ED00000020", e);
            }        
        }
    }
}
