using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
   public class Warehouses
    {
        public int ID { get; set; }
        public string HouseName { get; set; }
        public string Address { get; set; }
        public string HouseType { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreatDate { get; set; }
        public int Status { get; set; }

    }
}
