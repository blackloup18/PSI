using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Models
{
   public class ProductClass
    {
        public int ID { get; set; }
        public string SID { get; set; }
        public string ClassName { get; set;}
        public string ParentID { get; set; }
    }
}
