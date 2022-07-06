using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Serializable]
    public class User : IUser
    {
        public string Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

        public List<string> Roles { get; set; }
        public virtual string Password { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new List<string>();
        }

        public virtual void AddRole(string role) => Roles.Add(role);

        public virtual void RemoveRole(string role) => Roles.Remove(role);

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
            => await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

        public override string ToString()
            => UserName;
    }
}
