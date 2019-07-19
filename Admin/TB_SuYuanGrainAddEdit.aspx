<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanGrainAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanGrainAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanGrain</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="editpageback">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>供应商</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxGYSID" runat="server" DataTextField="GYSName" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <th>入库时间</th>
                            <td>
                                <input id="inputRuKuTime" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})"  maxlength="16" />
                            </td>
                        </tr>
                        <tr>
                            <th>原粮类型</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxCID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                           <th>数量</th>
                            <td>
                                <table style="border-collapse: collapse; table-layout: auto">
                                    <tr>
                                        <td>
                                            <input id="inputCount" width="100%" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="length2" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ComboBoxUNCID" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>入库批次</th>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <input id="inputPiCI" runat="server" type="text" readonly="readonly" maxlength="250"  />
                                        </td>
                                        <td>
                                            <asp:Button ID="ButtonShengCheng" runat="server" Text="生成" OnClick="ButtonShengCheng_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                          <th>库房</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxKuFang" runat="server" DataTextField="KuFang" DataValueField="ID" Width="120px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>检测机构</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxJCJG" runat="server" DataTextField="JGName" DataValueField="JCJGID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>质检员</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxZhiJianID" runat="server" DataTextField="ZhiJianName" DataValueField="ID" Width="60px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>质检报告</th>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_Logo" Width="120px" runat="server" />
                                        </td>
                                        <td>
                                            <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                                                src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200"
                                                style="vertical-align: text-bottom" width="250"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input id="inputRemarks" runat="server" type="text" maxlength="100"  />
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
    </form>
     <script src="../include/js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
     <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
