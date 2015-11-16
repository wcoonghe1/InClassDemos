<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListView.aspx.cs" Inherits="SamplePages_ListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <table align="center" style="width: 100%">
        <tr>
            <td align="center" style="height: 22px;">Select an Event:</td>
            <td style="height: 22px">
                <asp:DropDownList ID="SpecialEventList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode">
                <asp:ListItem Value="z">Select Event</asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="FetchRegistrations" runat="server">Fetch Registrations</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="height: 32px">

                <asp:ListView ID="ReservationList" runat="server" DataSourceID="ODSResevations">
                    <AlternatingItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label ID="ReservationIDLabel" runat="server" Text='<%# Eval("ReservationID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CustomerNameLabel" runat="server" Text='<%# Eval("CustomerName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ReservationDateLabel" runat="server" Text='<%# Eval("ReservationDate") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NumberInPartyLabel" runat="server" Text='<%# Eval("NumberInParty") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ContactPhoneLabel" runat="server" Text='<%# Eval("ContactPhone") %>' />
                            </td>                            
                            <td>
                                <asp:Label ID="ReservationStatusLabel" runat="server" Text='<%# Eval("ReservationStatus") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EventCodeLabel" runat="server" Text='<%# Eval("EventCode") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EventLabel" runat="server" Text='<%# Eval("Event") %>' />
                            </td>
                            <td>
                                <asp:Label ID="TablesLabel" runat="server" Text='<%# Eval("Tables") %>' />
                            </td>
                            <td>
                                <asp:Label ID="BillsLabel" runat="server" Text='<%# Eval("Bills") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate> 
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>                    
                    
                    <ItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label ID="ReservationIDLabel" runat="server" Text='<%# Eval("ReservationID") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CustomerNameLabel" runat="server" Text='<%# Eval("CustomerName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ReservationDateLabel" runat="server" Text='<%# Eval("ReservationDate") %>' />
                            </td>
                            <td>
                                <asp:Label ID="NumberInPartyLabel" runat="server" Text='<%# Eval("NumberInParty") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ContactPhoneLabel" runat="server" Text='<%# Eval("ContactPhone") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ReservationStatusLabel" runat="server" Text='<%# Eval("ReservationStatus") %>' />
                            </td>                           
                            <td>
                                <asp:Label ID="EventCodeLabel" runat="server" Text='<%# Eval("EventCode") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EventLabel" runat="server" Text='<%# Eval("Event") %>' />
                            </td>
                            <td>
                                <asp:Label ID="TablesLabel" runat="server" Text='<%# Eval("Tables") %>' />
                            </td>
                            <td>
                                <asp:Label ID="BillsLabel" runat="server" Text='<%# Eval("Bills") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                        <tr runat="server" style="">
                                            <th runat="server">ReservationID</th>
                                            <th runat="server">CustomerName</th>
                                            <th runat="server">ReservationDate</th>
                                            <th runat="server">NumberInParty</th>
                                            <th runat="server">ContactPhone</th>
                                            <th runat="server">ReservationStatus</th>
                                            <th runat="server">EventCode</th>
                                            <th runat="server">Event</th>
                                            <th runat="server">Tables</th>
                                            <th runat="server">Bills</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="">
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="Center">
                
            </td>
        </tr>
        <tr>
                <td colspan="2" align="Center" style="width: 50%">
                <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="SpecialEvens_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSResevations" runat="server" OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="GetResevatoinsByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </td>
            
        </tr>
        <tr>
            <td colspan="2" align="Center" style="width: 50%">
                
            </td>
            
        </tr>
        <tr>
            <td style="width:50%">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

