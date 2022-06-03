using System;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pageName = ContentPlaceHolder.Page.GetType().BaseType.Name;
            NavItems.Visible = pageName != nameof(LogInPage);
        }
    }
}