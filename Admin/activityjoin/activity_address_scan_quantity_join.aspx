<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activity_address_scan_quantity_join.aspx.cs" Inherits="Admin_activityjoin_activity_address_scan_quantity_join" %>

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
                <span>省份：</span>
                <asp:DropDownList runat="server" ID="Province">
                    <asp:ListItem Selected="True" Value="All">全部</asp:ListItem>
                    <asp:ListItem Value="AnHui">安徽省</asp:ListItem>
                    <asp:ListItem Value="BeiJjing">北京市</asp:ListItem>
                    <asp:ListItem Value="FuJian">福建省</asp:ListItem>
                    <asp:ListItem Value="GanSu">甘肃省</asp:ListItem>
                    <asp:ListItem Value="GuangDong">广东省</asp:ListItem>
                    <asp:ListItem Value="GuangXi">广西壮族自治区</asp:ListItem>
                    <asp:ListItem Value="GuiZhou">贵州省</asp:ListItem>
                    <asp:ListItem Value="HaiNan">海南省</asp:ListItem>
                    <asp:ListItem Value="HeBei">河北省</asp:ListItem>
                    <asp:ListItem Value="HeNan">河南省</asp:ListItem>
                    <asp:ListItem Value="HeiLongJiang">黑龙江省</asp:ListItem>
                    <asp:ListItem Value="HuBei">湖北省</asp:ListItem>
                    <asp:ListItem Value="HuNan">湖南省</asp:ListItem>
                    <asp:ListItem Value="JiLin">吉林省</asp:ListItem>
                    <asp:ListItem Value="JiangSu">江苏省</asp:ListItem>
                    <asp:ListItem Value="JiangXi">江西省</asp:ListItem>
                    <asp:ListItem Value="LiaoNing">辽宁省</asp:ListItem>
                    <asp:ListItem Value="ShanDong">山东省</asp:ListItem>
                    <asp:ListItem Value="ShanXi">山西省</asp:ListItem>
                    <asp:ListItem Value="ShanXiS">陕西省</asp:ListItem>
                    <asp:ListItem Value="ShangHai">上海市</asp:ListItem>
                    <asp:ListItem Value="SiChuan">四川省</asp:ListItem>
                    <asp:ListItem Value="TianJin">天津市</asp:ListItem>
                    <asp:ListItem Value="YunNan">云南省</asp:ListItem>
                    <asp:ListItem Value="ZheJiang">浙江省</asp:ListItem>
                    <asp:ListItem Value="ChongQing">重庆市</asp:ListItem>
                    <asp:ListItem Value="QingHai">青海省</asp:ListItem>
                    <asp:ListItem Value="TaiWan">台湾省</asp:ListItem>
                    <asp:ListItem Value="XiZang">西藏自治区</asp:ListItem>
                    <asp:ListItem Value="NeiMengGu">内蒙古自治区</asp:ListItem>
                    <asp:ListItem Value="NingXia">宁夏回族自治区</asp:ListItem>
                    <asp:ListItem Value="AoMen">澳门特别行政区</asp:ListItem>
                    <asp:ListItem Value="XiangGang">香港特别行政区</asp:ListItem>
                    <asp:ListItem Value="XinJiang">新疆维吾尔自治区</asp:ListItem>
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

        $(function () {
            //其他扫描量，Y坐标 
            //得到X坐标月份 
            Highcharts.setOptions({ 
                colors: [ '#668B8B','#B2DFEE', '#0000AA', '#8B0000', '#4169E1', '#006400','#8B1C62', '#8B7500', 
            '#008B45','#8B4500','#FFFF00','#00BFFF','#FFB5C5','#8B8378','#00EE76','#FF6347','#3D3D3D','#551A8B',
            '#5D478B','#C71585','#90EE90','#D2691E','#EE30A7','cc0099','#B4EEB4','#969696','#8B6508','#87CEFF',
            '#556B2F','#3B3B3B','#D9D9D9','33cc99','339933','3399cc']});
            $('#container').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text:<%=mProvince%>,
                    style:{
                        color: '#3E576F',
                        fontSize: '25px',
                        fontWeight: 'bold'
                    }
                },
                subtitle: {
                    text: <%=mSubTitleString%>,
                },
                tooltip: {
                    valueSuffix: '件',
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            formatter: function() {
                                return '<b>'+ this.point.name +'</b>: '+ Highcharts.numberFormat(this.percentage, 2) +'% ('+
                                             Highcharts.numberFormat(this.y, 0, ',') +' 件)';
                            },                        
                        },
                        showInLegend: (browserRedirect() == "phone") ? false : true
                    },
                    showInLegend: true,
                    states: {
                        hover: {
                            enabled: false
                        }  
                    },
                    slicedOffset: 1,
                    point: {   
                        events: {
                            // 鼠标滑过是，突出当前扇区
                            mouseOver: function() {
                                this.slice();
                            },
                            // 鼠标移出时，收回突出显示
                            mouseOut: function() {
                                this.slice();
                            },
                            // 默认是点击突出，这里屏蔽掉
                            click: function() {
                                return false;
                            }
                        }
                    }
                },
                series: [
                    <%=mResultString%>
                ],
                credits: {
                    enabled: false // 默认值，如果想去掉版权信息，设置为false即可 
                }
            });
        });
    </script>
</body>
</html>

