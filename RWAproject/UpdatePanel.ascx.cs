using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        public Apartment Apartment { get; set; }
        public Panel ApartmentsPanel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillPanel();
        }

        private void UpdateApartment()
        {
            string status = Request.Form["options"];

            ((IRepo)Application["database"]).UpdateApartment(
                Apartment.Guid,
                int.Parse(maxAdults.Value),
                int.Parse(maxChildren.Value),
                int.Parse(totalRooms.Value),
                status);
            Response.Redirect("Apartments.aspx");
        }

        private void FillPanel()
        {
            offcanvasTitle.InnerHtml = $"Edit apartment {Apartment.Name}";
            maxAdults.Value = Apartment.MaxAdults.ToString();
            maxChildren.Value = Apartment.MaxChildren.ToString();
            totalRooms.Value = Apartment.TotalRooms.ToString();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateApartment();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(Apartment.Guid);
            Response.Redirect("Apartments.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            ApartmentsPanel.Visible = false;
        }
    }
}