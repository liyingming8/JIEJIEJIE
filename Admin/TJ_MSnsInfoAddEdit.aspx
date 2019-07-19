<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MSnsInfoAddEdit.aspx.cs" Inherits="TJ_MSnsInfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_VipManage</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <%--<tr><td>VipID</td><td><input id="inputVipID" runat="server" type="text" maxlength="2" /></td><td></td></tr>--%>
                        <tr>
                            <th>产品批次</th>
                            <td style="height: 40px">
                                <asp:DropDownList ID="ComboBox_PC" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="FHPiCi" DataValueField="FHPiCi" Width="247px" Height="20px">
                                    <asp:ListItem Value="0">批次</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>使用时间</th>
                            <td>
                                <asp:TextBox ID="inputTime" runat="server"></asp:TextBox>
                                <cc2:CalendarExtender TargetControlID="inputTime" ID="CalendarExtender1" Format="yyyy-MM-dd" runat="server">
                                </cc2:CalendarExtender>
                            </td>
                        </tr>

                        <tr>
                            <th>使用药物</th>
                            <td>
                                <asp:TextBox runat="server" ID="TxtYW" TextMode="MultiLine" Height="45px" Width="164px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th>使用肥料</th>
                            <td>
                                <asp:TextBox ID="TxtFL" runat="server" Height="45px" TextMode="MultiLine" Width="164px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>防治方式</th>
                            <td>
                                <asp:TextBox ID="TxtFZ" runat="server" Height="47px" TextMode="MultiLine" Width="164px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <th>相关措施</th>
                            <td>
                                <asp:TextBox ID="TxtCS" runat="server" Height="47px" TextMode="MultiLine" Width="164px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputBZ" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                    </table>
                     <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
         <asp:HiddenField ID="HF_CMD" runat="server" />
         <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
