using DataLayer.Dal;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace RWAproject
{
    public class Global : HttpApplication
    {
        private readonly IRepo _repo;

        public Global()
        {
            _repo = RepoFactory.GetRepo();
        }

        void Application_Start(object sender, EventArgs e)
        {
            Application["database"] = _repo;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e) 
            => Response.Redirect("ErrorPage.aspx");
    }
}