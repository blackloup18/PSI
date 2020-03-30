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
   public  class UnitCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Units Unit { get; set; }
        public string Account { get; set; }
        public UnitCtrl(string _account)
        {
            Account = _account;
            Unit = new Units();
            Code = "0000";
            Msg = "初始化";
        }
        public UnitCtrl(string _account,string SID)
        {
            Account = _account;
            Unit = new Units();
            if (!string.IsNullOrWhiteSpace(SID))
            {
                string str = "select * from Units where SID='" + SID + "'";
                DataTable dt = db.getTable(str, Account);
                if(dt != null && dt.Rows.Count > 0){
                    DataRow dr = dt.Rows[0];
                    Unit.ID = int.Parse(SID);
                    Unit.Creator = dr["Creator"].ToString();
                    Unit.CreatDate = dr["CreatDate"].ToString();
                    Unit.Status = int.Parse(dr["Status"].ToString());
                    Unit.UnitName = dr["UnitName"].ToString();
                }
                else
                {
                    Code = "0001";
                    Msg = "未找到信息";
                }
            }
            else
            {
                Code = "0002";
                Msg = "编码为空";
            }
        }
        public void Save()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Unit.UnitName))
                {                    
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("insert into Units (");
                    sqlstr.Append("SID,UnitName, Creator, CreatDate, Status");
                    sqlstr.Append(") values (");
                    sqlstr.Append("'" + Unit.SID + "',");
                    sqlstr.Append("'" + Unit.UnitName + "',");
                    sqlstr.Append("'" + Unit.Creator + "',");
                    sqlstr.Append("'" + Unit.CreatDate + "',");
                    sqlstr.Append("'" + Unit.Status + "')");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "保存成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "名称为空";
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
                if (!string.IsNullOrWhiteSpace(Unit.UnitName))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("update Units set ");
                    sqlstr.Append("UnitName='" + Unit.UnitName + "' ");
                    sqlstr.Append("where SID='" + Unit.SID + "'");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "修改成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "名称为空";
                }
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public void ChangeStatus()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Unit.UnitName))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("update Units set ");
                    sqlstr.Append("Status='" + Unit.Status + "' ");
                    sqlstr.Append("where SID='" + Unit.SID + "'");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "状态修改成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "名称为空";
                }
            }
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }

        }
        public List<Units> GetList()
        {
            string str = "select * from Units order by SID desc";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<Units>(dt);
        }
        public List<Units> GetList(string sid)
        {
            string str = "select * from Units where SID <> '" + sid + "' order by SID desc";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<Units>(dt);
        }

    }
}
