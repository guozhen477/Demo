using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Comm
{
    public class HttpRequestHelper
    {
        public void Request()
        {
            //string json = string.Empty;
            //string type = string.Empty;

            //string url = "http://localhost/Qdcdc.ExamContainer.CustomsService/UserService.svc";

            //WebClient webClient = new WebClient();
            //Uri uri = new Uri(url, UriKind.Absolute);
            //if (!webClient.IsBusy)
            //{
            //    webClient.Encoding = Encoding.UTF8;
            //    json = webClient.DownloadString(uri);
            //}


            //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //string code = response.ContentType;
            //code = code.Split('=')[1];
            //using (Stream stream = response.GetResponseStream())
            //{
            //    StreamReader sr = new StreamReader(stream, Encoding.GetEncoding(code));
            //    json = sr.ReadToEnd();
            //}
            //response.Close();


            //using (HttpClient client = new HttpClient())
            //{
            //    using (HttpResponseMessage message = client.GetAsync(url))
            //    {
            //        message.EnsureSuccessStatusCode
            //    }
            //}
        }


        // Methods
        #region  WebClient
        public static object WebClientGet(string url)
        {
            string str = "";
            WebClient client = new WebClient();
            Uri address = new Uri(url, UriKind.Absolute);
            if (!client.IsBusy)
            {
                client.Encoding = Encoding.UTF8;
                str = client.DownloadString(address);
            }
            return str;
        }

        public static object WebClientRequest(String url, string jsonData, string method)
        {
            string str = "";
            WebClient client = new WebClient();
            Uri address = new Uri(url, UriKind.Absolute);
            if (!client.IsBusy)
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                //client.Headers[HttpRequestHeader.ContentLength] = Encoding.UTF8.GetByteCount(jsonData).ToString();
                str = client.UploadString(address, method, jsonData);
            }
            return str;
        }

        public static object WebClientRequest(string url, object jsonObj, string method)
        {
            string jsonData = JsonConvert.SerializeObject(jsonObj);
            return WebClientRequest(url, jsonData, method);
        }

        public static object WebClientPost(string url, string jsonData)
        {

            return WebClientRequest(url, jsonData, "POST");
        }

        public static object WebClientPost(string url, object jsonObj)
        {
            string jsonData = JsonConvert.SerializeObject(jsonObj);
            return WebClientPost(url, jsonData);
        }



        public static object WebClientPostXml(string url, object xmlObject, string method)
        {
            string str = "";
            XmlSerializer xmlSerializer = new XmlSerializer(xmlObject.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, xmlObject);
                ms.Seek(0, SeekOrigin.Begin);

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ms);

                WebClient client = new WebClient();
                Uri address = new Uri(url, UriKind.Absolute);
                if (!client.IsBusy)
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/xml";
                    //client.Headers[HttpRequestHeader.ContentLength] = Convert.ToString(Encoding.UTF8.GetByteCount(jsonData));
                    str = client.UploadString(address, method, xmlDocument.InnerXml);
                }
                return str;
            }
        } 
        #endregion

        #region HttpWebRequest
        public static object HttpWebRequest(string url, string jsonData, string method)
        {
            string str = "";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            request.ContentType = "application/json";

            request.Accept = "text/html,application/xhtml+xml,*/*";
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                request.ContentLength = Encoding.UTF8.GetByteCount(jsonData);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(Encoding.UTF8.GetBytes(jsonData), 0, (int)request.ContentLength);
                    stream.Close();
                }
            }

            using (WebResponse response = request.GetResponse())
            {
                Encoding name = null;
                if (string.IsNullOrWhiteSpace(response.ContentType))
                {
                    name = Encoding.UTF8;
                }
                else
                {
                    name = Encoding.GetEncoding(response.ContentType.Split(new char[] { '=' })[1]);
                }

                using (Stream stream2 = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream2, name))
                    {
                        str = reader.ReadToEnd();
                    }
                }
                response.Close();
            }
            return str;
        }

        public static object HttpWebRequest(string url, object jsonObj, string method)
        {
            string jsonData = JsonConvert.SerializeObject(jsonObj);
            return HttpWebRequest(url, jsonData, method);
        }

        public static object WebRequestGet(string url)
        {
            return HttpWebRequest(url, null, "GET");
        }

        public static object WebRequestPost(string url, string jsonData)
        {
            return HttpWebRequest(url, jsonData, "POST");
        }


        public static object WebRequestPost(string url, object jsonObj)
        {
            string jsonData = JsonConvert.SerializeObject(jsonObj);
            return WebRequestPost(url, jsonData);
        } 
        #endregion


        //            private async void button2_Click(object sender, EventArgs e)
        //{
        //     HttpClient client = new HttpClient();
        //     //由HttpClient发出Delete Method
        //     HttpResponseMessage response = await client.DeleteAsync("http://localhost:41558/api/Demo"+"/1");
        //     if (response.IsSuccessStatusCode)
        //         MessageBox.Show("成功");
        //}

        //private async void button3_Click(object sender, EventArgs e)
        //{
        //     //创建一个处理序列化的DataContractJsonSerializer
        //     DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(People));
        //     MemoryStream ms = new MemoryStream();
        //     //将资料写入MemoryStream
        //     serializer.WriteObject(ms, new People() { Id = 1, Name = "Hello ni" });
        //     //一定要在这设定Position
        //     ms.Position = 0;
        //     HttpContent content = new StreamContent(ms);//将MemoryStream转成HttpContent
        //     content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //     HttpClient client = new HttpClient();
        //     //由HttpClient发出Put Method
        //     HttpResponseMessage response = await client.PutAsync("http://localhost:41558/api/Demo"+ "/1", content);
        //     if (response.IsSuccessStatusCode)
        //         MessageBox.Show("成功");
        //}

        //using (WebClient client = new WebClient())
        //{
        //     client.Headers["Type"] = "GET";
        //     client.Headers["Accept"] = "application/json";
        //     client.Encoding = Encoding.UTF8;
        //     client.DownloadStringCompleted += (senderobj, es) =>
        //     {
        //         var obj = es.Result;
        //     };
        //     client.DownloadStringAsync("http://localhost:41558/api/Demo");
        //}


        //后台client方式GET提交
