using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using PSI.Models;
using PSI.Conn;
using System.Data.SqlClient;

namespace PSI.Process
{
    public class AccountManage
    {
        public static string ReadAppSettingsConfigEx(string exeConfigFileName, string key)
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
        public static void UpdateAppConfig(string exeConfigFileName,string newKey, string newValue)
        {
            ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap()
            {
                ExeConfigFilename = exeConfigFileName
            };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
            (configuration.GetSection("appSettings") as AppSettingsSection).Settings.Remove(newKey);
            configuration.AppSettings.Settings.Add(newKey, newValue);
            configuration.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static BasicParam getBasicParam()
        {
            BasicParam basic = new BasicParam();
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\connection.config");
            basic.server = ReadAppSettingsConfigEx(xmlPath, "Server");
            basic.Uid = ReadAppSettingsConfigEx(xmlPath, "Userid");
            basic.Pwd = ReadAppSettingsConfigEx(xmlPath, "Password");
            basic.defaulBase = ReadAppSettingsConfigEx(xmlPath, "Database");
            basic.AccountDir = ReadAppSettingsConfigEx(xmlPath, "AccountDir");
            basic.BackupDir = ReadAppSettingsConfigEx(xmlPath, "BackupDir");
            return basic;
        }
        public static void setBasicParam(BasicParam basic)
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\connection.config");
            UpdateAppConfig(xmlPath,"Server", basic.server);
            UpdateAppConfig(xmlPath,"Userid", basic.Uid);
            UpdateAppConfig(xmlPath,"Password", basic.Pwd);
            UpdateAppConfig(xmlPath,"Database", basic.defaulBase);
            UpdateAppConfig(xmlPath,"AccountDir", basic.AccountDir);
            UpdateAppConfig(xmlPath,"BackupDir", basic.BackupDir);
        }
        public static void creatAccount(string name)
        {
            string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\connection.config");
            string server = ReadAppSettingsConfigEx(xmlPath, "Server");
            string uid = ReadAppSettingsConfigEx(xmlPath, "Userid");
            string pwd = ReadAppSettingsConfigEx(xmlPath, "Password");
            string pathAccount = ReadAppSettingsConfigEx(xmlPath, "AccountDir");
            string filePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\jinxiaocun.mdf");
            string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\jinxiaocun_log.ldf");
            string file1 = pathAccount + "\\" + name + ".mdf";
            string file2 = pathAccount + "\\" + name + "_log.ldf";
            try
            {
                File.Copy(filePath1, file1,true);
                File.Copy(filePath2, file2,true);
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("EXEC sp_attach_db ");
                sqlstr.Append("@dbname='" + name + "', ");
                sqlstr.Append("@filename1='" + file1 + "', ");
                sqlstr.Append("@filename2='" + file2 + "' ");
                string sql = "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=master";
                SqlConnection myconn = new SqlConnection(sql);
                SqlCommand sqlCommand = new SqlCommand(sqlstr.ToString(), myconn);
                myconn.Open();
                sqlCommand.ExecuteNonQuery();
                myconn.Close();
            }
            catch(Exception ex) { }
        }



        public static List<Account> getAccountList(string accName)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select * from Account ");
            if (!string.IsNullOrWhiteSpace(accName))
            {
                sqlstr.Append("where accountName like '%" + accName + "%' ");
            }
            sqlstr.Append("order by accountID desc");
            DataTable dt = db.getTable(sqlstr.ToString(), "jinxiaocun");
            List<Account> list = new List<Account>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Account acc = new Account();
                acc.accountID = dr["accountID"].ToString();
                acc.accountName = dr["accountName"].ToString();
                acc.databaseName = dr["databaseName"].ToString();
                acc.databasePath = dr["databasePath"].ToString();
                acc.status =int.Parse(dr["status"].ToString());
                acc.remark = dr["remark"].ToString();
                acc.ID = int.Parse(dr["ID"].ToString());
                acc.creatDate = dr["creatDate"].ToString();
                list.Add(acc);
            }
            return list;
        }
    }
}
