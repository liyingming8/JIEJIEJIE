<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardTypeAddEdit.aspx.cs" Inherits="Admin_TJ_AwardTypeAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_AwardType</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="editpageback">
                    <div class="toptitle">奖品类型编辑</div>
                    <table class="gridtable">
                        <tr>
                            <th>奖品类型</th>
                            <td>
                                <input id="inputawardtype" runat="server" class="p60" type="text" maxlength="15" /></td> 
                        </tr>
                        <tr>
                            <th>父级</th>
                            <td>
                                <asp:DropDownList ID="ddl_parentid" runat="server">
                                </asp:DropDownList>
                            </td> 
                        </tr>
                        <tr>
                            <th>通用</th>
                            <td>
                                 <table style="width: 100%;"><tr><td>
                                     <input id="ckb_compid" runat="server" type="checkbox" onchange="check()" checked="True" value="通用" />
                                     </td><td><span id="input" runat="server" style="display:none;"><input id="input_compid" type="text" runat="server" class="p60"/></span></td></tr></table>
                            </td> 
                        </tr>
                        <tr>
                            <th>备 注</th>
                            <td>
                                <input id="inputremarks" class="p80" runat="server" type="text" maxlength="25" />
                            </td> 
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="../include/js/UploadImage.js"></script>
        <script type="text/javascript">
            function check() {
                var ck = $("#ckb_compid").attr("checked");
                console.log(ck);
                if (ck === 'checked') {
                    console.log("1");
                    $("#input").hide();
                } else {
                    console.log("0");
                    $("#input").show();
                }
            }
        </script>
        <input id="hdcompid" runat="server" type="hidden" />
    </form>
</body>
</html>
