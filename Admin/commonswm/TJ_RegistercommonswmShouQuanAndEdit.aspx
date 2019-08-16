<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegistercommonswmShouQuanAndEdit.aspx.cs" Inherits="Admin_commonswm_TJ_RegistercommonswmShouQuanAndEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RegisterCompanys</title> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" /> 
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
                <ContentTemplate>
                    <table class="gridtable"> 
                        <tr>
                            <th id="comp" runat="server"></th>
                            <td>
                                <input id="inputCompName" runat="server"
                                    type="text" maxlength="25" size="50" class="length8" />                             
                            </td>
                        </tr>
                        <tr>
                              <th id="intruct" runat="server"></th>
                            <td>
                                <textarea id="qiyejieshao" runat="server" class="p80" cols="20" name="S1" rows="3"></textarea></td>
                        </tr>
                        <tr>
                            <th>状态</th>
                            <td>
                                <asp:RadioButton ID="radOff" runat="server"  Text="未授权"  GroupName="type"/>
                                <asp:RadioButton ID="radOn" runat="server" Text="授权" GroupName="type" />
                            </td>
                        </tr>
                        <tr>
                            <th id="images" runat="server"></th>
                            <td>
                                <asp:Image ID="Image_Logo" runat="server" Height="120px" ImageUrl="~/Admin/Images/NoPic.gif" /> 
                            </td>
                        </tr>
                        <tr>
                            <th>手机</th>
                            <td>
                                <input id="inputTelNumber" runat="server"
                                    type="text" maxlength="25" size="50" class="length8" />
                            </td>
                        </tr>
                        <tr>
                             <th>地址</th>
                            <td>
                                <input id="inputAddress" runat="server" type="text" maxlength="75" class="p80" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="确定" />
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
     
  <script type="text/javascript">
        function closemyWindow() {
            
        }
    </script>
</body>
</html>
