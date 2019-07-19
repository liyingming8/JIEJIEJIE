<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoSelect.aspx.cs" Inherits="CRM_FaHuoSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
　　 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <style>
        ul {
            list-style: none;
            font-size:19px;
        }
        li {line-height:4em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem" style="padding-left:35px; "><span style="font-size:19px">标签序号:</span></div>

                <div class=" topitem" >
                    <asp:TextBox ID="inputSearchProduct" runat="server" CssClass=" inputsearch"></asp:TextBox></div>
                <div class="topitem">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查 询" CssClass=" btn btn-warning btnyd" /></div>
                  </div>
                <div style="overflow-x: auto"> 
                   <div style="line-height: 2.3rem;padding: 0.5rem 1.5rem;"><asp:Literal runat="server" ID="literalfahuoxinxi"></asp:Literal></div> 
                </div>
                <div >
                    <ul ><li id="nofahuo" runat="server" visible="false">没有发货数据！</li></ul></div>
            </div>
     
    </form>
</body>
</html>
