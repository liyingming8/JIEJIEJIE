<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_InforAddEdit.aspx.cs"
    Inherits="Admin_TB_Products_InforAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_Products_Infor</title>
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
                     <th>产品图片</th>
                    <td>
                          <table style="border-collapse: collapse;">
                            <tr>
                                <td>
                                    <asp:Image ID="Image_GoodPic" runat="server" Height="100px" ImageUrl="~/Admin/Images/NoPic.gif" /></td>
                                <td>
                                    <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=adfile&amp;TargetImg=Image_GoodPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=102400"
                                        style="vertical-align: text-bottom" width="250"></iframe>
                                </td>
                                <td>宽：500px 高：293px</td>
                            </tr>
                        </table></td>
                </tr>
                <tr>
                     <th>产品简介</th>
                    <td>
                        <asp:TextBox ID="txtProducts_Summary" runat="server" CssClass="p80" MaxLength="25"
                            Height="35px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <th>自主品牌
                    </th>
                    <td>
                        <asp:CheckBox ID="CheckBox_IsOwn" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>产品类别
                    </th>
                    <td>
                        <asp:DropDownList ID="ComboBox_TypeID" runat="server">
                        </asp:DropDownList>
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
                    <th>香型
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList_XiangXing" AppendDataBoundItems="true" DataTextField="XiangXing"
                            DataValueField="ID" runat="server">
                            <asp:ListItem Text="香型..." Selected="True" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                     <th>酒精度
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList_JiuJingDu" DataValueField="ID" DataTextField="JiuJingDuShu"
                            AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="酒精度..." Selected="True" Value="0"></asp:ListItem>
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
              <%--  <tr>
                     <th>原材料
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList_YuanLiao" DataTextField="MTname" AppendDataBoundItems="true"
                            DataValueField="mtID" runat="server">
                            <asp:ListItem Text="原材料..." Selected="True" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>标准
                    </th>
                    <td>
                        <asp:DropDownList ID="DropDownList_BiaoZhun" DataTextField="BiaoZhunname" AppendDataBoundItems="true"
                            DataValueField="QID" runat="server">
                            <asp:ListItem Text="标准..." Selected="True" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                </tr> --%>
                <tr id="CpPrice" runat="server" style="display: none">
                    <th>产品价格
                    </th>
                    <td>
                        <input id="inputProducts_Price" runat="server" type="text" class="length2" />
                    </td>
                </tr>
            </table>
            <div class="bottomdivbutton">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" /></div>
           
        </div>
         <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ImageURL" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
