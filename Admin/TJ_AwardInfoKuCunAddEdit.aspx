<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfoKuCunAddEdit.aspx.cs" Inherits="Admin_TJ_AwardInfoKuCunAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_AwardInfo</title>
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
                            <th>奖品名称</th>
                            <td>
                                <asp:Label ID="Label_award_name" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>现有库存</th>
                            <td>
                                <input id="input_kucunliang" runat="server" class="p20" type="text" />
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="确定" CssClass="btn btn-warning btnyd" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script type="text/javascript" src="../include/js/jquery-2.1.1.min.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </form>
</body>
</html>
