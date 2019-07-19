<%@ Page Language="C#" AutoEventWireup="true" CodeFile="show.aspx.cs" Inherits="yanshi_show" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="include/easyui.css" rel="stylesheet" />

    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        html {
            height: 100%;
        }

        body {
            position: relative;
            min-height: 100%;
        }

        .footershow {
            position: fixed;
            bottom: 10px;
            left:40%;
            _position: absolute;
            _top: expression(document.documentElement.clientHeight + document.documentElement.scrollTop - this.offsetHeight);
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div class=" div_WholePage">
            <div class="topdiv" style="position: relative; z-index: 100; bottom:5px">
                <div class="topitem">
                      <asp:Label ID="labelshowtext" runat="server" Text=""> </asp:Label>
                </div>
            </div>
        <div id="showdiv" style="overflow-y: auto">
        </div>
        <div id="footershow" class="footershow">
            <input type="button" id="finput" value="上一步" class="btn btn-warning btnyd" /> 
            
            <input type="button" id="ninput" value="下一步" style="margin-left: 50px;" class="btn btn-warning btnyd" />
         
        </div>

 </div>

        <asp:HiddenField ID="hf_show" runat="server" />
        <asp:HiddenField ID="hf_inputbuzou" runat="server" />
        <asp:HiddenField ID="hf_end" runat="server" />  
        <script src="include/js/jquery-2.1.1.min.js"></script>
        <script src="include/js/jquery.easyui.min.js"></script> 
        <script type="text/javascript">
            $(document).ready(function () {
                var url = "<%= returnurl(hf_show.Value,"url")%>";
                var showcontent = "<%= returnurl(hf_show.Value,"name")%>";

                $('#showdiv').load(url);
                $('#labelshowtext').html(showcontent);
                $('#labelshowtext').css({ color: "red" });

            })
            $(document).ready(function () {
                $("#finput").click(function () {
                    var modid = $("#hf_show").val();
                    var showorder = $("#hf_inputbuzou").val();
                    var endid = "0";
                    $.ajax({
                        type: "post",
                        url: " ajax/show.ashx",
                        data: { modid: modid, showorder: showorder, endid: endid },
                        success: function (result) {
                            console.log(result);
                            var back = JSON.parse(result);
                            if (back.url == "no") {
                                alert('最后一页');
                            } else if (back.url == "nono") {
                                alert('最前一页');
                            }
                            else {

                                $('#showdiv').load(back.url);
                                $('#labelshowtext').html(back.mesg);
                                $('#labelshowtext').css({ color: "red" });
                                $('#hf_inputbuzou').val(back.flag);
                                return;
                            }
                        }
                    })
                })
            })
            $(document).ready(function () {
                $("#ninput").click(function () {
                    var modid = $("#hf_show").val();
                    var showorder = $("#hf_inputbuzou").val();
                    var endid = $("#hf_end").val();
                    $.ajax({
                        type: "post",
                        url: " ajax/show.ashx",
                        data: { modid: modid, showorder: showorder, endid: endid },
                        success: function (result) {
                            console.log(result);
                            var back = JSON.parse(result);
                            if (back.url == "no") {
                                alert('最后一页');
                            } else if (back.msg == "nono") {
                                alert('最前一页');
                            }
                            else {
                                $('#showdiv').load(back.url);
                                $('#labelshowtext').html(back.mesg);
                                $('#labelshowtext').css({ color: "red" });
                                $('#hf_inputbuzou').val(back.flag);
                                return;
                            }
                        }
                    })
                })
            })
        </script>
    </form>
</body>
</html>
