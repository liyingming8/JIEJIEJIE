﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="swmactive.aspx.cs" Inherits="commonswm_swmactive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <link rel="stylesheet" href="../layuiadmin/layui/css/layui.css" media="all" />
    <link href="../include/public_v1.css" rel="stylesheet" />
    <title>三维码激活</title>
    <style type="text/css">
        body { font-size: 1.3rem; line-height: 2rem; } 
        .bg { position: absolute; top: 0; left: 0; right: 0; bottom: 0; z-index: -1; background-image: url('img/swmback.jpg'); background-repeat: no-repeat; font-size: 1.5rem; -ms-background-size: 100% 100%; background-size: 100% 100%; } 
        .topline { padding: 1rem; text-align: center; font-size: 1.5rem; font-weight: bold; margin-bottom: 1rem; } 
        .line { padding: 0.5rem 1rem; } 
            .line span { font-weight: bold; } 
        .bottomline { margin-top: 2rem; text-align: center; font-weight: bold; } 
        .finalline { margin-top: 1rem; text-align: right; } 
        .loginline { margin-bottom: 2rem; text-align: center; } 
        select { font-size: 1.1rem; line-height: 1.5rem; height: 2rem; margin-left: 0.3rem; border: 0.1rem solid #ff0000; -ms-border-radius: 0.2rem; border-radius: 0.2rem; }

        .btnactive { background-color: rgb(235,97,0); color: #fff !important; padding: 0.2rem 2rem; height: 2.8rem; -ms-border-radius: 0.2rem !important; border-radius: 0.2rem !important; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg"></div>
        <div style="margin-top: 1.5rem; padding: 0.5rem;"> 
            <div class="topline">
                <asp:Label runat="server" ID="labelcompname"></asp:Label>
            </div>
        <%--    <div class="line" id="divchanpin" runat="server">
                <span>产品:</span><asp:DropDownList runat="server" ID="ddl_product" DataTextField="Products_Name"
                    DataValueField="Infor_ID" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">不限</asp:ListItem>
                </asp:DropDownList>
                <a href="../Admin/wuliu/TB_Products_tongyong_singleAdd.aspx?fr=<%=FromURL %>"><i
                    class="layui-icon" style="font-size: 1.2rem"></i></a>
            </div>--%>
            <div class="line" style="font-size: 1.1rem; color: #111;">
                <asp:Literal ID="Literal_discription" runat="server"></asp:Literal>
            </div>
            <div class="line bottomline">
                <asp:Button ID="ButtonActive" runat="server" Text="确定激活" CssClass="btn btn-warning btnyd btnactive"
                    OnClick="ButtonActive_Click" /> 
            </div >
            <div class="line finalline" id="divout" runat="server" style="display: none;"><input type="button" id="btn_" class="btn btn-info btnyd" value="退出"/></div>
            <asp:HiddenField ID="hf_pid" runat="server" />
            <asp:HiddenField ID="hf_openid" runat="server" />
            <input id="isactive" type="hidden" value="0" runat="server" />
            <input id="hdmode" type="hidden" runat="server" />
            <input id="hdcount" type="hidden" runat="server" />
            <input id="hdcpid" type="hidden" runat="server"/>
            <asp:HiddenField runat="server" ID="hf_userid" />
            <asp:HiddenField runat="server" ID="hf_compid" />
        </div>
    </form>
    <script src="../include/js/jquery-2.1.1.min.js"></script>
    <script src="../include/js/public_v1.js"></script>
    <script type="text/javascript" src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script>
        window.onload = function() {

            if ($("#isactive").val() == "0") {
                var check = setInterval(function() {
                    $.ajax({
                        type: "post",
                        url: "ajax/checkactive.ashx",
                        data: { openid: $("#hf_openid").val() },
                        success: function(result) {
                            if (result == "1") {
                                window.location.reload();
                            } else {
                                tjpublic.loadingwx({ tckid: "tjloading", content: "资料审核中,请稍候", img: "img/jz2.gif" })
                            }
                        }
                    })
                }, 2000);
            }
        }
        document.getElementById("btn_").onclick = close;
        function close() {
            WeixinJSBridge.call('closeWindow');
        }
        function close() {
                WeixinJSBridge.call('closeWindow');
            }
        
    </script>
</body>
</html>
