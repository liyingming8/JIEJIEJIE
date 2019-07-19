<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_InforAddEditXF.aspx.cs" Inherits="Admin_TB_Agents_InforAddEditXF" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_Agents_Infor</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="updatepanel" runat="server">
                    <ContentTemplate> 
                    <table class="gridtable">
                            <tr>
                                <th>城市</th>
                                <td>
                                    <asp:DropDownList ID="ComboBox_CID" runat="server">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr runat="server" id="address">
                                <th >地址</th>
                                <td >
                                    <input id="inputAgent_Addrss" runat="server" type="text" maxlength="50" class="input6" /></td>
                                
                            </tr>
                            <tr runat="server" id="name">
                                <th>经销商名称</th>
                                <td>
                                    <input id="inputAgent_Name" runat="server" type="text" maxlength="50" class="input6" /></td>
                                
                            </tr>
                            <tr>
                                <th>编码</th>
                                <td>
                                    <input id="inputAgent_Code" runat="server"  type="text" maxlength="30" readonly="readonly" /></td>
                               
                            </tr>
                            <tr  runat="server" id="lxr">
                                <th>联系人</th>
                                <td>
                                    <input id="inputMiddleman" runat="server" type="text" maxlength="10" /></td>
                               
                            </tr>
                            <tr  runat="server" id="number">
                                <th>电话</th>
                                <td>
                                    <input id="inputTelephone" runat="server" type="text" maxlength="20" /></td>
                               
                            </tr>
                            <tr runat="server" id="phone">
                                <th>手机</th>
                                <td>
                                    <input id="inputMobiePhone" runat="server" type="text" maxlength="20" /></td>
                                
                            </tr>
                            <tr  runat="server" id="area">
                                <th>授权区域</th>
                                <td>
                                    <input id="inputAllowAreaInfo" runat="server" type="text" maxlength="100" /></td>
                                
                            </tr>
                            <tr >
                                <th>销售产品</th>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxList_PermitList" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID" RepeatColumns="5">
                                    </asp:CheckBoxList>
                                </td>
                                
                            </tr>
                            <tr runat="server" id="bz">
                                <th>备注</th>
                                <td>
                                    <input id="inputRemarks" runat="server" type="text" maxlength="50" class="input6" /></td> 
                            </tr> 
                            <tr>
                                <th></th>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /><asp:HiddenField ID="hf_file" runat="server" /> 
                                    <asp:Button ID="Button_upload" runat="server"  Height="21px" OnClick="Button_upload_Click" Text=" 上 传 " Width="70px" /> 
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="file"></asp:Label></td>
                            </tr>
                        </table>
                   <div class="bottomdivbutton"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添 加" CssClass="btn btn-warning btnyd" /> </div> 
                    </ContentTemplate>
                    <Triggers >
                        <asp:PostBackTrigger  ControlID="Button_upload"/>                
                    </Triggers>
                </asp:UpdatePanel>
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>