using Microsoft.ApplicationBlocks.Data;
using DataLayer.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System;

namespace DataLayer.Dal
{
    public class DBRepo : IRepo
    {
        private static readonly string APARTMENTS_CS = ConfigurationManager.ConnectionStrings["apartments"].ConnectionString;

        public User CkeckAdminUser(string email, string password)
        {
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, email, password).Tables[0];
            if (tblUsers.Rows.Count == 0) return null;
            return new User { UserName = tblUsers.Rows[0].ToString() };
        }

        public IList<User> LoadUsers()
        {
            IList<User> users = new List<User>();
            var tblUsers = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name).Tables[0];
            foreach (DataRow row in tblUsers.Rows)
            {
                users.Add(new User
                {
                    Guid = (Guid)row[nameof(User.Guid)],
                    Email = row[nameof(User.Email)].ToString(),
                    PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                    UserName = row[nameof(User.UserName)].ToString(),
                    Address = row[nameof(User.Address)].ToString()
                });
            }
            return users;
        }

        public IList<Tag> LoadTags()
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Guid = (Guid)row[nameof(Tag.Guid)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        Count = (int)row[nameof(Tag.Count)]
                    }
                );
            }
            return tags;
        }

        public IList<Apartment> LoadApartments()
        {
            IList<Apartment> apartments = new List<Apartment>();

            var tblApartments = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name).Tables[0];
            foreach (DataRow row in tblApartments.Rows)
            {
                Guid guid = (Guid)row[nameof(Apartment.Guid)];
                apartments.Add(
                    new Apartment
                    {
                        Guid = guid,
                        Name = row[nameof(Apartment.Name)].ToString(),
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        Price = (decimal)row[nameof(Apartment.Price)],
                        CityName = row[nameof(Apartment.CityName)].ToString(),
                        Status = (Status)Enum.Parse(typeof(Status), row[nameof(Apartment.Status)].ToString()),
                        PicturesCount = (int)row[nameof(Apartment.PicturesCount)],
                        Tags = LoadTagsByApartmentGuid(guid)
                    }
                );
            }
            return apartments;
        }

        private IList<Tag> LoadTagsByApartmentGuid(Guid guid)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Guid = (Guid)row["TagGuid"]
                    }
                );
            }
            return tags;
        }

        public void UpdateApartment(Guid guid, int maxAdults, int maxChildren, int totalRooms, string status)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid, maxAdults, maxChildren, totalRooms, status);

        public void DeleteApartment(Guid guid)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid);
    }
}
