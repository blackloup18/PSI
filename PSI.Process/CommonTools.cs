using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using PSI.Conn;
using System.Reflection;

namespace PSI.Process
{
   public class CommonTools
    {
        public static string newAccountID()
        {
            string ID = "";
            try
            {
                string strsql = "select top 1 accountID from Account order by accountID desc";
                DataTable dt = db.getTable(strsql, "jinxiaocun");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string[] d = dt.Rows[0][0].ToString().Split('-');
                    int s = int.Parse(d[1]) + 1;
                    ID = "AC-" + string.Format("{0:000}", s);
                }
                else
                {
                    ID ="AC-001";
                }
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetTableName(string val)
        {
            switch (val)
            {
                default:
                    return "";
                case "RY":
                    return "UserInfo";
                case "ZW":
                    return "Roles";
                case "BM":
                    return "Department";
                case "CK":
                    return "Warehouses";
                case "SP":
                    return "Products";
                case "KH":
                    return "Client";
                case "DW":
                    return "Units";
                case "LB":
                    return "ProductClass";
            }
        }
        public static string GetNewID(string type,string account)
        {
            string tableName = GetTableName(type);
            string ID = "";
            string str = "select TOP 1 SID from " + tableName + " order by SID desc";
            DataTable dt = db.getTable(str, account);
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] d = dt.Rows[0][0].ToString().Split('-');
                int s = int.Parse(d[1]) + 1;
                ID = type + "-" + string.Format("{0:000}", s);
            }
            else
            {
                ID = type + "-001";
            }
            return ID;
        }
        public static List<T> TableToList<T>(DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                {
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                }
                                else
                                {
                                    v = Convert.ChangeType(item[i], info.PropertyType);
                                }
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
        public static decimal ToDecimal(string val)
        {
            decimal value = 0;
            try
            {
                value = decimal.Parse(val);
            }
            catch
            {
                value = 0;
            }
            return value;
        }
        public static int ToInt(string val)
        {
            int value = 0;
            try
            {
                value = int.Parse(val);
            }
            catch
            {
                value = 0;
            }
            return value;
        }
    }
}
