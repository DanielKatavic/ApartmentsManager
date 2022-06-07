using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        public static Apartment Apartment { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateApartment();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(Apartment.Guid);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        internal void FillPanel()
        {
            FillTagsPanel();
            offcanvasTitle.InnerHtml = $"Edit apartment {Apartment.Name}";
            maxAdults.Value = Apartment.MaxAdults.ToString();
            maxChildren.Value = Apartment.MaxChildren.ToString();
            totalRooms.Value = Apartment.TotalRooms.ToString();
        }

        private void FillTagsPanel()
        {
            IList<Tag> selectedTags = Apartment.Tags;
            for (int i = 0; i < selectedTags.Count; i++)
            {
                TagsPanel.Controls.Add(new Literal
                {
                    Text = CreateTagCard(selectedTags[i].Name, i, true)
                });
            }
            IList<Tag> allTags = ((IRepo)Application["database"]).LoadTags();
            IList<Tag> notSelectedTags = selectedTags.Concat(allTags).GroupBy(t => t).Where(g => g.Count() == 1).Select(t => t.Key).ToList();
            for (int i = 0; i < notSelectedTags.Count; i++)
            {
                TagsPanel.Controls.Add(new Literal
                {
                    Text = CreateTagCard(notSelectedTags[i].Name, i, false)
                });
            }
        }

        private string CreateTagCard(string tagName, int i, bool isChecked)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine($"<label class=\"form-control\" id=\"LblTagName{i}\" runat=\"server\">{tagName}</label>");
            sb.AppendLine("<div class=\"input-group-text\">");
            sb.AppendLine($"<input type=\"checkbox\" class=\"align-items-baseline\" runat=\"server\" ID=\"CheckBox{i}\" {(isChecked ? "checked" : "")}/>");
            sb.AppendLine("</div></div>");
            return sb.ToString();
        }

        private void UpdateApartment()
        {
            string status = Request.Form["status"];

            ((IRepo)Application["database"]).UpdateApartment(
                Apartment.Guid,
                int.Parse(maxAdults.Value),
                int.Parse(maxChildren.Value),
                int.Parse(totalRooms.Value),
                status);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }
    }
}