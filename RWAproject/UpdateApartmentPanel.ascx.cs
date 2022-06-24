using DataLayer.Dal;
using DataLayer.Managers;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class UpdatePanel : UserControl
    {
        private const string _imgPath = "/img/";
        private static IList<User> _users;
        private static User _user;

        public static Apartment Apartment { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && Apartment != null)
            {
                FillTagsPanel();
            }
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
            IList<Tag> allTags = ((IRepo)Application["database"]).LoadTags();

            foreach (Tag tag in allTags)
            {
                TagsPanel.Controls.Add(new Literal
                {
                    Text = TagManager.CreateTagCard(tag.Name, tagId++, Apartment.Tags.Contains(tag))
                });
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
                SaveImageToDB(combined);
                //AddImageToPanel(combined);
                Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");
            }
            catch
            {
                Response.Write("<script>alert('Error while uploading file!')</script>");
            }
        }

        private void SaveImageToDB(string combined)
        {
            byte[] imageArray = File.ReadAllBytes(combined);
            string base64image = Convert.ToBase64String(imageArray);

            ((DBRepo)Application["database"]).AddImage(Apartment.Guid, base64image);
        }

        private void LoadImage(string base64image)
        {
            File.WriteAllBytes(_imgPath, Convert.FromBase64String(base64image));
        }

        protected void ChbRegisteredUser_CheckedChanged(object sender, EventArgs e)
        {
            if (ChbRegisteredUser.Checked)
            {
                UsersDDL.Attributes.Remove("disabled");
                _users = ((IRepo)Application["database"]).LoadUsers();
                UsersDDL.DataSource = _users;
                UsersDDL.DataBind();
                AddTagToElement("disabled");
            }
            else
            {
                _user = null;
                UsersDDL.Attributes.Add("disabled", "");
                UsersDDL.DataSource = Array.Empty<string>();
                UsersDDL.DataBind();
                RemoveTagFromElement("disabled");
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
                AddTagToElement("disabled");
                RemoveTagFromElement("required");
            }
            else
            {
                ChbRegisteredUser.Enabled = true;
                RemoveTagFromElement("disabled");
                AddTagToElement("required");
            }
        }

        private void AddTagToElement(string tag)
        {
            Username.Attributes.Add(tag, "");
            Email.Attributes.Add(tag, "");
            Address.Attributes.Add(tag, "");
            PhoneNumber.Attributes.Add(tag, "");
        }

        private void RemoveTagFromElement(string tag)
        {
            Username.Attributes.Remove(tag);
            Email.Attributes.Remove(tag);
            Address.Attributes.Remove(tag);
            PhoneNumber.Attributes.Remove(tag);
        }
    }
}