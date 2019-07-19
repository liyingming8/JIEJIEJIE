<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PGTest.aspx.cs" Inherits="PGTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="display: none">
        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"></asp:GridView>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="发送短信" />
        </div>
    </div>
        <asp:TextBox ID="TextBox_ToDecode" runat="server"></asp:TextBox>
        <asp:Button ID="Button_Decode" runat="server" OnClick="Button_Decode_Click" Text="二版解密" />
        <asp:Label ID="Label_Result" runat="server"></asp:Label>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="ShowNum" />
        <asp:Label ID="Label_ShowNM" runat="server"></asp:Label>
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Restore" />
        <asp:Label ID="Label_OriNum" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="MD5" />
        <asp:Label ID="Label_md5" runat="server"></asp:Label>
    </form>
</body>
</html>
