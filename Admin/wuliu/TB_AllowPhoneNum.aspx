<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_AllowPhoneNum.aspx.cs" Inherits="Admin_TB_AllowPhoneNum" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../../include/easyui.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
             <div class="div_WholePage">
              <div class="topdiv">
                <div class="topitem"><input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_AllowPhoneNumAddEdit.aspx?cmd=add', 450, 300, '窜货督察授权')" /></div> 
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                          <asp:ListItem Value="Phone_Num">电话号码</asp:ListItem>
                          <asp:ListItem Value="Master">姓名</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span> 包含</span></div>
                  <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容"  class="inputsearch" /></div>
                  <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>  
               </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" 　 AutoGenerateColumns="False" DataKeyNames="ID" onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" > 
                    <Columns> 
                        <asp:TemplateField HeaderText="电话号码">
                            <ItemTemplate>
                                <asp:Label ID="LabelPhone_Num" runat="server" Text='<%# Bind("Phone_Num") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelMaster" runat="server" Text='<%# Bind("Master") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="Labelisactive" runat="server" Text='<%# Eval("isactive").ToString() == "1" ? "已获授权" : "未获授权" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                   <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="GridViewPagerStyle" pagesize="15"   FirstPageText="首页" LastPageText="尾页" nextpagetext="下一页" prevpagetext="上一页" pageindexboxtype="DropDownList" submitbuttontext="Go" textafterpageindexbox="页" textbeforepageindexbox="转到" onpagechanging="AspNetPager1_PageChanging" NumericButtonCount="5"></webdiyer:aspnetpager>
            </div>
        </form>
        <script src="../../include/js/jquery.min.js" type="text/javascript"></script> 
        <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
        <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>