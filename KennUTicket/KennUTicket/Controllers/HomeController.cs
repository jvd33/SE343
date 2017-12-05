using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KennUTicket.Models;

namespace KennUTicket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "KennUTicket Home Page";

            return View();
        }

        public ActionResult Tests()
        {
            ViewBag.Title = "KennUTicket Tests";
            return View();
        }
    }
}
