<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_GoodsInfoSimpleForBind.aspx.cs" Inherits="Admin_TJ_GoodsInfoSimpleForBind" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv"> 
                <div class="topitem">
                    <asp:Button ID="btn_ok" runat="server" CssClass="btn btn-warning btnyd" Text="确定" OnClick="btn_ok_Click" />
                </div>  
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server" PageSize="18" Width="100%" AutoGenerateColumns="False" DataKeyNames="GoodsID,WLProID" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="选择"> 
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxSelect" Enabled="False" runat="server" />  
                                <asp:HiddenField ID="hd_goodid" runat="server" Value='<%# Bind("WLProID") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="产品名称"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsName" runat="server" Text='<%# Bind("GoodsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="上架时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelBeginSaleDate" runat="server" Text='<%# Bind("BeginSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下架时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelEndSaleDate" runat="server" Text='<%# Bind("EndSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>   
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />

                    <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

        </div>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
        <input type="hidden" id="hf_pid" runat="server" />
        <input type="hidden" id="hf_goodid" runat="server" />
        <script type="text/javascript">
            function select(index) { 
                var chbs = document.getElementsByTagName('input');   
                for (var i = 0; i < chbs.length; i++) { 
                    if (chbs[i].type == 'checkbox') {
                        chbs[i].checked = false;
                    } 
                }
                var eleid = 'GridView1_ctl' + (parseInt(index) + 2 + 100).toString().substring(1) + '_',
                    elename = eleid + "CheckBoxSelect";
                var hdid = 'GridView1_ctl' + (parseInt(index) + 2 + 100).toString().substring(1) + '_hd_goodid';
                if ((document.getElementById(elename)).checked) {
                    (document.getElementById(elename)).checked = false;
                   
                } else {
                    (document.getElementById(elename)).checked = true; 
                    document.getElementById("hf_goodid").value = (document.getElementById(hdid)).value;
                }
                console.log((document.getElementById(hdid)).value);
                console.log(document.getElementById("hf_goodid").value);
            }
        </script>
    </form>
    <script type="text/javascript" src="../include/js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../include/js/UploadImage.js"></script>
</body>
</html>
