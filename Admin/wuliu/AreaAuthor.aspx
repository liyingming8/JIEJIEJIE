<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AreaAuthor.aspx.cs" Inherits="Admin_wuliu_AreaAuthor" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <title>经销商区域授权</title>
    <link href="../../include/MasterPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_WholePage"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="topdiv">
                    <div class="topitem"><asp:DropDownList runat="server" DataTextField="vl" AppendDataBoundItems="True" DataValueField="id" ID="ddl_province" AutoPostBack="True" OnSelectedIndexChanged="ddl_province_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">全国</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="topitem"><asp:DropDownList runat="server" AppendDataBoundItems="True"  DataTextField="vl" DataValueField="id" ID="ddl_city" AutoPostBack="True" OnSelectedIndexChanged="ddl_city_SelectedIndexChanged">
                        <asp:ListItem Value="0">全省</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                </div> 
                <div style="overflow-x: auto;text-align: left;padding: 0.2rem;">
                       <asp:CheckBoxList ID="ckeckboxlist_area" DataTextField="vl" DataValueField="id" runat="server" CellPadding="2" CellSpacing="5" RepeatColumns="3" RepeatDirection="Horizontal"></asp:CheckBoxList> 
                </div>
                <div class="bottomdivbutton" style="text-align: center"><asp:Button runat="server" ID="btnok" Text="确定" CssClass="btn btn-warning btnyd" OnClick="btnok_Click"/></div>
            </ContentTemplate>
        </asp:UpdatePanel> 
    </div>
        <input type="hidden" runat="server" id="hd_agentid"/>
        <input type="hidden" runat="server" id="hd_author_areaid"/>
        <script src="../../include/js/jquery-1.7.1.js"></script>
        <script src="../../include/js/UploadImage.js"></script>
    </form>
</body>
</html>
