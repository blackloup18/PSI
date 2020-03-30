using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.Conn;
using PSI.Models;
using System.Data;

namespace PSI.Process
{
    public class UserInfoCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public UserInfo UserInfo { get; set; }
        public string Account { get; set; }
        public UserInfoCtrl(string _Account)
        {
            Account = _Account;
        }
        public UserInfoCtrl(string _Account, string UserID)
        {
            Account = _Account;
            string str = "select TOP (1) * from UserInfo where SID='" + UserID + "'";
            DataTable dt = db.getTable(str, Account);
            if (dt != null && dt.Rows.Count > 0)
            {
                UserInfo = new UserInfo();
                DataRow dr = dt.Rows[0];
                UserInfo.ID = int.Parse(dr["ID"].ToString());
                UserInfo.CreatDate = dr["CreatDate"].ToString();
                UserInfo.Creator = dr["Creator"].ToString();
                UserInfo.LoginID = dr["LoginID"].ToString();
                UserInfo.Password = dr["Password"].ToString();
                UserInfo.Phone = dr["Phone"].ToString();
                UserInfo.DepartID = dr["DepartID"].ToString();
                UserInfo.Status = int.Parse(dr["Status"].ToString());
                UserInfo.SID = UserID;
                UserInfo.UserName = dr["UserName"].ToString();
                Code = "0000";
                Msg = "成功";
            }
        }
        public void Save()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserInfo.UserName))
                {
                    UserInfo.SID = CommonTools.GetNewID("RY", Account);
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("insert into UserInfo (");
                    sqlstr.Append("SID, LoginID, Password, UserName, Phone, Status,DepartID, Creator, CreatDate");
                    sqlstr.Append(") values (");
                    sqlstr.Append("'" + UserInfo.SID + "',");
                    sqlstr.Append("'" + UserInfo.LoginID + "',");
                    sqlstr.Append("'" + UserInfo.Password + "',");
                    sqlstr.Append("'" + UserInfo.UserName + "',");
                    sqlstr.Append("'" + UserInfo.Phone + "',");
                    sqlstr.Append("'" + UserInfo.Status + "',");
                    sqlstr.Append("'" + UserInfo.DepartID + "',");
                    sqlstr.Append("'" + UserInfo.Creator + "',");
                    sqlstr.Append("'" + UserInfo.CreatDate + "')");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "新增成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "无新增用户信息";
                }
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public void Modifer()
        {
            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("update UserInfo set ");
                sqlstr.Append("LoginID='" + UserInfo.LoginID + "',");
                sqlstr.Append("Password='" + UserInfo.Password + "',");
                sqlstr.Append("Phone='" + UserInfo.Phone + "',");
                sqlstr.Append("DepartID='" + UserInfo.DepartID + "',");
                sqlstr.Append("UserName='" + UserInfo.UserName + "' ");
                sqlstr.Append("where SID='" + UserInfo.SID + "'");
                db.ExecVoid(sqlstr.ToString(), Account);
                Code = "0000";
                Msg = "修改成功";
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public void Delete()
        {
            if (!string.IsNullOrWhiteSpace(UserInfo.SID))
            {
                try
                {
                    string str = "update UserInfo set Status=1 where SID='" + UserInfo.SID + "'";
                    db.ExecVoid(str, Account);
                    Code = "0000";
                    Msg = "状态修改成功";
                }
                catch (Exception ex)
                {
                    Code = "0002";
                    Msg = ex.ToString();
                }
            }
            else
            {
                Code = "0001";
                Msg = "无用户信息";
            }
        }
        public List<UserInfo> UserList()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select a.*,b.DepartName from UserInfo as a ");
            sqlstr.Append("left join Department as b on a.DepartID=b.SID ");
            sqlstr.Append("order by a.SID desc");
            DataTable dt = db.getTable(sqlstr.ToString(), Account);
            return CommonTools.TableToList<UserInfo>(dt);
        }
        public void ChangeStatus()
        {
            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("update UserInfo set ");
                sqlstr.Append("Status='" + UserInfo.Status + "' ");
                sqlstr.Append("where SID='" + UserInfo.SID + "'");
                db.ExecVoid(sqlstr.ToString(), Account);
                Code = "0000";
                Msg = "修改状态成功";
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
    }
}
