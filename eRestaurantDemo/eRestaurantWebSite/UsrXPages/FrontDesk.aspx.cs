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

        MessegeUserControl.TryRun(() =>
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
    
}