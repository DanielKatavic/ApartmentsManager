using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PublicSite.Models.Auth;
using PublicSite.Models.CustomAttributes;
using PublicSite.Models.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Dal;

namespace PublicSite.Controllers
{
    public class LogInController : Controller
    {
        private UserManager _authManager;
        private SignInManager _signInManager;

        public SignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            private set => _signInManager = value;
        }
        public UserManager AuthManager
        {
            get => _authManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            private set => _authManager = value;
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Index(LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await AuthManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);
                ViewBag.username = user.UserName;
                return RedirectToAction(actionName: "Index", controllerName: "Apartment");
            }
            else
            {
                ModelState.AddModelError(key: "error", errorMessage: "Incorrect username or password");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = AuthManager.FindByName(model.Email);
            if (user is null)
            {
                Models.Auth.PasswordHasher ph = new Models.Auth.PasswordHasher();
                string hashedPassword = ph.HashPassword(model.Password);
                DBRepo.AddUser(
                    email: model.Email, 
                    password: hashedPassword, 
                    username: model.Username, 
                    phone: model.Phone, 
                    address: model.Address);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(key: "error", errorMessage: "User already exists!");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Apartment");
        }
    }
}