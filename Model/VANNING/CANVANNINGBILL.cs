using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.VANNING
{
   public  class CANVANNINGBILL
    {

       public CANVANNINGHEAD head { get; set; }

       public List<CANVANNINGLIST> list { get; set; }

       public SYS_SIGN sign { get; set; }

    }
}
