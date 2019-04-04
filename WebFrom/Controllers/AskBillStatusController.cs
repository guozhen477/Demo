using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;

namespace WebFrom.Controllers
{
    public class AskBillStatusController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AskBillStatusReceive([FromBody]AskBillStatusRequest receiveInfo)
        {
            // receiveInfo.MESSAGE 申请单状态集合

            // receiveInfo.SIGN 签名

            //bool isTure = Comm.MessageHandler.RSADefSign("公钥", Newtonsoft.Json.JsonConvert.SerializeObject(receiveInfo.MESSAGE), receiveInfo.SIGN); //签名验证
            
            //业务处理逻辑

            ReturnType info = new ReturnType() { retCode = 1, retInfo = "接收成功" };
            return ReturnResponseMessage(Newtonsoft.Json.JsonConvert.SerializeObject(info));
        }


        /// <summary>
        /// 返回响应结果
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public HttpResponseMessage ReturnResponseMessage(string info)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(info);
            return response;
        }
    }
}
