<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AgentAcceptInfo.aspx.cs" Inherits="Admin_TJ_AgentAcceptInfo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="cc2" Namespace="BL.Controls.ComboBox" Assembly="BL.Controls" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
        
            <div class="container topdiv">
            <div class="topitem">
                 <asp:DropDownList runat="server" ID="ddl_departid" DataTextField="department" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddl_departid_SelectedIndexChanged">
                 </asp:DropDownList>
            </div>
            <div class="topitem">
                    <cc2:ComboBox runat="server" ID="ddl_terminal" DataTextField="CompName" Width="250px" DataValueField="CompID" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddl_terminal_SelectedIndexChanged" RenderMode="ComboBoxSearch" OnComboBoxChanged="ddl_terminal_ComboBoxChanged">
                        <asp:ListItem Value="0" Selected="True">全部终端店</asp:ListItem>
                    </cc2:ComboBox>
            </div>
            <div class="topitem"><span>上传时间</span></div>
            <div class="topitem">
                 <input type="text" runat="server" id="tb_start"  class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
            </div>
            <div class="topitem">
                <span>至</span>
            </div>
            <div class="topitem">
                <input type="text" runat="server" id="tb_end" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
            </div>  
            <div class="topitem">
                <asp:CheckBox runat="server" ID="ckb_isexception" Text="异常"/>
            </div>
            <div class="topitem">
                <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
            </div>
            <div class="topitem">
                <asp:Button runat="server" ID="btn_createexcel" Style="text-decoration: underline; cursor: pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click"/>
            </div>
        </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <div style="overflow-x: auto;">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID,isexception" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="终端店">
                        <ItemTemplate>
                            <asp:Label ID="LabelAcceptAgentID" runat="server" Text='<%#Terminalname(Eval("AcceptAgentID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="箱标号码">
                        <ItemTemplate>
                            <asp:Label ID="LabelBoxLabel" runat="server" Text='<%# Bind("BoxLabel") %>'></asp:Label><asp:Label ID="lab_exception" runat="server" Text='<%# (int.Parse(Eval("isexception").ToString()).Equals(0)?"":"*") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:Label ID="LabelProID" runat="server" Text='<%# ProductName(Eval("ProID").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="上传时间">
                        <ItemTemplate>
                            <asp:Label ID="LabelAcceptDate" runat="server" Text='<%# Bind("AcceptDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="已确认">
                        <ItemTemplate>
                            <asp:CheckBox ID="Labelisprized" Enabled="false" runat="server" Checked='<%# Convert.ToBoolean(Eval("isprized")) %>'></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="扫码地址">
                        <ItemTemplate>
                            <asp:Label ID="lab_sm_address" runat="server" Text='<%# Eval("UploadAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView> 
              <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="12" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel> 
             </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
        <input runat="server" id="terminalidstring" type="hidden"/>
    </form> 
</body>
</html>
