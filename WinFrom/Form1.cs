using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Comm;
using Qdcdc.LessThanContainer.Comm;

namespace WinFrom
{
    public partial class Form1 : Form
    {
        #region 参数配置
        static string webApiPath = System.Configuration.ConfigurationManager.AppSettings["webApi"].ToString();
        static string tp;
        static string userCode = System.Configuration.ConfigurationManager.AppSettings["userCode"].ToString();
        static string publickey = System.Configuration.ConfigurationManager.AppSettings["publickey"].ToString();
        static string privatekey = System.Configuration.ConfigurationManager.AppSettings["privatekey"].ToString();

        static string pc1 = "<RSAKeyValue><Modulus>tg3a7FvBWwNzNmVr0bihcE2K/Zr3PhpCl12BtpNh9JamNYhAjhtrWi0a5K+bYdHfkKkOV4B42xh+gD/FXJjGYhE8mrDxR9yaz1mJ8kQaGw2KfS8NAMwUjtqcqg8lE0c1+7nWpS9XeLLlsrRWBgtIRfBBd82mtF4vU4e4Kv0w+GU=</Modulus><Exponent>AQAB</Exponent><P>+r4scXKZj+2JbPRRBLi/8DNzxPcjk1SgvjLMDwpSjFSmlvj+QHDevNiG1YcFHt9j49swaRifwwGTwAiZlGpLPw==</P><Q>ud8BK9FL8rfuxy1hT+dxygOt3yMyalc7P0BLL6jy8SnyjeHgnX7CXdGP5X0j0iW544bcSVJE5YwlNwTvRcaHWw==</Q><DP>2+2wN7Wn4akcj9dftL+DguDuW0XWC2UANLODbnHEY9ff4q30/HrDs2pMIb/zCtgv2myn8papkrawbJBefOIaqw==</DP><DQ>ELGEq2jKx2vZmC3tNX317EzUStwgZQicm5uspQUpfJtl23RT9hEyZ7awk83fb7pPra2pDzTCV1N07DIXQ08HTQ==</DQ><InverseQ>twZUyKiam80D3KNwheHQCrH9oojwlMFZqA8SMp0ErbGktXRMYOJmPW/PF7aguInt3yODKMtlX7vRyFo625kSEA==</InverseQ><D>PsK+AxeKq9afpFQIfG868F07GjyPGZCx/j6VFhzu+wPoXrIPFy5qrta327iSR4Gzoco2EIJQp69lBM93OXnNgbDMoSzUbAB5LMbPjqL/EynPpdJb3/hybZ8C0wMbCIeOueSEe5TayJ9JhXbI+wEvv6t/ggwMbYm/IWrX+Dc6uM0=</D></RSAKeyValue>";
        static string pc2 = "<RSAKeyValue><Modulus>tg3a7FvBWwNzNmVr0bihcE2K/Zr3PhpCl12BtpNh9JamNYhAjhtrWi0a5K+bYdHfkKkOV4B42xh+gD/FXJjGYhE8mrDxR9yaz1mJ8kQaGw2KfS8NAMwUjtqcqg8lE0c1+7nWpS9XeLLlsrRWBgtIRfBBd82mtF4vU4e4Kv0w+GU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        static string CompanyCode = "3702630015"; //招商代码
        static string CustomCode = "4258"; //现场代码
        static string StoreCode = "3702660042"; //仓库代码
        List<COMBIT_BILL_HEAD> askHeadList = new List<COMBIT_BILL_HEAD>();

        string CBID = string.Empty;
        #endregion

        #region 加载窗体
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetTimeSpan();//获取时差
     
        }
        #endregion

        #region 获取时差

