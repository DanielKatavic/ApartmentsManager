using System;

namespace DataLayer.Models
{
    public class Tag
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Count { get; set; }
    }
}
