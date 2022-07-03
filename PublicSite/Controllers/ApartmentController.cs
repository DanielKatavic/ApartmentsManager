using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Managers;
using System.Linq;

namespace PublicSite.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        private static IList<Apartment> apartments;
        
        // GET: Apartment
        public ActionResult Index() => View(repo);

        public ActionResult GetAllApartments(string city, string rooms, string adults, string children, string order)
        {
            apartments = repo.LoadApartments();
            ApartmentManager.FilterApartments(city, rooms, adults, children, ref apartments);
            ApartmentManager.OrderApartments(order, ref apartments);
            return PartialView("_ApartmentHelper", apartments);
        }

        public ActionResult Details(int apartmentId)
        {
            Apartment apartment = apartments.First(a => a.Id == apartmentId);
            return View(apartment);
        }
    }
}