using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWAproject
{
    public partial class TagCard : System.Web.UI.UserControl
    {
        public string TagName { get; set; }
        public bool IsChecked { get; set; }

        public TagCard(string tagName, bool isChecked)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LblTagName.InnerText = TagName;
        }
    }
}