<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfoAddEditjg.aspx.cs" Inherits="Admin_TJ_AwardInfoAddEditjg" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_AwardInfo</title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="../images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> 
                                                         活动奖品编辑</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_AwardInfo.aspx"><img title="返回" src="../images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                </asp:UpdatePanel>
	
                <table>
                    <tr><td>积分活动</td><td> 
                                            <asp:DropDownList runat="server" ID="ddl_agid" DataTextField="IntegralName" DataValueField="ITGRID" DataMember=""/>
                                     </td><td></td></tr>
                    <tr><td>奖品</td><td><input id="inputAwardThing" runat="server" type="text" 
                                              maxlength="50" class="input6" /></td><td>
                                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                                                                                   ControlToValidate="inputAwardThing" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                   </td></tr>
                    <tr><td>兑换积分</td><td>
                                         <input id="txtIntegralValue" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')"  runat="server" type="text" class="input5" /></td><td> 
                                                                                                                                                                                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                                                                                                                                                                                                         ControlToValidate="txtIntegralValue" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                                                                                                                         </td></tr>
                    <tr><td>奖品图片</td><td>
                                         <asp:Image ID="Image_AwardUrl" runat="server" />
                                     </td><td> <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" 
                                                       src="../Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_AwardUrl&amp;TargetHd=HF_ImageUrl&amp;imgMaxSize=200" 
                                                       style="vertical-align: text-bottom" width="250"></iframe></td></tr>
                    <tr><td>介绍</td><td>
                                       <textarea id="inputContents" cols="50" name="S1" runat="server" rows="5"></textarea></td><td>
                                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                                                                                                                ControlToValidate="inputContents" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                                </td></tr>
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="25" 
                                              class="input6" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <asp:HiddenField ID="HF_ImageUrl" runat="server" /> 
            </div>
        </form>
    </body>
</html>