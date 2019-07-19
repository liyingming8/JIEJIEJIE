<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WXXBtongjimap.aspx.cs" Inherits="TB_WXXBtongjimap" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" />
        <script src="http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js"></script>
        <script src="js/echarts/shanxi1.js"></script>
        <script src="js/echarts/echarts.js"></script>
        <script src="js/echarts/china.js"></script>
        <script src="js/echarts/esl.js"></script>
        <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/echarts-all-3.js"></script>
        <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/extension/dataTool.min.js"></script>
        <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/map/js/china.js"></script>
        <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/map/js/world.js"></script>
        <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=ZUONbpqGBsYGXNIYHicvbAbM"></script>
        <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/extension/bmap.min.js"></script>

   
    </head>
    <body>
        <form id="Form1" runat="server">
            <div style="position: relative; z-index: 100">
                <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager>

                <table  class="tdbg">
                    <tr>
                        <td>起始日期</td>
                        <td align="left" class="tdbg" style="width: 100px">
                            <asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender>
                        </td>
                        <td class="tdbg" style="width: 20px; text-align: center;">至</td>
                        <td class="tdbg" style="width: 100px">
                            <asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                        <td></td>
                        <td>
                            <input id="BtDC" runat="server" type="button" class="inputbutton" value="导出EXCEL" onserverclick="BtDC_ServerClick" />

                        </td>

                    </tr>
                </table> 
            </div>
            <div id="container" runat="server" style="width: 900px; height: 400px; margin: 0 auto; z-index: 1; position: relative;"></div>
            <%--  <div id="container" style="width: 900px; height: 400px;"></div>--%>

        </form>
        <script>
            $.get('../js/echarts/shanxi1.json', function(shanxi1) {
                    echarts.registerMap('shanxi1', shanxi1);
                    var chart = echarts.init(document.getElementById('container'));
                    chart.setOption({
                        backgroundColor: '#404a59',
                        title: {
                            text: '陕西省扫码量',

                            // sublink: 'http://www.pm25.in',
                            x: 'center',
                            textStyle: {
                                color: '#fff'
                            }
                        },
                        tooltip: {
                            trigger: 'item',
                        },
                        legend: {
                            orient: 'vertical',
                            y: 'bottom',
                            x: 'right',
                            data: ['扫码量'],
                            textStyle: {
                                color: '#fff'
                            }
                        },
                        visualMap: {
                            min: 0,
                            max: 15000,
                            calculable: true,
                            text: ['高', '低'],
                            inRange: {
                                color: ['lightblue', 'orange', 'red'],
                            },
                            textStyle: {
                                color: '#fff'
                            }
                        },
                        geo: {
                            map: 'shanxi1',
                            label: {
                                emphasis: {
                                    show: false
                                }
                            },
                            itemStyle: {
                                normal: {
                                    areaColor: '#323c48',
                                    borderColor: '#111'
                                },
                                emphasis: {
                                    areaColor: '#2a333d'
                                }
                            }
                        },
                        series: [
                            {
                                type: 'map',
                                mapType: 'shanxi1',
                                name: '扫码量',
                                //type: 'scatter'
                                roam: false,
                                label: {
                                    normal: {
                                        show: true
                                    },
                                    emphasis: {
                                        show: true
                                    }
                                },
                                data: [
                                    { name: "咸阳市", value: <%= returnnum(xianyang) %> },
                                    { name: "渭南市", value: <%= returnnum(weinan) %> },
                                    { name: "商洛市", value: <%= returnnum(shangluo) %> },
                                    { name: "榆林市", value: <%= returnnum(yulin) %> },
                                    { name: "汉中市", value: <%= returnnum(hanzhong) %> },
                                    { name: "延安市", value: <%= returnnum(yanan) %> },
                                    { name: "西安市", value: <%= returnnum(xian) %> },
                                    { name: "安康市", value: <%= returnnum(ankang) %> },
                                    { name: "宝鸡市", value: <%= returnnum(baoji) %> },
                                    { name: "铜川市", value: <%= returnnum(tongchuan) %> }
                                ]
                            }
                        ]

                    })

                }
            )
        </script>

    </body>
</html>