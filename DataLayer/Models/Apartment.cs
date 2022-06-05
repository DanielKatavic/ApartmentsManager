﻿using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public enum Status
    {
        Any,
        Occupied,
        Reserved,
        Vacant
    }

    [Serializable]
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
        public IList<Tag> Tags { get; set; }

        public override bool Equals(object obj) 
            => obj is Apartment other && Guid.Equals(other.Guid);

        public override int GetHashCode() 
            => Guid.GetHashCode();
    }
}
