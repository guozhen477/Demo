using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RUKU
{
    public  class RuKuBill
    {

        public RuKuHead head { get; set; }

        public List<RuKuList> list { get; set; }

        public SYS_SIGN sign { get; set; }

    }
}
