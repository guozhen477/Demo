using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TIDANCHANGE
{
   public  class TiDanBill
    {
       public List<TIDanHead> head { get; set; }

       public SYS_SIGN sign { get; set; }
    }
}
