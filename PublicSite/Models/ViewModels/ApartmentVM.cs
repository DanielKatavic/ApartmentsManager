using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models.ViewModels
{
    public class ApartmentVM
    {
        public IList<Apartment> Apartments { get; set; }
    }
}