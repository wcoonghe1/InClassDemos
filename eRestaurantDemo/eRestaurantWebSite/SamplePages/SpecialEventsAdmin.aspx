<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventsAdmin.aspx.cs" Inherits="SamplePages_SpecialEventsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    

    <table style="width: 85%">
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
            <td colspan="2" style="height: 32px"></td>
        </tr>
        <tr>
            <td colspan="2" align="Center"><asp:GridView ID="ResevationList" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ODSResevations" PageSize="15" BackColor="White" CellPadding="4" BorderStyle="None" GridLines="None">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName" >
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ReservationDate" HeaderText="Date" SortExpression="ReservationDate" DataFormatString="{0:MMM dd yyyy}" >
                    <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumberInParty" HeaderText="Size" SortExpression="NumberInParty" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContactPhone" HeaderText="Phone" SortExpression="ContactPhone" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ReservationStatus" HeaderText="Status" SortExpression="ReservationStatus" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No data to display, select an event type.
                </EmptyDataTemplate>
                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="Black" Height="25px" />
                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" Position="TopAndBottom" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvens_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSResevations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetResevatoinsByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
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

