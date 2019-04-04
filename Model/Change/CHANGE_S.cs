using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Change
{
   public  class CHANGE_S
    {
       public HEAD head { get; set; }

       public SYS_SIGN sign { get; set; }
    }

   public class HEAD
   {
       public string KEYCODE { get; set; }
       public string ASKDATE { get; set; }
       public string ASKUSER { get; set; }
       public string ASKKEY { get; set; }
       public string TARGETCODE { get; set; }
   }
}
