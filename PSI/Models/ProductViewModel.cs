using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSI.Models
{
    public class ProductViewModel
    {
        public string SID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        public string AssistCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs { get; set; }
        public string ClassID { get; set; }
        public string PinyinCode { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 是否单一计量单位
        /// </summary>
        public string OnlyUnit { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string FactryName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
        public string PurchaseRate { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        public string SaleUnit { get; set; }
        /// <summary>
        /// 销售转化率
        /// </summary>
        public string SaleRate { get; set; }
        /// <summary>
        /// 库存单位
        /// </summary>
        public string InventoryUnit { get; set; }
        /// <summary>
        /// 库存转化率
        /// </summary>
        public string InventoryRate { get; set; }
        /// <summary>
        /// 辅助单位
        /// </summary>
        public string AssistUnit { get; set; }
        /// <summary>
        /// 辅助转化率
        /// </summary>
        public string AssistRate { get; set; }
        /// <summary>
        /// 是否浮动 0=不浮动 1=浮动
        /// </summary>
        public string IsFloat { get; set; }

    }
    public class ProUnitViewModel
    {
        /// <summary>
        /// 计量单位编号
        /// </summary>
        public string UnitID { get; set; }
        /// <summary>
        /// 转化率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 是否主单位0=是，1=否
        /// </summary>
        public string IsMaster { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    public class QueryProduct
    {
        public string ProName { get; set; }
        public string ProID { get; set; }
        public string Specs { get; set; }
        public string Models { get; set; }
    }
    public class ProductClassViewModel
    {
        public string SID { get; set; }
        public string ClassName { get; set; }
        public string ParentID { get; set; }
    }
}