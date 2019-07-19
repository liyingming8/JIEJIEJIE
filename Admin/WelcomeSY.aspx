<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WelcomeSY.aspx.cs" Inherits="WelcomeSY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <style type="text/css">
        /*body {
            background: #b5ddf4;
            font: bold 12px Arial, Helvetica, sans-serif;
            margin: 0;
            padding: 0;
            width: 100%;
            height: 600px;
         
            position:relative;
        }

        ul {
            margin: 0;
            padding: 0;
            overflow: hidden;
            width: 100%;
            height: 100%;
            list-style:none;
        }

        .clock {
            margin: 5px auto;
            border: 0px solid #333;
            color: #fff;
            width: 90%;
            height:100%;

          
        }

            .clock li {
                float: left;
                width: 50%;
                height: 50%;
                text-align: center;
               
            }

        li p {
            padding-top: 2px;
            font-size: 20px;
            font-weight: bold;
            font-family: "微软雅黑";
            color: #333333;
        }*/
            body {
                font: bold 12px Arial, Helvetica, sans-serif;
                margin: 0;
                padding: 0;
                color: #D4E4F6;
            }

            a {
                text-decoration: none;
                color: #D4E4F6;
            }

            /* clock */

            .clock {
                margin: 30px 30px 0px 30px;
                border: 0px solid #333;
                color: #fff;
            }

            .clock #Date {
                font-family: 'BebasNeueRegular', Arial, Helvetica, sans-serif;
                font-size: 36px;
                text-align: center;
                -ms-text-shadow: 0 0 5px #00C6FF;
                text-shadow: 0 0 5px #00C6FF;
            }

            .clock ul {
                margin: 0 auto;
                padding: 0px;
                list-style: none;
                text-align: center;
            }

            .clock ul li {
                display: inline;
                font-size: 10em;
                text-align: center;
                font-family: 'BebasNeueRegular', Arial, Helvetica, sans-serif;
                -ms-text-shadow: 0 0 5px #00C6FF;
                text-shadow: 0 0 5px #00C6FF;
            }

            #point {
                position: relative;
                padding-left: 10px;
                padding-right: 10px;
            }

            /*#point{position:relative;-moz-animation:mymove 1s ease infinite;-webkit-animation:mymove 1s ease infinite;padding-left:10px;padding-right:10px;}*/

            @-webkit-keyframes mymove {
                0% {
                    opacity: 1.0;
                    text-shadow: 0 0 20px #00C6FF;
                }

                50% {
                    opacity: 0;
                    text-shadow: none;
                }

                100% {
                    opacity: 1.0;
                    text-shadow: 0 0 20px #00C6FF;
                }
            }

            @-moz-keyframes mymove {
                0% {
                    opacity: 1.0;
                    text-shadow: 0 0 20px #00C6FF;
                }

                50% {
                    opacity: 0;
                    text-shadow: none;
                }

                100% {
                    opacity: 1.0;
                    text-shadow: 0 0 20px #00C6FF;
                }
            }
        </style>

  

        <script type="text/javascript" src="http://code.jquery.com/jquery-1.6.4.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function() {

                // 创建两个变量，一个数组中的月和日的名称
                var monthNames = ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"];
                var dayNames = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"]

                // 创建一个对象newDate（）
                var newDate = new Date();
                // 提取当前的日期从日期对象
                newDate.setDate(newDate.getDate());
                //输出的日子，日期，月和年
                $('#Date').html(newDate.getFullYear() + " 年 " + monthNames[newDate.getMonth()] + ' ' + newDate.getDate() + ' 日 ' + dayNames[newDate.getDay()]);

                setInterval(function() {
                    // 创建一个对象，并提取newDate（）在访问者的当前时间的秒
                    var seconds = new Date().getSeconds();
                    //添加前导零秒值
                    $("#sec").html((seconds < 10 ? "0" : "") + seconds);
                }, 1000);

                setInterval(function() {
                    // 创建一个对象，并提取newDate（）在访问者的当前时间的分钟
                    var minutes = new Date().getMinutes();
                    // 添加前导零的分钟值
                    $("#min").html((minutes < 10 ? "0" : "") + minutes);
                }, 1000);

                setInterval(function() {
                    // 创建一个对象，并提取newDate（）在访问者的当前时间的小时
                    var hours = new Date().getHours();
                    // 添加前导零的小时值
                    $("#hours").html((hours < 10 ? "0" : "") + hours);
                }, 1000);

            });
        </script>
    </head>
    <body >
        <form id="form1" runat="server" style="height: 100%">
            <div class="clock">

                <div id="Date"></div>	
                <ul>
                    <li id="hours"> </li>
                    <li id="point">:</li>
                    <li id="min"> </li>
                    <li id="point">:</li>
                    <li id="sec"> </li>
                </ul>
                <div id="xiazai" runat="server"  style="text-align: center">
                    <br /><%--<p style="font-size:18px; color:red; font-weight:bold;">如果自己的扫码抢里面的文件名称不是以代理商命名的,而是以日期命名的,说明软件不是最新版的请与我联系.QQ:827137708</p>--%>
                    <%--  <a   href="../admin/software/JXS发货系统.rar" onserverclick="download_Click" style="font-size:23px;" > <span style="font-size:33px; color:red; font-weight:bold;">点击下载系统已开发最新发货软件,请按照操作说明进行操作,如有疑问请咨询:赵工,15808943684</span></a>--%>
                </div>
            </div>
            <%-- <div class="clock">
            <ul>
                <li>
                    <p>日常问题</p>

                    <div>
                        <asp:GridView ID="GridView_WT" Width="95%" runat="server" AllowPaging="True" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="5" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnPageIndexChanging="GridView_WT_PageIndexChanging">
                            <Columns>

                                <asp:TemplateField HeaderText="问题">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFHR" runat="server" Text='<%#Bind("BiaoTi")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="提交时间">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_jianshu" runat="server" Text='<%# Convert.ToDateTime(Eval("TiJiaoTime").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="提交人" ItemStyle-Width="160px">
                                    <ItemTemplate>
                                        <asp:Label ID="labeluser" runat="server" Text='<%# buser.GetList(int.Parse(Eval("userID").ToString())).LoginName %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                尚未查询到记录!
                            </EmptyDataTemplate>
                            <RowStyle Wrap="False" Height="28px" Font-Size="15px" ForeColor="Black" />
                            <PagerStyle BorderColor="Gray" BorderStyle="Double"  HorizontalAlign="Center" BorderWidth="1px"  BackColor="#3399ff" Font-Strikeout="False" />
                            <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="center"
                                VerticalAlign="Middle" Wrap="False" Font-Size="15px" />
                            <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                        </asp:GridView></div>

                    </div>

                </li>
                <li>
                    <p>发货库存</p>

                    <div>
                        <asp:GridView ID="GridView_FH" Width="95%" runat="server" AllowPaging="True" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="5" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnPageIndexChanging="GridView_FH_PageIndexChanging">
                            <Columns>

                                <asp:TemplateField HeaderText="发往经销商" ItemStyle-Width="160px">
                                    <ItemTemplate>
                                        <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="发货日期">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFHR" runat="server" Text='<%#Convert.ToDateTime(Eval("FHDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="总量">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                尚未查询到记录!
                            </EmptyDataTemplate>
                            <RowStyle Wrap="False" Height="28px" Font-Size="15px" ForeColor="Black" />
                            <PagerStyle BorderColor="Gray" BorderStyle="Double" HorizontalAlign="Center" BorderWidth="1px" BackColor="#3399ff" Font-Strikeout="False" />
                            <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="center"
                                VerticalAlign="Middle" Wrap="False" Font-Size="15px" />
                            <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                        </asp:GridView></div>

                    </div>

                </li>
                <li>
                    <p>入库信息</p>
                    <div style="text-align: center; padding: 5px">
                        <asp:GridView ID="GridView_RK" Width="95%" runat="server" AllowPaging="True" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="5" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnPageIndexChanging="GridView_RK_PageIndexChanging">
                            <Columns>

                                <asp:TemplateField HeaderText="入库日期">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelFHR" runat="server" Text='<%#Convert.ToDateTime(Eval("FHDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="总量">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="入库产品">
                                    <ItemTemplate>
                                        <asp:Label ID="labelProduct" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataTemplate>
                                尚未查询到记录!
                            </EmptyDataTemplate>
                            <RowStyle Wrap="False" Height="28px" Font-Size="15px" ForeColor="Black" />
                            <PagerStyle BorderColor="Gray" BorderStyle="Double" HorizontalAlign="Center" BackColor="#3399ff" BorderWidth="1px" Font-Strikeout="False" />
                            <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="center"
                                VerticalAlign="Middle" Wrap="False" Font-Size="15px" />
                            <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                        </asp:GridView></div>

                    </div>

                </li>
                <li>
                    <p>窜码信息</p>

                    <div style="text-align: center; padding: 5px">
                        <asp:GridView ID="GridView_CM" Width="95%" runat="server" AllowPaging="True" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="5" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnPageIndexChanging="GridView_CM_PageIndexChanging">
                            <Columns>

                                <asp:TemplateField HeaderText="标签序号">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelXH" runat="server" Text='<%# Bind("labelcode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="首次上传时间">
                                    <ItemTemplate>
                                        <asp:Label ID="Label_jianshu" runat="server" Text='<%# Convert.ToDateTime(Eval("firshangchuantime").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="产品">
                                    <ItemTemplate>
                                        <asp:Label ID="labelProduct" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataTemplate>
                                尚未查询到记录!
                            </EmptyDataTemplate>
                            <RowStyle Wrap="False" Height="28px" Font-Size="15px" ForeColor="Black" />
                            <PagerStyle BorderColor="Gray" BorderStyle="Double" HorizontalAlign="Center" BorderWidth="1px" BackColor="#3399ff" Font-Strikeout="False" />
                            <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="center"
                                VerticalAlign="Middle" Wrap="False" Font-Size="15px" />
                            <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                        </asp:GridView></div>

                    </div>


                </li>


            </ul>


        </div>--%>
        </form>
    </body>
</html>