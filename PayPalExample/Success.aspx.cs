using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PayPalExample
{
    public partial class Success : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NVPAPICaller PPAPICaller = new NVPAPICaller();
            NVPCodec decoder = new NVPCodec();
            string token = string.Empty;
            string payerID = string.Empty;
            string finalPaymentAmount = string.Empty;
            string retMsg = string.Empty;
            string currency = string.Empty;

            token = Session["token"].ToString();

            //use the PayPal token to get the details of payment
            bool ret = PPAPICaller.GetDetailsFromAPI(token, ref decoder, ref retMsg);
            if (ret)
            {
                payerID = decoder["PayerID"];
                token = decoder["token"];
                finalPaymentAmount = decoder["PAYMENTREQUEST_0_AMT"];
                currency = decoder["CURRENCYCODE"];

                lblSuccess.Text = "Payment successful for - PayerID: " + payerID + "; Amount in " + currency + ": " + finalPaymentAmount;
            }
            else
            {
                //error.LogError();
            }

            NVPCodec confirmdecoder = new NVPCodec();

            //confirm that payment was taken
            bool ret2 = PPAPICaller.ConfirmPayment(finalPaymentAmount, token, payerID, currency, ref confirmdecoder, ref retMsg);
            if (ret2)
            {
               token = confirmdecoder["token"];
            }
            else
            {
                //payment has not been successful
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx"); 
        }
    }
}