using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
   public class Products
    {
        public int ID { get; set; }
        public string SID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public string AssistCode { get; set; }
        public string PinyinCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 是否单一计量单位  0=是 1=否
        /// </summary>
        public int OnlyUnit { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        public string ClassID { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string FactryName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreatDate { get; set; }
        public int Status { get; set; }
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
        /// 辅助
        /// </summary>
        public string Spare1 { get; set; }
        public string Spare2 { get; set; }
        public string Spare3 { get; set; }
        public string Spare4 { get; set; }
        public string Spare5 { get; set; }
        /// <summary>
        /// 自定义
        /// </summary>
        public string DIY1 { get; set; }
        public string DIY2 { get; set; }
        public string DIY3 { get; set; }
        public string DIY4 { get; set; }
        public string DIY5 { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 主计量单位
        /// </summary>
        public string MainUnit { get; set; }
        /// <summary>
        /// 采购单位
        /// </summary>
        public string PurchaseUnit { get; set; }
        /// <summary>
        /// 采购转化率
        /// </summary>
        public decimal PurchaseRate { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        public string SaleUnit { get; set; }
        /// <summary>
        /// 销售转化率
        /// </summary>
        public decimal SaleRate { get; set; }
        /// <summary>
        /// 库存单位
        /// </summary>
        public string InventoryUnit { get; set; }
        /// <summary>
        /// 库存转化率
        /// </summary>
        public decimal InventoryRate { get; set; }
        /// <summary>
        /// 辅助单位
        /// </summary>
        public string AssistUnit { get; set; }
        /// <summary>
        /// 辅助转化率
        /// </summary>
        public decimal AssistRate { get; set; }
        /// <summary>
        /// 是否浮动 0=不浮动 1=浮动
        /// </summary>
        public int IsFloat { get; set; }
        public string ClassName { get   ; set; }
        public string MainUnitName { get; set; }
        public string AssistUnitName { get; set; }
        
    }
    
}
