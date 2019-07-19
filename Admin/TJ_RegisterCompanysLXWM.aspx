<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanysLXWM.aspx.cs" Inherits="Admin_TJ_RegisterCompanysLXWM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RegisterCompanys</title> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
  
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable"> 
                        <tr>
                            <th>单位名称</th>
                            <td>
                                <input id="inputCompName" runat="server"
                                    type="text" maxlength="25" size="50" class="p80" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputCompName" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>负责人</th>
                            <td>
                                <input id="inputLegalPerson" runat="server" maxlength="10" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputLegalPerson" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>联系电话</th>
                            <td>
                                <input id="inputTelNumber" runat="server" type="text" maxlength="25" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="inputTelNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>电子邮箱</th>
                            <td>
                                <input id="inputEMail" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                              <th>主页</th>
                            <td>
                                <input id="inputCompanyWebSite" runat="server" maxlength="40" class="length6"
                                    type="text" />
                            </td>
                        </tr> 
                        <tr>
                             <th>地址</th>
                            <td>
                                <textarea id="inputAddress" runat="server"
                                    type="text" maxlength="75" class="p90" ></textarea></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
          <asp:HiddenField ID="HF_LogoImage" runat="server" />
          <asp:HiddenField ID="hf_parentid" runat="server" />
          <asp:HiddenField ID="HF_ID" runat="server" />
          <asp:HiddenField ID="HF_LectureImage" runat="server" />
          <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
     <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
