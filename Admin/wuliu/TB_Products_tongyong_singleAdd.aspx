<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_tongyong_singleAdd.aspx.cs"
    Inherits="Admin_TB_Products_tongyong_singleAdd" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增产品</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/windows.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>产品编码
                            </th>
                            <td>
                                <input id="inputProduct_Code" runat="server" type="text" maxlength="30" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputProduct_Code"
                                    ErrorMessage="请输入产品编码！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>产品名称
                            </th>
                            <td>
                                <input id="inputProducts_Name" runat="server" type="text" class="p80" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputProducts_Name"
                                    ErrorMessage="请输入产品名称！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>产品规格</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_PSID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>净含量
                            </th>
                            <td>
                                <asp:DropDownList ID="DropDownList_JingHanLiang" DataTextField="JingHanLiang" AppendDataBoundItems="true"
                                    DataValueField="ID" runat="server">
                                    <asp:ListItem Text="净含量..." Selected="True" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>产品简介
                            </th>
                            <td>
                                <textarea id="txtProducts_Summary" runat="server" cols="20"  class="p80" rows="3"></textarea>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>  
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ImageURL" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField runat="server" ID="HF_From"/>
    </form>
    <script type="text/javascript" src="../../include/js/jquery-1.7.1.js"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
