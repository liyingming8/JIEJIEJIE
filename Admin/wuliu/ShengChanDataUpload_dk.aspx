<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShengChanDataUpload_dk.aspx.cs" Inherits="Admin_wuliu_ShengChanDataUpload_dk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" />
        <style type="text/css">
            .auto-style1 { height: 30px; }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
               <div class="div_WholePage"> 
                <div class="div_Nav">
                    <strong>请按以下步骤执行</strong>
                </div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="updatepanel" runat="server" >
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td >
                                    1</td>
                                <td >
                                    选择导入模式</td>
                                <td >
                                </td>
                            </tr>
                            <tr>
                                <td >
                                </td>
                                <td >
                                    <asp:RadioButtonList ID="RadioButtonList_Mode" runat="server" AutoPostBack="True"
                                                         OnSelectedIndexChanged="RadioButtonList_Mode_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">使用盘点机文件</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td >
                                    <asp:CheckBox ID="CheckBox_AllSelect" runat="server" AutoPostBack="True" 
                                                  oncheckedchanged="CheckBox_AllSelect_CheckedChanged" Text="全部指定" Visible="False" />
                                </td>
                            </tr>
                            <%-- <tr>
                <td >
                    2</td>
                <td >
                    生产线信息</td>
                <td >
                </td>
            </tr>
            <tr>
                <td >
                </td>
                <td >
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ComboBox_WorkShop" AppendDataBoundItems="true" runat="server"  DataTextField="Workshop" DataValueField="WSID" AutoPostBack="True" OnSelectedIndexChanged="ComboBox_WorkShop_SelectedIndexChanged">
                                <asp:ListItem Text="厂房（车间）..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>                  
                                <asp:DropDownList ID="ComboBox_workline" runat="server" AppendDataBoundItems="true" DataTextField="WorkLineName" DataValueField="WLID">                                   
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox_SelectWorkLine" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_SelectAgent_CheckedChanged" Text="指定" Checked="True" Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td >
                </td>
            </tr>--%>
                            <tr>
                                <td >
                                    2</td>
                                <td >
                                    入库单号</td>
                                <td >
                                    ;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="TextBox_rukudanhao" runat="server"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                            <td>3</td>
                            <td>产品信息</td>
                            <td></td>
                            <tr>
                                <td ></td>
                                <td >
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ComboBox_ProInfo" runat="server" AppendDataBoundItems="true" DataTextField="Products_Name" DataValueField="Infor_ID">
                                                    <asp:ListItem Selected="True" Text="产品信息..." Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox_SelectProductInfo" runat="server" AutoPostBack="True" Checked="True" Enabled="False" OnCheckedChanged="CheckBox_SelectProductInfo_CheckedChanged" Text="指定" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>货仓信息</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ComboBox_StoreHouseID" runat="server" AppendDataBoundItems="true" DataTextField="StoreHouseName" DataValueField="STID">
                                                    <asp:ListItem Selected="True" Text="货仓..." Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox_SelectStoreHouse" runat="server" AutoPostBack="True" Checked="True" Enabled="False" OnCheckedChanged="CheckBox_SelectStoreHouse_CheckedChanged" Text="指定" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>入库时间</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="TextBox_FaHuoTime" runat="server" Enabled="False"></asp:TextBox>
                                    <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="TextBox_FaHuoTime">
                                    </cc2:CalendarExtender>
                                    <asp:CheckBox ID="CheckBox_EnterFaHuoDate" runat="server" AutoPostBack="True" oncheckedchanged="CheckBox_EnterFaHuoDate_CheckedChanged" Text="指定" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>6</td>
                                <td>上传盘点机文件</td>
                                <td>
                                    <asp:HiddenField ID="HF_MaxMinValue" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="372px" />
                                    <asp:HiddenField ID="hf_file" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="Button_upload" runat="server" Height="21px" OnClick="Button_upload_Click" Text=" 上 传 " Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td>7</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="Button_DataCheck" runat="server" Height="20px" OnClick="Button_DataCheck_Click" Text="数据检查" Visible="False" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 22px;"></td>
                                <td style="height: 22px;">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="height: 22px;">
                                    <asp:Button ID="Button_Insert" runat="server" Enabled="False" Height="20px" OnClick="Button_Insert_Click" Text="确定导入" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px;">
                                    <asp:Label ID="Label_Exceptions" runat="server" Text="异常号码" Visible="False"></asp:Label>
                                </td>
                                <td style="height: 21px;">
                                    <asp:TextBox ID="TextBox_ExceptionLines" runat="server" Height="200px" TextMode="MultiLine" Visible="False" Width="500px"></asp:TextBox>
                                </td>
                                <td style="height: 21px;"></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers >
                        <asp:PostBackTrigger  ControlID="Button_upload"/>                
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </form>
    </body>
</html>