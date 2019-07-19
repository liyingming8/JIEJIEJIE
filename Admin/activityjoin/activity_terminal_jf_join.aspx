<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activity_terminal_jf_join.aspx.cs" Inherits="Admin_activityjoin_activity_jf_join" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
    <script type="text/javascript" src="../../js/tuxingshow/highcharts.js"></script>
    <script type="text/javascript" src="../../js/tuxingshow/highcharts-zh_CN.js"></script>
    <script type="text/javascript" src="../../js/tuxingshow/modules/exporting.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container topdiv">
            <div class="topitem">
                <asp:DropDownList runat="server" ID="ddl_depart" DataTextField="department" DataValueField="id" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddl_depart_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="所有战区" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="topitem">
                <span>起始时间</span>
            </div>
            <div class="topitem">
                <input id="input_start_date" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
            </div>
            <div class="topitem">
                <span>至</span>
            </div>
            <div class="topitem">
                <input id="input_end_date" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
            </div>
            <div class="topitem"><span>经销商</span></div>
            <div class="topitem">
                <asp:DropDownList runat="server" ID="ddl_jxs" Width="250px" DataValueField="CompID" DataTextField="CompName" AppendDataBoundItems="True">
                    <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="topitem">
                <asp:DropDownList runat="server" ID="ddl_jf_type">
                    <asp:ListItem Selected="True" Value="1">按天</asp:ListItem>
                    <%--<asp:ListItem Value="2">按月</asp:ListItem>--%>
                    <asp:ListItem Value="3">按经销商</asp:ListItem>
                    <asp:ListItem Value="4">按终端店</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="topitem">
                <asp:Button ID="BtnSearch0" runat="server" Text="汇总" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
            </div>
            <div class="topitem">
                    <asp:Button runat="server" ID="btn_createexcel" style="text-decoration:underline;cursor:pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click"/>
            </div>
        </div>
        <div style="overflow-x: auto;">
            <div id="container" style="min-width: 400px; height: 500px"></div>
            <script type="text/javascript">
                var chart = Highcharts.chart('container', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: '终端积分汇总'
                    },
                    subtitle: {
                        text: '<%=subtitlestring%>'
                    },
                    xAxis: {
                        categories: [<%=categories%>]
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: '积分值'
                        }
                    },
                    tooltip: {
                        pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b>' +
                            '({point.percentage:.0f}%)<br/>',
                        shared: true
                    },
                    plotOptions: {
                        column: {
                            stacking: 'normal'
                        }
                    },
                    series: [
                     <%=seriestring%> 
                    ],
                    credits: {
                        enabled: false
                    }
                });
            </script>
        </div>
    </form>
    <script src="../../include/js/jquery-2.1.1.min.js"></script>
    <script src="../../include/js/Wdate/WdatePicker.js"></script>
</body>
</html>
