<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_PublishInfoAddEdit.aspx.cs" Inherits="Admin_TJ_PublishInfoAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_PublishInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
    <link rel="stylesheet" href="getpic/css/cropper.css" />
    <link rel="stylesheet" href="getpic/css/cropperindex.css" />
</head>
<body>
    <form id="NewQyq" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>咨询类别</th>
                            <td>
                                <asp:DropDownList ID="ddl_infotype" DataTextField="TypeName" AppendDataBoundItems="True" DataValueField="IFTypeID" runat="server">
                                    <asp:ListItem Text="资讯类别..." Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>主题
                            </th>
                            <td>
                                <input id="inputTitle" runat="server" type="text" maxlength="50" class="p90" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputTitle" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>图片 </th>
                            <td><%--<table style="border: none;"><tr><td><iframe id="I1" runat="server" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=3000" style="vertical-align: text-bottom" width="250"></iframe></td></tr><tr><td><asp:Image ID="Image_Logo" runat="server" ImageUrl="~/images/nopic.gif" Width="100" /></td></tr></table> --%>
                                <div class="SeeCont">
                                    <div class="SeeImg">
                                        <img class="myimg" id="showimage" src=''  runat="server"/>
                                    </div>
                                    <button class="TxText xzBtn" id="imgReplaceBtn" type="button">选取图片</button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>内容
                            </th>
                            <td>
                                <textarea id="TextArea1" runat="server" class="p90" name="S1" rows="5"></textarea></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" class="p90" maxlength="250"
                                    type="text" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                         <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="删除" CssClass="btn btn-warning btnyd"
                            OnClientClick="javascript:return confirm('确定删除吗?');"></asp:Button>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" /> 
        <input type="hidden" runat="server" id="savefilepath" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script type="text/javascript" src="getpic/js/jquery-2.1.0.js"></script>
    <script type="text/javascript" src="getpic/js/upImg.js"></script>
    <script type="text/javascript">
        upImg(2 / 1);
    </script> 
</body>
</html>
