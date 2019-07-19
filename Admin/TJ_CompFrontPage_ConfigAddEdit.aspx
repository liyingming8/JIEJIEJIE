<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompFrontPage_ConfigAddEdit.aspx.cs" Inherits="Admin_TJ_CompFrontPage_ConfigAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>TJ_CompFrontPage_Config</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
<%--    <link rel="stylesheet" href="getpic/css/cropper.css" />
    <link rel="stylesheet" href="getpic/css/cropperindex.css" />--%>
    <style type="text/css">
        input[type="radio"] {
            margin: 10px;
        }
        input[type="checkbox"] {
            margin: 10px;
        }
        input[type="button"] {
            padding: 2px 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="editpageback">
                    <div class="toptitle">落地页面配置</div>
                    <table class="gridtable">
                        <tr>
                            <th>布局</th>
                            <td>
                                <asp:RadioButtonList ID="lbl_layout" DataTextField="layoutname" DataValueField="id"  runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="lbl_layout_SelectedIndexChanged">
                                    <asp:ListItem Value="2">大背景</asp:ListItem>
                                </asp:RadioButtonList>
                            </td> 
                        </tr>
                       <tr runat="server" id="dbjpic" style="display: none;">
                            <th></th>
                            <td>
                                  <table style="border-style:none; border-color:white; border-width: 0px; padding: 0px; margin: 0px; table-layout: auto"><tr><td><asp:Image ID="Image_dbjpic" runat="server" Width="120px" ImageUrl="~/Admin/Images/NoPic.gif" /></td></tr><tr><td><input id="btn_big_background" type="button" value="上传大背景图" onclick="openWinCenter('getpic/piccutter.aspx?key=Image_dbjpic&hdsv=HF_Image_dbjpic&bilv=0.653', 680, 680, '上传大背景图')"/></td></tr></table>  </td> 
                        </tr>
                        <tr>
                            <th>主题色</th>
                            <td>
                                <asp:RadioButtonList ID="lbl_theme_color" runat="server" DataTextField="remarks" DataValueField="id" OnDataBound="lbl_theme_color_DataBound" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>扫码客户</th>
                            <td> 
                                <asp:CheckBox ID="ckb_getnicknameandtouxiang" Checked="True" runat="server" AutoPostBack="True" Text="获取头像昵称" />
                                 <asp:CheckBox ID="ckb_saomadiliweizhi" Checked="True" runat="server" Text="获取地理位置" />
                            </td> 
                        </tr>
                        <tr>
                            <th>关注微信</th>
                            <td>
                                <asp:CheckBox ID="ckb_gzwx" runat="server"  Text="需要" />
                            </td> 
                        </tr>
                        <tr id="wxgzhm" runat="server" style="display: none;"><th>公众号名</th><td><input id="inputgongzhonghaomingcheng" runat="server" type="text" class="p60" maxlength="100" placeholder="关注公众号名称" /></td></tr>
                        <tr runat="server" id="wxlogo" style="display: none;">
                            <th>关注链接</th>
                            <td>
                                <textarea id="inputguanzhuqrcodeurl" runat="server" type="text" rows="4" class="p90" maxlength="100" ></textarea></td>  
                        </tr>
                        <tr>
                            <th>LOGO</th>
                            <td>
                                <asp:CheckBox ID="ckb_isshowlogo" runat="server" Text="显示" />
                            </td> 
                        </tr>
                        <tr id="trshowlogo" runat="server" style="display: none;">
                            <th></th>
                            <td>
                                <table style="border-style:none; border-color:white; border-width: 0px; padding: 0px; margin: 0px; table-layout: auto">
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_showlogo" runat="server" ImageUrl="~/Admin/Images/NoPic.gif" Width="120px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input id="Button_uplogo" type="button" value="上传LOGO图"  onclick="openWinCenter('getpic/piccutter.aspx?key=Image_showlogo&hdsv=HF_Image_showlogo&bilv=3.16', 680, 680, '上传LOGO图')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr> 
                        <tr>
                            <th>友情链接</th>
                            <td>
                               <input id="inputgourl" runat="server" type="text" class="p90"/></td> 
                        </tr>
                        <tr>
                            <th>底部栏</th>
                            <td>
                                <asp:CheckBox ID="ckb_show_bottom" runat="server" Text="显示" />
                            </td>
                        </tr>
                        <tr id="bottomrow" runat="server" style="display: none;"><th></th><td><input type="text" id="bottomcontent" placeholder="请输入底部广告语" runat="server" maxlength="30" class="p80"/></td></tr>  
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </div> 
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField runat="server" ID="HF_Image_showlogo"/>
        <asp:HiddenField runat="server" ID="HF_Image_dbjpic"/> 
         <script type="text/javascript" src="getpic/js/jquery-2.1.0.js"></script>
        <script src="../include/js/UploadImage.js"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
        <%--<script type="text/javascript" src="getpic/js/upImgauto.js"></script>  --%>
        <script type="text/javascript">
            function showuploadlogo() { 
                if ($("#ckb_isshowlogo").is(':checked')) {
                    $("#trshowlogo").show(); 
                } else {
                    $("#trshowlogo").hide();
                }
            }
            function guanzhuweixin() {
                if ($("#ckb_gzwx").is(':checked')) {
                    $("#wxgzhm").show();
                    $("#wxlogo").show();
                } else {
                    $("#wxgzhm").hide();
                    $("#wxlogo").hide();
                }
            }
            function showbottomrow() {
                if ($("#ckb_show_bottom").is(':checked')) {
                    $("#bottomrow").show(); 
                } else {
                    $("#bottomrow").hide(); 
                }
            }
        </script>

    </form>
</body>
</html>
