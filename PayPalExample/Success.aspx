<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="PayPalExample.Success" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblSuccess" runat="server"></asp:Label> 
    <br />
    <br />
    <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" />
    
    </div>
    </form>
</body>
</html>
