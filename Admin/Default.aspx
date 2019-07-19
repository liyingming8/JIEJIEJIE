<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title><asp:Literal ID="literal_title" runat="server"></asp:Literal></title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />  
        <script src="include/js/jquery.pack.js" type="text/javascript"></script>
        <script language="javascript" src="include/js/AdminIndex.js" type="text/javascript"></script>
        <script language="javascript" src="include/js/FrameTab.js" type="text/javascript"></script>
        <script src="include/js/common.js" type="text/javascript"></script>
        <link href="include/Guide.css" rel="stylesheet" type="text/css" />    
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="include/xtree.css" rel="stylesheet" type="text/css" />
        <link href="include/MyWeb.css" rel="stylesheet" type="text/css" />
        <link href="include/index.css" rel="stylesheet" />
    </head>
    <body id="Indexbody" onload=" onload(); ">
        <form id="myform" name="myform" method="post" runat="server">
            <table cellspacing="0" style="height: 100%; width: 100%; margin: 0px; padding: 0px;" cellpadding="0" border="0" >
       
                <tr>
                    <td colspan="3">
                        <div id="content">
                            <img id="logo" runat="server" />                        
                            <ul id="ChannelMenuItems" style="padding-left: 230px;">
                                <asp:PlaceHolder runat="server" ID="PH_TopMenu"></asp:PlaceHolder>
                            </ul>
                            <div id="SubMenu">
                                <div id="ChannelMenu_" style="width: 100%">
                                    <ul>
                                        <li>你好:<span><strong><asp:PlaceHolder ID="PH_OperRater" runat="server"></asp:PlaceHolder></strong></span>，欢迎您!</li>
                                        <li><a href="welcome.aspx" target="main_right">工作台首页</a></li>
                                        <li><a href="../Exit.aspx"><span>安全退出</span></a> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr style="vertical-align: top;">
                    <td id="frmTitle" style="width: 195px; height: 400px;">
                        <iframe id="left" style="z-index: 2; visibility: inherit; width: 195px; height: 100%;" name="left" src="LeftMenu.aspx" frameborder="0" tabid="1"></iframe>
                    </td>
                    <td class="but" style="width: 12px;" onclick=" switchSysBar(); ">
                        <img id="switchPoint" style="border-right: 0px; border-top: 0px; border-left: 0px; width: 12px; border-bottom: 0px;" alt="关闭左栏" src="images/butClose.gif" />
                    </td>
                    <td style="width: 100%;">                   
                        <!-- 书签结束 -->
                        <div id="main_right_frame" style="width: 100%; margin: 0px;">
                            <iframe id="main_right" style="z-index: 2; visibility: inherit; overflow: hidden; width: 100%; height: 600px;" onload=" setFrameHeight(this) " name="main_right" src="welcome.aspx" frameborder="0" scrolling="auto" tabid="1"></iframe>                       
                        </div>
                    </td>
                </tr>       
            </table>  
        </form>
    </body>
</html>