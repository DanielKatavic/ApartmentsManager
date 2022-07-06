using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PublicSite.Models.Auth;
using PublicSite.Models.CustomAttributes;
using PublicSite.Models.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        // GET: LogIn
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
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Apartment");
        }
    }
}