<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanysAddEdit.aspx.cs" Inherits="Admin_TJ_RegisterCompanysAddEdit" %>
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
                            <th>父级单位</th>
                            <td>
                                <input id="inputparentid" value="请选择..." runat="server" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                             <th>欢迎页</th>
                            <td>
                                <input id="TextWelcomePage" runat="server" class="length8" placeholder="默认时钟日期页" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>单位名称</th>
                            <td>
                                <input id="inputCompName" runat="server"
                                    type="text" maxlength="25" size="50" class="length8" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputCompName" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                             <th>公司类别</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CompTypeID" runat="server" DataTextField="CName" DataValueField="CID">
                                </asp:DropDownList>
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
                              <th>状态</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CompAutherID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>企业LOGO</th>
                            <td colspan="3">
                                <asp:Image ID="Image_Logo" runat="server" Height="120px" ImageUrl="~/Admin/Images/NoPic.gif" />
                                <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" style="vertical-align: text-bottom" width="250"></iframe>
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
                              <th>传真</th>
                            <td>
                                <input id="inputFaxNumber" runat="server" type="text" maxlength="25" />
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
                            <th>城市</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CTID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <th>地址</th>
                            <td>
                                <input id="inputAddress" runat="server"
                                    type="text" maxlength="75" class="length6" /></td>
                        </tr>
                        <tr>
                            <th>对公帐号</th>
                            <td>
                                <input id="inputAccountNumber" runat="server" type="text" maxlength="40" class="input7" /></td>
                        </tr>
                        <tr>
                             <th>所属银行</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_AccTypeID" runat="server" DataTextField="CName"
                                    DataValueField="CID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>精确位置</th>
                            <td colspan="3">
                                <input id="inputPosition" runat="server" type="text"
                                    maxlength="50" class="input1" />
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="3"><span style="font-weight: bold">获得坐标方法：</span><br />
                                登录：<a
                                    href="http://api.map.baidu.com/lbsapi/getpoint/index.html" target="_blank"
                                    style="color: #FF0000">百度地图</a>，搜索地址，在地图上右键点击所要获取的地址的具体位置（未提高精确度，请尽量放大地图），<br />
                                鼠标点击具体位置，页面右上角的输出框里的坐标就是百度地图坐标!</td>
                        </tr>
                        <tr>
                            <th>企业介绍</th><td><textarea id="qiyejieshao" runat="server" rows="3"  style="width: 85%;height:200px"></textarea>
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
          <asp:HiddenField ID="HF_LectureImage" runat="server" />
          <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
     <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
