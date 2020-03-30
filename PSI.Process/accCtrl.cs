using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PSI.Models;
using PSI.Conn;
using System.Data;

namespace PSI.Process
{
   public class accCtrl
    {
        public accCtrl() { }
        public string code { get; set; }
        public string result { get; set; }
        public Account account { get; set; }
        public accCtrl(string accID)
        {
            string str = "select * from Account where accountID='" + accID + "'";
            DataTable dt = db.getTable(str, "jinxiaocun");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                account = new Account();
                account.accountID = accID;
                account.accountName = dr["accountName"].ToString();
                account.databasePath = dr["databasePath"].ToString();
                account.databaseName = dr["databaseName"].ToString();
                account.remark = dr["remark"].ToString();
                account.creatDate = dr["creatDate"].ToString();
            }
        }
        /// <summary>
        /// 新增账套
        /// </summary>
        /// <param name="acc"></param>
        public void creatNew()
        {
            try
            {
                account.accountID = CommonTools.newAccountID();
                account.databaseName = account.accountID.Replace("-","");
                account.creatDate = DateTime.Now.ToString("yyyy-MM-dd");
                string filepath = AppDomain.CurrentDomain.BaseDirectory;
                account.databasePath = filepath + "Database\\";
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("insert into Account (");
                sqlstr.Append("accountID, accountName, databasePath, databaseName, remark,status, creatDate");
                sqlstr.Append(") values (");
                sqlstr.Append("'" + account.accountID + "', ");
                sqlstr.Append("'" + account.accountName + "', ");
                sqlstr.Append("'" + account.databasePath + "', ");
                sqlstr.Append("'" + account.databaseName + "', ");
                sqlstr.Append("'" + account.remark + "', ");
                sqlstr.Append("'" + account.status + "', ");
                sqlstr.Append("'" + account.creatDate + "' )");
                db.ExecVoid(sqlstr.ToString(), "jinxiaocun");
                string filePath1 = Path.Combine(filepath, "Default\\base.psi");
                string filePath2 = Path.Combine(filepath, "Default\\base_log.psi");
                string file1 = account.databasePath + account.databaseName + ".mdf";
                string file2 = account.databasePath + account.databaseName + "_log.ldf";
                File.Copy(filePath1, file1, true);
                AddSecurityControll(file1);
                File.Copy(filePath2, file2, true);
                AddSecurityControll(file2);
                StringBuilder str = new StringBuilder();
                str.Append("EXEC sp_attach_db ");
                str.Append("@dbname='" + account.databaseName + "', ");
                str.Append("@filename1='" + file1 + "', ");
                str.Append("@filename2='" + file2 + "' ");
                db.ExecVoid(str.ToString(), "master");
                code = "0000";
                result = "创建成功！";
            }
            catch (Exception ex)
            {
                code = "9999";
                result = ex.ToString();
            }
        }
        private void AddSecurityControll(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
            fileSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("Everyone", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
            fileSecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("Users", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
            fileInfo.SetAccessControl(fileSecurity);
        }
        /// <summary>
        /// 修改账套信息
        /// </summary>
        /// <param name="acc"></param>
        public void modify()
        {
            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("update Account set ");
                sqlstr.Append("accountName='" + account.accountName + "',");
                sqlstr.Append("remark='" + account.remark + "' ");
                sqlstr.Append("where accountID='" + account.accountID + "' ");
                db.ExecVoid(sqlstr.ToString(), "jinxiaocun");
                code = "0000";
                result = "修改成功！";
            }
            catch(Exception ex)
            {
                code = "9999";
                result = ex.ToString();
            }
        }
        /// <summary>
        /// 修改状态状态 0-启用 1-禁用
        /// </summary>
        /// <param name="stat"></param>
        public void changeStauts(string stat)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(account.accountID))
                {
                    string str = "update Account set status='" + stat + "' where accountID='" + account.accountID + "'";
                    db.ExecVoid(str, "jinxiaocun");
                    code = "0000";
                    result = "状态修改成功！";
                }
                else
                {
                    code = "0001";
                    result = "未找到对应的账套";
                }
            }
            catch(Exception ex)
            {
                code = "9999";
                result = ex.ToString();
            }
        }
        /// <summary>
        /// 删除账套
        /// </summary>
        public void delAcc()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(account.accountID))
                {
                    string str = "drop database " + account.databaseName;
                    db.ExecVoid(str, "master");
                    string str1 = "delete from Account where accountID='" + account.accountID + "'";
                    db.ExecVoid(str1, "jinxiaocun");
                    code = "0000";
                    result = "账套删除成功！";
                }
                else
                {
                    code = "0001";
                    result = "未找到对应的账套";
                }
            }
            catch(Exception ex)
            {
                code = "9999";
                result = ex.ToString();
            }
        }
    }
}
