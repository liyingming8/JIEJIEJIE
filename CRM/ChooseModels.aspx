<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseModels.aspx.cs" Inherits="CRM_ChooseModels" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <style type="text/css">
        ul {
            position: relative;
            float: left;
            width: 8%;
            margin: 0;
            padding: 15px 0 0 2%;
        }

        li {
            list-style: none;
            text-align: center;
            width: 100%;
        }

        ul li img {
            width: 45%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList ID="DDLCustomerGrade" runat="server" DataTextField="gradename" DataValueField="id" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLCustomerGrade_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">代理商级别</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:Button runat="server" Text="确  定" ID="btn_ok" CssClass="btn btn-warning btnyd" OnClick="btn_ok_Click" />
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="padding-bottom: 40px; overflow: hidden;">
                        <asp:Repeater ID="rpt_models" runat="server" OnItemDataBound="rpt_models_ItemDataBound">
                            <ItemTemplate>
                                <ul>
                                    <li>
                                        <img src='<%#Eval("logourl") %>' /></li>
                                    <li><%#Eval("pagename") %></li>
                                    <li>
                                        <asp:CheckBox runat="server" ID="ckb" />
                                        <asp:HiddenField runat="server" ID="hfid" Value='<%#Eval("id") %>' />
                                        <asp:HiddenField runat="server" ID="hfneeded" Value='<%#Eval("id") %>' />
                                        <asp:HiddenField runat="server" ID="hfneed" Value='<%#Eval("isneeded") %>' />
                                    </li>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js"></script>
        <asp:HiddenField ID="hf_authordedmdid_str" runat="server" />
    </form>
    </body>
</html>
