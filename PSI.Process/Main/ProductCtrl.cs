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
   public class ProductCtrl
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Products Product { get; set; }
        public string Account { get; set; }
        public ProductCtrl(string _account)
        {
            Account = _account;
            Product = new Products();
            Code = "0000";
            Msg = "成功";
        }
        public ProductCtrl(string _account,string ProID)
        {
            Account = _account;
            string str1 = "select * from Products where SID='" + ProID + "'";
            DataTable dt = db.getTable(str1, Account);
            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Product = new Products();
                Product.AssistCode = dr["AssistCode"].ToString();
                Product.BarCode = dr["BarCode"].ToString(); 
                Product.CreatDate = dr["CreatDate"].ToString();
                Product.Creator = dr["Creator"].ToString();
                Product.FactryName = dr["FactryName"].ToString();
                Product.ID = int.Parse(dr["ID"].ToString());
                Product.Model = dr["Model"].ToString();
                Product.OnlyUnit = int.Parse(dr["OnlyUnit"].ToString());
                Product.ProName = dr["ProName"].ToString();
                Product.Remark = dr["Remark"].ToString();
                Product.SID = dr["SID"].ToString();
                Product.Specs = dr["Specs"].ToString();
                Product.Status = int.Parse(dr["Status"].ToString());
                Product.Colour = dr["Colour"].ToString();
                Product.Place = dr["Place"].ToString();
                Product.Size = dr["Size"].ToString();
                Product.Spare1 = dr["Spare1"].ToString();
                Product.Spare2 = dr["Spare2"].ToString();
                Product.Spare3 = dr["Spare3"].ToString();
                Product.Spare4 = dr["Spare4"].ToString();
                Product.Spare5 = dr["Spare5"].ToString();
                Product.DIY1 = dr["DIY1"].ToString();
                Product.DIY2 = dr["DIY2"].ToString();
                Product.DIY3 = dr["DIY3"].ToString();
                Product.DIY4 = dr["DIY4"].ToString();
                Product.DIY5 = dr["DIY5"].ToString();
                Product.Year = dr["Year"].ToString();
                Product.MainUnit = dr["MainUnit"].ToString();
                Product.PurchaseUnit =dr["PurchaseUnit"].ToString();
                Product.PurchaseRate = decimal.Parse(dr["PurchaseRate"].ToString());
                Product.SaleUnit = dr["SaleUnit"].ToString();
                Product.SaleRate = decimal.Parse(dr["SaleRate"].ToString());
                Product.InventoryUnit = dr["InventoryUnit"].ToString();
                Product.InventoryRate = decimal.Parse(dr["InventoryRate"].ToString());
                Product.AssistUnit = dr["AssistUnit"].ToString();
                Product.AssistRate = decimal.Parse(dr["AssistRate"].ToString());
                Product.IsFloat = int.Parse(dr["IsFloat"].ToString());
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
                StringBuilder str1 = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Product.ProName))
                {
                    str1.Append("insert into Products (");
                    str1.Append("SID, ProName, AssistCode, Specs, Model, OnlyUnit, BarCode, FactryName, Remark, Creator, CreatDate, Status,ClassID,PinyinCode,");
                    str1.Append("Place, Size, Colour, Spare1, Spare2, Spare3, Spare4, Spare5, DIY1, DIY2, DIY3, DIY4, DIY5, Year, MainUnit,");
                    str1.Append("PurchaseUnit, PurchaseRate, SaleUnit, SaleRate, InventoryUnit, InventoryRate, AssistUnit, AssistRate, IsFloat");
                    str1.Append(") values (");
                    str1.Append("'" + Product.SID + "',");
                    str1.Append("'" + Product.ProName + "',");
                    str1.Append("'" + Product.AssistCode + "',");
                    str1.Append("'" + Product.Specs + "',");
                    str1.Append("'" + Product.Model + "',");
                    str1.Append("'" + Product.OnlyUnit + "',");
                    str1.Append("'" + Product.BarCode + "',");
                    str1.Append("'" + Product.FactryName + "',");
                    str1.Append("'" + Product.Remark + "',");
                    str1.Append("'" + Product.Creator + "',");
                    str1.Append("'" + Product.CreatDate + "',");
                    str1.Append("'" + Product.Status + "', ");
                    str1.Append("'" + Product.ClassID + "', ");
                    str1.Append("'" + Product.PinyinCode + "', ");
                    str1.Append("'" + Product.Place + "',");
                    str1.Append("'" + Product.Size + "',");
                    str1.Append("'" + Product.Colour + "',");
                    str1.Append("'" + Product.Spare1 + "',");
                    str1.Append("'" + Product.Spare2 + "',");
                    str1.Append("'" + Product.Spare3 + "',");
                    str1.Append("'" + Product.Spare4 + "',");
                    str1.Append("'" + Product.Spare5 + "',");
                    str1.Append("'" + Product.DIY1 + "',");
                    str1.Append("'" + Product.DIY2 + "',");
                    str1.Append("'" + Product.DIY3 + "',");
                    str1.Append("'" + Product.DIY4 + "',");
                    str1.Append("'" + Product.DIY5 + "',");
                    str1.Append("'" + Product.Year + "',");
                    str1.Append("'" + Product.MainUnit + "',");
                    str1.Append("'" + Product.PurchaseUnit + "',");
                    str1.Append("'" + Product.PurchaseRate + "',");
                    str1.Append("'" + Product.SaleUnit + "',");
                    str1.Append("'" + Product.SaleRate + "',");
                    str1.Append("'" + Product.InventoryUnit + "',");
                    str1.Append("'" + Product.InventoryRate + "',");
                    str1.Append("'" + Product.AssistUnit + "',");
                    str1.Append("'" + Product.AssistRate + "',");
                    str1.Append("'" + Product.IsFloat + "')");
                    SqlDataItem t1 = new SqlDataItem();
                    t1.CommandText = str1.ToString();
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
                else
                {
                    Code = "0002";
                    Msg = "未找到商品";
                }
            }
            catch (Exception ex)
            {
                Code = "0003";
                Msg = ex.ToString();
            }
        }
        public void Edit()
        {
            try
            {
                StringBuilder str1 = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Product.ProName))
                {
                    str1.Append("update Products set ");
                    str1.Append("ProName='" + Product.ProName + "',");
                    str1.Append("AssistCode='" + Product.AssistCode + "',");
                    str1.Append("Specs='" + Product.Specs + "',");
                    str1.Append("Model='" + Product.Model + "',");
                    str1.Append("OnlyUnit='" + Product.OnlyUnit + "',");
                    str1.Append("BarCode='" + Product.BarCode + "',");
                    str1.Append("FactryName='" + Product.FactryName + "',");
                    str1.Append("Remark='" + Product.Remark + "' ");
                    str1.Append("Place='" + Product.Place + "',");
                    str1.Append("Size='" + Product.Size + "',");
                    str1.Append("ClassID='" + Product.ClassID + "',");
                    str1.Append("PinyinCode='" + Product.PinyinCode + "',");
                    str1.Append("Colour='" + Product.Colour + "',");
                    str1.Append("Spare1='" + Product.Spare1 + "',");
                    str1.Append("Spare2='" + Product.Spare2 + "',");
                    str1.Append("Spare3='" + Product.Spare3 + "',");
                    str1.Append("Spare4='" + Product.Spare4 + "',");
                    str1.Append("Spare5='" + Product.Spare5 + "',");
                    str1.Append("DIY1='" + Product.DIY1 + "',");
                    str1.Append("DIY2='" + Product.DIY2 + "',");
                    str1.Append("DIY3='" + Product.DIY3 + "',");
                    str1.Append("DIY4='" + Product.DIY4 + "',");
                    str1.Append("DIY5='" + Product.DIY5 + "',");
                    str1.Append("Year'" + Product.Year + "',");
                    str1.Append("MainUnit'" + Product.MainUnit + "',");
                    str1.Append("PurchaseUnit'" + Product.PurchaseUnit + "',");
                    str1.Append("PurchaseRate'" + Product.PurchaseRate + "',");
                    str1.Append("SaleUnit'" + Product.SaleUnit + "',");
                    str1.Append("SaleRate'" + Product.SaleRate + "',");
                    str1.Append("InventoryUnit'" + Product.InventoryUnit + "',");
                    str1.Append("InventoryRate'" + Product.InventoryRate + "',");
                    str1.Append("AssistUnit'" + Product.AssistUnit + "',");
                    str1.Append("AssistRate'" + Product.AssistRate + "',");
                    str1.Append("IsFloat'" + Product.IsFloat + "' ");
                    str1.Append("where SID='" + Product.SID + "'");
                    SqlDataItem t1 = new SqlDataItem();
                    t1.CommandText = str1.ToString();
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
                else
                {
                    Code = "0002";
                    Msg = "未找到商品";
                }
            }
            catch (Exception ex)
            {
                Code = "0003";
                Msg = ex.ToString();
            }

        }
        public void Delete()
        {
            try
            {
                string str1 = "delete from Products where SID='" + Product.SID + "'";
                SqlDataItem t1 = new SqlDataItem();
                t1.CommandText = str1;
                if (db.ExecSQL(t1, Account))
                {
                    Code = "0000";
                    Msg = "删除成功";
                }
                else
                {
                    Code = "0001";
                    Msg = "删除失败";
                }
            }
            catch(Exception ex)
            {
                Code = "0002";
                Msg = ex.ToString();
            }
        }
        public List<Products> GetList()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select a.*,b.ClassName,c.UnitName as MainUnitName,d.UnitName as AssistUnitName from Products as a ");
            sqlstr.Append("left join ProductClass as b on a.ClassID=b.SID ");
            sqlstr.Append("left join Units as c on a.MainUnit = c.SID ");
            sqlstr.Append("left join Units as d on a.AssistUnit = d.SID ");
            sqlstr.Append("order by a.SID desc");
            DataTable dt = db.getTable(sqlstr.ToString(), Account);
            return CommonTools.TableToList<Products>(dt);
        }

    }
}
