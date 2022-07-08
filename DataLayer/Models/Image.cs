namespace DataLayer.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Base64Content { get; set; }
        public bool IsRepresentative { get; set; }
        public bool IsNew { get; set; }
    }
}
