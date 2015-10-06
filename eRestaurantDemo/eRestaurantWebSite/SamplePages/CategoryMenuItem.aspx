<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CategoryMenuItem.aspx.cs" Inherits="SamplePages_CategoryMenuItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <asp:Repeater ID="MenuCategories" runat="server" DataSourceID="ODSCategoryMenuItems">
            <ItemTemplate>
                <h3>
                    <%# Eval("Description") %>
                    <div class="well">
                        <asp:Repeater ID="MenuItems" runat="server" DataSource='<%# Eval("MenuItems") %>'>
                            <ItemTemplate>
                                <h5>
                                    <%# Eval("Description") %>
                                    <%# ((decimal)Eval("Price")).ToString("C") %>
                                    <span class="badge"><%# Eval("Calories") %></span>                                   
                                    <%# Eval("Comment") %>                                    
                                </h5>                                
                            </ItemTemplate>
                        </asp:Repeater>   
                    </div>
                </h3>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:ObjectDataSource ID="ODSCategoryMenuItems" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="MenuCategoryItem_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
</asp:Content>

