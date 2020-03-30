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
   public class ClientCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set;}
        public Client Client { get; set; }
        public string Account { get; set;}
        public ClientCtrl(string _account)
        {
            Account = _account;
            Client = new Client();
            Code = "0000";
            Msg = "成功";
        }
        public ClientCtrl(string _account,string SID)
        {
            Account = _account;
            Client = new Client();
            string str = "select * from Client where SID='" + SID + "'";
            DataTable dt = db.getTable(str, Account);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Client.Account = dr["Account"].ToString();
                Client.Address = dr["Address"].ToString();
                Client.Bank = dr["Bank"].ToString();
                Client.ClientName = dr["ClientName"].ToString();
                Client.ClientType = dr["ClientType"].ToString();
                Client.Contact = dr["Contact"].ToString();
                Client.CreatDate = dr["CreatDate"].ToString();
                Client.Creator = dr["Creator"].ToString();
                Client.Email = dr["Email"].ToString();
                Client.Fax = dr["Fax"].ToString();
                Client.ID = int.Parse(dr["ID"].ToString());
                Client.Phone = dr["Phone"].ToString();
                Client.QQ = dr["QQ"].ToString();
                Client.Remark = dr["Remark"].ToString();
                Client.SID = dr["SID"].ToString();
                Client.Status = int.Parse(dr["Status"].ToString());
                Client.WeChat = dr["WeChat"].ToString();
                Code = "0000";
                Msg = "成功";
            }
            else
            {
                Code = "0001";
                Msg = "未找到数据";
            }

        }
        public void Save()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Client.ClientCode))
                {
                    Client.SID = CommonTools.GetNewID("KH", Account);
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("insert into Client (");
                    sqlstr.Append("SID, ClientCode, ClientName, ClientType, Address, Contact, Phone, WeChat, QQ, Email, Fax, Bank, Account, Remark, Creator, CreatDate, Status");
                    sqlstr.Append(") values (");
                    sqlstr.Append("'" + Client.SID + "',");
                    sqlstr.Append("'" + Client.ClientCode + "',");
                    sqlstr.Append("'" + Client.ClientName + "',");
                    sqlstr.Append("'" + Client.ClientType + "',");
                    sqlstr.Append("'" + Client.Address + "',");
                    sqlstr.Append("'" + Client.Contact + "',");
                    sqlstr.Append("'" + Client.Phone + "',");
                    sqlstr.Append("'" + Client.WeChat + "',");
                    sqlstr.Append("'" + Client.QQ + "',");
                    sqlstr.Append("'" + Client.Email + "',");
                    sqlstr.Append("'" + Client.Fax + "',");
                    sqlstr.Append("'" + Client.Bank + "',");
                    sqlstr.Append("'" + Client.Account + "',");
                    sqlstr.Append("'" + Client.Remark + "',");
                    sqlstr.Append("'" + Client.Creator + "',");
                    sqlstr.Append("'" + Client.CreatDate + "',");
                    sqlstr.Append("'" + Client.Status + "')");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "保存成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "没有可保存的信息";
                }
            }
            catch(Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public void Edit()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Client.ClientCode))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("update Client set ");
                    sqlstr.Append("ClientCode='" + Client.ClientCode + "',");
                    sqlstr.Append("ClientName='" + Client.ClientName + "',");
                    sqlstr.Append("ClientType='" + Client.ClientType + "',");
                    sqlstr.Append("Address='" + Client.Address + "',");
                    sqlstr.Append("Contact='" + Client.Contact + "',");
                    sqlstr.Append("Phone='" + Client.Phone + "',");
                    sqlstr.Append("WeChat='" + Client.WeChat + "',");
                    sqlstr.Append("QQ='" + Client.QQ + "',");
                    sqlstr.Append("Email='" + Client.Email + "',");
                    sqlstr.Append("Fax='" + Client.Fax + "',");
                    sqlstr.Append("Bank='" + Client.Bank + "',");
                    sqlstr.Append("Account='" + Client.Account + "',");
                    sqlstr.Append("Remark='" + Client.Remark + "' ");
                    sqlstr.Append("where SID='" + Client.SID + "'");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "修改成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "未找到信息";
                }
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }

        }
        public void ChangeStatus(string status)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Client.SID))
                {
                    string str = "update Client set Status='" + status + "' where SID='" + Client.SID + "'";
                    db.ExecVoid(str, Account);
                    Code = "0000";
                    Msg = "状态改变成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "未找到信息";
                }
            }
            catch(Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public List<Client> GetList()
        {
            string str = "select * from Client order by SID desc";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<Client>(dt);
        }
    }
}
