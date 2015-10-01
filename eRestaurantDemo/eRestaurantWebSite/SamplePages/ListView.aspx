<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListView.aspx.cs" Inherits="SamplePages_ListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <table align="Center" style="width: 85%">
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
            <td colspan="2" style="height: 32px">

                <asp:ListView ID="ReservationList" runat="server" DataSourceID="ODSResevations">
                    <AlternatingItemTemplate>
                        <tr style="background-color: #FFFFFF;color: #284775;">
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
                        </tr>
                    </AlternatingItemTemplate>                    
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                            <tr runat="server">
                                <td runat="server">No data, select a&nbsp; event type</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>                    
                    <ItemTemplate>
                        <tr style="background-color: #E0FFFF;color: #333333;">
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
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr runat="server" style="background-color: #E0FFFF;color: #333333;">
                                            <th runat="server">ReservationID</th>
                                            <th runat="server">CustomerName</th>
                                            <th runat="server">ReservationDate</th>
                                            <th runat="server">NumberInParty</th>
                                            <th runat="server">ContactPhone</th>
                                            <th runat="server">ReservationStatus</th>                                            
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
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
                <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvens_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ODSResevations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetResevatoinsByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
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

