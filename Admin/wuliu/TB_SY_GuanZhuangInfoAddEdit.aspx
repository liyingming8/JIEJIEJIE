<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_GuanZhuangInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_GuanZhuangInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_GuanZhuangInfo</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
           <div class="editpageback">
                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate> 
                <table class="gridtable">
                    <tr><th>灌装ID</th><td><input id="inputGuanZhuangID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>勾调ID</th><td><input id="inputGouTiaoID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
                    <tr><th>大洋酒PICI</th><td><input id="inputDaYangJiuPICI" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>GuanZhuangCheJian</th><td><input id="inputGuanZhuangCheJian" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>GuanZhuangChanXian</th><td><input id="inputGuanZhuangChanXian" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>GuanZhuangBanZu</th><td><input id="inputGuanZhuangBanZu" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>ShengChanPICI</th><td><input id="inputShengChanPICI" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>GuanZhuangShiJian</th><td><input id="inputGuanZhuangShiJian" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>GuanZhuangXianZhiJianYuan</th><td><input id="inputGuanZhuangXianZhiJianYuan" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>GuanZhuangXianZhiJianShiJian</th><td><input id="inputGuanZhuangXianZhiJianShiJian" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>Remarks1</th><td><input id="inputRemarks1" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>Remarks2</th><td><input id="inputRemarks2" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>Remarks3</th><td><input id="inputRemarks3" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>Remarks4</th><td><input id="inputRemarks4" runat="server" type="text" maxlength="15" /></td></tr>
                </table>
                    <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/></div> 
　            </ContentTemplate>
            </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>