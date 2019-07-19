<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MeshPointAddEdit.aspx.cs" Inherits="Admin_TJ_MeshPointAddEdit" %>
<%@ Register assembly="CKEditor.NET" namespace="CKEditor.NET" tagprefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_MeshPoint</title>
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
                            <tr>
                                <th>所属城市</th>
                                <td>
                                    <asp:DropDownList ID="Comb_PlaceID" runat="server" DataTextField="CName" 
                                                  DataValueField="CID">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>网点名称</th>
                                <td>
                                    <input ID="inputMeshPointName" runat="server" maxlength="25" type="text" 
                                           class="input6" /></td>
                            </tr>
                            <tr>
                                <th>图片</th>
                                <td>
                                    <asp:Image ID="Image_Logo" runat="server" /><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" 
                                                                                        src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" 
                                                                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <th>联系人</th>
                                <td>
                                    <input ID="inputContracter" runat="server" maxlength="25" type="text" />
                                </td>
                            </tr>
                            <tr><th>电话</th><td><input id="inputTel" runat="server" type="text" maxlength="15" /></td></tr>
                            <tr><th>传真</th><td><input id="inputFax" runat="server" type="text" maxlength="15" /></td></tr>
                            <tr><td>详细地址</td><td><input id="inputAddressInfo" runat="server" type="text" 
                                                        maxlength="100" class="input6" /></td></tr>
                            <tr><th>网点介绍</th><td colspan="2">
                                                 <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" Height="250px">
                                                 </CKEditor:CKEditorControl>
                                             </td></tr>
                            <tr>
                                <th>网点特色</th>
                                <td>
                                    <input id="TextAdvantageString" runat="server" maxlength="600" type="text" style="width: 400px; height: 80px" />
                                </td>
                            </tr>
                            <tr>
                                <th>坐标信息</th>
                                <td>
                                    <input id="inputPosition" runat="server" type="text" 
                                           maxlength="50" class="input1" />
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td><span style="font-weight: bold">获得坐标方法：</span><br />登录：<a 
                                                                                               href="http://api.map.baidu.com/lbsapi/getpoint/index.html" target="_blank" 
                                                                                               style="color: #FF0000">百度地图</a>，搜索地址，在地图上右键点击所要获取的地址的具体位置（未提高精确度，请尽量放大地图），<br />鼠标点击具体位置，页面右上角的输出框里的坐标就是百度地图坐标!</td>
                            </tr>
                            <tr><th>备注</th><td><input id="inputRemark" runat="server" type="text" 
                                                      maxlength="25" class="input7" /></td><td></td></tr>
                           
                        </table>
                              <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
              
            </div>
              <asp:HiddenField  ID="HF_LogoImage" runat="server"/>
                 <asp:HiddenField ID="HF_CMD" runat="server" />
                <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>