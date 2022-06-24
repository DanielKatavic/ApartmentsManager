using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicSite.Controllers
{
    public class ApartmentController : Controller
    {
        // GET: Apartment
        public ActionResult Index()
        {
            return View();
        }
    }
}