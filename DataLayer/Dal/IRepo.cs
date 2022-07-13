using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Dal
{
    public interface IRepo
    {
        IList<Tag> LoadTags();
        IList<City> LoadCities();
        IList<Apartment> LoadApartments();
        IList<Review> LoadReviewsByApartmentId(int apartmentId);
        Apartment LoadApartmentDetails(int apartmentId);
        User LoadReservationByApartmentId(int apartmentId);
        User CkeckAdminUser(string email, string password);
        IList<User> LoadUsers();
        void AddImage(int imageId, int apartmentId, string path = null, string base64image = null, string imageName = "", bool isRepresentative = false);
        void AddReview(int apartmentId, int userId, string details, int stars);
        void AddTag(string name, string typeName);
        void AddTaggedApartment(int apartmentId, int tagId);
        void AddReservation(int apartmentId, int? id, string username = null, string email = null, string phoneNumber = null, string address = null, string details = null);
        void UpdateApartment(Guid guid, string name, int maxAdults, int maxChildren, int totalRooms, string status, int beachDistance, decimal price);
        void DeleteApartment(Guid guid);
        void DeleteTag(Guid guid);
        void DeleteTaggedApartment(int apartmentId, int tagId);
    }
}
