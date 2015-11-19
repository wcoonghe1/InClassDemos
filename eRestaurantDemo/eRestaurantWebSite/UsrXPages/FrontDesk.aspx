﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FrontDesk.aspx.cs" Inherits="UsrXPages_FrontDesk" %>


<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>
<%@ Register Src="~/UserControls/DateTimeMocker.ascx" TagPrefix="uc1" TagName="DateTimeMocker" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">    
    
    <uc1:DateTimeMocker runat="server" id="Mocker" />  
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    <div class="col-md-7">
        <details open>
            <summary>Tables</summary>
            <asp:GridView ID="SeatingGridView" runat="server" AutoGenerateColumns="False"
                DataSourceID="SeatingObjectDataSource" ItemType="eRestaurantSystem.DAL.POCOs.SeatingSummary" OnSelectedIndexChanged="SeatingGridView_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Table" HeaderText="Table" SortExpression="Table"></asp:BoundField>
                    <asp:BoundField DataField="Seating" HeaderText="Seating" SortExpression="Seating"></asp:BoundField>
                    <asp:CheckBoxField DataField="Taken" HeaderText="Taken" SortExpression="Taken" ItemStyle-HorizontalAlign="Center"></asp:CheckBoxField>
                    <asp:BoundField DataField="BillID" HeaderText="BillID" SortExpression="BillID"></asp:BoundField>
                    <asp:BoundField DataField="BillTotal" HeaderText="BillTotal" SortExpression="BillTotal"></asp:BoundField>
                    <asp:BoundField DataField="Waiter" HeaderText="Waiter" SortExpression="Waiter"></asp:BoundField>
                    <asp:BoundField DataField="ReservationName" HeaderText="ReservationName" SortExpression="ReservationName"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </details>
    </div>
    <div class="pull-right col-md-5">
        <details open>
            <summary>Reservations by Date/Time</summary>
            <h4>Today's Reservations</h4>
            <asp:Repeater ID="ReservationsRepeater" runat="server"
                ItemType="eRestaurantSystem.DAL.POCOs.ReservationCollection" DataSourceID="ReservationsDataSource">
                <ItemTemplate>
                    <div>
                        <h4><%# Item.Time %></h4>
                        <asp:ListView ID="ReservationSummaryListView" runat="server"
                                ItemType="eRestaurant.Entities.DTOs.ReservationSummary"
                                DataSource="<%# Item.Reservations %>">
                            <LayoutTemplate>
                                <div class="seating">
                                    <span runat="server" id="itemPlaceholder" />
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div>
                                    <%# Item.Name %> —
                                    <%# Item.NumberInParty %> —
                                    <%# Item.Status %> —
                                    PH:
                                    <%# Item.Contact %>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource runat="server" ID="ReservationsDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ReservationsByTime" TypeName="eRestaurant.BLL.SeatingController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Mocker" PropertyName="MockDate" Name="date" Type="DateTime"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </details>
    </div>
    <asp:ObjectDataSource runat="server" ID="SeatingObjectDataSource" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SeatingByDateTime" TypeName="eRestaurant.BLL.SeatingController">
        <SelectParameters>
            <asp:ControlParameter ControlID="Mocker" PropertyName="MockDate" Name="date" Type="DateTime"></asp:ControlParameter>
            <asp:ControlParameter ControlID="Mocker" PropertyName="MockTime" DbType="Time" Name="time"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

