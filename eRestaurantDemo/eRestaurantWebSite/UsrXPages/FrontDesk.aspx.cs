using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;

public partial class UsrXPages_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    protected void SeatingGridView_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
    {
        //extarct the table number in the party and the awaiter ID from the grid view
        //will also create the time from the mockDateTime contrtorls at the top of this form
        //once the date uis collected then it will ne sent to the BLL for processeing
        //the comman will be done under the control of the MessegeUserControll
        //so if there is an error the MUS will handle it.
        // we wll use the Inline MUC tryRun technique

        MessageUserControl.TryRun(() =>
        {
            //obtain the selected grid view
            GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
            //asscessing a wen control on the gridview row
            //uses .FindControl("xxx") as a datatype
            string tablenumber = (agvrow.FindControl("TableNumber") as Label).Text;
            string numberinparty = (agvrow.FindControl("NumberInParty")as TextBox).Text;
            string waiterID = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
            DateTime when = Mocker.MockDate.Add(Mocker.MockTime);

            //standerd call insert a record to the DB
            AdminController sysmgr = new AdminController();
            sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty), int.Parse(waiterID));

            //refresh the gridview
            SeatingGridView.DataBind();
         }, "Customer Seated", "New Wall-in Customer has been saved"
        );
    }

    protected void ReservationSummaryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //this is the mthod which will gather the seating  information for reservation and pass to the BLL for proceosessing

        if (e.CommandName.Equals("Seat"))
        { 
            //execuion if the code wukk ve uynder tghe controll
            MessageUserControl.TryRun(() =>
            {
                int reservationid = int.Parse(e.CommandArgument.ToString());
                    int waiterid = int.Parse(WaiterDropDownList.SelectedValue);
                    DateTime when = Mocker.MockDate.Add(Mocker.MockTime);
                List<byte> selectedTables = new List<byte>();
                //walk throug the list box row by row
                foreach (ListItem item_tableid in ReservationTableListBox.Items)
                {
                    if (item_tableid.Selected)
                    {
                        selectedTables.Add(byte.Parse(item_tableid.Text.Replace("Table ", "")));
                    }
                }

                //with all data gatherd, connet to your library controller, and send data for processing
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when reservationid, selectedTables, waiterid);

                SeatingGridView.DataBind();
                ReservationsRepeater.DataBind();
            }, "customer Seated", "Reservation  customer has been saeated");
        }

    }
    protected bool ShowReservationSeating()
    {
        bool anyReservationsToday = false;
        MessageUserControl(() =>
                {
                    DateTime when = Mocker.MockDate.Add
                        (Mocker.MockTIme);
                    AdminController system = new AdminController();
                    anyReservationsToday = sysmgr.ReservationForToday(when);
                }

            );
        return anyReservationsToday;
    }
}