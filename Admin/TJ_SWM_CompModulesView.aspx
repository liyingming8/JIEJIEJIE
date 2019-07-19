<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_CompModulesView.aspx.cs" Inherits="Admin_TJ_SWM_CompModulesView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <style type="text/css">
        .searchbox {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_WholePage">
        <div style="overflow-x: auto;"> 
            <asp:DataList ID="datalist" runat="server" RepeatColumns="2"  CellPadding="10" CellSpacing="10" Width="100%">
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Image ID="Image1" Width="60px" ImageUrl='<%#"Images/swmlogo/"+LPH+"/"+Eval("lg") %>' runat="server" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("pg") %>'></asp:Label>
                    <input type="hidden" id="hdlg" runat="server" value='<%# Eval("lg").ToString() %>'/>
                    <input type="hidden" id="hdpg" runat="server" value='<%# Eval("pg").ToString() %>'/>
                    <input type="hidden" id="hdlk" runat="server" value='<%# Eval("lk").ToString() %>'/>
                    <input type="hidden" id="hdky" runat="server" value='<%# Eval("ky").ToString() %>'/>
                    <input type="hidden" id="hdid" runat="server" value='<%# Eval("id").ToString() %>'/>
                </ItemTemplate>
            </asp:DataList>
            </div>
           <div style="width: 100%;text-align: center;margin-top: 10px;margin-bottom: 15px;"><asp:Button runat="server" ID="btn_setbymyself" CssClass="btn btn-warning btnyd" Text="自定义" OnClick="btn_setbymyself_Click"/></div>
    </div> 
        <input type="hidden" runat="server" id="hdpid"/>
    </form>
</body>
</html>
