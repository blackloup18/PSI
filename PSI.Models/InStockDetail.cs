using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{

    /// <summary>
    /// 单据类型 RK
    /// </summary>
    public class InStockDetail
    {
        public int ID { get; set; }
        public string BillSID { get; set; }
        public string ProSID { get; set; }
        public decimal Num { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public decimal Amount { get; set; }
        public int Index { get; set; }
    }
}
