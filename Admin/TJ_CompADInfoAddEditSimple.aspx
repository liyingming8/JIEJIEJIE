<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompADInfoAddEditSimple.aspx.cs" Inherits="Admin_TJ_CompADInfoAddEditSimple" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_CompADInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
    <link rel="stylesheet" href="getpic/css/cropper.css" />
    <link rel="stylesheet" href="getpic/css/cropperindex.css" />
    <style type="text/css">
        input[type='radio'] {
            margin: 8px 5px !important;
        }
    </style> 
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
                            <th></th>
                            <td>
                                    <div class="SeeCont">
                                    <div class="SeeImg">
                                        <img class="myimg" id="showimage" src=''  runat="server"/>
                                    </div>
                                    <button class="TxText xzBtn" id="imgReplaceBtn" type="button">选取图片</button>
                                </div>
                            </td>
                        </tr> 
                        <tr>
                            <th>产品</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_GoodsID" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>外链</th>
                            <td>
                                <input id="inputSpecialURLLink" placeholder="另外跳转到新的网址（可不填）" runat="server" type="text" maxlength="50" class="p80" />
                            </td> 
                        </tr>
                        <tr>
                            <th>说明</th>
                            <td>
                                <input id="inputDiscriptions" placeholder="简单说明（可不填）" runat="server" type="text" maxlength="50" class="p80" />
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" placeholder="简单备注（可以不填）" type="text" maxlength="25" class="p80" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
         <input type="hidden" runat="server" id="savefilepath" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <input type="hidden" runat="server" value="1 / 1" id="hdbilv"/>
        <input type="hidden" runat="server" id="hdadid"/>
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script type="text/javascript" src="getpic/js/jquery-2.1.0.js"></script>
    <script type="text/javascript" src="getpic/js/upImg.js"></script>
    <script type="text/javascript"> 
        window.onload = function () {
            upImg(<%=Bilv%>);
        } 
    </script> 
</body>
</html>
