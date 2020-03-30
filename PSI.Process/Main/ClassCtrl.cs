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
   public class ClassCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public string Account { get; set; }
        public ProductClass ProductClass { get; set; }
        public List<ProductClass> AllClassList { get; set; }
        public List<ProductClass> ParentClassList { get; set; }
        public ClassCtrl(string _acc)
        {
            Account = _acc;
            string str = "select * from ProductClass where ParentID=''";
            DataTable dt = db.getTable(str, Account);
            ParentClassList = CommonTools.TableToList<ProductClass>(dt);
            string str1 = "select * from ProductClass order by SID";
            AllClassList = CommonTools.TableToList<ProductClass>(db.getTable(str1, Account));
        }
        public ClassCtrl(string _acc,string ClassID)
        {
            Account = _acc;
            string str = "select * from ProductClass where ParentID=''";
            DataTable dt = db.getTable(str, Account);
            ParentClassList = CommonTools.TableToList<ProductClass>(dt);
            string strsql = "select * from ProductClass where SID='" + ClassID + "'";
            DataTable dt1 = db.getTable(strsql, Account);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                ProductClass = new ProductClass();
                ProductClass.SID = dr["SID"].ToString();
                ProductClass.ClassName = dr["ClassName"].ToString();
                ProductClass.ParentID = dr["ParentID"].ToString();
                ProductClass.ID = int.Parse(dr["ID"].ToString());
                Code = "0000";
                Msg = "成功";
            }
            else
            {
                ProductClass = new ProductClass();
                Code = "0001";
                Msg = "未查到该类别";
            }
        }
        public void Save()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into ProductClass (");
            sqlstr.Append("SID, ClassName, ParentID");
            sqlstr.Append(") values (");
            sqlstr.Append("'" + ProductClass.SID + "',");
            sqlstr.Append("'" + ProductClass.ClassName + "',");
            sqlstr.Append("'" + ProductClass.ParentID + "')");
            SqlDataItem t1 = new SqlDataItem();
            t1.CommandText = sqlstr.ToString();
            if (db.ExecSQL(t1, Account))
            {
                Code = "0000";
                Msg = "保存成功";
            }
            else
            {
                Code = "0001";
                Msg = "保存失败";
            }
        }
        public void Edit()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("update ProductClass set ");
            sqlstr.Append("ClassName='" + ProductClass.ClassName + "',");
            sqlstr.Append("ParentID='" + ProductClass.ParentID + "' ");
            sqlstr.Append("where SID='" + ProductClass.SID + "'");
            SqlDataItem t1 = new SqlDataItem();
            t1.CommandText = sqlstr.ToString();
            if (db.ExecSQL(t1, Account))
            {
                Code = "0000";
                Msg = "修改成功";
            }
            else
            {
                Code = "0001";
                Msg = "修改失败";
            }
        }
        public bool HasChild(string classID)
        {
            string str = "select * from ProductClass where ParentID='" + classID + "'";
            DataTable dt = db.getTable(str, Account);
            if(dt !=null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Delete(string classID)
        {
            bool flag = HasChild(classID);
            if(!flag)
            {
                try
                {
                    string str = "delete from ProductClass where SID='" + classID + "'";
                    db.ExecVoid(str, Account);
                    Code = "0000";
                    Msg = "删除成功";
                }
                catch(Exception ex)
                {
                    Code = "0002";
                    Msg = ex.ToString();
                }
            }
            else
            {
                Code = "0001";
                Msg = "有子类，无法删除";
            }
        }
        public List<ProductClass> ChildClassList(string parentID)
        {
            string str = "select * from ProductClass where ParentID='" + parentID + "'";
            DataTable dt = db.getTable(str, Account);
            return CommonTools.TableToList<ProductClass>(dt);
        }
    }
}
