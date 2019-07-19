<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiQuAndGainAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanZhiQuAndGainAddEdit" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>TB_SuYuanZhiQuAndGain</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
　　<link href="../include/windows.css" rel="stylesheet" /> 
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
  <div class="editpageback">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
         <table class="gridtable">
             <tr><th>制曲批次</th><td><asp:Label runat="server" ID="labelzhiqupic"></asp:Label></td></tr> 
            <tr><th>原粮类别</th><td>
                <asp:DropDownList ID="ComboBoxYLID" runat="server" AutoPostBack="True" OnComboBoxChanged="ComboBoxYLID_ComboBoxChanged">
                </asp:DropDownList>
                </td></tr>
            <tr><th>入库批次</th><td>
                <asp:DropDownList ID="ComboBoxYLPC" runat="server" DataTextField="PiCI" DataValueField="ID">
                </asp:DropDownList>
                </td></tr> 
            <tr><th>所占比例</th><td><input id="inputPercentValue" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" style="display:inline-block"  />%</td></tr>
        </table>
      <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
    </ContentTemplate>
    </asp:UpdatePanel> 
   </div>
    <asp:HiddenField ID="HF_CMD" runat="server" />
     <asp:HiddenField ID="HF_ID" runat="server" />
     <asp:HiddenField runat="server" ID="HF_ZQID"/>
      <asp:HiddenField ID="HF_ZQPC" runat="server" />
</form>
     <script src="../include/js/jquery-1.7.1.js"></script>
   
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
