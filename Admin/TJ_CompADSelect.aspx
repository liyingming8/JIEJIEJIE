<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompADSelect.aspx.cs" Inherits="Admin_TJ_CompADSelect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CompADInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" /> 
    <style type="text/css">
        input[type='radio'] {
            margin: 20px !important;
        }
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="layim-title">指定广告位置</div>
                    <table class="gridtable">
                        <tr> 
                            <td>
                                  <asp:RadioButtonList ID="rbl_ad" runat="server" CellPadding="0" CellSpacing="0" DataTextField="ADName" DataValueField="ADID" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="rbl_ad_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>  
        <input type="hidden" runat="server" id="fromurl"/>
    </form> 
</body>
</html>
