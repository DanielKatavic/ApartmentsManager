using DataLayer.Dal;
using DataLayer.Managers;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class AddApartmentUserControl : System.Web.UI.UserControl
    {
        private const string _imgPath = "~/img";

        protected void Page_Load(object sender, EventArgs e)
            => FillTagsPanel();

        protected void BtnClose_Click(object sender, EventArgs e)
            => Response.Redirect($"{Page.GetType().BaseType.Name}.aspx");

        protected void BtnAdd_Click(object sender, EventArgs e)
        {

        }

        private void FillTagsPanel()
        {
            int tagId = 0;
            IList<Tag> allTags = ((IRepo)Application["database"]).LoadTags();

            foreach (Tag tag in allTags)
            {
                TagsPanel.Controls.Add(new Literal
                {
                    Text = TagManager.CreateTagCard(tag.Name, tagId++)
                });
            }
        }

        protected void BtnAddFile_Click(object sender, EventArgs e)
        {
            string fileName = FileUpload.PostedFile.FileName;
            string path = Server.MapPath(_imgPath);
            string combined = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);
            
            try
            {
                FileUpload.PostedFile.SaveAs(combined);
                AddImageToPanel(combined);
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
            sb.AppendLine("<h5>Apartment interior</h5>");
            sb.AppendLine("</div></div>");
            ImagesLiteral.Text = sb.ToString();
        }
    }
}