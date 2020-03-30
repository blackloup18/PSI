using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSI.Models
{
    public class InventoryViewModel
    {
        public string ID { get; set; }
        public string ProID { get; set; }
         public string ProName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public string AssistCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 是否单一计量单位
        /// </summary>
        public string OnlyUnit { get; set; }
        public string MainUnit { get; set; }
        public string MainUnitID { get; set; }
        public decimal MainNumber { get; set; }
        public string InventoryUnit { get; set; }
        public string InventoryUnitID { get; set; }
        public decimal InventoryNumber { get; set; }
        public string AssistUnit { get; set; }
        public string AssistUnitID { get; set; }
        public decimal AssistNumber { get; set; }
        /// <summary>
        /// 是否浮动 0=不浮动 1=浮动
        /// </summary>
        public string IsFloat { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string FactryName { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Colour { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }
        public string HouseID { get; set;}
        public string HouseName { get; set; }
        /// <summary>
        /// 辅助
        /// </summary>
        public string Spare1 { get; set; }
        public string Spare2 { get; set; }
        public string Spare3 { get; set; }
        public string Spare4 { get; set; }
        public string Spare5 { get; set; }
    }
    public class QueryInventory
    {
        public string ProName { get; set; }
        public string AssistCode { get; set; }
        public string Spec { get; set; }
        public string Model { get; set; }
        public string HouseID { get; set; }
    }
}