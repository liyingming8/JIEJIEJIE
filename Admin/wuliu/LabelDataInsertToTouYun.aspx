<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelDataInsertToTouYun.aspx.cs" Inherits="Admin_wuliu_LabelDataInsertToTouYun" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>  
        <link href="../../include/MasterPage.css" rel="stylesheet" />  
    </head>
    <body>
        <form id="form1" runat="server">
           <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem"><span><strong>请按以下步骤执行</strong></span></div> 
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="padding: 5px; border: 1px solid #C0C0C0; line-height: 35px; border-collapse: collapse; width: 60%; margin-top:10px;">
                            <tr><td style="width: 40px;text-align: center;">1</td><td style="text-align: left;">指定数据类型</td>
                                <td style="text-align: left;"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:RadioButtonList ID="RBL_DataType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBL_DataType_SelectedIndexChanged" CellPadding="8" CellSpacing="8">
                                        <asp:ListItem Selected="True" Value="1">物流关系</asp:ListItem>
                                        <asp:ListItem Value="30">活动物流对应关系</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                            <tr id="rowtoinput" runat="server">
                                <td></td>
                                <td>
                                    <asp:RadioButtonList ID="RBL_CodeMode" runat="server" RepeatDirection="Horizontal" CellPadding="8" CellSpacing="8">
                                        <asp:ListItem Value="1" Selected="True">1-6</asp:ListItem>
                                        <asp:ListItem Value="2">2-6</asp:ListItem>
                                        <asp:ListItem Value="3">2-6-6</asp:ListItem>
                                        <asp:ListItem Value="4">2-4</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 40px;text-align: center;">2</td>
                                <td style="text-align: left;">上传标签数文件</td>
                                <td style="text-align: left;"></td>
                            </tr>
                            <tr><td ></td><td ><asp:FileUpload ID="FileUpload1" runat="server" CssClass="fileupload" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_upload" runat="server" CssClass="btn btn-info btnyd" OnClick="Button_upload_Click" Text="上 传" />
                                </td>
                            </tr><tr><td style="width: 40px;text-align: center;">3</td><td >
                                  <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Button ID="Button_DataCheck" runat="server" CssClass="btn btn-info btnyd" Enabled="False" OnClick="Button_DataCheck_Click" Text="数据检查" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40px;text-align: center;" >
                                    4</td>
                                <td >
                                    <asp:Label ID="Label2" runat="server"></asp:Label></td>
                                <td>
                                    <asp:Button ID="Button_Insert" runat="server" CssClass="btn btn-warning btnyd" Enabled="False" OnClick="Button_Insert_Click" Text="数据传输" />
                                </td>
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