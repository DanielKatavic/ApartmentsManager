using DataLayer.Dal;
using System;
using DataLayer.Models;

namespace RWAproject
{
    public partial class LogInPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnSignIn_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Value;
            string password = txtPassword.Value;

            User adminUser = ((DBRepo)Application["database"]).CkeckAdminUser(email, password);

            if (adminUser is null)
            {
                ShowAlert.Visible = true;
                return;
            }

            Session["user"] = adminUser;
            Response.Redirect("Apartments.aspx");
        }
    }
}