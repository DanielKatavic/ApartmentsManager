using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace RWAproject
{
    public partial class RegisteredUsers : System.Web.UI.Page
    {
        private IList<User> _users;

        protected void Page_Load(object sender, EventArgs e)
        {
            _users = ((IRepo)Application["database"]).LoadUsers();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Session["user"] is null) Response.Redirect("LogInPage.aspx");
            base.OnLoad(e);
        }

        private void LoadData()
        {
            rptUsers.DataSource = _users;
            rptUsers.DataBind();
        }
    }
}