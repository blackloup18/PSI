using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class Company
    {
        public int ID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 公司税号
        /// </summary>
        public string taxCode { get; set; }
        /// <summary>
        /// 秘钥，用于解密注册码
        /// </summary>
        public string appSecret { get; set; }
        /// <summary>
        /// 注册码
        /// </summary>
        public string appCode { get; set; }
    }
}
