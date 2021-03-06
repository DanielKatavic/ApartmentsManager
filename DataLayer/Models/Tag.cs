using System;

namespace DataLayer.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Count { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public override bool Equals(object obj)
            => obj is Tag other && this.Id.Equals(other.Id);

        public override int GetHashCode()
            => this.Id.GetHashCode();

        public override string ToString()
            => Name;
    }
}
