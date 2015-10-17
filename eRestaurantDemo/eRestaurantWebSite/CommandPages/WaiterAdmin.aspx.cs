using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Addtional NameSpacess
using eRestaurantSystem.BLL;//controller
using eRestaurantSystem.DAL.Entities;//entities
using EatIn.UI;//deligate process request
#endregion

public partial class CommandPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessegeUserControl.HandleDataBoundException(e);
    }
    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        //to prpperly interface with our MessegeUserCOntroll we will deligate the execution of this click event under the Messege user controll.
        if (WaiterList.SelectedIndex == 0)
        {
            //issue our own error messege through the user cotrol
            MessegeUserControl.ShowInfo("Please select a Waiter to process");
        }
        else
        { 
            //execute the nessary standerd Lookup code under the controll of the MessegeUserControll
            MessegeUserControl.TryRun((ProcessRequest)GetWaiterInfo);
        }
    }

    public void GetWaiterInfo()
    { 
        //a standerd LookUp Process
        AdminController sysmgr = new AdminController();
        var waiter = sysmgr.Get_Waiter_By_ID(int.Parse(WaiterList.SelectedValue));
        WaiterID.Text = waiter.WaiterID.ToString();
        FirstName.Text = waiter.FirstName;
        LastName.Text = waiter.LastName;
        Address.Text = waiter.Address;
        Phone.Text = waiter.Phone;
        HireDate.Text = waiter.HireDate.ToShortDateString();
        //null field check
        if (waiter.ReleaseDate.HasValue)
        {
            ReleaseDate.Text = waiter.ReleaseDate.ToString();
        }
        else
        {
            ReleaseDate.Text = "";
        }
    }
}