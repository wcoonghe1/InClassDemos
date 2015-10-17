<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="WaiterAdmin.aspx.cs" Inherits="CommandPages_WaiterAdmin" %>


<%@ Register Src="~/UserControls/MessegeUserControl.ascx" TagPrefix="uc1" TagName="MessegeUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Waiter Admin CRUD</h1>
    <uc1:MessegeUserControl runat="server" ID="MessegeUserControl" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Waiter Names"></asp:Label>
    <asp:DropDownList ID="WaiterList" runat="server" AppendDataBoundItems="True">
        <asp:ListItem Value="0">Select A Waiter</asp:ListItem>
    </asp:DropDownList>
    <asp:LinkButton ID="FetchWaiter" runat="server">Fetch Waiter</asp:LinkButton>
    <asp:ObjectDataSource ID="ODSWaiter" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Waiters_List" TypeName="eRestaurantSystem.BLL.AdminController" OnSelected="CheckForException"></asp:ObjectDataSource>

    <table align="center" style="width: 90%">
        <tr>
            <td>ID</td>
            <td>
                <asp:Label ID="WaiterID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>First Name</td>
            <td>
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td>
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Address</td>
            <td>
                <asp:TextBox ID="Address" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Phone</td>
            <td>
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Hire Date</td>
            <td>
                <asp:TextBox ID="HireDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Release Date</td>
            <td>
                <asp:TextBox ID="ReleaseDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="WaiterAdd" runat="server">Insert</asp:LinkButton>
            </td>
            <td>
                <asp:LinkButton ID="WaiterUpdate" runat="server">Update</asp:LinkButton>
            </td>
        </tr>
    </table>

</asp:Content>

