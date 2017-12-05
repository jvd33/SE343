using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KennUTicket.Models;
using KennUTicket.DAL;
using System.Net;
using System.Data.Entity;

namespace KennUTicket.Controllers
{
    public class SettingsController : Controller
    {
        private TicketContext db = new TicketContext();
        // GET: Settings
        public ActionResult Global()
        {
            using(var db = new TicketContext())
            {
                var script = db.Scripts.FirstOrDefault();
                return View(script);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Global([Bind(Include = "ID,ScriptText")] Script script)
        {
            return Create(script);
        }
            // GET: Settings/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ScriptText")] Script script)
        {
            if (ModelState.IsValid)
            {
                if(db.Scripts.FirstOrDefault() != null)
                {
                    return Edit(script);
                }
                db.Scripts.Add(script);
                db.SaveChanges();
                return RedirectToAction("Global");
            }

            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Script", script.ID);
            return View(script);
        }

        // GET: TicketEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Script script = db.Scripts.Find(id);
            if (script == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketID = new SelectList(db.Tickets, "ID", "Script", script.ID);
            return View(script);
        }

        // POST: TicketEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ScriptText")] Script script)
        {
            if (ModelState.IsValid)
            {
                db.Scripts.FirstOrDefault().ScriptText = script.ScriptText;
                db.SaveChanges();
                return RedirectToAction("Global");
            }
            ViewBag.TicketID = new SelectList(db.Scripts, "ID", "Script", script.ID);
            return View(script);
        }
        // GET: Settings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Settings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Global");
            }
            catch
            {
                return View();
            }
        }
    }
}
