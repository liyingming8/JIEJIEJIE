<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_GongYingShangInfoAddEdit.aspx.cs" Inherits="Admin_TB_SY_GongYingShangInfoAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SY_GongYingShangInfo</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />

       
    </head>
    <body>
        <form id="form1" runat="server">
               <div class="editpageback">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <table class="gridtable">
                    <tr><th>供应商</th><td ><input id="inputGYSMingCheng" runat="server" type="text" /></td></tr>
                    <tr><th>供应商类别</th><td>

                                          <asp:DropDownList ID="DropDownList_leibie" DataValueField="GYSTYPEID" DataTextField="GYSTYPEMingCheng" AppendDataBoundItems="true" runat="server">
                                              <asp:ListItem Text="类别..." Selected="True" Value="0"></asp:ListItem>
             
                                          </asp:DropDownList>
                                      </td></tr>
           
                    <tr><th>供应商联系人</th><td><input id="inputGYSLianXiRen" runat="server" type="text"  /></td></tr>
                    <tr><th>地址</th><td><input id="inputGYSAddress" runat="server" type="text"  /></td></tr>
                    <tr><th>电话</th><td><input id="inputGYSPhone" runat="server" type="text"/></td></tr>
                    <%-- <tr><td></td><td> 
                <asp:TextBox ID="inputGYSCreateTime" runat="server"></asp:TextBox>
              
                
                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="inputGYSCreateTime">
                            </cc1:CalendarExtender></td><td></td></tr>--%>
          
                </table>　
               <div class="bottomdivbutton"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/> </div>
            </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script> 
    </body>
</html>