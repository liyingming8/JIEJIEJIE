<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_InforAddEditjg.aspx.cs" Inherits="Admin_TB_Products_InforAddEditjg" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_Products_Infor</title>
        <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="div_WholePage"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <table>
                    <tr><td>产品类别</td><td>
                                         <asp:DropDownList ID="ComboBox_TypeID" runat="server">
                                         </asp:DropDownList>
                                     </td><td></td></tr>
                    <tr><td>产品规格</td><td>
                                         <asp:DropDownList ID="ComboBox_PSID" runat="server">
                                         </asp:DropDownList>
                                     </td><td></td></tr>
                    <tr><td>产品编码</td><td><input id="inputProduct_Code" runat="server" type="text" maxlength="30"  /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputProduct_Code" ErrorMessage="请输入产品编码！"></asp:RequiredFieldValidator>
                                     </td><td></td></tr>
                    <tr><td>产品名称</td><td><input id="inputProducts_Name" runat="server" type="text" class="input6" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="inputProducts_Name" ErrorMessage="请输入产品名称！"></asp:RequiredFieldValidator>
                                     </td><td></td></tr>
                    <tr><td>香型</td><td>
                                       <asp:DropDownList ID="DropDownList_XiangXing" AppendDataBoundItems="true" DataTextField="XiangXing" DataValueField="ID" runat="server">
                                           <asp:ListItem Text="香型..." Selected="True" Value="0"></asp:ListItem>
                                       </asp:DropDownList>
                                   </td><td></td></tr>
                    <tr><td>酒精度</td><td>
                                        <asp:DropDownList ID="DropDownList_JiuJingDu" DataValueField="ID" DataTextField="JiuJingDuShu" AppendDataBoundItems="true" runat="server">
                                            <asp:ListItem Text="酒精度..." Selected="True" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td></tr>
                    <tr><td>净含量</td><td>
                                        <asp:DropDownList ID="DropDownList_JingHanLiang" DataTextField="JingHanLiang" AppendDataBoundItems="true" DataValueField="ID" runat="server">
                                            <asp:ListItem Text="净含量..." Selected="True" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td></tr>
                    <tr><td>原材料</td><td>
                                        <asp:DropDownList ID="DropDownList_YuanLiao" DataTextField="MTname" AppendDataBoundItems="true" DataValueField="mtID" runat="server">
                                            <asp:ListItem Text="原材料..." Selected="True" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td></tr>
                    <tr><td>标准</td><td>
                                       <asp:DropDownList ID="DropDownList_BiaoZhun" DataTextField="BiaoZhunname" AppendDataBoundItems="true" DataValueField="QID" runat="server">
                                           <asp:ListItem Text="标准..." Selected="True" Value="0"></asp:ListItem>
                                       </asp:DropDownList>
                                   </td><td></td></tr>
                    <tr><td>自主品牌</td><td>
                                         <asp:CheckBox ID="CheckBox_IsOwn" runat="server" />
                                     </td><td></td></tr>
                    <tr><td>产品简介</td><td><asp:TextBox ID="txtProducts_Summary" runat="server" CssClass="input6" MaxLength="25" Height="35px" TextMode="MultiLine"></asp:TextBox></td><td></td></tr>
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="25" class="input6" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
         <script src="../../../include/js/UploadImage.js" type="text/javascript"></script> 
    </body>
</html>