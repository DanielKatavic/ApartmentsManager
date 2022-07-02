using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using System.Linq;
using DataLayer.Managers;

namespace PublicSite.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IRepo repo = RepoFactory.GetRepo();

        // GET: Apartment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllApartments(string city, string rooms, string adults, string children)
        {
            IList<Apartment> apartments = repo.LoadApartments();
            ApartmentManager.FilterApartments(city, rooms, adults, children, ref apartments);
            return PartialView("_Demo", apartments);
        }

        public ActionResult Details(int apartmentId)
        {
            Apartment apartment = repo.LoadApartmentDetails(apartmentId);
            return View(apartment);
        }
    }
}