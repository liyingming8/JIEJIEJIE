<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_Infor.aspx.cs" Inherits="Admin_TB_Agents_Infor" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                   <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('/admin/wuliu/TB_Agents_InforAddEdit.aspx?cmd=add', 780, 580, '经销商信息')" /></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="CompName">经销商</asp:ListItem>
                        <asp:ListItem Value="LegalPerson">联系人</asp:ListItem>
                        <asp:ListItem Value="TelNumber">电话</asp:ListItem>
                        <asp:ListItem Value="MobilePhoneNumber">手机</asp:ListItem>
                        <asp:ListItem Value="AllowAreaInfo">授权区域</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:DropDownList ID="ComboBox_CID"  runat="server"></asp:DropDownList></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server" PageSize="20" Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="CompID,CompName,AllowAreaInfo,AllowProduct"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="省份城市">
                            <ItemTemplate>
                                <asp:Label ID="LabelCID" runat="server" Text='<%# commfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="经销商名称"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelAgent_Name" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="编码">
                            <ItemTemplate>
                                <asp:Label ID="LabelAgent_Code" runat="server" Text='<%# Bind("Agent_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="联系人"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelMiddleman" runat="server" Text='<%# Bind("LegalPerson") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="手机"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelMobiePhone" runat="server" Text='<%# Bind("MobilePhoneNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授权区域"> 
                            <ItemTemplate>               
                                <asp:HyperLink ID="LabelAllowAreaInfo" runat="server" ToolTip='<%# Eval("AllowAreaInfo").ToString()%>'  Text='<%# Eval("AllowAreaInfo").ToString().Length>20?Eval("AllowAreaInfo").ToString().Substring(0,20)+"...":Eval("AllowAreaInfo").ToString() %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="授权产品"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="LabelAllowProducts" runat="server" ToolTip='<%# Bind("AllowProduct") %>' Text='<%# (Eval("AllowProduct").ToString().Length>10?Eval("AllowProduct").ToString().Substring(0,10)+"...":Eval("AllowProduct").ToString()) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="部门授权">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlinkdepartment" Text="授权"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <%-- 
                        <asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <PagerTemplate> 
                    </PagerTemplate>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div> 
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <input runat="server" type="hidden" id="hd_allow_area"/>
        <input runat="server" type="hidden" id="hd_allow_product"/>
    </form> 
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
  
    </script>
</body>
</html>
