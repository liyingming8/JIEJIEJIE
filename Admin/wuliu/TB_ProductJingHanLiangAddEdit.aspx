﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_ProductJingHanLiangAddEdit.aspx.cs" Inherits="Admin_TB_ProductJingHanLiangAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_ProductJingHanLiang</title>
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
                    <tr><th>净含量</th><td><input id="inputJingHanLiang" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td></tr>
                </table>
              <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                  <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
              </div><asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
                       </ContentTemplate>
  </asp:UpdatePanel> 
            </div>
        </form>
          <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>