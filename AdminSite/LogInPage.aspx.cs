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

        protected override void OnLoad(EventArgs e)
        {
            if (Session["user"] != null) Response.Redirect("Apartments.aspx");
            base.OnLoad(e);
        }

        protected void BtnSignIn_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Value;
            string password = txtPassword.Value;

            User adminUser = ((DBRepo)Application["database"]).CkeckAdminUser(
                email: email,
                password: HashPassword(password));

            if (adminUser is null)
            {
                ShowAlert.Visible = true;
                return;
            }

            Session["user"] = adminUser;
            Response.Redirect("Apartments.aspx");
        }

        private string HashPassword(string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}