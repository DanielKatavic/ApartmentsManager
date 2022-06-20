using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Managers
{
    public static class ApartmentManager
    {
        private static IList<Apartment> apartmentsList = new List<Apartment>();

        public static IList<Apartment> GetFilteredApartments(string selectedCity, string selectedStatus, IList<Apartment> apartments)
        {
            IList<Apartment> filteredByCity = selectedCity == "Any" ? apartments : apartments.ToList().FindAll(a => a.CityName == selectedCity);
            IList<Apartment> filteredByStatus = selectedStatus == "Any" ? apartments : apartments.ToList().FindAll(a => a.Status.ToString() == selectedStatus);
            apartmentsList = filteredByCity.Concat(filteredByStatus).GroupBy(a => a).Where(g => g.Count() > 1).Select(a => a.Key).ToList();
            return apartmentsList; //return duplicates
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
