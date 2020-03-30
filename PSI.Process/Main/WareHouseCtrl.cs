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
   public class WareHouseCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Warehouses Warehouses { get; set; }
        public string Account { get; set; }
        public WareHouseCtrl(string _account)
        {
            Account = _account;
            Warehouses = new Warehouses();
        }
        public WareHouseCtrl(string _account,string SID)
        {
            Account = _account;
            string str = "select * from Warehouses where ID='" + SID + "'";
            DataTable dt = db.getTable(str, Account);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Warehouses = new Warehouses();
                Warehouses.ID = int.Parse(SID);
                Warehouses.Address = dr["Address"].ToString();
                Warehouses.CreatDate = dr["CreatDate"].ToString();
                Warehouses.Creator = dr["Creator"].ToString();
                Warehouses.HouseName = dr["HouseName"].ToString();
                Warehouses.HouseType = dr["HouseType"].ToString();
                Warehouses.Remark = dr["Remark"].ToString();
                Warehouses.Status = int.Parse(dr["Status"].ToString());
                Code = "0000";
                Msg = "成功";
            }
            else
            {
                Code = "0001";
                Msg = "未找到信息";
            }            
        }
        public void Save()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Warehouses.HouseName))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("insert into Warehouses (");
                    sqlstr.Append("HouseName, Address, HouseType, Remark, Creator, CreatDate, Status");
                    sqlstr.Append(") values (");
                    sqlstr.Append("'" + Warehouses.HouseName + "',");
                    sqlstr.Append("'" + Warehouses.Address + "',");
                    sqlstr.Append("'" + Warehouses.HouseType + "',");
                    sqlstr.Append("'" + Warehouses.Remark + "',");
                    sqlstr.Append("'" + Warehouses.Creator + "',");
                    sqlstr.Append("'" + Warehouses.CreatDate + "',");
                    sqlstr.Append("'" + Warehouses.Status + "')");
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
            catch (Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public void Edit()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Warehouses.HouseName))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("update Warehouses set ");
                    sqlstr.Append("HouseName='" + Warehouses.HouseName + "',");
                    sqlstr.Append("Address='" + Warehouses.Address + "',");
                    sqlstr.Append("HouseType='" + Warehouses.HouseType + "',");
                    sqlstr.Append("Remark='" + Warehouses.Remark + "' ");
                    sqlstr.Append("where ID='" + Warehouses.ID + "'");
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
                if (!string.IsNullOrWhiteSpace(Warehouses.HouseName))
                {
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("update Warehouses set ");
                    sqlstr.Append("Status='" + Warehouses.Status + "' ");
                    sqlstr.Append("where ID='" + Warehouses.ID + "'");
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
        public List<Warehouses> GetList()
        {
            string str = "select * from Warehouses order by ID desc";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<Warehouses>(dt);
        }
        public List<string> GetWareType()
        {
            string str = "select HouseType from Warehouses group by HouseType";
            DataTable dt = db.getTable(str, Account);
            List<string> list = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(dt.Rows[i]["HouseType"].ToString());
            }
            return list;
        }
    }
}
