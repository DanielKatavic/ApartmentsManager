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
            FillStatusDDL();
            FillCityDDL();
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

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = sender as DropDownList;
            Status selectedItem = (Status)Enum.Parse(typeof(Status), dropDownList.SelectedItem.ToString());
            rptApartments.DataSource = selectedItem == Status.Any ? _apartments : _apartments.ToList().Where(a => a.Status == selectedItem);
            rptApartments.DataBind();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = sender as DropDownList;
            string selectedItem = dropDownList.SelectedItem.ToString();
            rptApartments.DataSource = selectedItem == "Any" ? _apartments : _apartments.ToList().Where(a => a.CityName == selectedItem);
            rptApartments.DataBind();
        }

        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList dropDownList = sender as DropDownList;
            string selectedItem = dropDownList.SelectedItem.ToString();
            rptApartments.DataSource = GetSortedApartments(selectedItem);
            rptApartments.DataBind();
        }

        private IOrderedEnumerable<Apartment> GetSortedApartments(string selectedItem)
        {
            switch (selectedItem)
            {
                case "Number of rooms":
                    return _apartments.OrderByDescending(a => a.TotalRooms);
                case "Number of space":
                    return _apartments.OrderByDescending(a => a.MaxChildren + a.MaxAdults);
                case "Price":
                    return _apartments.OrderByDescending(a => a.Price);
            }
            return null;
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            ((IRepo)Application["database"]).DeleteApartment(Guid.Parse(linkButton.CommandArgument));
            Response.Redirect("Apartments.aspx");
        }
    }
}