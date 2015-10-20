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
        if (Page.IsPostBack)
        {
            HireDate.Text = DateTime.Today.ToShortDateString();
        }
        
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
    protected void WaiterUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(WaiterID.Text))
        {
            MessegeUserControl.ShowInfo("Please Select a Waiter");
        }
        else
        {
            
            Waiter item = new Waiter();
            item.WaiterID = int.Parse(WaiterID.Text);
            item.FirstName = FirstName.Text;
            item.LastName = LastName.Text;
            item.Address = Address.Text;
            item.Phone = Phone.Text;
            item.HireDate = DateTime.Parse(HireDate.Text);

            if (string.IsNullOrEmpty(ReleaseDate.Text))
            {
                item.ReleaseDate = null;
            }
            else
            {
                item.ReleaseDate = DateTime.Parse(ReleaseDate.Text);
            }
            AdminController sysmgr = new AdminController();
            sysmgr.Waiter_Update(item);
            MessegeUserControl.ShowInfo("Waiter Updated.");
        
        }
    }
    protected void WaiterAdd_Click(object sender, EventArgs e)
    {
        //inline version of using messegeUserCOntroll
        MessegeUserControl.TryRun(() =>
            //remainder of the code is what would have gone in the externel method of ProcessRequest(Method Name)
            {
                Waiter item = new Waiter();
                item.FirstName = FirstName.Text;
                item.LastName = LastName.Text;
                item.Address = Address.Text;
                item.Phone = Phone.Text;
                item.HireDate = DateTime.Parse(HireDate.Text);

                if (string.IsNullOrEmpty(ReleaseDate.Text))
                {
                    item.ReleaseDate = null;
                }
                else 
                {
                    item.ReleaseDate = DateTime.Parse(ReleaseDate.Text);
                }
                AdminController sysmgr = new AdminController();
                WaiterID.Text = sysmgr.Waiter_Add(item).ToString();
                MessegeUserControl.ShowInfo("Waiter Added. ");                
            }
            );
    }
}