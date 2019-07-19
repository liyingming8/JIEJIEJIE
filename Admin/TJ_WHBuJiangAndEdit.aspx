<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_WHBuJiangAndEdit.aspx.cs" Inherits="Admin_TJ_WHBuJiangAndEdit" %>
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
        #Jifen_Text,#inputRemarks{
            width:45%;
        }
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
                            <th>产品名称</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_Product" AppendDataBoundItems="true" DataTextField="Products_Name" DataValueField="Products_Name" runat="server">
                                    <asp:ListItem Text="请指定产品名称" Value="0" Selected="True" ></asp:ListItem>
                                </asp:DropDownList>
                            </td>                                       
                        </tr>     
                          <tr>
                            <th>标签包编码</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_DanHao" AppendDataBoundItems="true" DataTextField="DanHao" DataValueField="DanHao" runat="server">
                                    <asp:ListItem Text="请指定标签包编码" Value="0" Selected="True" ></asp:ListItem>
                                </asp:DropDownList>
                            </td>                                       
                        </tr>      
                        <tr>
                            <th>积分值</th>
                            <td>
                                <input id="Jifen_Text" runat="server" type="text"  placeholder="积分值为不大于100的正整数" />
                            </td>
                        </tr>                                     
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text"  class="input6" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                    </div>
                    <asp:HiddenField ID="HF_CMD" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" />                
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
