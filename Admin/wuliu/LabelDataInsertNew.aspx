<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelDataInsertNew.aspx.cs" Inherits="Admin_wuliu_LabelDataInsertNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="../../include/windows.css" rel="stylesheet" />
     
    <link href="../../include/easyui.css" rel="stylesheet" /> 
    <style type="text/css">
        .auto-style1 {
            height: 38px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <div >
                <strong class="strongtitle" >请按以下步骤执行</strong>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>1</th>
                            <td>上传标签数文件</td>
                           
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="372px" /> 
                             
                                <asp:Button ID="Button_upload" runat="server" Text="上 传" Height="25px"
                                    OnClick="Button_upload_Click" Width="70px" /></td>
                        </tr>
                        <tr>
                            <th  >2</th>
                            <td  >
                                <asp:Label ID="Label1" runat="server"  Width="372px"></asp:Label> 
                          
                                <asp:Button ID="Button_DataCheck" runat="server" Text="数据检查" Height="25px" Enabled="False" OnClick="Button_DataCheck_Click" Width="70px"></asp:Button></td>
                        </tr>
                        <tr>
                            <th>3</th>
                            <td>
                                <asp:Label ID="Label2" runat="server"  Width="372px"></asp:Label> 
                    
                                <asp:Button ID="Button_Insert" runat="server" Enabled="False" Height="25px" OnClick="Button_Insert_Click"
                                    Text="数据导入" Width="70px" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button_upload" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
