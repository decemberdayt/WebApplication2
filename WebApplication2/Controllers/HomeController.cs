using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Regards()
        {
            return View();
        }
    }
}