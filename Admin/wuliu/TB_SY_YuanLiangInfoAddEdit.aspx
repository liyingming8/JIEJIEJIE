<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_YuanLiangInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_YuanLiangInfoAddEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_YuanLiangInfo</title>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <div class="editpageback">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                <table class="gridtable">
           
                    <tr><th>原粮名称：</th><td> 
                                          <asp:DropDownList ID="ComboBox_ylID" runat="server" DataTextField="CName" DataValueField="CID">
                                          </asp:DropDownList>

                                     </td></tr>
                    <tr><th>产地</th><td>
                                       <asp:DropDownList ID="ComboBox_CTID" runat="server">
                                       </asp:DropDownList>
                                   </td></tr>
                    <tr><th>供应商</th><td><%--<input id="inputYLGongYingShangID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />--%>
                                        <asp:DropDownList ID="ComboBox_ZID" runat="server" DataTextField="GYSMingCheng" 
                                                      DataValueField="GYSID">
                                        </asp:DropDownList>
                                    </td></tr>
                    <tr><th>采购单号</th><td><input id="inputYLCaiGouDanHao" runat="server" type="text"  /></td></tr>
                    <tr><th>采购时间</th><td>

                                         <asp:TextBox ID="inputYLCaiGouShiJian" runat="server"></asp:TextBox>
                                         <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="inputYLCaiGouShiJian"></cc2:CalendarExtender>
                                     </td></tr>
                    <tr><th>采购数量</th><td><input id="inputYLCaiGouShuLiang" runat="server" type="text" /></td></tr>
                    <tr><th>采购单位</th><td><input id="inputYLCaiGouDanWei" runat="server" type="text" /></td></tr>
                    <tr><th>采购人</th><td><input id="inputYLCaiGouRen" runat="server" type="text" /></td></tr>
                    <tr><th>审核人</th><td><input id="inputYLShenHeRen" runat="server" type="text"  /></td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" /></td></tr>
                </table>　
                       <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加"  CssClass="btn btn-warning btnyd"/></div> 
    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>