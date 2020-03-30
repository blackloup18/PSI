using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
   public class ProductUnit
    {
        public int ID { get; set; }
        public string UnitID { get; set; }
        public string ProID { get; set; }
        public int IsMaster { get; set; }
        public float Rate { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public string CreatDate { get; set; }

    }
}
