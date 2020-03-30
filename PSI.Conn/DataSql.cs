using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace PSI.Conn
{
    public class DataSql
    {
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlTransaction trans = null;
        private string connstr = null;

        private string ReadAppSettingsConfigEx(string exeConfigFileName, string key)
        {
            if (!File.Exists(exeConfigFileName))
            {
                return string.Empty;
            }
            string empty = string.Empty;
            ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = exeConfigFileName
            };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = configuration.GetSection("appSettings") as AppSettingsSection;
            if (section.Settings.Count != 0)
            {
                string[] allKeys = section.Settings.AllKeys;
                int num = 0;
                while (num < (int)allKeys.Length)
                {
                    string str = allKeys[num];
                    if (str != key)
                    {
                        num++;
                    }
                    else
                    {
                        empty = section.Settings[str].Value;
                        break;
                    }
                }
            }
            return empty;
        }
        private string connUrl(string database)
        {
            if (!string.IsNullOrWhiteSpace(database))
            {
                string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\connection.config");
                string server = ReadAppSettingsConfigEx(xmlPath, "Server");
                string Uid = ReadAppSettingsConfigEx(xmlPath, "Userid");
                string pwd = ReadAppSettingsConfigEx(xmlPath, "Password");
                return "server=" + server + ";database=" + database + ";uid=" + Uid + ";pwd=" + pwd;
            }
            else
            {
                return "";
            }
        }

        private void Init()
        {
            try
            {
                if (connstr == null)
                {
                    throw (new Exception("无法读出"));
                }
                this.conn = new SqlConnection(connstr);
                this.cmd = new SqlCommand();
                cmd.Connection = this.conn;
                this.conn.Open();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSql(string database)
        {
            this.connstr = connUrl(database);
            Init();
        }
        private char parameterToken
        {
            get { return '@'; }
        }
        private string buildParameterName(string name)
        {
            if (name == null) throw new ArgumentNullException("ParameterName");

            if (name[0] != parameterToken)
            {
                return name.Insert(0, new string(parameterToken, 1));
            }
            return name;
        }
        private void addParameters(SqlCommand cmd, SqlDataItem sqlDataItem)
        {
            if (sqlDataItem.ParameterList == null) return;
            foreach (SqlDataParameter param in sqlDataItem.ParameterList)
            {
                cmd.Parameters.Add(new SqlParameter(buildParameterName(param.ParameterName), param.ParameterValue));
            }
        }
        private int executeNonQuery(SqlCommand cmd, SqlDataItem sqlDataItem)
        {
            //if (cmd == null) throw new ArgumentNullException("Sql Command");
            cmd.Parameters.Clear();
            try
            {
                Open();
                cmd.CommandText = sqlDataItem.CommandText;
                cmd.CommandType = Common.GetCommandType(sqlDataItem.CommandType);
                addParameters(cmd, sqlDataItem);
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void BeginTrans()
        {
            trans = conn.BeginTransaction();
            cmd.Transaction = trans;
        }
        public void CommitTrans()
        {
            trans.Commit();
        }
        public void RollbackTrans()
        {
            trans.Rollback();
        }
        public void Open()
        {
            if (conn.State.ToString().ToUpper() != "OPEN")
            {
                Init();
            }
        }
        public void Close()
        {
            if (conn.State.ToString().ToUpper() == "OPEN")
            {
                this.conn.Close();
                //this.conn.Dispose();
            }
        }
        public DataTable ExeSql_DT(SqlDataItem sqlDataItem)
        {
            Open();
            DataTable dt = new DataTable("data");
            SqlDataAdapter ad = new SqlDataAdapter();
            cmd.CommandType = Common.GetCommandType(sqlDataItem.CommandType);
            cmd.CommandText = sqlDataItem.CommandText;
            try
            {
                addParameters(cmd, sqlDataItem);
                ad.SelectCommand = cmd;
                ad.Fill(dt);
                ad.Dispose();
                return dt;
            }
            catch (Exception ee)
            {
                Close();
                throw ee;
            }
        }
        public void ExeSql_void(SqlDataItem sqlDataItem) 
        {
            try
            {
                Open();
                cmd.CommandType = Common.GetCommandType(sqlDataItem.CommandType);
                cmd.CommandText = sqlDataItem.CommandText;
                addParameters(cmd, sqlDataItem);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }

        public bool ExeSqls_bool(SqlDataItemArray arySqlDataItem) 
        {
            try
            {
                Open();
                BeginTrans();
                foreach (SqlDataItem sqlDataItem in arySqlDataItem.SqlDataItemList)
                {
                    cmd.Transaction = trans;
                    executeNonQuery(cmd, sqlDataItem);
                }
                CommitTrans();
                return true;
            }
            catch (Exception ex)
            {
                RollbackTrans();
                Close();
                throw ex;
            }
        }
        public int ExeSql_int(SqlDataItem sqlDataItem) 
        {
            try
            {
                cmd.CommandType = Common.GetCommandType(sqlDataItem.CommandType); ;
                cmd.CommandText = sqlDataItem.CommandText;
                addParameters(cmd, sqlDataItem);
                int t = cmd.ExecuteNonQuery();
                return t;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }
        public string ExeSql_string(SqlDataItem sqlDataItem) 
        {
            try
            {
                cmd.CommandType = Common.GetCommandType(sqlDataItem.CommandType); ;
                cmd.CommandText = sqlDataItem.CommandText;
                addParameters(cmd, sqlDataItem);
                string result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }
        public bool ExistSql_bool(SqlDataItem sqlDataItem)
        {
            DataTable dt = ExeSql_DT(sqlDataItem);
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
