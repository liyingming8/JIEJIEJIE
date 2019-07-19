<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterInnerCompanysAddEdit.aspx.cs" Inherits="Admin_TJ_RegisterInnerCompanysAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RegisterCompanys</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>公司名称</th>
                            <td colspan="3">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputCompName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <input id="inputCompName" runat="server"
                                    type="text" maxlength="25" size="50" /></td> 
                        </tr>
                        <tr>
                            <th>产品类型</th>
                            <td colspan="2">
                                <asp:DropDownList ID="ComboBox_GoodsTypeID" runat="server" AppendDataBoundItems="true" DataTextField="GoodsType"
                                    DataValueField="CompGoodsTypeID">
                                    <asp:ListItem Text="产品类型..." Selected="True" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                              <th>最低价</th>
                            <td>
                                <asp:TextBox ID="TextBox_MinPrice" runat="server" Width="108px">280</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>企业LOGO</th>
                            <td colspan="3">
                                <asp:Image ID="Image_Logo" Width="120px" runat="server" />
                                <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200" style="vertical-align: text-bottom" width="250"></iframe>
                            </td> 
                        </tr>
                        <tr>
                            <th>公司法人</th>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputLegalPerson" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <input id="inputLegalPerson" runat="server" maxlength="10" type="text" /></td> 
                        </tr>
                        <tr>
                               <th>主页</th>
                            <td>
                                <input id="inputCompanyWebSite" runat="server" maxlength="25" size="40"
                                    type="text" /></td>
                        </tr>
                        <tr>
                            <th>电子邮箱</th>
                            <td>
                                <input id="inputEMail" runat="server" type="text" maxlength="25" /></td> 
                        </tr>
                        <tr>
                             <th>注册资金</th>
                            <td>
                                <input id="inputZhuCeZiJin" runat="server"
                                    type="text" onkeyup="if(isNaN(value))execCommand('undo')"
                                    onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20"
                                    class="input4" />万<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="inputZhuCeZiJin" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>联系电话</th>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="inputTelNumber" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <input id="inputTelNumber" runat="server" type="text" maxlength="25" /></td> 
                        </tr>
                        <tr>
                              <th>传真</th>
                            <td>
                                <input id="inputFaxNumber" runat="server" type="text" maxlength="25" /></td>
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
                                    type="text" maxlength="75" size="50" /></td>
                        </tr>
                        <tr>
                            <th>税务登记证号</th>
                            <td>
                                <input id="inputTaxRegisterCode" runat="server" type="text" maxlength="25" /></td> 
                        </tr>
                        <tr>
                            <th>状态</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CompAutherID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>公司帐号</th>
                            <td>
                                <input id="inputAccountNumber" runat="server" type="text" maxlength="40" /></td>
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
                            <th>备注</th>
                            <td colspan="4">
                                <asp:TextBox ID="inputRemarks" runat="server" MaxLength="25"
                                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>营业执照</th>
                            <td colspan="3">
                                <asp:Image ID="Image_LectureImage" runat="server" Width="16px" />
                                <iframe id="I2" runat="server" frameborder="0" height="23" name="I2" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_LectureImage&amp;TargetHd=HF_LectureImage&amp;imgMaxSize=200" style="vertical-align: text-bottom" width="250"></iframe>
                            </td> 
                        </tr>
                        <tr>
                            <th>折扣授权</th>
                            <td colspan="4">
                                <asp:CheckBoxList ID="CheckBoxList_Discout" runat="server" DataTextField="Remarks" DataValueField="CID" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <th>公司简介</th>
                            <td colspan="4">
                                 <textarea id="textarea" runat="server" rows="5" cols="20" style="width:98%;"></textarea>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_LogoImage" runat="server" />
    </form>
       
</body>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</html>
