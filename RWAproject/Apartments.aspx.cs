using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class Apartments : System.Web.UI.Page
    {
        private IList<Apartment> _apartments;
        private static Guid apartmentGuid;
        private static Apartment selectedApartment;

        protected void Page_Load(object sender, EventArgs e)
        {
            _apartments = ((IRepo)Application["database"]).LoadApartments();
            if (!IsPostBack)
            {
                DataLoad();
            }
        }

        private void DataLoad()
        {
            rptApartments.DataSource = _apartments;
            rptApartments.DataBind();
            city.DataSource = _apartments.Select(a => a.CityName).Distinct();
            city.DataBind();
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            apartmentGuid = Guid.Parse(linkButton.CommandArgument);
            selectedApartment = _apartments.ToList().FirstOrDefault(a => a.Guid == apartmentGuid);
            ApartmentsPanel.Visible = true;
            FillPlaceholder();
        }

        private void FillPlaceholder()
        {
            UpdatePanel updatePanel = LoadControl("UpdatePanel.ascx") as UpdatePanel;
            updatePanel.Apartment = selectedApartment;
            updatePanel.ApartmentsPanel = ApartmentsPanel;
            PanelPlaceholder.Controls.Add(updatePanel);
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}