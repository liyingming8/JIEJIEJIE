<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_LJjiangAdd.aspx.cs" Inherits="Admin_TB_LJjiangAdd" %>


<!DOCTYPE html>
<html >
    <head id="Head1" runat="server">
        <title></title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
  
      
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <table class="gridtable">
        
        
                    <tr>
                        <td>奖项</td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" Width="127px">
                                <asp:ListItem Value="0" Selected="True">选择奖项</asp:ListItem>
                                <asp:ListItem Value="1">微信红包</asp:ListItem>
                                <asp:ListItem Value="2">实物</asp:ListItem>
                            </asp:DropDownList>
                        </td>        
                    </tr>
              
                    <tr>
                        <th id="jpje" runat="server" >奖品</th>
                        <td>
                            <input id="inputJxContent" placeholder="红包直接填写金额：如1元就填  |实物示例:iphone7手机一部" runat="server" type="text" maxlength="50" /></td>
<%--                        <td>
                            <span>
                                |实物示例： <span style="color: red">iphone7手机一部</span>
                          
                            </span>
                    
                        </td>--%>
                    
                    
                    </tr>
                    <tr id="renshu" runat="server" visible="false">
                        <th>裂变人数</th>
                        <td>
                            <input id="Text_renshu" runat="server" type="text"  /></td>
                        
                        <td>
                            <span style="color: Red;">
                                至少3人,且金额必须等于大于人数!
                          
                            </span>
                    
                        </td>
                    </tr>
                
                    <tr>
                        <th>截止日期</th>
                        <td>
                            <input type="date" id="InTime" onblur=" $('#JzTime').val($('#InTime').val()) "    />
                            <input type="text" id="JzTime" runat="server"  style="display: none"  />
                        </td>
                    </tr>

                    <tr>
                        <th>开始号码</th>
                        <td>
                            <input id="inputStartlabelcode" runat="server" type="text" maxlength="50" /></td>
                       
                    </tr>
                    <tr>
                        <th>结束号码</th>
                        <td>
                            <asp:TextBox ID="inputEndlabelcode" runat="server" AutoPostBack="True" OnTextChanged="inputEndlabelcode_TextChanged"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <th>可布奖数量</th>
                        <td>
                            <input id="inputcount" runat="server" type="text" readonly="readonly" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <th>设奖数量</th>
                        <td>
                            <input id="inputnum" runat="server" type="text" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <th>布奖产品</th>
                        <td>
                        <asp:DropDownList ID="Dpro" runat="server" DataValueField="Infor_ID"  DataTextField="Products_Name"  AutoPostBack="true" Width="170px">
                           
                        </asp:DropDownList>
                        <td></td>
                    </tr>
                
                  


                    <%--  <tr>
                    <td>备注</td>
                    <td>
                        <input id="inputRemarks" runat="server" type="text" /></td>
                    <td></td>
                </tr>--%>

                    
                </table> 
                      <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
            </div> 
              <asp:HiddenField ID="HF_CMD" runat="server" />
               <asp:HiddenField ID="HF_ID" runat="server" /> 
        </form> 
          <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="js/jquery-2.1.1.min.js"></script>
        <script>
            window.onload = function() {
                $('#InTime').val($('#JzTime').val()); 
            }


        </script>
    
    </body>
</html>