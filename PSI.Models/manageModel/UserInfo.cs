using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string SID { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public string Creator { get; set; }
        public string CreatDate { get; set; }
        public string DepartID { get; set; }
        public string DepartName { get; set; }
    }
}
