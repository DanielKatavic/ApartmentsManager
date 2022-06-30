using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class AddApartmentUserControl : System.Web.UI.UserControl
    {
        private const string _imgPath = "/img/";
        private const string ErrorMessage = "<script>alert('Error while uploading file!')</script>";
        private static IList<City> _allCities;
        private static IList<Tag> _allTags;
        private static Apartment _apartment = new Apartment();

        protected void Page_Load(object sender, EventArgs e)
        {
            _allCities = ((DBRepo)Application["database"]).LoadCities();
            _allTags = ((DBRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                _apartment.Images = new List<DataLayer.Models.Image>();
                _apartment.Tags = new List<Tag>();
                FillCityDdl();
                FillTagsRpt();
                FillTagsDdl();
            }
        }

        private void FillTagsDdl()
        {
            AllTagsDdl.DataSource = _allTags;
            AllTagsDdl.DataBind();
        }

        private void FillCityDdl()
        {
            CityDDl.DataSource = _allCities;
            CityDDl.DataBind();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
            => Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            string selectedCity = CityDDl.SelectedValue;
            City city = _allCities.FirstOrDefault(c => c.Name == selectedCity);

            int insertedId = ((DBRepo)Application["database"]).AddApartment(
                cityId: city.Id,
                name: apartmentName.Value,
                price: int.Parse(price.Value),
                maxAdults: int.Parse(maxAdults.Value),
                maxChildren: int.Parse(maxChildren.Value),
                totalRooms: int.Parse(totalRooms.Value),
                beachDistance: int.Parse(distanceFromSea.Value));

            _apartment.Images.ToList().ForEach(i => SaveImageToDB(i));
            _apartment.Images.Clear();

            _apartment.Tags.ToList().ForEach(t => SaveTagToDB(t, insertedId));
            _apartment.Tags.Clear();

            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        private void SaveImageToDB(DataLayer.Models.Image image)
            => ((DBRepo)Application["database"]).AddImage(
                apartmentId: _apartment.Id,
                path: image.Path,
                imageName: string.Empty,
                isRepresentative: image.IsRepresentative);

        private void SaveTagToDB(Tag tag, int insertedId)
            => ((DBRepo)Application["database"]).AddTaggedApartment(
                apartmentId: insertedId,
                tagId: tag.Id);

        protected void BtnAddFile_Click(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                FileUpload.Focus();
                return;
            }

            string fileName = FileUpload.PostedFile.FileName;
            string combined = Path.Combine(_imgPath, fileName);

            try
            {
                _apartment.Images.Add(new DataLayer.Models.Image { Path = combined });
                FillImages();
            }
            catch
            {
                Response.Write(ErrorMessage);
            }
        }

        private void FillImages()
        {
            ImagesRpt.DataSource = _apartment.Images;
            ImagesRpt.DataBind();
        }

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            string selectedTag = AllTagsDdl.SelectedItem.ToString();
            Tag tag = _allTags.FirstOrDefault(t => t.Name == selectedTag);
            _apartment.Tags.Add(tag);
            FillTagsRpt();
        }

        protected void BtnRemoveTag_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int tagId = int.Parse(linkButton.CommandArgument);
            _apartment.Tags.Remove(_apartment.Tags.FirstOrDefault(t => t.Id == tagId));
            FillTagsRpt();
        }

        private void FillTagsRpt()
        {
            TagsRepeater.DataSource = _apartment.Tags;
            TagsRepeater.DataBind();
        }
    }
}