<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderInfoAcceptIng.aspx.cs" Inherits="msite_OrderInfoAcceptIng" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title></title>
        <link href="../css/bootstrap.min.css" rel="stylesheet" />
        <link href="default.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="container-fluid">
                <div class="row navibar">
                    <a href="OrderInfoNoAccept.aspx" target="_self"><div class="col-xs-4 notcurrentpage">待处理</div></a>
                    <a href="OrderInfoAcceptIng.aspx" target="_self"><div class="col-xs-4 currentpage">处理中</div></a>
                    <a href="OrderInfoAccepted.aspx" target="_self"><div class="col-xs-4 notcurrentpage">已处理</div></a>
                </div>
                <asp:PlaceHolder ID="phorderitem" runat="server"></asp:PlaceHolder>
                <%--<div class="row orderitem">
            <div class="col-xs-3 prodpic"><img src="../Admin/Images/upload/1/sm_150704113822.jpg" /></div>
            <div class="col-xs-5 goodsname"><div>西凤三七五顶顶顶顶</div><div>数量：1件</div></div>
            <div class="col-xs-4"><asp:Button Text="我来接单" ID="button_ok" CssClass="ButtonOK" runat="server" /></div>
            <div class="col-xs-12">收货人:某某某 联系电话:13988875564</div>
            <div class="col-xs-12">收货地址:北京市朝阳区解放路123号谢谢欧大厦6楼402室</div>
        </div>--%>
            </div>
        </form>
    </body>
</html>