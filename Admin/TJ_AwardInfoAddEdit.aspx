<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfoAddEdit.aspx.cs" Inherits="Admin_TJ_AwardInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_AwardInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <link href="../include/windows.css" rel="stylesheet" />  
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable"> 
                <tr>
                    <th>奖品类型</th>
                    <td>
                        <asp:DropDownList ID="DDL_AwardType" runat="server" DataTextField="awardtype" DataValueField="id">
                        </asp:DropDownList>
                    </td> 
                </tr>
                        <tr>
                            <th>奖品名称</th>
                            <td>
                                <input id="inputAwardThing" class="p80" runat="server" type="text" maxlength="50"  />
                            </td>
                        </tr>
                <tr>
                    <th>市场价格</th>
                    <td>
                        ￥<input id="input_price" type="text" runat="server" class="p10" />
                        <asp:CheckBox ID="ckb_exchangexianjin" Text="允许兑换现金" runat="server" />
                    </td> 
                </tr>
                        <tr>
                            <th>积分额度</th>
                            <td>
                                <input id="txtIntegralValue" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" runat="server" class="p10" type="text" />
                                <asp:CheckBox ID="ckb_exchangebyjifen" Text="可用于积分兑换" runat="server" Checked="True" />
                            </td>
                        </tr>
                <tr>
                    <th>奖品图片</th>
                    <td>
                        <%--<table style="border-collapse: collapse; padding: 0px; margin: 0px; border-style: none"><tr><td><asp:Image ID="Image_AwardUrl" Height="100" runat="server" ImageUrl="~/Admin/Images/NoPic.gif" /></td><td><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                            src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_AwardUrl&amp;TargetHd=HF_ImageUrl&amp;imgMaxSize=200"
                            style="vertical-align: text-bottom" width="250"></iframe></td></tr></table>--%>
                        <table style="border-style:none; border-color:white; border-width: 0px; padding: 0px; margin: 0px; table-layout: auto">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_AwardUrl" runat="server" ImageUrl="~/Admin/Images/NoPic.gif" Width="120px" />
                                        </td>
                                        <td><input id="Button_uplogo" type="button" class="btn" value="上传LOGO图"  onclick="openWinCenter('getpic/piccutter.aspx?key=Image_AwardUrl&hdsv=HF_ImageUrl&bilv=1', 680, 680, '上传奖品图')" /></td>
                                    </tr> 
                                </table>
                    </td>
                </tr> 
                <tr>
                    <th>介绍</th>
                    <td>
                        <textarea id="inputContents" cols="50" name="S1" class="p80" runat="server" rows="3"></textarea></td> 
                </tr>
                <tr>
                    <th>有效</th>
                    <td>
                        <asp:CheckBox ID="CKB_IsActive" runat="server" Checked="True" />
                    </td> 
                </tr>
                        <tr>
                            <th>奖励对象</th>
                            <td>
                                <asp:DropDownList ID="ddl_faceto" runat="server">
                                    <asp:ListItem Selected="True" Value="0">通用</asp:ListItem>
                                    <asp:ListItem Value="1">消费者</asp:ListItem>
                                    <asp:ListItem Value="3">终端店</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" class="p80" runat="server" type="text" maxlength="25"/>
                            </td> 
                        </tr>
            </table>
            <div class="bottomdivbutton">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel> 
            
        </div>
         <asp:HiddenField ID="HF_CMD" runat="server" />
         <asp:HiddenField ID="HF_ID" runat="server" />
         <asp:HiddenField ID="HF_ImageUrl" runat="server" /> 
         <script type="text/javascript" src="../include/js/jquery-2.1.1.min.js"></script> 
         <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    </form> 
</body>
</html>
