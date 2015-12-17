using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asignment6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "How it Works";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Me At";

            return View();
        }
    }
}