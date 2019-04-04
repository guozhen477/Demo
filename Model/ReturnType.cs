using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReturnType
    {
        /// <summary>
        /// 返回状态编码(状态0-接收失败；1-接收成功；)
        /// </summary>
        public int retCode { get; set; }
        /// <summary>
        /// 返回信息(接收成功：空；接收失败：返回错误信息)
        /// </summary>
        public string retInfo { get; set; }

        public string keyCode { get; set; }
    }
}
