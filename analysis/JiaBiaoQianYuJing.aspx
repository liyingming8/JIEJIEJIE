<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JiaBiaoQianYuJing.aspx.cs" Inherits="analysis_JiaBiaoQianYuJing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=no" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <script src="../js/vue.min.js"></script>
    <script src="../js/jquery-2.1.1.min.js"></script>
    <script src="../js/layer/layer.js"></script> 
    <link href="../include/public.css" rel="stylesheet" />
    <script src="../include/js/publicNew.js"></script>
    <title>非法标签预警</title>
    <style>
        .btn { color: #356c04; text-align: center }
        .input-date { height: 30px; padding-left: 3px }
        input[type="date"]::-webkit-clear-button { display: none; }
        ::-webkit-inner-spin-button { visibility: hidden; }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="vim">
            <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem">
                        <span>日期选择：</span>
                    </div>
                    <div class="topitem">
                        <input class="input-date" type="date" v-model="startDate" />
                    </div>
                    <div class="topitem">
                        到
                    </div>
                    <div class="topitem">
                        <input class="input-date" type="date" v-model="endDate" />
                    </div>
                    <div class="topitem">
                        <input type="button" id="btn_search" class="btn btn-warning btnyd" value="查找" @click="getstatistics" />
                    </div> 
                </div>
                <div style="overflow-x: auto">
                    <table style="width: 100%" id="table_info">
                        <tr class="GridViewHeaderStyle">
                            <th>序号</th>
                            <th>具有假货嫌疑的经销商</th>
                            <th>已知假货数量</th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr class="GridViewRowStyle" v-for="(s,index) in statistics" >
                            <td v-text="index+1"></td>
                            <td v-text="s.agt"></td>
                            <td v-text="s.cnt"></td>
                            <td class="btn"><span @click="lookmore(s.agtid,s.agt)" style="cursor:pointer">详细</span></td>
                            <td class="btn"><span @click="gomap(s.agtid)" style="cursor:pointer">在地图中查看</span></td>
                        </tr>
                    </table>
                    <p v-show="statistics.length==0" style="text-align:center">无假货数据~</p>
                </div>
                <div id="container" style="height: 100%; display: none">
                </div>
            </div>
            <input type="hidden" id="TCompid" runat="server" value="" />
            <input type="hidden" id="ShowMode" runat="server" value=""/>
        </div>
    </form>
    <script>
        var vm = new Vue({
            el: "#vim",
            data: {
                startDate: null,
                endDate: null,
                compid: null,
                sm: null,
                statistics: [{ cnt: 1, agt: "" }]
            },
            methods: {
                getstatistics: function () {
                    if (this.startDate == "" || this.startDate == null || this.endDate == "" || this.endDate == null) {
                        alert("请选择日期");
                        return;
                    }
                    loading.open({ hasBg: false });
                    var jscript = document.createElement("script");
                    jscript.setAttribute("src", "http://117.34.70.23:32176/gis/fakelabel/statistics/" + this.compid + "/?" + (this.sm == '' ? "" : "mode=1&") + "date_from=" + this.startDate + "&date_to=" + this.endDate + "&callback=vm.receiveStatistics");
                    document.body.appendChild(jscript);
                    document.body.removeChild(jscript);
                },
                receiveStatistics: function (result) {
                    console.log(result);
                    this.statistics = result;
                    loading.close();
                    pointbox.open({ text: "查询完成" })
                },
                gomap: function (index) { 
                    layer.open({
                        type: 2,
                        title: '假货预警地图',
                        maxmin: true,
                        area: ['90%', '90%'],
                        content: '../analysis/jiabiaoqianmap.html?compid=' + this.compid+(this.sm==''?'':'&mode=1') + '&agtid=' + index + "&date_from=" + this.startDate + "&date_to=" + this.endDate,
                        end: function () {
                        }
                    });
                },
                lookmore: function (index, agtname) { 
                    layer.open({
                        type: 2,
                        title:agtname+ ' 的假货数据',
                        maxmin: true,
                        area: ['90%', '90%'],
                        content: '../analysis/jiabiaoqiandata.html?compid=' + this.compid + (this.sm==''?'':'&mode=1') + '&agtid=' + index + "&date_from=" + this.startDate + "&date_to=" + this.endDate,
                        end: function () {
                        }
                    });
                },
                getDate: function (flag) {
                    var date = new Date();
                    var seperator1 = "-";
                    var year = date.getFullYear();
                    var month = date.getMonth() + 1;
                    var strDate = date.getDate();
                    if (month >= 1 && month <= 9)
                        month = "0" + month;
                    if (strDate >= 0 && strDate <= 9)
                        strDate = "0" + strDate;
                    if (flag == 'start')
                        return  year + seperator1 + month + seperator1 + "01";
                    else
                        return  year + seperator1 + month + seperator1 + strDate;
                }
            },
            created: function () {
                this.compid = document.getElementById("TCompid").value; 
                this.sm = document.getElementById("ShowMode").value;
                console.log("sm:" + this.sm);
                this.startDate = this.getDate('start');
                this.endDate = this.getDate('end');
                this.getstatistics(this.startDate);
            }
        }) 
    </script>
</body>
</html>
