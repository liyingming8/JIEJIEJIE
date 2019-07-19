<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoQuery.aspx.cs" Inherits="analysis_FaHuoQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发货汇总</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv" style="position: relative; z-index: 100">
                <div class="topitem"><span>时间段</span></div>
                <div class="topitem">
                    <asp:DropDownList ID="ddl_md" runat="server">
                        <asp:ListItem Value="week">本周</asp:ListItem>
                        <asp:ListItem Value="month">本月</asp:ListItem>
                        <asp:ListItem Value="season">本季度</asp:ListItem>
                        <asp:ListItem Value="year">本年</asp:ListItem>
                        <asp:ListItem Value="lyear">去年</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager>
            </div>
            <div id="container" style="width: 100%; margin: 20px auto; z-index: 1; position: relative;"></div>
        </div>
    </form>
    <script src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/highcharts.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/highcharts-zh_CN.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/modules/exporting.js"></script>
    <script src="../include/js/UploadImage.js"></script>
    <script type="text/javascript">
        Highcharts.setOptions({
            lang: {
                contextButtonTitle: "图表导出菜单",
                decimalPoint: ".",
                downloadJPEG: "下载JPEG图片",
                downloadPDF: "下载PDF文件",
                downloadPNG: "下载PNG文件",
                downloadSVG: "下载SVG文件",
                drillUpText: "返回 {series.name}",
                loading: "加载中",
                noData: "没有数据",
                printChart: "打印图表",
                resetZoom: "恢复缩放",
                resetZoomTitle: "恢复图表",
            }
        });

        $(function () {
            //其他扫描量，Y坐标 
            //得到X坐标月份 
            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: '发货量统计',
                    x: -20
                },
                subtitle: {
                    text: '(<%=Dtst%>至<%=Dted%>)发货量:<%=Totalshuliang%>件'
                },
                tooltip: {
                    valueSuffix: '件',
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: (browserRedirect() == "phone") ? false : true
                    }
                },
                series: [
                    <%=Sbfhl.ToString()%>
                ],
                credits: {
                    enabled: false // 默认值，如果想去掉版权信息，设置为false即可 
                }
            });
        });
    </script>
</body>
</html>
