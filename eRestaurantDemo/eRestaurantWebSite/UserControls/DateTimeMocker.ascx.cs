using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eRestaurantSystem.BLL;

public partial class UserControls_DateTimeMocker : System.Web.UI.UserControl
{
    public DateTime MockDate
    {
        get
        {
            DateTime date = DateTime.MinValue;

            //override the default the contense of the text box search date
            DateTime.TryParse(SearchDate.Text, out date);
            return date;
        }
        set 
        {
            SearchDate.Text = value.ToString("yyyy-MM-dd");
        }
    }

    public TimeSpan MockTime
    {
        get
        {
            TimeSpan time = TimeSpan.MinValue;

            //override the default the contense of the text box search date
            TimeSpan.TryParse(SearchDate.Text, out time);
            return time;
        }
        set
        {
            SearchTime.Text = DateTime.Today.Add(value).ToString("HH:mm:ss");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        DateTime info = sysmgr.GetLastBillDateTime();
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        SearchTime.Text = info.ToString("hh:mm:ss");

    }
}