<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_FaHuoConfirm.aspx.cs" Inherits="Admin_wuliu_TJ_FaHuoConfirm" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.16598, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>发货确认</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1"  EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate><div class="div_WholePage">
        <div class="topdiv">
            <div class="topitem"><span>时间</span></div>
            <div class="topitem"><asp:TextBox class="inputdatenew" runat="server" id="inputsdate"></asp:TextBox><ajaxToolkit:CalendarExtender runat="server" TargetControlID="inputsdate"  
Format="yyyy-MM-dd"/></div>
            <div class="topitem"><span>至</span></div>
            <div class="topitem"><asp:TextBox class="inputdatenew" runat="server" id="inputenddate"></asp:TextBox><ajaxToolkit:CalendarExtender runat="server" TargetControlID="inputenddate" 
Format="yyyy-MM-dd"/></div> 
            <div class="topitem"><span>经销商</span></div>
            <div class="topitem"><input placeholder="点击选择经销商" id="inputagentid" onclick="ReturnAgentSelectScript()" runat="server" class="inputsearch"/><%--<asp:DropDownList runat="server" DataTextField="CompName" DataValueField="CompID" ID="ddlagent" AutoPostBack="True" OnSelectedIndexChanged="ddlagent_SelectedIndexChanged"/>--%></div>
            <div class="topitem"><span>发货批次</span></div>
            <div class="topitem"><asp:DropDownList runat="server" ID="ddlfhpici" DataTextField="FHPiCi" DataValueField="FHID" AutoPostBack="True" OnSelectedIndexChanged="ddlfhpici_SelectedIndexChanged"/></div>
            <div class="topitem"><asp:Button runat="server" ID="btn_go" CssClass="btn btn-warning btnyd" Text="搜索" OnClick="btn_go_Click"/></div>
        </div> 
        <div style="overflow-x: auto;">
            <asp:GridView runat="server" ID="gvfhinfo" CssClass="GridViewStyle" OnRowDataBound="gvfhinfo_RowDataBound">
                 <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
    </div> <input type="hidden" id="go" value="0" runat="server"/>  </ContentTemplate></asp:UpdatePanel> 
       <input id="hd_agentid" type="hidden" value="0" runat="server"/>
    </form>
   <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js"></script>
     <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    <script>
        //window.onload = function() {
        //    setInterval(function() {
        //        if ($("#go").val() == "1") {
        //            $("#btn_go").click();
        //            console.log("go");
        //        }
        //    }, 10000); 
        //}
    </script>
</body>
</html>
