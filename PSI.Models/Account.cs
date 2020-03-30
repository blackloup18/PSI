using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PSI.Conn;

namespace PSI.Models
{
    /// <summary>
    /// 账套
    /// </summary>
   public class Account
    {
        public Account() { }
        public int ID { get; set; }
        /// <summary>
        /// 账套编号，主键
        /// </summary>
        public string accountID { get; set; }
        /// <summary>
        /// 账套名称
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 文件保存地址
        /// </summary>
        public string databasePath { get; set; }
        /// <summary>
        /// 数据库名，默认为账套名的拼音首写
        /// </summary>
        public string databaseName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string creatDate { get; set; }
        /// <summary>
        /// 状态 0-启用 1-禁用
        /// </summary>
        public int status { get; set; }
    }
}
