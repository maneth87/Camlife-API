<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myToken.aspx.cs" Inherits="CamlifeAPI1.myToken" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnToken" Text="GetToken" runat="server" OnClick="btnToken_Click" />
        <asp:Button ID="btnTokenJson" Text="GetToken Json" runat="server" OnClick="btnTokenJson_Click" />
        <asp:Button ID="btnMyData" Text="My Data" runat="server"  OnClick="btnMyData_Click"/>
    </div>
    </form>
</body>
</html>