//HttpClient myHttpClient = new HttpClient();
////提交当前地址的webapi
//string url = "http://" + System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port.ToString();
//myHttpClient.BaseAddress = new Uri(url);
////GET提交 返回string
//HttpResponseMessage response = myHttpClient.GetAsync("api/ApiDemo/Get2").Result;
//string result = "";
//if (response.IsSuccessStatusCode)
//{
//    result = response.Content.ReadAsStringAsync().Result;
//}
////return Content(JsonConvert.SerializeObject(result));
 
//Product product = null;
////GET提交 返回class
//response = myHttpClient.GetAsync("api/ProductsAPI/GetProduct/1").Result;
//if (response.IsSuccessStatusCode)
//{
//    product = response.Content.ReadAsAsync<Product>().Result;
//}
////return Content (JsonConvert.SerializeObject(product));
 
////put 提交 先创建一个和webapi对应的类           
//var content = new FormUrlEncodedContent(new Dictionary<string, string>()
//    {
//        {"Id","2"},
//        {"Name","Name:"+DateTime.Now.ToString() },
//        {"Category","111"},
//        {"Price","1"}
//     });
//response = myHttpClient.PutAsync("api/ProductsAPI/PutProduct/2", content).Result;
//if (response.IsSuccessStatusCode)
//{
//    result = response.Content.ReadAsStringAsync().Result;
//}
 
 
////post 提交 先创建一个和webapi对应的类
//content = new FormUrlEncodedContent(new Dictionary<string, string>()
//    {
//        {"Id","382accff-57b2-4d6e-ae84-a61e00a3e3b5"},
//        {"Name","Name" },
//        {"Category","111"},
//        {"Price","1"}
//     });
//response = myHttpClient.PostAsync("api/ProductsAPI/PostProduct", content).Result;
//if (response.IsSuccessStatusCode)
//{
//    result = response.Content.ReadAsStringAsync().Result;
//}
////delete 提交
//response = myHttpClient.DeleteAsync("api/ProductsAPI/DeleteProduct/1").Result;
//if (response.IsSuccessStatusCode)
//{
//    result = response.Content.ReadAsStringAsync().Result;
//}
 
////GET提交 返回List<class>
//response = myHttpClient.GetAsync("api/ProductsAPI/GetAllProducts").Result;
//List<Product> listproduct = new List<Models.Product>();
//if (response.IsSuccessStatusCode)
//{
//    listproduct = response.Content.ReadAsAsync<List<Product>>().Result;
//}
//return Content(JsonConvert.sSerializeObject(listproduct));
    }
}
