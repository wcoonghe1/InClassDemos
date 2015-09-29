<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventsAdmin.aspx.cs" Inherits="SamplePages_SpecialEventsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    

    <table align="center" style="width: 70%">
        <tr>
            <td align="Right" style="height: 22px; width: 50%">Select an Event:</td>
            <td style="height: 22px">
                <asp:DropDownList ID="SpecialEventList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode">
                <asp:ListItem Value="z">Select Event</asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="FetchRegistrations" runat="server">Fetch Registrations</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="Center"><asp:GridView ID="ResevationList" runat="server"></asp:GridView>
                <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvens_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSResevations" runat="server"></asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:50%">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    

</asp:Content>

