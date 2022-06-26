using DataLayer.Dal;
using DataLayer.Managers;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        private const string _imgPath = "/img/";
        private static int _id = 0;
        private static IList<User> _users;
        private static IList<Tag> _allTags;
        private static IList<DataLayer.Models.Image> _images = new List<DataLayer.Models.Image>();
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
                    _user.Id,
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
            FillTagsPanel();
            FillElements();
            FillStatusDdl();
            FillImages();
        }

        private void FillImages()
        {
            if (Apartment.Images.Count != 0)
            {
                Apartment.Images.ToList().ForEach(i => AddImageToPanel(i.Path));
            }
        }

        private void FillStatusDdl()
        {
            StatusDDL.Items.Add(new ListItem
            {
                Value = Status.Reserved.ToString(),
                Text = Status.Reserved.ToString()
            });
            StatusDDL.Items.Add(new ListItem
            {
                Value = Status.Vacant.ToString(),
                Text = Status.Vacant.ToString()
            });
            StatusDDL.Items.Add(new ListItem
            {
                Value = Status.Occupied.ToString(),
                Text = Status.Occupied.ToString()
            });
        }

        private void FillElements()
        {
            offcanvasTitle.InnerHtml = $"Edit apartment {Apartment.Name}";
            maxAdults.Value = Apartment.MaxAdults.ToString();
            maxChildren.Value = Apartment.MaxChildren.ToString();
            totalRooms.Value = Apartment.TotalRooms.ToString();
        }

        private void FillTagsPanel()
        {
            int tagId = 0;
            
            foreach (Tag tag in _allTags)
            {
                TagsLiteral.Text += TagManager.CreateTagCard(tag.Name, tagId++, Apartment.Tags.Contains(tag));
            }
        }

        private void UpdateApartment(string status)
        {
            ((IRepo)Application["database"]).UpdateApartment(
                Apartment.Guid,
                int.Parse(maxAdults.Value),
                int.Parse(maxChildren.Value),
                int.Parse(totalRooms.Value),
                status);
            _images.ToList().ForEach(i => SaveImageToDB(i));
            _images.Clear();
        }

        protected void BtnAddFile_Click(object sender, EventArgs e)
        {
            if (!FileUpload.HasFile)
            {
                FileUpload.Focus();
                return;
            }

            string fileName = FileUpload.PostedFile.FileName;
            string path = Server.MapPath(_imgPath);
            string combined = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);

            try
            {
                FileUpload.PostedFile.SaveAs(combined);
                _images.Add(new DataLayer.Models.Image { Path = combined });
                AddImageToPanel(Path.Combine(_imgPath, fileName));
            }
            catch
            {
                Response.Write("<script>alert('Error while uploading file!')</script>");
            }
        }

        private void AddImageToPanel(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"carousel-item\">");
            sb.AppendLine($"<img src=\"{path}\" class=\"d-block w-100\" style=\"height: 17em\">");
            sb.AppendLine("<div class=\"carousel-caption d-none d-md-block\">");
            sb.AppendLine($"<input type=\"text\" class=\"image-desc\" runat=\"server\" ID=\"ApartmentDesc{_id++}\" placeholder=\"IMAGE DESCRIPTION\"></asp:input>");
            sb.AppendLine("</div></div>");
            ImagesLiteral.Text += sb.ToString();
        }

        private void SaveImageToDB(DataLayer.Models.Image image)
            => ((DBRepo)Application["database"]).AddImage(
                apartmentId: Apartment.Id,
                path: image.Path,
                imageName: string.Empty,
                isRepresentative: image.IsRepresentative);

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
            FillInputs();
        }

        private void FillInputs()
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
    }
}