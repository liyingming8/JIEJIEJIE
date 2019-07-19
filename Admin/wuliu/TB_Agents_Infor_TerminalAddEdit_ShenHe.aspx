<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_Infor_TerminalAddEdit_ShenHe.aspx.cs" Inherits="Admin_TB_Agents_Infor_TerminalAddEdit_ShenHe" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_Agents_Infor</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/windows.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" />
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
                            <th>父级代理商</th>
                            <td><input id="inputparentid" runat="server" class="p80" readonly="readonly"/>
                                </td>
                        </tr>
                        <tr>
                            <th>终端名称</th>
                            <td>
                                <input id="inputAgent_Name" runat="server" class="p80" type="text" maxlength="50" />
                            </td>
                        </tr>
                        <tr>
                            <th>城市</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CID" runat="server" MaxHeight="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>详细地址</th>
                            <td>
                                <input id="inputAgent_Addrss" runat="server" type="text" class="p80" maxlength="50" />
                            </td>
                        </tr>
                        <tr>
                            <th>GPS地址</th>
                            <td>
                                <asp:Label id="lab_gps_address" runat="server" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <th>联系人</th>
                            <td>
                                <input id="inputMiddleman" runat="server" type="text" maxlength="10" /></td>
                        </tr> 
                        <tr>
                            <th>类型</th>
                            <td>
                                <asp:DropDownList ID="ddl_comptype" runat="server">
                                    <asp:ListItem Text="终端店" Value="486" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <th>城市经理</th>
                            <td>
                                <input id="inputcitymanager" runat="server" class="p80" readonly="readonly"/>
                              <%--  <asp:DropDownList ID="ddl_city_managerid" runat="server" DataTextField="LoginName" DataValueField="UserID">
                                </asp:DropDownList>--%>
                            </td>
                        </tr> 
                         <tr>
                             <th>编码</th>
                             <td>
                                 <input id="inputAgent_Code" disabled="True" runat="server" type="text" maxlength="30" />
                             </td>
                        </tr>
                         <tr>
                            <th>手机</th>
                            <td>
                                <input id="inputMobiePhone" runat="server" type="text" maxlength="20" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" onafterpaste="this.value=this.value.replace(/[^0-9]/g,'')" /></td>
                        </tr> 
                        <tr>
                            <th>店面门头</th>
                            <td>
                                <asp:Image ID="Image_yyzz" runat="server" ImageUrl="~/images/nopic.gif" Width="200px" />
                            </td>
                        </tr> 
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添 加" CssClass="btn btn-warning btnyd" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <input type="hidden" runat="server" id="hdagentid"/> 
        <input type="hidden" runat="server" id="hd_input_parentname"/>
        <input type="hidden" runat="server" id="hdcitymnid"/>
        <input type="hidden" runat="server" id="hdcitymnnm"/>
        <asp:HiddenField ID="HF_CMD" Value="edit" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" /> 
    </form>
    <script type="text/javascript" src="../../include/js/jquery.min.js"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
