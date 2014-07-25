using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text;

namespace PayPalExample
{
    public class NVPAPICaller
    {
        private string pendpointurl = "https://api-3t.paypal.com/nvp";
        private const string CVV2 = "CVV2";

        private string returnURL = "http://"+ HttpContext.Current.Request.Url.Authority+"/success.aspx";
        private string cancelURL = "http://"+ HttpContext.Current.Request.Url.Authority+"/cancel.aspx";

        //Flag for PayPal environment (live or sandbox)
        private const bool bSandbox = true;

        private const string SIGNATURE = "SIGNATURE";
        private const string PWD = "PWD";
        private const string ACCT = "ACCT";

        //Set API Credentials
        public string APIUsername = "amitkaushik183-facilitator_api1.gmail.com";
        private string APIPassword = "1406113953";
        private string APISignature = "AQlcP3HG4QBHtfKW5SgiCSTuuQgtAeOEEpGTEI0cITGlfzI0.f50exHL";

        private string Subject = "";
        private string BNCode = "PP-ECWizard";

        //HttpWebRequest Timeout specified in milliseconds 
        private const int Timeout = 10000;
        private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };

        public bool ExpressCheckout(string name0, string description0, string price0, string quantity0, string name1, string description1, string price1, string quantity1, string name2, string description2, string price2, string quantity2, string currency, ref string token, ref string retMsg)
        {

            string host = "www.paypal.com";
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
                host = "www.sandbox.paypal.com";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "SetExpressCheckout";
            encoder["RETURNURL"] = returnURL;
            encoder["CANCELURL"] = cancelURL;

            double totalPrice0 = 0;
            double totalPrice1 = 0;
            double totalPrice2 = 0;

            encoder["L_PAYMENTREQUEST_0_NAME0"] = name0;
            encoder["L_PAYMENTREQUEST_0_DESC0"] = description0;
            encoder["L_PAYMENTREQUEST_0_AMT0"] = price0;
            encoder["L_PAYMENTREQUEST_0_QTY0"] = quantity0;
            totalPrice0 = Convert.ToDouble(quantity0) * Convert.ToDouble(price0);

            encoder["L_PAYMENTREQUEST_0_NAME1"] = name1;
            encoder["L_PAYMENTREQUEST_0_DESC1"] = description1;
            encoder["L_PAYMENTREQUEST_0_AMT1"] = price1;
            encoder["L_PAYMENTREQUEST_0_QTY1"] = quantity1;
            totalPrice1 = Convert.ToDouble(quantity1) * Convert.ToDouble(price1);
            

            encoder["L_PAYMENTREQUEST_0_NAME2"] = name2;
            encoder["L_PAYMENTREQUEST_0_DESC2"] = description2;
            encoder["L_PAYMENTREQUEST_0_AMT2"] = price2;
            encoder["L_PAYMENTREQUEST_0_QTY2"] = quantity2;
            totalPrice2 = Convert.ToDouble(quantity2) * Convert.ToDouble(price2);
      
            encoder["PAYMENTREQUEST_0_AMT"] = (totalPrice0+totalPrice1+totalPrice2).ToString();
            encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "SALE";
            encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = currency;
      
            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = MakeHttpCall(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                token = decoder["TOKEN"];

                string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token + "&useraction=COMMIT";

                retMsg = ECURL;
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        /// <summary>
        /// GetDetailsFromAPI: The method that calls GetExpressCheckoutDetails API
        /// </summary>
        /// <param name="token"></param>
        /// <param ref name="retMsg"></param>
        /// <returns></returns>
        public bool GetDetailsFromAPI(string token, ref NVPCodec decoder, ref string retMsg)
        {

            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = token;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp =MakeHttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }


        /// <summary>
        /// ConfirmPayment: The method that calls SetExpressCheckout API
        /// </summary>
        /// <param name="token"></param>
        /// <param ref name="retMsg"></param>
        /// <returns></returns>
        public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, string currency, ref NVPCodec decoder, ref string retMsg)
        {
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = token;
            encoder["PAYMENTACTION"] = "Sale";
            encoder["PAYERID"] = PayerId;
            encoder["AMT"] = finalPaymentAmount;
            encoder["CURRENCYCODE"] = currency;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = MakeHttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }


        /// <summary>
        /// MakeHttpCall: Method used for all API calls
        /// </summary>
        /// <param name="NvpRequest"></param>
        /// <returns></returns>
        public string MakeHttpCall(string NvpRequest) //Call NVP Server
        {
            string url = pendpointurl;

            //To Add the credentials from the profile
            string strPost = NvpRequest + "&" + buildCredentialsStringNVP();
            strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = Timeout;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            
            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception e)
            {
              //do some error handling
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Credentials added to the NVP string
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private string buildCredentialsStringNVP()
        {
            NVPCodec codec = new NVPCodec();

            if (!IsEmpty(APIUsername)) 
                codec["USER"] = APIUsername;

            if (!IsEmpty(APIPassword))
                codec["PWD"] = APIPassword;

            if (!IsEmpty(APISignature))
                codec["SIGNATURE"] = APISignature;

            if (!IsEmpty(Subject))
                codec["SUBJECT"] = Subject;

            codec["VERSION"] = "84.0";

            return codec.Encode();
        }

        /// <summary>
        /// Returns if a string is empty or null
        /// </summary>
        /// <param name="s">the string</param>
        /// <returns>true if the string is not null and is not empty or just whitespace</returns>
        public static bool IsEmpty(string s)
        {
            return s == null || s.Trim() == string.Empty;
        }
    }


    public sealed class NVPCodec : NameValueCollection
    {
        private const string AMPERSAND = "&";
        private const string EQUALS = "=";
        private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
        private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

        /// <summary>
        /// Returns the built NVP string of all name/value pairs in the Hashtable
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            bool firstPair = true;
            foreach (string kv in AllKeys)
            {
                string name = HttpUtility.UrlEncode(kv);
                string value = HttpUtility.UrlEncode(this[kv]);

                if (!firstPair)
                {
                    sb.Append(AMPERSAND);
                }
                sb.Append(name).Append(EQUALS).Append(value);
                firstPair = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decoding the string
        /// </summary>
        /// <param name="nvpstring"></param>
        public void Decode(string nvpstring)
        {
            Clear();
            foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
            {
                string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
                if (tokens.Length >= 2)
                {
                    string name = HttpUtility.UrlDecode(tokens[0]);
                    string value = HttpUtility.UrlDecode(tokens[1]);
                    Add(name, value);
                }
            }
        }


        #region Array methods
        public void Add(string name, string value, int index)
        {
            this.Add(GetArrayName(index, name), value);
        }

        public void Remove(string arrayName, int index)
        {
            this.Remove(GetArrayName(index, arrayName));
        }

        /// <summary>
        /// 
        /// </summary>
        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
            }
            return name + index;
        }
        #endregion
    }
}