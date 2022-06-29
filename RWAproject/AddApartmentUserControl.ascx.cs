using DataLayer.Dal;
using DataLayer.Managers;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class AddApartmentUserControl : System.Web.UI.UserControl
    {
        private const string _imgPath = "/img/";
        private const string ErrorMessage = "<script>alert('Error while uploading file!')</script>";
        private static IList<Tag> _apartmentTags = new List<Tag>();
        private static IList<Tag> _allTags;

        protected void Page_Load(object sender, EventArgs e)
        {
            _allTags = ((IRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                FillTagsDdl();
                FillTagsRpt();
            }
        }

        private void FillTagsDdl()
        {
            TagsDdl.DataSource = _allTags;
            TagsDdl.DataBind();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
            => Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            throw new Exception();
        }

        private void FillTagsRpt()
        {
            TagsRepeater.DataSource = _apartmentTags.Distinct();
            TagsRepeater.DataBind();
        }

        protected void BtnAddTag_Click(object sender, EventArgs e)
        {
            string selectedTag = TagsDdl.SelectedItem.ToString();
            Tag tag = _allTags.FirstOrDefault(t => t.Name == selectedTag);
            _apartmentTags.Add(tag);
            FillTagsRpt();
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
                //SaveImageToDB(combined);
                //AddImageToPanel(Path.Combine(_imgPath, fileName));
            }
            catch
            {
                Response.Write(ErrorMessage);
            }
        }
    }
}