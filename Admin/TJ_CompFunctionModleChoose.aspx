<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompFunctionModleChoose.aspx.cs" Inherits="Admin_TJ_CompFunctionModleChoose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CompADInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../layuiadmin/layui/css/layui.css" rel="stylesheet" />
    <link href="../layuiadmin/style/admin.css" rel="stylesheet" />
    <link href="../include/MasterPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage" style="overflow: auto; padding: 0.5rem;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="layui-row layui-col-space15">
                        <asp:Repeater ID="DataList_FunctionMoudle" runat="server" OnItemDataBound="DataList_FunctionMoudle_ItemDataBound">
                            <ItemTemplate>
                                <div class="layui-col-md4 layui-col-sm6">
                                    <div class="layadmin-contact-box">
                                        <div class="layui-col-md4 layui-col-sm6">
                                            <a href="#">
                                                <div class="layadmin-text-center">
                                                    <img runat="server" style="width: 80%" id="ImageCss" src='<%# Eval("LogoName").ToString() %>' alt="示意图" /></div>
                                            </a>
                                        </div>
                                        <div class="layui-col-md8 layadmin-padding-left20 layui-col-sm6">
                                            <a href="#">
                                                <h3 class="layadmin-title">
                                                    <strong><%# Eval("LogoName").ToString() %></strong>
                                                </h3>
                                            </a>
                                            <div class="layadmin-address">
                                                <asp:CheckBox runat="server" ID="ckb_choose" Text='<%# Eval("PageName") %>' />
                                                <asp:HiddenField ID="HF_SiteID" runat="server" Value='<%# Eval("SiteID").ToString().Trim() %>' />
                                                <br />
                                                <span>顺序</span><input id="txtShowOrder" style="width: 3rem; height: 1.5rem;" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value='<%# Eval("ShowOrder").ToString() %>' runat="server" /><br/>
                                                <asp:CheckBoxList ID="ckblistridstr" RepeatDirection="Horizontal" RepeatLayout="Table" runat="server" DataTextField="rname" DataValueField="rid"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" ForeColor="#ffffff" OnClick="Button1_Click" Text="确定" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
