<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Agents_Infor_ForAuthor.aspx.cs" Inherits="Admin_TB_Agents_Infor_ForAuthor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
    <link href="../../include/demo.css" rel="stylesheet" />
    <link href="../../include/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <style type="text/css">
        .ztree {
            width: 90% !important;
            height: 95% !important;
            border: none !important;
            background-color: white !important;
        }
    </style>
    <script type="text/javascript" src="../js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ztree.core.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ztree.excheck.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ztree.exedit.min.js"></script>
    <script type="text/javascript">
		<!--
    var setting = {
        async: {
            enable: true,
            url: getUrl
        },
        check: {
            enable: false
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        view: {
            expandSpeed: ""
        },
        callback: {
            beforeExpand: beforeExpand,
            onAsyncSuccess: onAsyncSuccess,
            onAsyncError: onAsyncError
        }
    };

    var zNodes = [
            <%= Parentstring %> 
        ];

        var log, className = "dark",
            startTime = 0, endTime = 0, perCount = 100, perTime = 100;
        function getUrl(treeId, treeNode) {
            //var curCount = (treeNode.children) ? treeNode.children.length : 0;
            //var getCount = (curCount + perCount) > treeNode.count ? (treeNode.count - curCount) : perCount;
            var param = "pid=" + treeNode.id; 
            return "ajax/returnterminal.ashx?" + param;
        }
        function beforeExpand(treeId, treeNode) {
            if (!treeNode.isAjaxing) {
                startTime = new Date();
                treeNode.times = 1;
                ajaxGetNodes(treeNode, "refresh");
                return true;
            } else {
                alert("zTree 正在下载数据中，请稍后展开节点。。。");
                return false;
            }
        }
        function onAsyncSuccess(event, treeId, treeNode, msg) {
            console.log(treeNode);
            if (!msg || msg.length == 0) {
                return;
            }
            var zTree = $.fn.zTree.getZTreeObj("treeDemo"),
                totalCount = treeNode.count;
            if (treeNode.children.length < totalCount) {
                //setTimeout(function () { ajaxGetNodes(treeNode); }, perTime);
            } else {
                treeNode.icon = "";
                zTree.updateNode(treeNode);
                zTree.selectNode(treeNode.children[0]);
                endTime = new Date();
                var usedTime = (endTime.getTime() - startTime.getTime()) / 1000;
                className = (className === "dark" ? "" : "dark");
                showLog("[ " + getTime() + " ]&nbsp;&nbsp;treeNode:" + treeNode.name);
                showLog("加载完毕，共进行 " + (treeNode.times - 1) + " 次异步加载, 耗时：" + usedTime + " 秒");
            }
        }
        function onAsyncError(event, treeId, treeNode, XMLHttpRequest, textStatus, errorThrown) {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            alert("异步获取数据出现异常。");
            treeNode.icon = "";
            zTree.updateNode(treeNode);
        }
        function ajaxGetNodes(treeNode, reloadType) {
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            if (reloadType == "refresh") {
                treeNode.icon = "../../../css/zTreeStyle/img/loading.gif";
                zTree.updateNode(treeNode);
            }
            zTree.reAsyncChildNodes(treeNode, reloadType, true);
        }
        function showLog(str) {
            if (!log) log = $("#log");
            log.append("<li class='" + className + "'>" + str + "</li>");
            if (log.children("li").length > 4) {
                log.get(0).removeChild(log.children("li")[0]);
            }
        }
        function getTime() {
            var now = new Date(),
                h = now.getHours(),
                m = now.getMinutes(),
                s = now.getSeconds(),
                ms = now.getMilliseconds();
            return (h + ":" + m + ":" + s + " " + ms);
        }

        $(document).ready(function () {
            $.fn.zTree.init($("#treeDemo"), setting, zNodes); 
        });
		//-->
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_Agents_InforAddEdit.aspx?cmd=add', 780, 560, '经销商信息')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDL_DepartID" DataTextField="department" DataValueField="id" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_DepartID_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="CompName">经销商</asp:ListItem>
                        <asp:ListItem Value="LegalPerson">联系人</asp:ListItem>
                        <asp:ListItem Value="TelNumber">电话</asp:ListItem>
                        <asp:ListItem Value="MobilePhoneNumber">手机</asp:ListItem>
                        <asp:ListItem Value="AllowAreaInfo">授权区域</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <div style="overflow-x: auto">


                <ul id="treeDemo" class="ztree"></ul>

                <asp:TreeView ID="tvagent" runat="server" OnSelectedNodeChanged="tvagent_SelectedNodeChanged"></asp:TreeView>
            </div>
        </div>
    </form>
</body>
</html>
