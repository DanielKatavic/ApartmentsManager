using System;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        private const string redirectUrl = "LogInPage.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            string pageName = ContentPlaceHolder.Page.GetType().BaseType.Name;
            NavItems.Visible = pageName != nameof(LogInPage);
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(redirectUrl);
        }
    }
}