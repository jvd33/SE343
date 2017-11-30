using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KennUTicket.DAL;
using KennUTicket.Models;
using KennUTicket.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace KennUTicket.Controllers
{
    public class TicketsController : Controller
    {

        // GET: Tickets

        public ActionResult Index()
        {
            ViewBag.SearchOptions = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Category", Value = "Category" },
                new SelectListItem() { Text = "Order Number", Value = "OrderNumber" },
                new SelectListItem() { Text = "Status", Value = "StatusName" },
                new SelectListItem() { Text = "Created By", Value = "CreatedByName" },
                new SelectListItem() { Text = "Assigned To", Value = "AssignedToName" },
                new SelectListItem() { Text = "Priority", Value = "Priority" },
            };
            System.Web.HttpContext.Current.Session["searchProp"] = "";
            System.Web.HttpContext.Current.Session["searchField"] = "";
            using (var db = new TicketContext())
            {
                var tickets = db.Tickets.Where(c => !c.Status.StatusName.Contains("Closed")).ToList();
                return View(tickets);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Index(TicketSearchModel searchModel, TicketSearchModel filterModel)
        {
            ViewBag.SearchOptions = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Category", Value = "Category" },
                new SelectListItem() { Text = "Order Number", Value = "OrderNumber" },
                new SelectListItem() { Text = "Status", Value = "StatusName" },
                new SelectListItem() { Text = "Created By", Value = "CreatedByName" },
                new SelectListItem() { Text = "Assigned To", Value = "AssignedToName" },
                new SelectListItem() { Text = "Priority", Value = "Priority" },
            };

            using (var db = new TicketContext())
            {
                var tickets = await db.Tickets.ToListAsync();
                if (searchModel.Strategy != null)
                {
                    System.Web.HttpContext.Current.Session["searchProp"] = searchModel.GetType().GetProperty(searchModel.GetSearchValue()).GetValue(searchModel, null);
                    System.Web.HttpContext.Current.Session["searchField"] = searchModel.GetType().GetProperty(searchModel.GetSearchValue()).Name;
                    searchModel = searchModel.ResolveRef();
                    var searchType = searchModel.GetSearchValue();
                    var searchField = searchModel.GetType().GetProperty(searchType).Name;
                    var searchVal = searchModel.GetType().GetProperty(searchType).GetValue(searchModel, null);
                    if (searchModel.Strategy == "search")
                    {
                        tickets = tickets.Where(c => c.GetType().GetProperty(searchType).GetValue(c, null).ToString() == searchVal.ToString()).ToList();
                    }
                    if (filterModel.Strategy != null)
                    {
                        switch (filterModel.Strategy)
                        {
                            case "filter_asc":
                                tickets = tickets.OrderBy(c => c.GetType().GetProperty(searchType)).ToList();
                                break;
                            case "filter_desc":
                                tickets = tickets.OrderByDescending(c => c.GetType().GetProperty(searchType)).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                }
                return View(tickets);
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new TicketContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Category,Title,AssignedToID,Priority, ProductID, OrderNumber, CustomerAddress")] Ticket ticket)
        {
            var ctx = new TicketContext();
            var userStore = new UserStore<User>(ctx);
            var manager = new UserManager<User>(userStore);
            var user = manager.FindByName(ticket.AssignedToID);
            if (!ModelState.IsValid)
            {
                return View();
            }

            ticket.AssignedTo = user;
            ticket.AssignedToID = user.Id;
            ticket = ticket.CreateTicket(User.Identity.Name);
            return RedirectToAction("Index");
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new TicketContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        public ActionResult Close(int? id)
        {
            if(id != null)
            {
                using (var db = new TicketContext())
                {
                    Ticket ticket = db.Tickets.Find(id);
                    ticket.CloseTicket();
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Category,Title,Priority")] Ticket ticket)
        {
            using (var db = new TicketContext())
            {
                if (ModelState.IsValid)
                {
                    var t = db.Tickets.FirstOrDefault(c => c.ID == ticket.ID);
                    t.Description = ticket.Description;
                    t.Category = ticket.Category;
                    t.Title = ticket.Title;
                    t.Priority = ticket.Priority;
                    db.SaveChanges();
                    t.UpdateTicketStatus("Active - Updated");
                    return RedirectToAction("Index");
                }
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new TicketContext())
            {
                Ticket ticket = db.Tickets.Find(id);

                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new TicketContext())
            {
                Ticket ticket = db.Tickets.Find(id);
                db.Tickets.Remove(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
