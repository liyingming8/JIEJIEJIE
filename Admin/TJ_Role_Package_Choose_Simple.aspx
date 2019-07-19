<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Role_Package_Choose_Simple.aspx.cs"
    Inherits="Admin_TJ_Role_Package_Choose_Simple" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <link href="../include/public_v1.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-size: 0.8rem;
        }
    </style>
</head>
<body style="background-color: #ffffff;">
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div style="width: 98%; text-align: center; font-size: 20px; font-weight: bold; margin: 1rem 0;padding: 0;">
                功能模块选择
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                    DataKeyNames="id"
                    OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxSelect" Enabled="False" runat="server" />
                                <asp:HiddenField runat="server" Value='<%# Bind("id") %>' ID="hf_id" />
                                <asp:HiddenField runat="server" Value='<%# Bind("pricevalue") %>' ID="hf_price" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="功能包">
                            <ItemTemplate>
                                <asp:Label ID="Labelrpackage" Enabled="False" runat="server" Text='<%# Bind("rpackage") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" />
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="单价">
                            <ItemTemplate>
                                <asp:Label ID="Labelprice" runat="server" Text='<%# Convert.ToDecimal(Eval("pricevalue")).ToString("0.00") %>'></asp:Label>元/枚 
                            </ItemTemplate>
                            <ItemStyle Font-Bold="True" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="简介">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" BackColor="#26A2FF" />
                </asp:GridView>
            </div>
            <div style="width: 98%; text-align: center; font-size: 20px; font-weight: bold; margin: 1rem 0;padding: 0;">
                <input type="button" class="btn-warning" style="color: White; background-color: #26A2FF;padding: 0 0.8rem"
                    value="确定" onclick="gopay()" />
                <input type="button" class="btn-info" onclick="gotofrantpage()" style="border-style: none; border-color: inherit; border-width: medium; color: White; background-color: #888888; margin-left:0.6rem; height: 2.2rem;padding: 0 0.5rem;" value="不要了"/>
                <%--<asp:Button ID="ButtonYes" BackColor="#26a2ff" ForeColor="white" runat="server" OnClientClick="javascript: return checkinput();" Text="确 定" CssClass="btn btn-warning btnyd" OnClick="ButtonYes_Click" />--%>
            </div>
        </div>
        <input type="hidden" id="hf_compid" runat="server" />
        <input type="hidden" id="hf_count" runat="server" />
           <input type="hidden" id="hf_pid" runat="server" />
           <input type="hidden" id="hf_proid" runat="server" />
        <input type="hidden" id="hf_cpid" runat="server"/>
         <input type="hidden" id="hf_zk" runat="server" />
        <script src="../include/js/jquery-2.1.1.min.js"></script>
        <script src="../include/js/public_v1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script type="text/javascript">
            var tjswmpayapi,
                allmoney = 0,
                cnt = Number($("#hf_count").val());
            function select(index) {
                var eleid = 'GridView1_ctl' + (parseInt(index) + 2 + 100).toString().substring(1) + '_',
                    elename = eleid + "CheckBoxSelect",
                    moneyname = eleid + "hf_price";
                if ((document.getElementById(elename)).checked) {
                    (document.getElementById(elename)).checked = false;
                    allmoney = allmoney - (Number($("#" + moneyname).val()) * Number($("#hf_zk").val())* cnt) ;
                } else {
                    (document.getElementById(elename)).checked = true;
                    allmoney = allmoney + (Number($("#" + moneyname).val()) * Number($("#hf_zk").val())* cnt) ;
                } 
                tjpublic.addtsbox({ content: "总计为:" + Math.round(allmoney) / 100  + "元" })
            }

            function gopay() {
                var checkbox = $("table input[type='checkbox']");
                var checkid = "",
                    price = "",
                    ispay = "1";
                checkbox.each(function() {
                    if ($(this)[0].checked) {
                        if ($(this).attr("disabled") != "disabled") {
                            checkid = checkid + "," + $(this).next().val();
                            ispay = "1";
                        } else {
                            checkid = checkid + "," + $(this).parent().next().val();
                        }
                    }
                });
                console.log(checkid);
                if (checkid !== "") {
                    $.ajax({
                        type: "post",
                        url: "ajax/modulepay.ashx",
                        data: {
                            compid: $("#hf_compid").val(),
                            rid: checkid.substring(1),
                            ispay: ispay,
                            paymoney: parseInt(allmoney),
                            cnt: $("#hf_count").val(),
                            pid: $("#hf_pid").val(),
                            proid: $("#hf_proid").val()
                        },
                        success: function(result) {
                            var back = JSON.parse(result);
                            if (back.msg === "ok") {
                                tjswmpayapi = back.jsapi;
                                callpay();
                            } else if (back.msg === "nopay") { 
                                gotofrantpage();
                            } else {
                                alert("出错了！");
                            }
                        }
                    });
                }
                else {
                    alert("请先选择模块");
                }
            }
            function jsApiCall() {
                WeixinJSBridge.invoke(
                    'getBrandWCPayRequest',
                    tjswmpayapi,//josn串
                    function (res) {
                        if (res.err_msg === "get_brand_wcpay_request:ok") {
                            gotofrantpage();
                        }
                        else {
                            showbox("操作超时！");
                        }
                        WeixinJSBridge.log(res.err_msg);
                        //alert(res.err_code + res.err_desc + res.err_msg);
                    }
                );
            }
            function callpay() {
                console.log("no");
                if (typeof WeixinJSBridge == "undefined") {
                    if (document.addEventListener) {
                        document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                    }
                    else if (document.attachEvent) {
                        document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                        document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                    }
                }
                else {
                    jsApiCall();
                }
            }
            function checkinput() {
                var inputs = document.getElementsByTagName("input");//获取所有的input标签对象 
                var count = 0;
                for (var i = 0; i < inputs.length; i++) {
                    var obj = inputs[i];
                    if (obj.type === 'checkbox') {
                        if (obj.checked) {
                            count++;
                        }
                    }
                }
                if (count === 0) {
                    alert('请选择模块！');
                    return false;
                } else {
                    return true;
                }
            }

            function gotofrantpage() {
                window.location.href = "http://tjfnew.china315net.com/common/route.ashx?cpid=" + $("#hf_cpid").val();
            }
        </script>
    </form>
</body>
</html>
