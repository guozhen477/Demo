using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.VANNING
{
   public  class VANNINGBILL
    {
       public VANNINGHEAD head { get; set; }

       public List<VANNINGLIST> list { get; set; }

       public SYS_SIGN sign { get; set; }

    }
}
