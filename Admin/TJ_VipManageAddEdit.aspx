<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_VipManageAddEdit.aspx.cs" Inherits="Admin_TJ_VipManageAddEdit" %> 
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
                            <%--<tr><td>VipID</td><td><input id="inputVipID" runat="server" type="text" maxlength="2" /></td><td></td></tr>--%>
                            <tr><th>会员名称</th><td><input id="inputVipName" runat="server" type="text" maxlength="25" /></td></tr>
                            <tr><th>手机号码</th><td><input id="inputMPhone" runat="server" type="text" maxlength="25" /></td></tr>
                            <tr><th>邮箱</th><td><input id="inputEmail" runat="server" type="text" maxlength="25" /></td></tr>
                            <tr><th>注册时间</th><td><input id="inputZhuCTime" runat="server" type="text" maxlength="8" /></td></tr>
                            <tr><th>可用积分</th><td><input id="inputJiFen" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                            <tr><th>所在地区</th><td><input id="inputAddress" runat="server" type="text" maxlength="25" /></td></tr>
                            <tr><th>所在城市</th><td>

                                                 <asp:DropDownList ID="Comb_PlaceID" runat="server" DataTextField="CName" 
                                                               DataValueField="CID">
                                                 </asp:DropDownList>

                                             </td></tr>
                            <%--<tr><td>CompID</td><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
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