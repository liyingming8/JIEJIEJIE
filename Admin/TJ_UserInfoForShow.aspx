<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UserInfoForShow.aspx.cs" Inherits="Admin_TJ_UserInfoForShow" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_User</title> 
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
                            <th>头像</th>
                            <td> 
                                <img alt="" src="Images/NoPic.gif" runat="server" id="imgheader" />
                            </td>
                        </tr>
                        <tr>
                            <th>昵称</th>
                            <td>
                                <asp:Label runat="server" ID="labnickname"></asp:Label></tr>
                        <tr>
                            <th>注册时间</th>
                            <td>
                                <asp:Label ID="Label_RegisterDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                   <%--     <tr>
                            <th>打折授权</th>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList_Discout" runat="server" DataTextField="Remarks" DataValueField="CID" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </td>
                        </tr>--%>
                    </table>
                    <div class="bottomdivbutton">
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>  
        </div> 
         <asp:HiddenField ID="HF_IsSuperAministrator" runat="server" />
          <asp:HiddenField ID="HF_CMD" runat="server" />
          <asp:HiddenField ID="HF_ID" runat="server" />
         <asp:HiddenField ID="hf_compid" runat="server" />
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script> 
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>  
</body>
</html>
