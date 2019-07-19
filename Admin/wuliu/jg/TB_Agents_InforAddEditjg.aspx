<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_InforAddEditjg.aspx.cs" Inherits="Admin_TB_Agents_InforAddEditjg" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_Agents_Infor</title>
        <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <table class="gridtable">
                    <tr><td>城市</td><td>
                                       <asp:DropDownList ID="ComboBox_CID" runat="server">
                                       </asp:DropDownList>
                                   </td><td></td></tr>
                    <tr><td >地址</td><td ><input id="inputAgent_Addrss" runat="server" type="text" maxlength="50" class="input6" /></td><td ></td></tr>
                    <tr><td>经销商名称</td><td><input id="inputAgent_Name" runat="server" type="text" maxlength="50" class="input6" /></td><td> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入经销商名称！" ControlToValidate="inputAgent_Name"></asp:RequiredFieldValidator> </td></tr>
                    <tr><td>编码</td><td><input id="inputAgent_Code" runat="server" disabled="disabled" type="text" maxlength="30" /></td><td></td></tr>
                    <tr><td>联系人</td><td><input id="inputMiddleman" runat="server" type="text" maxlength="10" /></td><td></td></tr>
                    <tr><td>电话</td><td><input id="inputTelephone" runat="server" type="text" maxlength="20" /></td><td></td></tr>
                    <tr><td>手机</td><td><input id="inputMobiePhone" runat="server" type="text" maxlength="20" /></td><td></td></tr>
                    <tr><td>授权区域</td><td><input id="inputAllowAreaInfo" runat="server" type="text" maxlength="100" /></td><td></td></tr>
                    <tr><td>销售产品</td><td>
                                         <asp:CheckBoxList ID="CheckBoxList_PermitList" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID" RepeatColumns="5">
                                         </asp:CheckBoxList>
                                     </td><td> 
                                              </td></tr>
                    <tr runat="server" id="qujl" visible="false" ><td>区域经理</td><td><asp:DropDownList ID="ComboBox_QUJL" runat="server" AppendDataBoundItems="True"  AutoPostBack="True" DataTextField="LoginName" DataValueField="UserID"  Width="150px">
                                                                                       <asp:ListItem Value="0">选取区域经理</asp:ListItem>
                                                                                   </asp:DropDownList></td><td></td></tr>
                                                      
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="50" class="input6" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添 加" Width="50px" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
    </body>
</html>