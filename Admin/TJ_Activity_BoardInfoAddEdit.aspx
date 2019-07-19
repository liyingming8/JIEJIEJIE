<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_BoardInfoAddEdit.aspx.cs" Inherits="Admin_TJ_Activity_BoardInfoAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_Activity_BoardInfo</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="toptitle">活动模板编辑</div>
                <table class="gridtable">
                    <tr>
                        <th>模板名称</th>
                        <td>
                            <input id="inputActivityName" runat="server" type="text" class="p80" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>实用类型</th>
                        <td>
                            <asp:DropDownList ID="ddl_typeid" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>有效</th>
                        <td>
                            <asp:CheckBox ID="ckb_isactive" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>LOGO</th>
                        <td>
                              <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_logo" runat="server" Height="200" ImageUrl="~/images/nopic.gif" />
                                        </td>
                                        <td>
                                            <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_logo&amp;TargetHd=HF_Logo&amp;imgMaxSize=300" style="vertical-align: text-bottom" width="250"></iframe>
                                        </td>
                                    </tr>
                                </table></td>
                    </tr>
                    <tr><th>路径</th><td><input id="input_path" runat="server" class="p60" placeholder="前端存储路径"/></td></tr> 
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputRemarks" runat="server" type="text" class="p80" maxlength="25" /></td>
                    </tr>
                </table>
                <div class="bottomdivbutton">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script src="../js/jquery-1.7.1.js"></script>
        <asp:HiddenField ID="HF_Logo" runat="server" />
        <script src="../include/js/UploadImage.js"></script>
    </form>
</body>
</html>
