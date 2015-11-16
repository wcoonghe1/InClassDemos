<%@ Page Title="Special Events CRUD" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventsAdmin.aspx.cs" Inherits="CommandPages_SpecialEventsAdmin" %>

<%@ Register Src="~/UserControls/MessegeUserControl.ascx" TagPrefix="uc1" TagName="MessegeUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Special Events CRUD Using ListView and ODS</h1>
    <%--added a user controll by dragging the file to this location--%>
    <uc1:MessegeUserControl runat="server" ID="MessegeUserControl" />
    <%--Dont forget to add the primary key in the listview property, DataKeyName--%>
    <asp:ListView ID="SpecialEventsCRUD" runat="server" DataSourceID="ODSSpecialEvents" InsertItemPosition="FirstItem" DataKeyNames="EventCode">
        <AlternatingItemTemplate>
            <tr style="background-color: #b6b3b3;color: #284775;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" class="btn btn-danger" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" class="btn btn-info" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="EventCodeLabel" runat="server" Text='<%# Eval("EventCode") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                </td>                
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="EventCodeTextBox" runat="server" Text='<%# Bind("EventCode") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                </td>                
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #5D7B9D;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="background-color: #999999;">
                <td>
                    <asp:Button ID="InsertButton" class="btn btn-primary" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" class="btn btn-info" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="EventCodeTextBox" runat="server" Text='<%# Bind("EventCode") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                </td>                
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #939393;color: #333333;">
                <td>
                    <asp:Button ID="DeleteButton" class="btn btn-danger" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" class="btn btn-info" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="EventCodeLabel" runat="server" Text='<%# Eval("EventCode") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                </td>                
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #FFFFFF;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #7d8ea1;color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">EventCode</th>
                                <th runat="server">Description</th>
                                <th runat="server">Active</th>                                
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #284775">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6;font-weight: bold;color: #333333;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="EventCodeLabel" runat="server" Text='<%# Eval("EventCode") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Eval("Active") %>' Enabled="false" />
                </td>               
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    
    <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" 
        DataObjectTypeName="eRestaurantSystem.DAL.Entities.SpecialEvent" 
        DeleteMethod="SpecialEvents_Delete"
        InsertMethod="SpecialEvents_Add"
        UpdateMethod="SpecialEvents_Update" 
        SelectMethod="SpecialEvens_List"
        OldValuesParameterFormatString="original_{0}"         
        TypeName="eRestaurantSystem.BLL.AdminController" 
        OnDeleted="CheckForException" 
        OnInserted="CheckForException" 
        OnSelected="CheckForException" 
        OnUpdated="CheckForException">
    </asp:ObjectDataSource>
</asp:Content>

