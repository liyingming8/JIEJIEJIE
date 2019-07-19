<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testtouyun.aspx.cs" Inherits="testtouyun" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <script src="include/js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="获取Token" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label> <br/><br/>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="刷新Token" />
       <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="查询产品" />
        <br />
        <br/>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br/>
        <asp:Button ID="Button3" Text="文件上传" runat="server" OnClick="Button3_Click"/>
    </div> 
        <p>
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" />
            </p>
        <p>
           </p>
        <p>
           </p>
        <p>
            <input id="btn_test_myinterface"  type="button" onclick="test()" value="接口测试"/>  
            </p>
    </form>
    <script type="text/javascript">
        function test() { 
            $.ajax({
                type: "post",
                datatype:"xml",
                url: "http://ws.china315net.com/cos/xferpfhd.ashx",
                data: { "data": "<xml>hello</xml>" },
                success: function (msg) {
                    alert(msg);
                }
            });
        }
    </script>
</body>
</html>
