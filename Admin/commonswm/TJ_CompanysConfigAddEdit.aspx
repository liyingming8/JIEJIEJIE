<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompanysConfigAddEdit.aspx.cs" Inherits="Admin_commonswm_TJ_CompanysConfigAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RegisterCompanys</title> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" /> 
  
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
                                    type="text" maxlength="25" size="50" class="p80" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                             <th>公司类别</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CompTypeID" runat="server" DataTextField="CName" DataValueField="CID" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>状态</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CompAutherID" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>负责人</th>
                            <td>
                                <input id="inputLegalPerson" runat="server" maxlength="10" type="text" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                              <th>企业介绍</th>
                            <td>
                                <textarea id="qiyejieshao" runat="server" class="p80" cols="20" name="S1" rows="3"></textarea></td>
                        </tr>
                        <tr>
                            <th>LOGO</th>
                            <td>
                                <asp:Image ID="Image_Logo" runat="server" Height="120px" ImageUrl="~/Admin/Images/NoPic.gif" /> 
                                <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" style="vertical-align: text-bottom" width="240"></iframe>
                            </td>

                        </tr> 
                        <tr>
                            <th>城市</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CTID" runat="server" Enabled="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <th>地址</th>
                            <td>
                                <textarea id="inputAddress" runat="server"  cols="20" rows="2" class="p80"></textarea>
                                </td>
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
          <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
     <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
