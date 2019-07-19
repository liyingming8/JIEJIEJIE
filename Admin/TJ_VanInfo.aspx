<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_VanInfo.aspx.cs" Inherits="Admin_TJ_VanInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="VanID">VanID</asp:ListItem>
                                             <asp:ListItem Value="NumberPlate">NumberPlate</asp:ListItem>
                                             <asp:ListItem Value="VanPicture">VanPicture</asp:ListItem>
                                             <asp:ListItem Value="VanIntructions">VanIntructions</asp:ListItem>
                                             <asp:ListItem Value="VehicleLicenseCode">VehicleLicenseCode</asp:ListItem>
                                             <asp:ListItem Value="VehicleLicensePicture">VehicleLicensePicture</asp:ListItem>
                                             <asp:ListItem Value="OperationCertificateCode">OperationCertificateCode</asp:ListItem>
                                             <asp:ListItem Value="OperationCertificatePicture">OperationCertificatePicture</asp:ListItem>
                                             <asp:ListItem Value="Remarks">Remarks</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TJ_VanInfoAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                              AutoGenerateColumns="False" DataKeyNames="VanID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" PageSize="18" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="VanID" DataNavigateUrlFormatString="TJ_VanInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="VanID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanID" runat="server"  MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanID" runat="server" Text='<%# Bind("VanID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanTypeID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanTypeID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanTypeID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanTypeID" runat="server" Text='<%# Bind("VanTypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanBrandID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanBrandID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanBrandID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanBrandID" runat="server" Text='<%# Bind("VanBrandID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanCarryAbID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanCarryAbID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanCarryAbID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanCarryAbID" runat="server" Text='<%# Bind("VanCarryAbID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanSizeID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanSizeID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanSizeID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanSizeID" runat="server" Text='<%# Bind("VanSizeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanMaterID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanMaterID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanMaterID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanMaterID" runat="server" Text='<%# Bind("VanMaterID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DriverID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDriverID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("DriverID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDriverID" runat="server" Text='<%# Bind("DriverID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanCertifID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanCertifID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("VanCertifID") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanCertifID" runat="server" Text='<%# Bind("VanCertifID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NumberPlate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNumberPlate" runat="server"  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelNumberPlate" runat="server" Text='<%# Bind("NumberPlate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanPicture">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanPicture" runat="server"  MaxLength="200"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanPicture" runat="server" Text='<%# Bind("VanPicture") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VanIntructions">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVanIntructions" runat="server"  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVanIntructions" runat="server" Text='<%# Bind("VanIntructions") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VehicleLicenseCode">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVehicleLicenseCode" runat="server"  MaxLength="30"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVehicleLicenseCode" runat="server" Text='<%# Bind("VehicleLicenseCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VehicleLicensePicture">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVehicleLicensePicture" runat="server"  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelVehicleLicensePicture" runat="server" Text='<%# Bind("VehicleLicensePicture") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OperationCertificateCode">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationCertificateCode" runat="server"  MaxLength="30"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelOperationCertificateCode" runat="server" Text='<%# Bind("OperationCertificateCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OperationCertificatePicture">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOperationCertificatePicture" runat="server"  MaxLength="100"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelOperationCertificatePicture" runat="server" Text='<%# Bind("OperationCertificatePicture") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>