using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.Conn;
using System.Data;

namespace PSI.Process
{
  public  class loginManage
    {
        public static bool checkLogin(string userName,string pass,string accName)
        {
            bool flag = false;
            string str = "select * from Users where userName='" + userName + "' and password='" + pass + "'";
            SqlDataItem item = new SqlDataItem();
            item.CommandText = str;
            try
            {
                DataTable dt = db.getTable(item, accName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    flag = true;
                }
            }
            catch(Exception ex) { }
            return flag;
        }
    }
}
