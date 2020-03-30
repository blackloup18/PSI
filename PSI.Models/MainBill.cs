using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
   public class MainBill
    {
        public int ID { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string SID { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string BillDate { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string ClientID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 入库仓库
        /// </summary>
        public string HouseID_in { get; set; }
        /// <summary>
        /// 出库仓库
        /// </summary>
        public string HouseID_out { get; set;}
        /// <summary>
        /// 单据类型 RK=入库单
        /// </summary>
        public string BillType { get; set; }
        /// <summary>
        /// 单据金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 完成金额
        /// </summary>
        public decimal FinishAmount { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string CreatID { get; set; }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreatDate { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditID { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public string AuditDate { get; set; }
        /// <summary>
        /// 状态 0-未审核 1-已审核 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
