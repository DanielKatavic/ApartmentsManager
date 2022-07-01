using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Dal
{
    public interface IRepo
    {
        IList<Tag> LoadTags();
        IList<Apartment> LoadApartments();
        Apartment LoadApartmentDetails(int apartmentId);
        User CkeckAdminUser(string email, string password);
        IList<User> LoadUsers();
        void AddTag(string name, string typeName);
        void AddReservation(int apartmentId, int? id, string username = null, string email = null, string phoneNumber = null, string address = null, string details = null);
        void UpdateApartment(Guid guid, int maxAdults, int maxChildren, int totalRooms, string status);
        void DeleteApartment(Guid guid);
        void DeleteTag(Guid guid);

    }
}
