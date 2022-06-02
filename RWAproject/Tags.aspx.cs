using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class Tags : System.Web.UI.Page
    {
        private IList<Tag> _tags;

        protected void Page_Load(object sender, EventArgs e)
        {
            _tags = ((DBRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = _tags;
            rptTags.DataBind();
        }
    }
}