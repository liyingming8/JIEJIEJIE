<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ProductSettingAndEdited.aspx.cs" Inherits="Admin_TJ_ProductSettingAndEdited" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_DeployJiangXiangInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
     <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    <style type="text/css">
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button1" />
                </Triggers>
                <ContentTemplate>
                    <table class="gridtable">
                           <tr>
                            <th>产品编码</th>
                            <td>
                                <input id="Product_Code" runat="server" type="text" maxlength="100"  placeholder="编码为4-8位数字" />
                            </td>
                        </tr>       
                         <tr>
                            <th>产品名称</th>
                            <td>
                                <input id="Product_Name" runat="server" type="text" maxlength="100"   placeholder="名称不超过25位"/>
                            </td>
                        </tr>                                              
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                    </div>
                    <asp:HiddenField ID="HF_CMD" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" />  
                    <asp:HiddenField ID="ProductCode" runat="server" />
                    <asp:HiddenField ID="ProductsName" runat="server" />             
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
   
    <script type="text/javascript">
        function closemyWindow() {
            
        }
    </script>
</body>
</html>

