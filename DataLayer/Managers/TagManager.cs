using System.Text;

namespace DataLayer.Managers
{
    public static class TagManager
    {
        public static string CreateTagCard(string tagName, int i)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine($"<label class=\"form-control\" id=\"LblTagName{i}\" runat=\"server\" for=\"CheckBox{i}\">{tagName}</label>");
            sb.AppendLine($"<input type=\"submit\" runat=\"server\" ID=\"BtnDeleteTag{i}\" value=\"Del\" class=\"btn btn-danger\" />");
            sb.AppendLine("</div>");
            return sb.ToString();
        }
    }
}
