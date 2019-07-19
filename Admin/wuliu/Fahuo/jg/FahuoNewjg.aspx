<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FahuoNewjg.aspx.cs" Inherits="Admin_wuliu_Fahuo_FahuoNewjg" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="../uploadify/jquery.uploadify.min.js"></script>
        <link rel="stylesheet" type="text/css" href="../uploadify/uploadify.css" />

        <title></title>
        <link href="../../../../include/MasterPage.css" rel="stylesheet" />
        <script type="text/javascript">
            window.onload = xtcx;

            function xtcx() {

                window.history.go(1);
                //bgheight = $(".BigBg").height(),
                var
                    whght = $(window).height(),
                    wwd = $(window).width(),
                    srwd = $("#Srid").outerWidth(),
                    srhg = $("#Srid").outerHeight(),
                    Pleft = (wwd - srwd) / 2 + "px",
                    Ptop = (whght - srhg) / 2 + "px";


                $("#Zwmark").width(wwd);
                $("#Srid").css({ "left": Pleft, "top": Ptop });
                $("#Zwmark").height(whght);

            }

            function divBlock() {
                $("#Srid").css("display", "block");
                $("#Zwmark").css("display", "block");

            }

            function divNone() {
                $("#Srid").css({ "display": "none" });
                $("#Zwmark").css({ "display": "none" });
                alert('操作完毕!')
            }

            $(function() {

                $('#file_upload').uploadify({
                    'auto': false,
                    'swf': 'uploadify/uploadify.swf?var=' + (new Date()).getTime(),
                    'uploader': 'Fahuo.aspx',
                    'checkExisting': 'uploadify/check-exists.php',
                    'fileTypeDesc': '文本文件',
                    'fileTypeExts': '*.txt',
                    'onCancel': function(file) {
                        alert('文件 ' + file.name + ' 已删除')
                    },
                    'onUploadSuccess': function(file, data, response) {
                        $("#imgBox").html(data);

                    },
                    ' onUploadError': function(file, errorCode, errorMsg, errorString) {
                        alert('The file ' + file.name + ' could not be uploaded: ' + errorString);
                    }

                    // Your options here
                });


            });
        </script>
        <style type="text/css"> 
            #Zwmark {
                background-color: rgba(0, 0, 0, .8);
                position: absolute;
                left: 0;
                display: none;
                top: 0;
                z-index: 50;
            }

            #Srid {
                position: fixed;
                z-index: 300;
                display: none;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="div_WholePage">
                <div style="background-color: white; border-radius: 6px; border: 1px solid gray; padding: 5px 5px;">
                    <strong>请按以下步骤执行</strong>
           
                </div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="updatepanel" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>1</td>
                                <td>选择发货模式</td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td class="auto-style8"></td>
                                <td class="auto-style8">
                                    <asp:RadioButtonList ID="RadioButtonList_Mode" runat="server" AutoPostBack="false"
                                                         OnSelectedIndexChanged="RadioButtonList_Mode_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">使用盘点机文件</asp:ListItem>
                                        <%--<asp:ListItem Value="2">使用号码段发货</asp:ListItem>--%>
                                        <asp:ListItem Value="3">退货处理</asp:ListItem>
                                        <asp:ListItem Value="4">转库</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td class="auto-style9">
                                    <asp:CheckBox ID="CheckBox_AllSelect" runat="server" AutoPostBack="True"
                                                  OnCheckedChanged="CheckBox_AllSelect_CheckedChanged" Text="全部指定" />
                                </td>
                            </tr>
                        
                            <tr>

                                <td>2</td>
                                <td>上传盘点机文件</td>

                                <td ></td>
                            </tr>
                          
                            <tr>
                                <td></td>
                                <td>
                                    <input type="file" name="file_upload" id="file_upload" />
                                </td>
                                <td >
                                    <input  type="button"  id="Button_upload1" style="height: 21px; width: 70px" value="上传" onclick=" $('#file_upload').uploadify('upload', '*') "  />

                                    <%-- <asp:Button ID="Button_upload" runat="server" Height="21px"   OnClientClick="javascript:$('#file_upload').uploadify('upload','*')" Text=" 上 传 " Width="70px" />  --%>
                                </td>
                            </tr>

                            <tr>
                                <td>3</td>
                                <td>指定出库单号</td>
                                <td ></td>
                            </tr> 

                            <tr>
                                <td></td>
                                <td  style="color: #FF0000">
                                    <input runat="server" id="TextBox_ChuKuDanHao"  type="text"   />                        
                                    默认为系统时间（可不填写）</td>
                        
                                <td >    
                                    <asp:Button ID="Button_DataCheck" runat="server" Height="20px" OnClick="Button_DataCheck_Click" Text="数据检查" Width="70px" />
                                </td>
                            </tr>
                            <tr>

                                <td></td>
                                <td></td>

                                <td  >




                                </td>
                            </tr>



                            <tr>
                                <td>3</td>
                                <td>代理商或目的地信息</td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ComboBox_DaiLiShangID" AppendDataBoundItems="true" runat="server"  DataTextField="CompName" DataValueField="CompID" Enabled="False" Width="345px">
                                                    <asp:ListItem Text="代理商..." Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox_SelectAgent" runat="server" Text="指定代理商" AutoPostBack="True" OnCheckedChanged="CheckBox_SelectAgent_CheckedChanged" /></td>
                                            <td>
                                                <asp:HyperLink ID="HyperLink_AddAgent" runat="server"
                                                               NavigateUrl="~/Admin/wuliu/TB_Agents_Infor.aspx">添加代理商</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>产品信息</td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ComboBox_ProInfo" AppendDataBoundItems="true" runat="server"  DataTextField="Products_Name" DataValueField="Infor_ID" Enabled="False" Width="345px">
                                                    <asp:ListItem Text="产品信息..." Value="0" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox_SelectProductInfo" runat="server" Text="指定产品信息" AutoPostBack="True" OnCheckedChanged="CheckBox_SelectProductInfo_CheckedChanged" /></td>
                                            <td>
                                                <asp:HyperLink
                                                    ID="HyperLink_AddProdinfo" runat="server"
                                                    NavigateUrl="~/Admin/wuliu/TB_Products_Infor.aspx">添加产品</asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>货仓信息</td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ComboBox_StoreHouseID" AppendDataBoundItems="true" runat="server" DataTextField="StoreHouseName" DataValueField="STID" Enabled="False" Width="345px">
                                                    <asp:ListItem Text="货仓..." Value="2" Selected="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckBox_SelectStoreHouse" runat="server" AutoPostBack="True"
                                                              OnCheckedChanged="CheckBox_SelectStoreHouse_CheckedChanged" Text="指定货仓" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td class="auto-style6">6</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="TextBox_FaHuoTime" runat="server" Enabled="False"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_FaHuoTime" Format="yyyy-MM-dd"></cc2:CalendarExtender>
                                    <asp:CheckBox ID="CheckBox_EnterFaHuoDate" runat="server" Text="指定发货时间"
                                                  AutoPostBack="True" OnCheckedChanged="CheckBox_EnterFaHuoDate_CheckedChanged" />

                                </td>
                                <td class="auto-style7">
                                    <asp:HiddenField ID="HFMaxMinValue" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>7</td>
                                <%--<td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>--%>
                                <td></td>
                                <td >

                                
                                    <asp:Button ID="Button_Insert" runat="server" Enabled="False" Height="20px" OnClick="Button_Insert_Click" Text="确定发货" Width="70px" />
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="清空缓存" />

                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:HiddenField ID="hf_file"  runat="server" />
                                    <p>
                                        <%--<input type="file" name="file_upload" id="file_upload" />--%>
                                    </p>
                                </td>
                                <td >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                <td rowspan="2" >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 22px"></td>
                                <td style="height: 22px" valign="middle">
                                    <asp:Label ID="Label_StarNum" runat="server" Text="开始号码:" Visible="False"></asp:Label>
                                    <asp:TextBox ID="TextBox_StarNum" runat="server" Font-Size="15px" Visible="False"
                                                 Width="120px"></asp:TextBox>
                                    <asp:Label ID="Label_EndNum" runat="server" Text="结束号码:" Visible="False"></asp:Label>
                                    <asp:TextBox ID="TextBox_EndNum" runat="server" Font-Size="15px" Visible="False"
                                                 Width="120px"></asp:TextBox>
                                    <asp:DropDownList ID="DropDownList_BiLiGuanXi" runat="server" Visible="False">
                                        <asp:ListItem Value="0">请选择瓶箱比例关系</asp:ListItem>
                                        <asp:ListItem Value="1">1托1</asp:ListItem>
                                        <asp:ListItem Value="2">1托2</asp:ListItem>
                                        <asp:ListItem Value="3">1托3</asp:ListItem>
                                        <asp:ListItem Value="4">1托4</asp:ListItem>
                                        <asp:ListItem Value="6">1托6</asp:ListItem>
                                        <asp:ListItem Value="12">1托12</asp:ListItem>
                                        <asp:ListItem Value="20">1托20</asp:ListItem>
                                        <asp:ListItem Value="24">1托24</asp:ListItem>
                                        <asp:ListItem Value="40">1托40</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="Checkbox_Enforced" runat="server" Text="强制发货"
                                                  Visible="False" />
                                    <asp:CheckBox ID="CheckBox_CodeSpanTuiHuo" runat="server" Text="号码段退货"
                                                  Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 22px;"></td>
                                <td style="height: 22px;">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
                                <td >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label_Exceptions" runat="server" Text="异常号码" Visible="False"></asp:Label></td>
                                <td >
                                    <asp:TextBox ID="TextBox_ExceptionLines" runat="server" Height="250px" TextMode="MultiLine"
                                                 Visible="False" Width="500px"></asp:TextBox></td>
                                <td class="auto-style5"></td>
                            </tr>
                       
                        </table>
                    </ContentTemplate>
               
                </asp:UpdatePanel>
            </div>
            <div>

            
                <table>
                    <tr>
                    </tr>

                </table>

            </div>
            <div id="Zwmark" runat="server"></div>
            <div id="Srid" runat="server" style="width: 530px; border-radius: 8px; text-align: center;">
                <img src="fhz2.gif" style="width: 300px; height:200px;" />
            </div>
        </form>
    </body>
</html>