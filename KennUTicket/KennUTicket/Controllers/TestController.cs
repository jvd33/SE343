using KennUTicket.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KennUTicket.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        // GET: Tickets
        public ActionResult Test()
        {

            using (var db = new TicketContext())
            {
                return View(db.Tickets.ToList());
            }
        }
    }
}