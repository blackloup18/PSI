using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSI.Models
{
    public class InStockBillViewModel
    {
        public mainBill MainBill { get; set; }
        public List<detailBill> DetailBill { get; set; }
    }
    public class mainBill
    {
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string BillDate { get; set; }
        public string SID { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public string WareHouseID { get; set; }
    }
    public class detailBill
    {
        public string AssistCode { get; set; }
        public string BarCode { get; set; }
        public string CreatDate { get; set; }
        public string Creator { get; set; }
        public string FactryName { get; set; }
        public string ID { get; set; }
        public string Model { get; set; }
        public string PinyinCode { get; set; }
        public string OnlyUnit { get; set; }
        public string ClassName { get; set; }
        public string ClassID { get; set; }
        public string MainUnitName { get; set; }
        public string AssistUnitName { get; set; }
        public string AssistRate { get; set; }
        public string IsFloat { get; set; }
        public string ProName { get; set; }
        public string Remark { get; set; }
        public string SID { get; set; }
        public string Specs { get; set; }
        public string Status { get; set; }
        public string LAY_TABLE_INDEX { get; set; }

    }
}