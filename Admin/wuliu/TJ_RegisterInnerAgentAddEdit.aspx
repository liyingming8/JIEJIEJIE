<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterInnerAgentAddEdit.aspx.cs" Inherits="Admin_TJ_RegisterInnerAgentAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_RegisterCompanys</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
        
    </head>
    <body>
        <form id="form1" runat="server">
             <div class="editpageback">
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="../images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> 代理商信息编辑</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_RegisterInnerAgent.aspx"><img title="返回" src="../images/back.png" border="0" /></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager><br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       
                        <table class="gridtable">
                            <tr><th>代理商名称</th><td colspan="3"><input id="inputCompName" runat="server" 
                                                                     type="text" maxlength="25" size="50" /></td>
                             <%--   <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="inputCompName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                 </td>--%>
                            </tr>
                            <tr>
                                <th>
                                    联系人</th>
                                <td>
                                    <input ID="inputLegalPerson" runat="server" maxlength="10" type="text" /></td>
                              <%--  <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                ControlToValidate="inputLegalPerson" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>--%>
                            </tr>
                            <tr><th>联系电话</th><td><input id="inputTelNumber" runat="server" type="text" maxlength="25" /></td><%--<td>
                                                                                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                                                                                                             ControlToValidate="inputTelNumber" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                              
                            </tr>
                            <tr>
                                
                                  <th>传真</th><td><input id="inputFaxNumber" runat="server" type="text" maxlength="25" /></td>
                            </tr>
                            <tr><th>城市</th><td>
                                               <asp:DropDownList ID="ComboBox_CTID" runat="server">
                                               </asp:DropDownList>
                                           </td>  
                            </tr>
                            <tr>
                                <th>地址</th><td><input id="inputAddress" runat="server" 
                                                                               type="text" maxlength="75" size="50" /></td>
                            </tr>
                            <tr><th>备注</th><td colspan="4">
                                               <asp:TextBox ID="inputRemarks" runat="server" MaxLength="25" 
                                                            TextMode="MultiLine" Width="400px"></asp:TextBox>
                                           </td>
                            </tr>
                        </table>
                          <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
	          

                
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
            <asp:HiddenField ID="HF_LogoImage" runat="server" />
             <asp:HiddenField ID="HF_LectureImage" runat="server" />
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>