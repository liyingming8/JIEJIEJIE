<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activity_customer_jf_join.aspx.cs" Inherits="Admin_activity_customer_jf_join" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container topdiv">
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
            <div class="topitem">
                <asp:DropDownList runat="server" ID="ddl_jf_type">
                    <asp:ListItem Selected="True" Value="day">按天</asp:ListItem>
                    <asp:ListItem  Value="month">按月</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="topitem">
                <asp:Button ID="BtnSearch0" runat="server" Text="汇总" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
            </div>
            <div class="topitem">
                    <asp:Button runat="server" ID="btn_createexcel" style="text-decoration:underline;cursor:pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click"/>
            </div>

        </div>
        <div id="container" style="min-width: 400px; height: 500px"></div>
    </form>

    <script src="../../include/js/jquery-2.1.1.min.js"></script>
    <script src="../../include/js/UploadImage.js"></script>
    <script src="../../include/js/Wdate/WdatePicker.js"></script>

    <script src="../../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../../js/tuxingshow/highcharts.js"></script>
    <script type="text/javascript" src="../../js/tuxingshow/highcharts-zh_CN.js"></script>
    <script type="text/javascript" src="../../js/tuxingshow/modules/exporting.js"></script>
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

        var getdata = function () {
            $.get("http://199333", { compid: 2, userid: 34 }, function (msg) {
                return msg;
            });
        }

        $(function () {
            //其他扫描量，Y坐标 
            //得到X坐标月份 
            $('#container').highcharts({
                chart: {
                    type: 'line'
                },
                title: {
                    text: '消费者积分汇总'
                },
                subtitle: {
                    text: <%=mResultScore%>,
                },
                xAxis: {
                    categories: [<%=mMonthString%>]
                },
                yAxis: {
                    title: {
                        text: '分值'
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            // 开启数据标签
                            enabled: true
                        },
                        // 关闭鼠标跟踪，对应的提示框、点击事件会失效
                        enableMouseTracking: true
                    }
                },
                series: [<%=mResultString%>],
                credits: {
                    enabled: false // 默认值，如果想去掉版权信息，设置为false即可 
                }
            });
        });

    </script>


    

</body>
</html>
