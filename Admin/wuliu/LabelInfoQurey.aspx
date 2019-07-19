<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelInfoQurey.aspx.cs" Inherits="Admin_wuliu_LabelInfoQurey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>标签情况查询</title>
    <link href="../../include/MasterPage.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_WholePage">
    <div class="topdiv">
        <div class="topitem"><span>标签号码</span></div>
        <div class="topitem"><input type="text"  id="TextBox_Label" runat="server" class="inputsearch" /></div>
        <div class="topitem"><asp:Button ID="Button1" CssClass="btn btn-warning btnyd" runat="server" OnClick="Button1_Click" Text="单枚查询" /></div> 
    </div>   
                 <div style="padding: 0.5rem 1rem 0 1rem;width: 100%;"><asp:Literal ID="literalrelate" runat="server"></asp:Literal></div>
                 <div  style="padding: 0.5rem 1rem 0 1rem;width: 100%;"><asp:Literal ID="literaltihuan" runat="server"></asp:Literal></div>
                <div style="overflow-x: auto;padding: 0.5rem 1rem 0 1rem;">
                    <asp:GridView ID="GridView_FaHuoXinXi" Width="100%"  runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False" Caption="发货信息" CaptionAlign="Left" OnRowDataBound="GridView_FaHuoXinXi_RowDataBound"> 
                     <Columns>
                         <asp:TemplateField HeaderText="级别">
                             <ItemTemplate>
                                 <asp:Label runat="server" ID="LabelIndex"></asp:Label>级
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:BoundField DataField="BoxLabel01" HeaderText="箱标号码" />
                          <asp:TemplateField HeaderText="产品名称">                           
                             <ItemTemplate>
                                 <asp:Label ID="LabelPRODU" runat="server" Text='<%# ReturnProductNameByID(Eval("PID").ToString())%>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="时间">  
                             <ItemTemplate>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToInt32(Eval("FHType")).Equals(3)?Eval("FHDate"):Eval("tm_confirmed") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="从">                           
                             <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# ReturnAgentNameAndCity(Eval("FromAgentID").ToString(),"1") %>'></asp:Label><asp:Label ID="Label2" runat="server" Text='<%# ReturnStoreHouseNameByID(Eval("FromStorehouseID").ToString()) %>'></asp:Label><asp:HiddenField runat="server" ID="hf_from" Value='<%#Eval("FromAgentID").ToString() %>'/>
                             </ItemTemplate>
                         </asp:TemplateField> 
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:Label ID="LabelFHType" runat="server" Text='<%# com.FHTypeName(Eval("FHType").ToString()) %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="至">                             
                             <ItemTemplate>
                                 <asp:Label ID="Label3" runat="server" Text='<%# ReturnAgentNameAndCity(Eval("ToAgentID").ToString(),"2") %>'></asp:Label>
                                 <asp:HiddenField runat="server" ID="hd_toagentid" Value='<%# Eval("ToAgentID").ToString()%>'/>
                             </ItemTemplate>
                         </asp:TemplateField> 
                          <asp:TemplateField HeaderText="数量(件)">                           
                             <ItemTemplate>
                                 <asp:Label ID="Labelpicinum" runat="server" Text='<%# (string.IsNullOrEmpty(Eval("FHKey").ToString())?"":(comm.getfhinfobyFhKey((Eval("FHKey").ToString()),Eval("FromAgentID").ToString())).Rows[0]["XiangNumber"].ToString()) %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="发货批次">                           
                             <ItemTemplate>
                                 <asp:Label ID="Labelpici" runat="server" Text='<%#(string.IsNullOrEmpty(Eval("FHKey").ToString())?"":(comm.getfhinfobyFhKey((Eval("FHKey").ToString()),Eval("FromAgentID").ToString())).Rows[0]["FHPiCi"].ToString())%>'></asp:Label>
                             </ItemTemplate>
                             <ItemStyle ForeColor="Black" />
                         </asp:TemplateField>  
                     </Columns>
                    <EmptyDataTemplate>
                        尚未查询到满足条件的发货记录!
                    </EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                    <asp:GridView ID="GridViewSMInfo" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID,UserID"
                       CssClass="GridViewStyle" Caption="消费者扫码信息" OnRowDataBound="GridViewSMInfo_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标签号码">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="省份">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMProc" runat="server" Text='<%# Bind("SMProc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="市">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMsj" runat="server" Text='<%# Bind("SMsj") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="县">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMxj" runat="server" Text='<%# Bind("SMxj") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMAddress" runat="server" Text='<%# Bind("SMAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="扫码时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMTime" runat="server" Text='<%# Bind("SMTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="客户">
                              <ItemTemplate>  
                                  <asp:HyperLink ID="hyperlinkuser" runat="server">详细</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate> 
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                </div> 
    <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hfbox" runat="server" />
    </div>
    </form>
        <script src="../../js/jquery-1.7.1.js"></script>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../../include/js/jquery.easyui.min.js"></script>
</body>
</html>
