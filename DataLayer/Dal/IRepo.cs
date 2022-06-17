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
        User CkeckAdminUser(string email, string password);
        IList<User> LoadUsers();
        void AddTag(string name, string typeName);
        void UpdateApartment(Guid guid, int maxAdults, int maxChildren, int totalRooms, string status);
        void DeleteApartment(Guid guid);
        void DeleteTag(Guid guid);
    }
}
