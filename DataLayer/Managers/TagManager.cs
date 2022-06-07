using System.Text;

namespace DataLayer.Managers
{
    public static class TagManager
    {
        public static string CreateTagCard(string tagName, int i, bool isChecked)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine($"<label class=\"form-control\" id=\"LblTagName{i}\" runat=\"server\">{tagName}</label>");
            sb.AppendLine("<div class=\"input-group-text\">");
            sb.AppendLine($"<input type=\"checkbox\" class=\"align-items-baseline\" runat=\"server\" ID=\"CheckBox{i}\" {(isChecked ? "checked" : "")}/>");
            sb.AppendLine("</div></div>");
            return sb.ToString();
        }
    }
}
