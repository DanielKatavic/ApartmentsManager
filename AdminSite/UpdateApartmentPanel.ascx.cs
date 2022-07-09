using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        private const string _imgPath = "/img/";
        private const string ErrorMessage = "<script>alert('Error while uploading file!')</script>";
        private static IList<User> _users;
        private static IList<Tag> _allTags;
        private static User _user;
        private IList<HtmlControl> _inputs;

        public static Apartment Apartment { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            _inputs = new List<HtmlControl> { Username, Email, PhoneNumber, Address };
            _allTags = ((IRepo)Application["database"]).LoadTags();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string status = StatusDDL.SelectedItem.ToString();
            UpdateApartment(status);
            AddReservation(status);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        private void AddReservation(string status)
        {
            if (status == Status.Reserved.ToString() || status == Status.Occupied.ToString())
            {
                if (_user is null)
                {
                    ((IRepo)Application["database"]).AddReservation(
                    Apartment.Id,
                    null,
                    Username.Value,
                    Email.Value,
                    PhoneNumber.Value,
                    Address.Value,
                    Details.Value);
                }
                else
                {
                    ((IRepo)Application["database"]).AddReservation(
                    Apartment.Id,
                    int.Parse(_user.Id),
                    Details.Value);
                    _user = null;
                }
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).DeleteApartment(Apartment.Guid);
            Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
        }

        protected void BtnClose_Click(object sender, EventArgs e)
            => Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");

        internal void FillPanel()
        {
            FillTagsRpt();
            FillApartmentInfo();
            FillStatusDdl();
            FillTagsDdl();
            FillImages();
        }

        private void FillTagsDdl()
        {
            AllTagsDdl.DataSource = _allTags;
            AllTagsDdl.DataBind();
        }

        private void FillImages()
        {
            FirstImage.Src = Apartment.Images[0].Path;
            ImagesRpt.DataSource = Apartment.Images.Skip(1);
            ImagesRpt.DataBind();
        }

        private void FillStatusDdl()
        {
            string[] statusList = Enum.GetNames(typeof(Status));
            StatusDDL.DataSource = statusList.Where(s => s != Status.Any.ToString());
            StatusDDL.DataBind();
        }

        private void FillApartmentInfo()
        {
            offcanvasTitle.InnerHtml = $"Edit apartment {Apartment.Name}";
            apartmentName.Value = Apartment.Name;
            price.Value = Apartment.Price.ToString();
            maxAdults.Value = Apartment.MaxAdults.ToString();
            maxChildren.Value = Apartment.MaxChildren.ToString();
            totalRooms.Value = Apartment.TotalRooms.ToString();
            distanceFromSea.Value = Apartment.BeachDistance.ToString();
        }

        private void FillTagsRpt()
        {
            TagsRepeater.DataSource = Apartment.Tags.Where(t => !t.IsDeleted).Distinct();
            TagsRepeater.DataBind();
        }

        private void UpdateApartment(string status)
        {
            ((IRepo)Application["database"]).UpdateApartment(
                Apartment.Guid,
                apartmentName.Value,
                int.Parse(maxAdults.Value),
                int.Parse(maxChildren.Value),
                int.Parse(totalRooms.Value),
                status,
                int.Parse(distanceFromSea.Value),
                decimal.Parse(price.Value));

            Apartment.Images.Where(i => i.IsNew).ToList().ForEach(i => SaveImageToDB(i));
            Apartment.Tags.Where(t => t.IsNew).ToList().ForEach(t => SaveTagToDB(t));
            Apartment.Tags.Where(t => t.IsDeleted).ToList().ForEach(t => DeleteTagFromDB(t));
        }

        protected void BtnAddFile_Click(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                FileUpload.Focus();
                return;
            }

            string fileName = FileUpload.PostedFile.FileName;
            string absolutePath = Server.MapPath(_imgPath);
            string combinedAP = Path.Combine(absolutePath, fileName);
            string combinedRP = Path.Combine(_imgPath, fileName);

            Directory.CreateDirectory(absolutePath);

            try
            {
                FileUpload.PostedFile.SaveAs(combinedAP);
                Apartment.Images.Add(new DataLayer.Models.Image { Path = combinedRP, IsNew = true });
                FillImages();
            }
            catch
            {
                Response.Write(ErrorMessage);
            }
        }

        private void SaveImageToDB(DataLayer.Models.Image image)
            => ((DBRepo)Application["database"]).AddImage(
                apartmentId: Apartment.Id,
                path: image.Path,
                imageName: string.Empty,
                isRepresentative: image.IsRepresentative);

        private void SaveTagToDB(Tag tag)
            => ((DBRepo)Application["database"]).AddTaggedApartment(
                apartmentId: Apartment.Id,
                tagId: tag.Id);

        private void DeleteTagFromDB(Tag tag)
            => ((DBRepo)Application["database"]).DeleteTaggedApartment(
                apartmentId: Apartment.Id,
                tagId: tag.Id);

        protected void ChbRegisteredUser_CheckedChanged(object sender, EventArgs e)
        {
            if (ChbRegisteredUser.Checked)
            {
                UsersDDL.Attributes.Remove("disabled");
                _users = ((IRepo)Application["database"]).LoadUsers();
                UsersDDL.DataSource = _users;
                UsersDDL.DataBind();
                AddAttributeToInputs("disabled");
            }
            else
            {
                _user = null;
                UsersDDL.Attributes.Add("disabled", "");
                UsersDDL.DataSource = Array.Empty<string>();
                UsersDDL.DataBind();
                RemoveAttributeFromInputs("disabled");
            }
        }

        protected void UsersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUser = ((DropDownList)sender).SelectedItem.ToString();
            _user = _users.FirstOrDefault(u => u.UserName == selectedUser);
            FillUserInfo();
        }

        private void FillUserInfo()
        {
            Username.Value = _user.UserName;
            Email.Value = _user.Email;
            PhoneNumber.Value = _user.PhoneNumber;
            Address.Value = _user.Address;
        }

        protected void StatusDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedItem.ToString() == Status.Vacant.ToString())
            {
                ChbRegisteredUser.Enabled = false;
                UsersDDL.Attributes.Add("disabled", "");
                AddAttributeToInputs("disabled");
                RemoveAttributeFromInputs("required");
            }
            else
            {
                ChbRegisteredUser.Enabled = true;
                RemoveAttributeFromInputs("disabled");
                AddAttributeToInputs("required");
            }
        }

        private void AddAttributeToInputs(string tag)
            => _inputs.ToList().ForEach(i => i.Attributes.Add(tag, ""));

        private void RemoveAttributeFromInputs(string tag)
            => _inputs.ToList().ForEach(i => i.Attributes.Remove(tag));

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            string selectedTag = AllTagsDdl.SelectedItem.ToString();
            Tag tag = _allTags.FirstOrDefault(t => t.Name == selectedTag);
            tag.IsNew = true;
            tag.IsDeleted = false;
            Apartment.Tags.Add(tag);
            FillTagsRpt();
        }

        protected void BtnRemoveTag_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int tagId = int.Parse(linkButton.CommandArgument);
            Tag tag = Apartment.Tags.FirstOrDefault(t => t.Id == tagId);
            tag.IsNew = false;
            tag.IsDeleted = true;
            FillTagsRpt();
        }
    }
}