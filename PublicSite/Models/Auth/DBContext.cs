using DataLayer.Dal;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace PublicSite.Models.Auth
{
    public class DBContext : IDisposable
    {
        private static readonly IRepo repo = RepoFactory.GetRepo();

        public IList<User> Users { get; set; }

        public DBContext(IList<User> users)
        {
            Users = users;
        }

        public void Dispose()
        {
        }

        public static DBContext Create() => new DBContext(repo.LoadUsers());
    }
}