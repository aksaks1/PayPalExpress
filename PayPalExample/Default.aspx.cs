using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PayPalExample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            double dblTotalPrice1 = 0;
            double dblTotalPrice2 = 0;
            double dblTotalPrice3 = 0;
            lblError.Text = "";
        
            if (IsNumeric(txtPrice1.Text) && IsNumeric(txtQuantity1.Text))
            {
                dblTotalPrice1 = Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text);
            }

            if (IsNumeric(txtPrice2.Text) && IsNumeric(txtQuantity2.Text))
            {
                dblTotalPrice2 = Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text);
            }

            if (IsNumeric(txtPrice3.Text) && IsNumeric(txtQuantity3.Text))
            {
                dblTotalPrice3 = Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text);
            }

            if (dblTotalPrice1 == 0 || dblTotalPrice2 == 0 || dblTotalPrice3 == 0)
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            }

            LblTotal1.Text = dblTotalPrice1.ToString();
            LblTotal2.Text = dblTotalPrice2.ToString();
            LblTotal3.Text = dblTotalPrice3.ToString();
            LblGrandTotal.Text = (dblTotalPrice1 + dblTotalPrice2 + dblTotalPrice3).ToString();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string currency = "USD"; 
            string token = string.Empty;
            string retMsg = string.Empty;
            NVPAPICaller PPNAVAPICaller = new NVPAPICaller(); 

            try
            {
                if (IsNumeric(txtPrice1.Text) && IsNumeric(txtPrice2.Text) && IsNumeric(txtPrice3.Text) &&
                    IsNumeric(txtQuantity1.Text) && IsNumeric(txtQuantity2.Text) && IsNumeric(txtQuantity3.Text) &&
                    !IsEmpty(txtName1.Text) && !IsEmpty(txtName2.Text) && !IsEmpty(txtName3.Text) &&
                    !IsEmpty(txtDescription1.Text) && !IsEmpty(txtDescription2.Text) && !IsEmpty(txtDescription3.Text))
                {
                    bool ret = PPNAVAPICaller.ExpressCheckout(txtName1.Text, txtDescription1.Text, txtPrice1.Text, txtQuantity1.Text, txtName2.Text, txtDescription2.Text, txtPrice2.Text, txtQuantity2.Text, txtName3.Text, txtDescription3.Text, txtPrice3.Text, txtQuantity3.Text, currency, ref token, ref retMsg);
                    
                    if (ret)
                    {
                        HttpContext.Current.Session["token"] = token;
                        Response.Redirect(retMsg);
                    }
                    else
                    {
                        lblError.Text = "There seems some error in the transaction, please check the input parameters.";
                    }
                } 
                else
                {
                    lblError.Text = "There seems some non-numeric / invalid values for either Price or Quantity or blank value for Item Name or Description. Please check the same.";
                }                
            }
            catch(Exception e1)
            {
               lblError.Text = e1.Message;
            }
        }
        
        protected void txtPrice1_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            double dblTotalPrice1 = 0;
            double dblGrandTotal = 0;

            if (IsNumeric(txtPrice1.Text) && IsNumeric(txtQuantity1.Text))
            {
                dblTotalPrice1 = Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text);
                dblGrandTotal += dblTotalPrice1;
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            LblTotal1.Text = dblTotalPrice1.ToString();
                        
            if (IsNumeric(txtPrice2.Text) && IsNumeric(txtQuantity2.Text))
            {
                dblGrandTotal += Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            if (IsNumeric(txtPrice3.Text) && IsNumeric(txtQuantity3.Text))
            {
                dblGrandTotal += Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            LblGrandTotal.Text = dblGrandTotal.ToString();
        }

        protected void txtPrice2_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            double dblTotalPrice2 = 0;
            double dblGrandTotal = 0;

            if (IsNumeric(txtPrice1.Text) && IsNumeric(txtQuantity1.Text))                 
            {
                dblGrandTotal += Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 
            
            if (IsNumeric(txtPrice2.Text) && IsNumeric(txtQuantity2.Text))
            {
                dblTotalPrice2 = Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text);
                dblGrandTotal += dblTotalPrice2;
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            LblTotal2.Text = dblTotalPrice2.ToString();

            if (IsNumeric(txtPrice3.Text) && IsNumeric(txtQuantity3.Text))
            {
                dblGrandTotal += Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            LblGrandTotal.Text = dblGrandTotal.ToString();
            //lblError.Text = ""; 
            //double dblTotalPrice2 = Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text);
            //LblTotal2.Text = dblTotalPrice2.ToString();
            //LblGrandTotal.Text = (Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text) + Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text) + Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text)).ToString();
        }

        protected void txtPrice3_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            double dblTotalPrice3 = 0;
            double dblGrandTotal = 0;
            
            if (IsNumeric(txtPrice1.Text) && IsNumeric(txtQuantity1.Text))
            {
                dblGrandTotal += Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            if (IsNumeric(txtPrice2.Text) && IsNumeric(txtQuantity2.Text))          
            {
                dblGrandTotal += Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text);
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 

            if (IsNumeric(txtPrice3.Text) && IsNumeric(txtQuantity3.Text))
            {
                dblTotalPrice3 = Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text);
                dblGrandTotal += dblTotalPrice3;
            }
            else
            {
                lblError.Text = "There seems some non-numeric or invalid value for either Price or Quantity, please check the same.";
            } 
            
            LblTotal3.Text = dblTotalPrice3.ToString();
            LblGrandTotal.Text = dblGrandTotal.ToString();
            //lblError.Text = ""; 
            //double dblTotalPrice3 = Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text);
            //LblTotal3.Text = dblTotalPrice3.ToString();
            //LblGrandTotal.Text = (Convert.ToDouble(txtPrice1.Text) * Convert.ToDouble(txtQuantity1.Text) + Convert.ToDouble(txtPrice2.Text) * Convert.ToDouble(txtQuantity2.Text) + Convert.ToDouble(txtPrice3.Text) * Convert.ToDouble(txtQuantity3.Text)).ToString();
        }

        public static bool IsNumeric(string s)
        {
            try
            {
                int result = int.Parse(s);

                if (result>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            } 
        }

        public static bool IsEmpty(string s)
        {
            return s == null || s.Trim() == string.Empty;
        }
    }
}