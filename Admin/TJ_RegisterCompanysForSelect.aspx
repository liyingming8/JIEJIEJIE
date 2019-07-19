<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanysForSelect.aspx.cs" Inherits="Admin_TJ_RegisterCompanysForSelect" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="div_WholePage">
            <div class="topdiv">  
                <div class="topitem"><span> 
                    单位名 :</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /> 
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="false"
                AutoGenerateColumns="False" DataKeyNames="CompID,CompName" 
                OnRowDataBound="GridView1_RowDataBound"
                CssClass="GridViewStyle"  PageSize="18">
                <Columns>  
                    <asp:TemplateField HeaderText="公司名称">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <asp:Label ID="LabelCTID" runat="server" Text='<%# (Eval("Address").ToString().Length>13?Eval("Address").ToString().Substring(0,12)+"...":Eval("Address").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyleSmall" />
                <RowStyle CssClass="GridViewRowStyleSmall" /> 
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />  
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyleSmall" />
                <HeaderStyle CssClass="GridViewHeaderStyleSmall" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server"  CssClass="GridViewPagerStyle"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth="" PageSize="6" ></webdiyer:AspNetPager>
        <asp:HiddenField ID="hf_objcompid" runat="server" />
        <asp:HiddenField ID="hf_f" runat="server" />
        <asp:HiddenField ID="hf_r" runat="server" />
        </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="AspNetPager1" />
                <asp:AsyncPostBackTrigger ControlID="BtnSearch0" />
            </Triggers>
        </asp:UpdatePanel> 
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
       
    </form>
</body>
</html>
