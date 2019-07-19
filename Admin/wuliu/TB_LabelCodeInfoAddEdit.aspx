<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_LabelCodeInfoAddEdit.aspx.cs" Inherits="Admin_TB_LabelCodeInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_LabelCodeInfo</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
      
    </head>
    <body>
        <form id="form1" runat="server"> 
           <div class="editpageback">
                <table class="gridtable">
                    <tr><th>ID</th><td><input id="inputID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>前四位</th><td><input id="inputstartfournum" runat="server" type="text" maxlength="6" /></td></tr>
                    <tr><th>表名</th><td><input id="inputtablenameinfo" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>开始号码</th><td><input id="inputstartvalue" runat="server" type="text" maxlength="6" /></td></tr>
                    <tr><th>结束号码</th><td><input id="inputendvalue" runat="server" type="text" maxlength="6" /></td></tr>
                    <tr><th>套数</th><td><input id="inputtaozongshu" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>箱数</th><td><input id="inputxiangnum" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>瓶数</th><td><input id="inputpingnum" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>偏移量</th><td><input id="inputpianyiliang" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>记录数</th><td><input id="inputrowcout" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="8" /></td></tr>
                    <tr><th>备注</th><td><input id="inputremarks" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>CompID</th><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                </table>
              <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
            </div>
      </div>
        </form>
          <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>