using System;

namespace DataLayer.Models
{
    public enum Status
    {
        Occupied,
        Reserved,
        Vacant
    }

    public class Apartment
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public decimal Price { get; set; }
        public string CityName { get; set; }
        public Status Status { get; set; }
        public int PicturesCount { get; set; }
    }
}
