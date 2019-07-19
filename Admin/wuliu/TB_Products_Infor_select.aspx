<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_Infor_select.aspx.cs" Inherits="Admin_TB_Products_Infor_select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="div_WholePage" style="text-align: left; padding: 0.1rem;"> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="topdiv">
                        <div class="topitem"><input type="text" id="inputSearchKeyword" runat="server" /></div>
                        <div class="topitem">
                            <asp:Button ID="btn_search" runat="server" CssClass="btn btn-success btnyd" Text="检索" OnClick="btn_search_Click" />  
                        </div> 
                    </div>
                    <div style="overflow-x: auto;width:100%; "><asp:CheckBoxList ID="checkboxlist_product" RepeatColumns="3" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID" CellPadding="5" CellSpacing="2"></asp:CheckBoxList></div> 
                    <div class="bottomdivbutton" style="text-align: center;">
                        <asp:Button runat="server" ID="btn_ok" CssClass="btn btn-warning btnyd" Text="确定" OnClick="btn_ok_Click" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    <input type="hidden" id="hd_agentid" runat="server" />
    <input type="hidden" id="hd_ITGRID" value="0" runat="server"/>
    <input type="hidden" id="hd_author_products" runat="server" />
    <input type="hidden" id="hd_ProductsInfo" runat="server"/>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../include/js/UploadImage.js"></script> 
    </form>
    </body>
</html>
