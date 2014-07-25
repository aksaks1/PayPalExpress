<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayPalExample._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
        }
        .auto-style2 {
            height: 23px;
            width: 168px;
        }
        .auto-style5 {
            width: 244px;
        }
        .auto-style6 {
            height: 23px;
            width: 244px;
        }
        .auto-style7 {
            width: 137px;
        }
        .auto-style8 {
            height: 23px;
            width: 137px;
        }
        .auto-style9 {
            width: 168px;
        }
        .auto-style10 {
            width: 168px;
            font-weight: bold;
        }
        .auto-style11 {
            width: 137px;
            font-weight: bold;
        }
        .auto-style12 {
            width: 244px;
            font-weight: bold;
        }
        .auto-style13 {
            width: 171px;
            font-weight: bold;
        }
        .auto-style14 {
            height: 23px;
            width: 171px;
        }
        .auto-style15 {
            width: 171px;
        }
        .auto-style16 {
            width: 149px;
            font-weight: bold;
        }
        .auto-style17 {
            height: 23px;
            width: 149px;
        }
        .auto-style18 {
            width: 149px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <div>
            <table class="auto-style1">
                <tr>
                    <td align="center" bgcolor="#996600" class="auto-style13" style="color: #FFFFFF">Item Name</td>
                    <td align="center" bgcolor="#996600" class="auto-style12" style="color: #FFFFFF">Item Description</td>
                    <td align="center" bgcolor="#996600" class="auto-style11" style="color: #FFFFFF">Price</td>
                    <td align="center" bgcolor="#996600" class="auto-style16" style="color: #FFFFFF">Quantity</td>
                    <td align="center" bgcolor="#996600" class="auto-style10" style="color: #FFFFFF">Total</td>
                </tr>
                <tr>
                    <td align="center" class="auto-style14"><asp:TextBox ID="txtName1" runat="server" Text="Books"></asp:TextBox></td>
                    <td align="center" class="auto-style6"><asp:TextBox ID="txtDescription1" runat="server" Text="Science and Technology" Width="173px"></asp:TextBox></td>
                    <td align="center" class="auto-style8"><asp:TextBox ID="txtPrice1" runat="server" Text="5" OnTextChanged="txtPrice1_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style17"><asp:TextBox ID="txtQuantity1" runat="server" Text="2" OnTextChanged="txtPrice1_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style2"><asp:Label ID="LblTotal1" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" class="auto-style15"><asp:TextBox ID="txtName2" runat="server" Text="Toys"></asp:TextBox></td>
                    <td align="center" class="auto-style5"><asp:TextBox ID="txtDescription2" runat="server" Text="For Kids" Width="175px"></asp:TextBox></td>
                    <td align="center" class="auto-style7"><asp:TextBox ID="txtPrice2" runat="server" Text="5" OnTextChanged="txtPrice2_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style18"><asp:TextBox ID="txtQuantity2" runat="server" Text="3" OnTextChanged="txtPrice2_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style9"><asp:Label ID="LblTotal2" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" class="auto-style15"><asp:TextBox ID="txtName3" runat="server" Text="Clothes"></asp:TextBox></td>
                    <td align="center" class="auto-style5"><asp:TextBox ID="txtDescription3" runat="server" Text="Uniforms" Width="175px"></asp:TextBox></td>
                    <td align="center" class="auto-style7"><asp:TextBox ID="txtPrice3" runat="server" Text="10" OnTextChanged="txtPrice3_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style18"><asp:TextBox ID="txtQuantity3" runat="server" Text="2" OnTextChanged="txtPrice3_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    <td align="center" class="auto-style9"><asp:Label ID="LblTotal3" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" class="auto-style15">&nbsp;</td>
                    <td align="center" class="auto-style5">&nbsp;</td>
                    <td align="center" class="auto-style7">&nbsp;</td>
                    <td align="center" class="auto-style18"><strong>Grand Total</strong></td>
                    <td align="center" class="auto-style9"><asp:Label ID="LblGrandTotal" runat="server" Text="" style="font-weight: 700"></asp:Label></td>
                </tr>
            </table></div>
            <br />
           
        <asp:ImageButton ID="imgbtnPay" runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" OnClick="btnPay_Click" />
    </div>
    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>