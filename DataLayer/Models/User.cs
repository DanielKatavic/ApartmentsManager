using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    //[PasswordHash], [SecurityStamp], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount]

    [Serializable]
    public class User
    {
        private const char DELIMITER = '|';

        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber{ get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

        public User() { }

        //public User(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}

        //public User(string fName, string lName, City city, string username, string password)
        //{
        //    FName = fName;
        //    LName = lName;
        //    City = city;
        //    Username = username;
        //    Password = password;
        //}
    }
}
