<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Comp_ZhengShuAddEdit.aspx.cs" Inherits="Admin_TJ_Comp_ZhengShuAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_Comp_ZhengShu</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" /> 
    <link rel="stylesheet" href="getpic/css/cropper.css" />
    <link rel="stylesheet" href="getpic/css/cropperindex.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="editpageback">
                    <div class="toptitle">资质证书</div>
                    <table class="gridtable">
                        <tr>
                            <th>证书名称</th>
                            <td>
                                <input id="inputzsnm" runat="server" class="p80" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>照片</th>
                            <td>
                              <%--  <table style="border: none;">
                                    <tr>
                                        <td>
                                            <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_ZSPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=3000" style="vertical-align: text-bottom" width="250"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_ZSPic" runat="server" Height="100" ImageUrl="~/images/nopic.gif" /></td>
                                    </tr>
                                </table>--%>
                                    <div class="SeeCont">
                                    <div class="SeeImg">
                                        <img class="myimg" id="showimage" src=''  runat="server"/>
                                    </div>
                                    <button class="TxText xzBtn" id="imgReplaceBtn" type="button">选取图片</button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>显示</th>
                            <td>
                                <asp:CheckBox ID="ckb_isshow" runat="server" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <input type="hidden" runat="server" id="savefilepath" /> 
        <script type="text/javascript" src="getpic/js/jquery-2.1.0.js"></script>
        <script src="../include/js/UploadImage.js"></script>  
    <script type="text/javascript" src="getpic/js/upImg.js"></script>
    <script type="text/javascript">
        upImg(4 / 3);
    </script> 
    </form>
</body>
</html>
