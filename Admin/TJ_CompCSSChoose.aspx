<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompCSSChoose.aspx.cs" Inherits="Admin_TJ_CompCSSChoose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CompADInfo</title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto;">
                    <table class="gridtable">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonListCSS" runat="server" RepeatColumns="10" RepeatDirection="Horizontal" DataTextField="CSSName" DataValueField="CSID" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListCSS_SelectedIndexChanged" CellPadding="10" CellSpacing="10" RepeatLayout="Flow">
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Image ID="ImageShow" runat="server" /></td>
                        </tr>
                    </table>
                        </div>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" OnClick="Button1_Click" Text="确 定" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" /> 
    </form>
</body>
</html>
