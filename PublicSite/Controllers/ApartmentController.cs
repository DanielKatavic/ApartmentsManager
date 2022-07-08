using DataLayer.Dal;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Managers;
using PublicSite.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using PublicSite.Models.Auth;
using System.Linq;
using System.IO;

namespace PublicSite.Controllers
{
    [Authorize]
    public class ApartmentController : Controller
    {
        private const string AdminSiteDir = "../AdminSite/";
        private readonly IRepo repo = RepoFactory.GetRepo();
        private UserManager userManager;

        public UserManager UserManager
        {
            get => userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            private set => userManager = value;
        }

        // GET: Apartment
        [AllowAnonymous]
        public ActionResult Index() => View(repo);

        [AllowAnonymous]
        public ActionResult GetAllApartments(string city, string rooms, string adults, string children, string order)
        {
            IList<Apartment> apartments = repo.LoadApartments().Where(a => a.Status == Status.Vacant).ToList();
            ApartmentManager.FilterApartments(city, rooms, adults, children, ref apartments);
            ApartmentManager.OrderApartments(order, ref apartments);
            return PartialView("_ApartmentHelper", apartments);
        }

        [AllowAnonymous]
        public ActionResult Details(int apartmentId)
        {
            Apartment apartment = repo.LoadApartmentDetails(apartmentId);
            User user = UserManager.FindByName(User.Identity.Name);
            ApartmentDetailsVM model = new ApartmentDetailsVM
            {
                Apartment = apartment
            };
            if (user != null)
            {
                model.UserId = user.Id;
                model.FullName = user.Name;
                model.Phone = user.PhoneNumber;
                model.Email = user.Email;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ImageSrc(string path)
        { 
            string publicRoot = Server.MapPath("~");
            string adminRoot = Path.Combine(publicRoot, AdminSiteDir);
            string picturePath = Path.Combine(adminRoot, path);
            string mimeType = MimeMapping.GetMimeMapping(picturePath);
            return new FilePathResult(picturePath, mimeType);
        }

        public ActionResult AddReview(string userId, int apartmentId, int stars, string details)
        {
            repo.AddReview(apartmentId: apartmentId, userId: int.Parse(userId), details: details, stars: stars);
            return new EmptyResult();
        }
    }
}