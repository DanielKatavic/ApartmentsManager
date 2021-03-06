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
                string paswHash = row[nameof(User.PasswordHash)].ToString();
                users.Add(new User
                {
                    Id = row[nameof(User.Id)].ToString(),
                    Guid = (Guid)row[nameof(User.Guid)],
                    Email = row[nameof(User.Email)].ToString(),
                    PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                    UserName = row[nameof(User.Email)].ToString(),
                    Name = row[nameof(User.Name)].ToString(),
                    Address = row[nameof(User.Address)].ToString(),
                    PasswordHash = paswHash,
                    Password = paswHash
                });
            }
            return users;
        }

        public IList<City> LoadCities()
        {
            IList<City> cities = new List<City>();
            var tblCities = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name).Tables[0];
            foreach (DataRow row in tblCities.Rows)
            {
                cities.Add(new City
                {
                    Id = (int)row[nameof(City.Id)],
                    Name = row[nameof(City.Name)].ToString()
                });
            }
            return cities;
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
                        Id = (int)row[nameof(Tag.Id)],
                        Guid = (Guid)row[nameof(Tag.Guid)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        Count = (int)row[nameof(Tag.Count)],
                        TypeName = row[nameof(Tag.TypeName)].ToString()
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
                int id = (int)row[nameof(Apartment.Id)];
                IList<Image> images = LoadImagesByApartmentId(id);
                apartments.Add(
                    new Apartment
                    {
                        Id = id,
                        Guid = (Guid)row[nameof(Apartment.Guid)],
                        Name = row[nameof(Apartment.Name)].ToString(),
                        MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                        MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                        TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                        Price = Math.Round((decimal)row[nameof(Apartment.Price)], 2),
                        CityName = row[nameof(Apartment.CityName)].ToString(),
                        Status = (Status)Enum.Parse(typeof(Status), row[nameof(Apartment.Status)].ToString()),
                        BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                        Tags = LoadTagsByApartmentId(id),
                        Images = images,
                        ImageCount = images.Count,
                        Reviews = LoadReviewsByApartmentId(id)
                    }
                );
            }
            return apartments;
        }

        public Apartment LoadApartmentDetails(int apartmentId)
        {
            DataTable tblApartmentDetails = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId).Tables[0];
            int id = (int)tblApartmentDetails.Rows[0][nameof(Apartment.Id)];
            IList<Image> images = LoadImagesByApartmentId(id);
            var row = tblApartmentDetails.Rows[0];
            return new Apartment
            {
                Id = id,
                Name = row[nameof(Apartment.Name)].ToString(),
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                Price = (decimal)row[nameof(Apartment.Price)],
                CityName = row[nameof(Apartment.CityName)].ToString(),
                Status = (Status)Enum.Parse(typeof(Status), row[nameof(Apartment.Status)].ToString()),
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                Tags = LoadTagsByApartmentId(id),
                Images = images,
                ImageCount = images.Count,
                Reviews = LoadReviewsByApartmentId(id)
            };
        }

        public User LoadReservationByApartmentId(int apartmentId)
        {
            var tblReservations = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId).Tables[0];
            var row = tblReservations.Rows[0];
            return new User
            {
                Name = row[nameof(User.UserName)].ToString(),
                Address = row[nameof(User.Address)].ToString(),
                Email = row[nameof(User.Email)].ToString(),
                PhoneNumber = row[nameof(User.PhoneNumber)].ToString(),
                Details = row[nameof(User.Details)].ToString()
            };
        }

        public IList<Review> LoadReviewsByApartmentId(int apartmentId)
        {
            IList<Review> reviews = new List<Review>();
            var tblReviews = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId).Tables[0];
            foreach (DataRow row in tblReviews.Rows)
            {
                reviews.Add(
                    new Review
                    {
                        Id = (int)row[nameof(Review.Id)],
                        ApartmentId = (int)row[nameof(Review.ApartmentId)],
                        UserName = row[nameof(Review.UserName)].ToString(),
                        CreatedAt = (DateTime)row[nameof(Review.CreatedAt)],
                        Details = row[nameof(Review.Details)].ToString(),
                        Stars = (int)row[nameof(Review.Stars)]
                    });
            }
            return reviews;
        }

        private IList<Tag> LoadTagsByApartmentId(int id)
        {
            IList<Tag> tags = new List<Tag>();

            var tblTags = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, id).Tables[0];
            foreach (DataRow row in tblTags.Rows)
            {
                tags.Add(
                    new Tag
                    {
                        Id = (int)row[nameof(Tag.Id)],
                        Name = (string)row["TagName"],
                    }
                );
            }
            return tags;
        }

        public IList<Image> LoadImagesByApartmentId(int id)
        {
            IList<Image> images = new List<Image>();

            var tblImages = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, id).Tables[0];

            foreach (DataRow row in tblImages.Rows)
            {
                images.Add(new Image
                {
                    Id = (int)row[nameof(Image.Id)],
                    Path = row[nameof(Image.Path)].ToString(),
                    Base64Content = row[nameof(Image.Base64Content)].ToString(),
                    Name = row[nameof(Image.Name)].ToString(),
                    IsRepresentative = (bool)row[nameof(Image.IsRepresentative)]
                });
            }

            return images;
        }

        public static void AddUser(string email, string password, string username, string phone, string address)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, email, password, username, phone, address);

        public void AddReview(int apartmentId, int userId, string details, int stars)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId, userId, details, stars);

        public void AddTaggedApartment(int apartmentId, int tagId)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId, tagId);

        public int AddApartment(int cityId, string name, int price, int maxAdults, int maxChildren, int totalRooms, int beachDistance)
        {
            var dataSet = SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, cityId, name, name, price, maxAdults, maxChildren, totalRooms, beachDistance).Tables[0];
            return int.Parse(dataSet.Rows[0][0].ToString());
        }

        public void AddImage(int imageId, int apartmentId, string path = null, string base64image = null, string imageName = "", bool isRepresentative = false)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, imageId, apartmentId, path, base64image, imageName, isRepresentative);

        public void AddTag(string name, string typeName)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, name, typeName);

        public void AddReservation(int apartmentId, int? id, string username = null, string email = null, string phoneNumber = null, string address = null, string details = null)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId, id, username, email, phoneNumber, address, details);

        public void UpdateApartment(Guid guid, string name, int maxAdults, int maxChildren, int totalRooms, string status, int beachDistance, decimal price)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid, name, maxAdults, maxChildren, totalRooms, status, beachDistance, price);

        public void DeleteTaggedApartment(int apartmentId, int tagId)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, apartmentId, tagId);

        public void DeleteApartment(Guid guid)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid);

        public void DeleteTag(Guid guid)
            => SqlHelper.ExecuteDataset(APARTMENTS_CS, MethodBase.GetCurrentMethod().Name, guid);
    }
}
