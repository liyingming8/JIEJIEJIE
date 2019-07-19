<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MSjbInfoAddEdit.aspx.cs" Inherits="TJ_MSjbInfoAddEdit" %>
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
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="gridtable">
                            <tr><th>发货批次：</th><td><asp:DropDownList ID="ComboBox_PC" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="FHPiCi" DataValueField="FHPiCi"  Width="247px" Height="20px">
                                                      <asp:ListItem Value="0">批次</asp:ListItem>
                                                  </asp:DropDownList></td></tr>
                            <tr><th>设施编号：</th><td><input id="inputSHNum" runat="server" type="text" /></td></tr>
                            <tr><th>合作社名：</th><td><input id="inputHZSName" runat="server" type="text"  /></td></tr>
                            <tr><th >农户姓名：</th><td ><input id="inputNHName" runat="server" type="text"  /></td></tr>
                            <tr><th>所属区域：</th><td><input id="inputSSQuYu" runat="server" type="text" /></td></tr>
                            <tr><th>产品批次：</th><td><input id="inputChanPinPiCi" runat="server" type="text"  /></td></tr>
                            <tr><th>产品等级;</th><td><input id="inputChanPinDengJi" runat="server" type="text"/></td></tr>
                            <tr><th>质检员;</th><td><input id="inputZhiJianYuan" runat="server" type="text"  /></td></tr>
                            <tr><th>联系人：</th><td><input id="inputLianXiRen" runat="server" type="text"  /></td></tr>
                            <tr><th>联系电话：</th><td><input id="inputPhone" runat="server" type="text" /></td></tr>
                            <tr><th>产品说明：</th><td>
                                                  <asp:TextBox ID="TxtChanPin" runat="server" TextMode="MultiLine" Height="100px" Width="204px"></asp:TextBox></td></tr>
                        </table> 
                  <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/></div>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" /> 
        </form>
         <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>