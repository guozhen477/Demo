using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CHAIFEN
{
   public  class BILL
    {
       public List<HEAD> head { get; set; }
       public List<LIST> list { get; set; }

       public SYS_SIGN sign { get; set; }

    }
}
