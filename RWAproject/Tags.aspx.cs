using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class Tags : System.Web.UI.Page
    {
        private IList<Tag> _tags;
        private static IList<LinkButton> buttons = new List<LinkButton>();

        protected void Page_Load(object sender, EventArgs e)
        {
            _tags = ((IRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                LoadData();
                ShowDeleteButtons();
            }
        }

        private void ShowDeleteButtons()
        {
            for(int i = 0; i < rptTags.Items.Count; i++)
            {
                Button button = (Button)rptTags.Items[i].FindControl("BtnDeleteTag");
                if (_tags[i].Count == 0)
                {
                    button.Visible = true;
                }
            }
        }

        private void LoadData()
        {
            _tags = _tags.OrderBy(t => t.Name).ToList();
            rptTags.DataSource = _tags;
            rptTags.DataBind();
            ddlTypeName.DataSource = _tags.ToList().Select(t => t.TypeName).Distinct();
            ddlTypeName.DataBind();
        }

        protected void BtnShowPanel_Click(object sender, EventArgs e)
        {
            TagAddPanel.Visible = true;
        }

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            string tagName = TagName.Value;
            string selectedType = ddlTypeName.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(tagName))
            {
                TagName.Focus();
                return;
            }
            ((IRepo)Application["database"]).AddTag(tagName, selectedType);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        protected void BtnDeleteTag_Click(object sender, EventArgs e)
        {
            Guid tagGuid = Guid.Parse(((LinkButton)sender).CommandArgument);
            ((IRepo)Application["database"]).DeleteTag(tagGuid);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            TagAddPanel.Visible = false;
        }
    }
}