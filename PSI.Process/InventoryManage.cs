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
    public class InventoryManage
    {
        public static DataTable GetInventory(string acc)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select a.ID,a.ProID,b.ProName,b.AssistCode,b.Specs,b.Model,b.OnlyUnit,b.MainUnit as MainUnitID,n.UnitName as MainUnit,a.MainNumber,");
            sqlstr.Append("b.InventoryUnit as InventoryUnitID, c.UnitName as InventoryUnit,(a.MainNumber * b.InventoryRate) as InventoryNumber,");
            sqlstr.Append("b.AssistUnit as AssistUnitID, m.UnitName as AssistUnit,a.AssistNumber,");
            sqlstr.Append("b.IsFloat,b.BarCode,b.FactryName,b.Place,b.Size,b.Colour,b.Year,a.HouseID,d.HouseName,");
            sqlstr.Append("b.Spare1, b.Spare2, b.Spare3, b.Spare4, b.Spare5 from Inventory as a ");
            sqlstr.Append("left join Products as b on a.ProID=b.SID ");
            sqlstr.Append("left join Units as c on b.InventoryUnit = c.SID ");
            sqlstr.Append("left join Units as m on b.AssistUnit = m.SID ");
            sqlstr.Append("left join Units as n on b.MainUnit =n.SID ");
            sqlstr.Append("left join Warehouses as d on a.HouseID=d.ID ");
            return db.getTable(sqlstr.ToString(), acc);
        }
    }
}
