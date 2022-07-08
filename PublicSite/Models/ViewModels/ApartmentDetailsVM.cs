using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models.ViewModels
{
    public class ApartmentDetailsVM
    {
        public Apartment Apartment { get; set; }

        public string UserId { get; set; } = "0";
        public string FullName { get; set; } = "";
        public string FirstName{ get => FullName.Split(' ').Count() > 0 ? FullName.Split(' ')[0] : ""; }
        public string LastName { get => FullName.Split(' ').Count() > 1 ? FullName.Split(' ')[1] : ""; }
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}