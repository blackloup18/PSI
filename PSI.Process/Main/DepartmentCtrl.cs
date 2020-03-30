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
   public class DepartmentCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Department Department { get; set; }
        public string Account { get; set; }
        public DepartmentCtrl(string _account)
        {
            Account = _account;
            Department = new Department();
        }
        public DepartmentCtrl(string _account,string SID)
        {
            Account = _account;
            Department = new Department();
            string str = "select * from Department where SID='" + SID + "'";
            DataTable dt = db.getTable(str, Account);
            if(dt !=null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Department.CreatDate = dr["CreatDate"].ToString();
                Department.Creator = dr["Creator"].ToString();
                Department.DepartName = dr["DepartName"].ToString();
                Department.ID = int.Parse(dr["ID"].ToString());
                Department.SID = SID;
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
                if (!string.IsNullOrWhiteSpace(Department.DepartName))
                {
                    Department.SID = CommonTools.GetNewID("BM", Account);
                    StringBuilder sqlstr = new StringBuilder();
                    sqlstr.Append("insert into Department (");
                    sqlstr.Append("SID, DepartName, Creator, CreatDate");
                    sqlstr.Append(") values (");
                    sqlstr.Append("'" + Department.SID + "',");
                    sqlstr.Append("'" + Department.DepartName + "',");
                    sqlstr.Append("'" + Department.Creator + "',");
                    sqlstr.Append("'" + Department.CreatDate + "')");
                    db.ExecVoid(sqlstr.ToString(), Account);
                    Code = "0000";
                    Msg = "新增成功";
                }
                else
                {
                    Code = "0002";
                    Msg = "名称为空";
                }
            }
            catch(Exception ex)
            {
                Code = "0001";
                Msg = ex.ToString();
            }
        }
        public void Modifer()
        {
            try
            {
                StringBuilder sqlstr = new StringBuilder();
                sqlstr.Append("update Department set ");
                sqlstr.Append("DepartName='" + Department.DepartName + "' ");
                sqlstr.Append("where SID='" + Department.SID + "'");
                db.ExecVoid(sqlstr.ToString(), Account);
                Code = "0000";
                Msg = "修改成功";
            }
            catch(Exception ex)
            {
                Code = "0001";
                Msg = ex.ToString();
            }
        }
        public void Delete()
        {
            try
            {
                string str = "delete from Department where SID='" + Department.SID + "'";
                db.ExecVoid(str.ToString(), Account);
                Code = "0000";
                Msg = "删除成功";
            }
            catch(Exception ex)
            {
                Code = "0001";
                Msg = ex.ToString();
            }
        }
        public List<Department> GetList()
        {
            string str = "select * from Department order by SID desc";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<Department>(dt);
        }
    }
}
