<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiQuAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanZhiQuAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanZhiQu</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>原曲批次</th>
                            <td>
                                <table style="table-layout: auto; border-collapse: collapse">
                                    <tr>
                                        <td>
                                            <input id="inputZhiQuPC" runat="server" type="text" readonly="readonly" maxlength="250" class="length6" />
                                        </td>
                                        <td>
                                            <asp:Button ID="ButtonShengCheng" runat="server" OnClick="ButtonShengCheng_Click" Text="生成" />
                                        </td>
                                    </tr>
                                </table>
                            </td> 
                        </tr>
                        <tr>
                            <th>质检员</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxZJYID"  runat="server" DataTextField="ZhiJianName" DataValueField="ID" Width="70px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>开始时间</th>
                            <td>
                                <input id="inputStartTime" runat="server" class="date" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})"  maxlength="16" />
                            </td>
                        </tr>
                        <tr>
                             <th>结束时间</th>
                            <td>
                                <input id="inputEndTime" runat="server" class="date" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})"  maxlength="16" />
                            </td>
                        </tr>
                        <tr>
                            <th>库房</th>
                            <td>
                                <asp:DropDownList ID="CompBox_KuFang" runat="server" DataTextField="KuFang" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                              <th>房排</th>
                            <td>
                                <asp:DropDownList ID="CompBox_FangPai" runat="server" DataTextField="Name" DataValueField="ID" Width="100px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="3">
                                <input id="inputRemarks" runat="server" type="text" maxlength="250"/>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
      <script src="../include/js/jquery-1.7.1.js"></script>
      <script src="../include/js/UploadImage.js" type="text/javascript"></script>
      <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
