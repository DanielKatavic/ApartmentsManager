using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Managers;

namespace PublicSite.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly IRepo repo = RepoFactory.GetRepo();

        // GET: Apartment
        public ActionResult Index() => View(repo);

        public ActionResult GetAllApartments(string city, string rooms, string adults, string children, string order)
        {
            IList<Apartment> apartments = repo.LoadApartments();
            ApartmentManager.FilterApartments(city, rooms, adults, children, ref apartments);
            ApartmentManager.OrderApartments(order, ref apartments);
            return PartialView("_ApartmentHelper", apartments);
        }

        public ActionResult Details(int apartmentId)
        {
            Apartment apartment = repo.LoadApartmentDetails(apartmentId);
            return View(apartment);
        }

        public ActionResult AddReview(int userId, int apartmentId, int stars, string details)
        {
            repo.AddReview(apartmentId, userId, details, stars);
            return new EmptyResult();
        }
    }
}