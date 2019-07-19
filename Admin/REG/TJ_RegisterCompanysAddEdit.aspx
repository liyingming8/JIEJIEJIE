<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanysAddEdit.aspx.cs" Inherits="Admin_REG_TJ_RegisterCompanysAddEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RegisterCompanys</title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>父级单位</td>
                            <td colspan="5">
                                <asp:DropDownList id="ComboBox_ParentID" runat="server" datatextfield="CompName" datavaluefield="CompID" >
                                                             </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>单位名称</td>
                            <td colspan="2">
                                <input id="inputCompName" runat="server"
                                    type="text" maxlength="25" size="50" />
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputCompName" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <%--<asp:CheckBoxList ID="CBLIST_ProductType" runat="server" DataTextField="GoodsType" DataValueField="CompGoodsTypeID" Enabled="False" RepeatDirection="Horizontal">
                </asp:CheckBoxList>--%>
                            </td>
                            <td>;</td>
                        </tr>
                        <tr>
                            <td>公司类别</td>
                            <td>
                                <asp:DropDownList id="ComboBox_CompTypeID" runat="server" datatextfield="CName" datavaluefield="CID">
                                    </asp:DropDownList>
                            </td>
                            <td></td>
                            <td><%--产品类别--%>状态</td>
                            <td><%--<asp:CheckBoxList ID="CBLIST_ProductType" runat="server" DataTextField="GoodsType" DataValueField="CompGoodsTypeID" Enabled="False" RepeatDirection="Horizontal">
                </asp:CheckBoxList>--%>
                                <asp:DropDownList id="ComboBox_CompAutherID" runat="server">
                                    </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>企业LOGO</td>
                            <td colspan="5">
                                <asp:Image ID="Image_Logo" runat="server" />
                                <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" style="vertical-align: text-bottom" width="250"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td>负责人</td>
                            <td colspan="2">
                                <input id="inputLegalPerson" runat="server" maxlength="10" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputLegalPerson" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>主页</td>
                            <td>
                                <input id="inputCompanyWebSite" runat="server" maxlength="25" size="40"
                                    type="text" /></td>
                            <td>;</td>
                        </tr>
                        <tr>
                            <td>电子邮箱</td>
                            <td colspan="2">
                                <input id="inputEMail" runat="server" type="text" maxlength="25" /></td>
                            <td></td>
                            <td></td>
                            <td>;</td>
                        </tr>
                        <tr>
                            <td>联系电话</td>
                            <td colspan="2">
                                <input id="inputTelNumber" runat="server" type="text" maxlength="25" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="inputTelNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>传真</td>
                            <td>
                                <input id="inputFaxNumber" runat="server" type="text" maxlength="25" /></td>
                            <td>;</td>
                        </tr>
                        <tr>
                            <td>城市</td>
                            <td>
                                <asp:DropDownList ID="ComboBox_CTID" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>地址</td>
                            <td>
                                <input id="inputAddress" runat="server"
                                    type="text" maxlength="75" size="50" /></td>
                            <td>;</td>
                        </tr>
                        <tr>
                            <td>对公帐号</td>
                            <td colspan="2">
                                <input id="inputAccountNumber" runat="server" type="text" maxlength="40" class="input7" /></td>
                            <td>所属银行</td>
                            <td>
                                <asp:DropDownList ID="ComboBox_AccTypeID" runat="server" DataTextField="CName" DataValueField="CID"></asp:DropDownList>
                            </td>
                            <td>;</td>
                        </tr>
                        <%--   <tr><td>备注</td><td colspan="4">
                <asp:TextBox ID="inputRemarks" runat="server" MaxLength="25" 
                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                </td>
                <td>
                    ;</td>
            </tr>--%>
                        <%--            <tr>
                <td>
                    营业执照</td>
                <td colspan="3">
                    <asp:Image ID="Image_LectureImage" runat="server" />
                </td>
                <td colspan="2">
                    <iframe ID="I2" runat="server" frameborder="0" height="23" name="I2" scrolling="no" 
                        src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_LectureImage&amp;TargetHd=HF_LectureImage&amp;imgMaxSize=200" 
                        style="vertical-align: text-bottom" width="250"></iframe>
                </td>
            </tr>--%>
                        <tr>
                            <td>精确位置</td>
                            <td colspan="5">
                                <input id="inputPosition" runat="server" type="text"
                                    maxlength="50" class="input1" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="5"><span style="font-weight: bold">获得坐标方法：</span><br />
                                登录：<a
                                    href="http://api.map.baidu.com/lbsapi/getpoint/index.html" target="_blank"
                                    style="color: #FF0000">百度地图</a>，搜索地址，在地图上右键点击所要获取的地址的具体位置（未提高精确度，请尽量放大地图），<br />
                                鼠标点击具体位置，页面右上角的输出框里的坐标就是百度地图坐标!</td>
                        </tr>
                        <tr>
                            <td>企业介绍</td>
                            <td colspan="5">
                                <CKEditor:CKEditorControl ID="CKEditorControl01" BodyClass="auto-style1" runat="server">
                                </CKEditor:CKEditorControl></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HF_CMD" runat="server" />
                                <asp:HiddenField ID="HF_ID" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="HF_LogoImage" runat="server" />
                                <asp:HiddenField ID="HF_LectureImage" runat="server" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
