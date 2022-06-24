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

        public static Apartment Apartment { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && Apartment != null)
            {
                FillTagsPanel();
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e) 
            => UpdateApartment();

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
                UsersDDL.DataSource = ((IRepo)Application["database"]).LoadUsers();
                UsersDDL.DataBind();
                DisableInputs();
            }
            else
            {
                UsersDDL.Attributes.Add("disabled", "");
                UsersDDL.DataSource = Array.Empty<string>();
                UsersDDL.DataBind();
                EnableInputs();
            }
        }

        private void EnableInputs()
        {
            Username.Attributes.Remove("disabled");
            Email.Attributes.Remove("disabled");
            Address.Attributes.Remove("disabled");
            PhoneNumber.Attributes.Remove("disabled");
        }

        private void DisableInputs()
        {
            Username.Attributes.Add("disabled", "");
            Email.Attributes.Add("disabled", "");
            Address.Attributes.Add("disabled", "");
            PhoneNumber.Attributes.Add("disabled", "");
        }

        protected void UsersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUser = ((DropDownList)sender).SelectedItem.ToString();
            User user = ((IRepo)Application["database"]).LoadUsers().FirstOrDefault(u => u.UserName == selectedUser);
            FillInputs(user);
        }

        private void FillInputs(User user)
        {
            Username.Value = user.UserName;
            Email.Value = user.Email;
            PhoneNumber.Value = user.PhoneNumber;
            Address.Value = user.Address;
        }
    }
}