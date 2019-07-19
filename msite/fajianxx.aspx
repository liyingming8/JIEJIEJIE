<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fajianxx.aspx.cs" Inherits="msite_fajianxx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
        <title></title>
        <link href="default.css" rel="stylesheet" />
        <link href="../css/bootstrap.min.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container-fluid"></div>
            <div class="row WuLiuInfoInputDiv">
                <div class="col-xs-4 leftarea">物流公司:</div>
                <div class="col-xs-8 rightarea"><asp:DropDownList ID="DDL_WuLiuGongSi" runat="server">
                                                    <asp:ListItem Text="请选择..." Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="申通" Value="申通"></asp:ListItem>
                                                    <asp:ListItem Text="圆通" Value="圆通"></asp:ListItem>
                                                    <asp:ListItem Text="国通" Value="国通"></asp:ListItem>
                                                    <asp:ListItem Text="顺丰" Value="顺丰"></asp:ListItem>
                                                    <asp:ListItem Text="EMS" Value="EMS"></asp:ListItem>
                                                    <asp:ListItem Text="韵达" Value="韵达"></asp:ListItem>
                                                    <asp:ListItem Text="中通" Value="中通"></asp:ListItem>
                                                    <asp:ListItem Text="EMS" Value="EMS"></asp:ListItem>
                                                </asp:DropDownList></div>
                <div class="col-xs-4 leftarea">运单号:</div>
                <div class="col-xs-8 rightarea">
                    <asp:TextBox ID="TextBox_YunDanHao" Height="28px" runat="server"></asp:TextBox></div>  </div>
            <div>
                <asp:Button ID="Button_OK" CssClass="BottonQuerenJieDan" runat="server" Text="确认" OnClick="Button_OK_Click" /></div>
            <asp:HiddenField ID="HF_OrderID" runat="server" />
        </form>
    </body>
</html>