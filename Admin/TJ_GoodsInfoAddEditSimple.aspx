<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_GoodsInfoAddEditSimple.aspx.cs" Inherits="Admin_TJ_GoodsInfoAddEditSimple" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_GoodsInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
    <link rel="stylesheet" href="getpic/css/cropper.css" />
    <link rel="stylesheet" href="getpic/css/cropperindex.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
        </asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>产品名称</th>
                            <td>
                                <input id="inputGoodsName" runat="server" type="text" maxlength="25" class="p75" /></td>
                        </tr>
                        <tr>
                            <th>产品描述</th>
                            <td>
                                <textarea id="TextArea1" runat="server" cols="20" name="S1" class="p75" rows="3"></textarea></td>
                        </tr>
                        <tr>
                            <th>产品图片</th>
                            <td>
                                <%--  <table>
                                    <tr><td><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_GoodPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=3000" style="vertical-align: text-bottom" width="250"></iframe></td></tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_GoodPic" runat="server" Height="100" ImageUrl="~/images/nopic.gif" />
                                        </td> 
                                    </tr>
                                </table>--%>
                                <div class="SeeCont">
                                    <div class="SeeImg">
                                        <img class="myimg" id="showimage" src='' runat="server" />
                                    </div>
                                    <button class="TxText xzBtn" id="imgReplaceBtn" type="button">选取图片</button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>售价</th>
                            <td>
                                <div class="flex f-center">
                                    ￥
                                    <input id="inputPrice" class="date" runat="server" maxlength="20"
                                        onafterpaste="if(isNaN(value))execCommand('undo')"
                                        onkeyup="if(isNaN(value))execCommand('undo')" type="text" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputPrice" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    /
                                    <asp:TextBox ID="txtSaleUnitID" runat="server" CssClass="date"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSaleUnitID" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>

                            </div>
                        </tr>
                        <tr>
                            <th>上架时间</th>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBox_BeginSaleDate" CssClass="date" runat="server"></asp:TextBox>
                                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="TextBox_BeginSaleDate">
                                            </cc2:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox_BeginSaleDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>至</td>
                                        <td>
                                            <asp:TextBox ID="TextBox_EndSaleDate" CssClass="date" runat="server"></asp:TextBox>
                                            <cc2:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="TextBox_EndSaleDate">
                                            </cc2:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox_EndSaleDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>外部链接</th>
                            <td>
                                <asp:TextBox ID="txtOuterLinkURL" runat="server" CssClass="p75" MaxLength="100" placeholder="该链接只有在需要外链的时候使用（可不填）"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <asp:TextBox ID="inputRemarks" runat="server" MaxLength="25" TextMode="SingleLine" CssClass="p75"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />   
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
                    </div>                      
                    <asp:HiddenField ID="HF_CMD" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" /> 
                    <input type="hidden" runat="server" id="savefilepath" /> 
                    <asp:HiddenField ID="HF_ShowInputName" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form> 
    <script type="text/javascript" src="getpic/js/jquery-2.1.0.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>  
    <script type="text/javascript" src="getpic/js/upImg.js"></script>
    <script type="text/javascript">
        upImg(4 / 3);
    </script> 
</body>
</html>