        public static void GetTimeSpan()
        {
            try
            {
                DateTime clientDate = DateTime.Now;
                string serverDate = HttpRequestHelper.WebClientGet(webApiPath + "GetNowDate").ToString().Replace("\"", "");
                TimeSpan timeSpan = DateTime.ParseExact(serverDate.ToString(), "yyyyMMddHHmmss", null) - clientDate;
                tp = timeSpan.TotalSeconds.ToString();
            }
            catch (Exception ex)
            {
                tp = "0";
            }
        }


        #endregion

        #region 加载数据

        COMBIT_BILL_HEAD askHead()
        {
            COMBIT_BILL_HEAD head = new COMBIT_BILL_HEAD();
            head.ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss");
            head.ASKUSER = "张三";
            head.BILLNO = "B90027";
            head.COMPANYCODE = CompanyCode;
            head.CUSTOMCODE = CustomCode;
            head.STORECODE = StoreCode;
            head.TRAFNAME = "B90027";
            head.TYPE = "0";
            head.VOYANO = "B90027";
            return head;
        }

        COMBIT_BILL_LIST askList()
        {
            COMBIT_BILL_LIST info = new COMBIT_BILL_LIST();
            info.HSCODE = "02023000";
            info.GOODSNAME = "玉米";
            info.QTY = "1000";
            info.UNIT = "千克";
            return info;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <returns></returns>
        SYS_SIGN getSign()
        {
            SYS_SIGN info = new SYS_SIGN();
            info.SIGNKEY = MessageHandler.RSAEncrypt(publickey, DateTime.Now.AddSeconds(Convert.ToDouble(tp)).ToString("yyyyMMddHHmmss"));
            return info;
        }


        string getMessageSign(string message)
        {
            return MessageHandler.Sign(message,  privatekey);
        }

        #endregion

        #region  仓库命令操作
        /// <summary>
        /// 申报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthSubmit_Click(object sender, EventArgs e)
        {

            COMBIT_BILL bill = new COMBIT_BILL()
            {
                head = askHead(),
                list = new List<COMBIT_BILL_LIST>() { askList() },
                sign = getSign()
            };

           string jsonstr=Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillReport", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            CBID = type.keyCode;
            MessageBox.Show(type.retCode+"|"+type.retInfo+"|"+type.keyCode);
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthCombit_Click(object sender, EventArgs e)
        {
            Model.HEBING.BILL bill = new Model.HEBING.BILL()
            {
                head = new Model.HEBING.HEAD() { ORIGIN = "f93e516c-f30b-4e1c-bc3c-c7a28909bac6|998fc08d-bb3e-4df1-bbbb-45a260312c70", TRAFNAME = "B90023", VOYNO = "B90023", BILLNO = "B90023", CUSTOMCODE = CustomCode, STORECODE = StoreCode, ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.HEBING.LIST>() { new Model.HEBING.LIST() { HSCODE = "02023000", GOODSNAME = "品名", QTY = "2000", UNIT = "千克", PLACE = "10000000002" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillCombine", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthSplit_Click(object sender, EventArgs e)
        {
            Model.CHAIFEN.BILL bill = new Model.CHAIFEN.BILL()
            {
                head = new List<Model.CHAIFEN.HEAD>() { new Model.CHAIFEN.HEAD() { KEYCODE = "01bd4f07-9acc-4b12-a449-a4ec7fe9e4d4", TARGET = "b3d8302e-3554-4225-8519-5fe1936f802C", CUSTOMCODE = "4258", TRAFNAME = "A90030", VOYNO = "A90030", BILLNO = "A90030", ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "ZHANGSAN" }, new Model.CHAIFEN.HEAD() { KEYCODE = "01bd4f07-9acc-4b12-a449-a4ec7fe9e4d4", TARGET = "b3d8302e-3554-4225-8519-5fe1936f53a2", CUSTOMCODE = "4258", TRAFNAME = "A90031", VOYNO = "A90031", BILLNO = "A90031  ", ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "ZHANGSAN" } },
                list = new List<Model.CHAIFEN.LIST>() { new Model.CHAIFEN.LIST() { HEADKEY = "b3d8302e-3554-4225-8519-5fe1936f802C", GOODSNAME = "品名1", HSCODE = "02023000", QTY = "100", UNIT = "千克", PLACE = "10000000002" }, new Model.CHAIFEN.LIST() { HEADKEY = "b3d8302e-3554-4225-8519-5fe1936f53a2", GOODSNAME = "品名1", HSCODE = "02023000", QTY = "900", UNIT = "千克", PLACE = "10000000003" } },
                sign = getSign()
            };

            string jsonstr=Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillSplit", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 换货接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthReveice_Click(object sender, EventArgs e)
        {
            Model.Change.CHANGE_R bill = new Model.Change.CHANGE_R()
            {
                head = new Model.Change.rHEAD() { ASKKEY = "0e1b4110-63cd-4575-96d5-5e1d58099d15", TRAFNAME = "B90030", VOYNO = "B90030", BILLNO = "B90030", ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "三三" },
                list = new List<Model.Change.rLIST>() { new Model.Change.rLIST() { HSCODE = "02023000", PLACE = "10000000004" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);
            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE =jsonstr ,
                SIGN = getMessageSign(jsonstr),
                CODE = "13"
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillBarterReceive", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        ///  换货申请
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthChange_Click(object sender, EventArgs e)
        {
            Model.Change.CHANGE_S bill = new Model.Change.CHANGE_S()
            {
                head = new Model.Change.HEAD() { KEYCODE = "0e1b4110-63cd-4575-96d5-5e1d58099d15", ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "zhangsan", ASKKEY = "0e1b4110-63cd-4575-96d5-5e1d58099d15", TARGETCODE = "1222222222" },
                sign = getSign()
            };

            string jsonstr=Newtonsoft.Json.JsonConvert.SerializeObject(bill);
            
            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillBarterRequest", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 入库确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Model.RUKU.RuKuBill bill = new Model.RUKU.RuKuBill()
            {

                head = new Model.RUKU.RuKuHead() { STORECODE = StoreCode, ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.RUKU.RuKuList>() { new Model.RUKU.RuKuList() { HSCODE = "02023000", KEYWORD = CBID, PLACE = "1000000001", QTY = "1000" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "ConfirmationInfo", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 提单变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthBillChange_Click(object sender, EventArgs e)
        {
            Model.TIDANCHANGE.TiDanBill bill = new Model.TIDANCHANGE.TiDanBill()
            {

                head = new List<Model.TIDANCHANGE.TIDanHead>() { new Model.TIDANCHANGE.TIDanHead() { KEYCODE = "17d77a95-344a-406d-8001-7b34c1d8a170", TRAFNAME = "B90012", VOYANO = "B90012", BILLNO = "T90013" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillInfoUpdate", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 库位变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthPlaceChange_Click(object sender, EventArgs e)
        {
            Model.PLACECHANGE.PLACEBILL bill = new Model.PLACECHANGE.PLACEBILL()
            {

                head = new List<Model.PLACECHANGE.PLACEHEAD>() { new Model.PLACECHANGE.PLACEHEAD() { KEYCODE = "17d77a95-344a-406d-8001-7b34c1d8a170", HSCODE = "02023000", PLACE = "10000000002" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "AskBillGoodPlaceUpdate", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        #endregion

        #region 装箱报告
        /// <summary>
        /// 申报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthSVanning_Click(object sender, EventArgs e)
        {
            Model.VANNING.VANNINGBILL bill = new Model.VANNING.VANNINGBILL()
            {
                head = new Model.VANNING.VANNINGHEAD() { TRAFNAME = "B90012", VOYNO = "B90012", CONTAINERCODE = "3000003", STORECODE = "3333333333", CUSTOMCODE = CustomCode, COMPANYCODE = CompanyCode, ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.VANNING.VANNINGLIST>() { new Model.VANNING.VANNINGLIST() { TIDANCODE = "B90012", HSCODE = "", QTY = "" }, new Model.VANNING.VANNINGLIST() { TIDANCODE = "T90013", HSCODE = "", QTY = "" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            //object result = HttpRequestHelper.WebClientPost(webApiPath + "VanningReport", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            //AskBillStatusReceive
            object result = HttpRequestHelper.WebClientPost(webApiPath + "Send", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthCVanning_Click(object sender, EventArgs e)
        {
            Model.VANNING.CANVANNINGBILL bill = new Model.VANNING.CANVANNINGBILL()
            {
                head = new Model.VANNING.CANVANNINGHEAD() { ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.VANNING.CANVANNINGLIST>() { new Model.VANNING.CANVANNINGLIST() { KEYCODE = "ea9cd072-057e-4b26-ad99-0aa794877211" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "VanningCancel", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }
        #endregion

        #region  出场确认
        /// <summary>
        /// 申报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthChu_Click(object sender, EventArgs e)
        {
            Model.CHUCHANG.CHUBILL bill = new Model.CHUCHANG.CHUBILL()
            {
                head = new Model.CHUCHANG.CHUHEAD() { COMPANYCODE = CompanyCode, ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.CHUCHANG.CHULIST>() { new Model.CHUCHANG.CHULIST() { VOYNO = "B90012", TRAFNAME = "B90012", CONTAINERCODE = "3000003" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "ConfirmExit", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bthCChu_Click(object sender, EventArgs e)
        {
            Model.CHUCHANG.CHUBILL bill = new Model.CHUCHANG.CHUBILL()
            {
                head = new Model.CHUCHANG.CHUHEAD() { COMPANYCODE = CompanyCode, ASKDATE = DateTime.Now.ToString("yyyyMMddHHmmss"), ASKUSER = "张三" },
                list = new List<Model.CHUCHANG.CHULIST>() { new Model.CHUCHANG.CHULIST() { VOYNO = "B90012", TRAFNAME = "B90012", CONTAINERCODE = "3000003" } },
                sign = getSign()
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(bill);

            SENDMESSAGE SendInfo = new SENDMESSAGE()
            {
                MESSAGE = jsonstr,
                SIGN = getMessageSign(jsonstr),
                CODE = userCode
            };
            object result = HttpRequestHelper.WebClientPost(webApiPath + "ConfirmExitCancel", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<AskBillStatusInfo> MESSAGE = new List<AskBillStatusInfo>();

            AskBillStatusInfo message = new AskBillStatusInfo();

            message.TIDANCODE = "DIMJKT1705032|DIMJKT1705030B|";
            message.CONTAINERCODE = "TRHU3551496";
            message.TRAFNAME = "HAI LIAN";
            message.VOYANO = "1708S";
            message.STATUS = "1";

            MESSAGE.Add(message);

            AskBillStatusRequest receiveInfo = new AskBillStatusRequest();
            receiveInfo.MESSAGE.info = MESSAGE;
            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(receiveInfo.MESSAGE);
            receiveInfo.SIGN = getMessageSign(jsonstr);

            RequestMessage rq = new RequestMessage();
            rq.MESSAGE = jsonstr;
            rq.SIGN = getMessageSign(jsonstr);

            //object result = HttpRequestHelper.WebClientPost(webApiPath + "VanningReport", Newtonsoft.Json.JsonConvert.SerializeObject(SendInfo));
            //AskBillStatusReceive
            object result = HttpRequestHelper.WebClientPost(webApiPath + "VanningReportStatusReceive", Newtonsoft.Json.JsonConvert.SerializeObject(rq));
            ReturnType type = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnType>(result.ToString());
            MessageBox.Show(type.retCode + "|" + type.retInfo + "|" + type.keyCode);
        }
    }

    public class RequestMessage
    {
        public string MESSAGE { get; set; }
        public string CODE { get; set; }
        public string SIGN { get; set; }

    }
}
