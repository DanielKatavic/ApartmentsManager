using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Managers
{
    public static class ApartmentManager
    {
        private const string PriceLowToHigh = "LTH";
        private static IList<Apartment> apartmentsList = new List<Apartment>();

        public static IList<Apartment> GetFilteredApartments(string selectedCity, string selectedStatus, IList<Apartment> apartments)
        {
            IList<Apartment> filteredByCity = selectedCity == "Any" ? apartments : apartments.ToList().FindAll(a => a.CityName == selectedCity);
            IList<Apartment> filteredByStatus = selectedStatus == "Any" ? apartments : apartments.ToList().FindAll(a => a.Status.ToString() == selectedStatus);
            apartmentsList = filteredByCity.Concat(filteredByStatus).GroupBy(a => a).Where(g => g.Count() > 1).Select(a => a.Key).ToList();
            return apartmentsList; //return duplicates
        }

        public static void OrderApartments(string order, ref IList<Apartment> apartments)
        {
            if (!string.IsNullOrEmpty(order))
            {
                apartments = order == PriceLowToHigh ? apartments.OrderBy(a => a.Price).ToList() : apartments.OrderByDescending(a => a.Price).ToList();
            }
        }

        public static void FilterApartments(string cityName, string rooms, string adults, string children, ref IList<Apartment> apartments)
        {
            if (cityName != "Any")
            {
                apartments = apartments.Where(a => a.CityName == cityName).ToList();
            }
            if (!string.IsNullOrWhiteSpace(rooms))
            {
                apartments = apartments.Where(a => a.TotalRooms == int.Parse(rooms)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(adults))
            {
                apartments = apartments.Where(a => a.MaxAdults == int.Parse(adults)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(children))
            {
                apartments = apartments.Where(a => a.MaxChildren == int.Parse(children)).ToList();
            }
        }

        public static IOrderedEnumerable<Apartment> GetSortedApartments(string selectedItem, IList<Apartment> apartments)
        {
            if (apartmentsList.Count == 0) apartmentsList = apartments;
            
            switch (selectedItem)
            {
                case "Number of rooms":
                    return apartmentsList.OrderByDescending(a => a.TotalRooms);
                case "Number of space":
                    return apartmentsList.OrderByDescending(a => a.MaxChildren + a.MaxAdults);
                case "Price":
                    return apartmentsList.OrderByDescending(a => a.Price);
            }
            return null;
        }
    }
}
