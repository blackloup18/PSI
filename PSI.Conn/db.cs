using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PSI.Conn
{
   public class db
    {
        public static DataTable getTable(SqlDataItem item,string accountName)
        {
            try
            {
                DataSql agent = new DataSql(accountName);
                DataTable dt = agent.ExeSql_DT(item);
                agent.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable getTable(string SQLstr,string accountName)
        {
            try
            {
                SqlDataItem item = new SqlDataItem();
                item.CommandText = SQLstr;
                DataSql agent = new DataSql(accountName);
                DataTable dt = agent.ExeSql_DT(item);
                agent.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool ExecSQL(SqlDataItem item,string accountName)
        {
            try
            {
                DataSql agent = new DataSql(accountName);
                int result = agent.ExeSql_int(item);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool ExecSQLs(SqlDataItemArray items,string accountName)
        {
            try
            {
                DataSql agent = new DataSql(accountName);
                bool flag = agent.ExeSqls_bool(items);
                agent.Close();
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ExecVoid(string sqlstr,string accountName)
        {
            DataSql agent = new DataSql(accountName);
            try
            {
                SqlDataItem item = new SqlDataItem();
                item.CommandText = sqlstr;
                agent.ExeSql_void(item);
                agent.Close();
            }
            catch (Exception ee)
            {
                agent.Close();
                throw ee;
            }
        }
        public static bool testConn(string accountName)
        {
            try
            {
                DataSql agent = new DataSql(accountName);
                agent.Open();
                agent.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
