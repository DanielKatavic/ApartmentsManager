using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;

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

        public ActionResult GetAllApartments()
        {
            IList<Apartment> model = repo.LoadApartments();
            return PartialView("_Demo", model);
        }

        public ActionResult Details(int apartmentId)
        {
            Apartment apartment = repo.LoadApartmentDetails(apartmentId);
            return View(apartment);
        }
    }
}