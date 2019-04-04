using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HEBING
{
   public class BILL
    {
       public HEAD head { get; set; }

       public List<LIST> list { get; set; }

       public SYS_SIGN sign { get; set; }
    }
}
