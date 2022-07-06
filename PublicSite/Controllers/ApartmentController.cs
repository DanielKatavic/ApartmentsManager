using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Managers;
using PublicSite.Models.Auth;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using PublicSite.Models.ViewModels;

namespace PublicSite.Controllers
{
    [Authorize]
    public class ApartmentController : Controller
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        
        // GET: Apartment
        [AllowAnonymous]
        public ActionResult Index() => View(repo);

        [AllowAnonymous]
        public ActionResult GetAllApartments(string city, string rooms, string adults, string children, string order)
        {
            IList<Apartment> apartments = repo.LoadApartments();
            ApartmentManager.FilterApartments(city, rooms, adults, children, ref apartments);
            ApartmentManager.OrderApartments(order, ref apartments);
            return PartialView("_ApartmentHelper", apartments);
        }

        [AllowAnonymous]
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