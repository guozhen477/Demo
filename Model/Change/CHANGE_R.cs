using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Change
{
   public  class CHANGE_R
    {
       public rHEAD head { get; set; }
       public List<rLIST> list { get; set; }
       public SYS_SIGN sign { get; set; }
    }

   public class rHEAD
   {
       public string ASKKEY { get; set; }
       public string TRAFNAME { get; set; }
       public string VOYNO { get; set; }

       public string BILLNO { get; set; }
       public string ASKDATE { get; set; }
       public string ASKUSER { get; set; }
   }

   public class rLIST
   {
       public string HSCODE { get; set; }
       public string PLACE { get; set; }

   }
}
