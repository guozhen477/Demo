using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AskBillStatusRequest
    {
        //public List<AskBillStatusInfo> MESSAGE { get; set; }
        public AskBillStatusRequest()
        {
            MESSAGE = new INFO();
        }

        public INFO MESSAGE { get; set; }

        public string SIGN { get; set; }
    }

    public class INFO
    {
        public INFO()
        {
            info = new List<AskBillStatusInfo>();
        }
        public List<AskBillStatusInfo> info { get; set; }
    }

    public class AskBillStatusInfo
    {
        //public string KEYWORD { get; set; }
        //public string TRAFNAME { get; set; }
        //public string VOYANO { get; set; }
        //public string BILLNO { get; set; }
        //public string STATUS { get; set; }

        public string TRAFNAME { get; set; }
        public string VOYANO { get; set; }
        public string CONTAINERCODE { get; set; }
        public string STATUS { get; set; }
        public string TIDANCODE { get; set; }

    }

    public class AskBillSignInfo
    {
        public string SignInfo { get; set; }
    }

}
