using DataLayer.Dal;
using DataLayer.Managers;
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

        protected override void OnLoad(EventArgs e)
        {
            //if (Session["user"] is null) Response.Redirect("LogInPage.aspx");
            base.OnLoad(e);
        }

        private void DataLoad()
        {
            FillRptApartments();
            FillStatusDDL();
            FillCityDDL();
        }

        private void FillRptApartments()
        {
            rptApartments.DataSource = _apartments;
            rptApartments.DataBind();
        }

        private void FillCityDDL()
        {
            ddlCity.DataSource = _apartments.Select(a => a.CityName).Distinct();
            ddlCity.DataBind();
            ddlCity.Items.Add(new ListItem { Selected = true, Value = "Any" });
        }

        private void FillStatusDDL()
        {
            ddlStatus.DataSource = _apartments.Select(a => a.Status).Distinct();
            ddlStatus.DataBind();
            ddlStatus.Items.Add(new ListItem { Selected = true, Value = Status.Any.ToString() });
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            apartmentGuid = Guid.Parse(linkButton.CommandArgument);
            selectedApartment = _apartments.ToList().FirstOrDefault(a => a.Guid == apartmentGuid);
            ApartmentsPanel.Visible = true;
            FillUserControl();
        }

        private void FillUserControl()
        {
            UpdatePanel.Apartment = selectedApartment;
            updatePanel.FillPanel();
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCity = ddlCity.SelectedItem.ToString();
            string selectedStatus = ddlStatus.SelectedItem.ToString();

            rptApartments.DataSource = ApartmentManager.GetFilteredApartments(selectedCity, selectedStatus, _apartments); 
            rptApartments.DataBind();
        }

        protected void DdlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = ddlSortBy.SelectedItem.ToString();
            rptApartments.DataSource = ApartmentManager.GetSortedApartments(selectedItem, _apartments);
            rptApartments.DataBind();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            ((IRepo)Application["database"]).DeleteApartment(Guid.Parse(linkButton.CommandArgument));
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }
    }
}