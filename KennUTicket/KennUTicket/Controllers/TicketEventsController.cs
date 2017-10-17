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

namespace KennUTicket.Controllers
{
    public class TicketEventsController : Controller
    {
        private TicketContext db = new TicketContext();

        // GET: TicketEvents
        public ActionResult Index()
        {
            var ticketEvents = db.TicketEvents.Include(t => t.Ticket);
            return View(ticketEvents.ToList());
        }

        // GET: TicketEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketEvent ticketEvent = db.TicketEvents.Find(id);
            if (ticketEvent == null)
            {
                return HttpNotFound();
            }
            return View(ticketEvent);
        }

        // GET: TicketEvents/Create
        public ActionResult Create()
        {
            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Description");
            return View();
        }

        // POST: TicketEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EventName,Time,TicketID")] TicketEvent ticketEvent)
        {
            if (ModelState.IsValid)
            {
                db.TicketEvents.Add(ticketEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Description", ticketEvent.TicketID);
            return View(ticketEvent);
        }

        // GET: TicketEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketEvent ticketEvent = db.TicketEvents.Find(id);
            if (ticketEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Description", ticketEvent.TicketID);
            return View(ticketEvent);
        }

        // POST: TicketEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EventName,Time,TicketID")] TicketEvent ticketEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Description", ticketEvent.TicketID);
            return View(ticketEvent);
        }

        // GET: TicketEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketEvent ticketEvent = db.TicketEvents.Find(id);
            if (ticketEvent == null)
            {
                return HttpNotFound();
            }
            return View(ticketEvent);
        }

        // POST: TicketEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketEvent ticketEvent = db.TicketEvents.Find(id);
            db.TicketEvents.Remove(ticketEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
