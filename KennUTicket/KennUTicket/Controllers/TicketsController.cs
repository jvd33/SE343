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

namespace KennUTicket.Controllers
{
    public class TicketsController : Controller
    {

        // GET: Tickets
        public ActionResult Index()
        {

            using (var db = new TicketContext())
            {
                return View(db.Tickets.ToList());
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
        public ActionResult Create([Bind(Include = "ID,Description,Category,Title,AssignedToID,Priority, WearableNumber, OrderNumber, CustomerAddress")] Ticket ticket)
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

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,TicketType,Category,Title,Details,CreatedBy,AssignedTo,CreatedDate,LastUpdatedBy,LastUpdateDate,Priority")] Ticket ticket)
        {
            using (var db = new TicketContext())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();
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
