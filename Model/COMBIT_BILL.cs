using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class COMBIT_BILL
    {
       public COMBIT_BILL_HEAD head { get; set; }

       public List<COMBIT_BILL_LIST> list { get; set; }

       public SYS_SIGN sign { get; set; }
    }
}
