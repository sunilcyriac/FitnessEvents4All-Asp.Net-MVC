using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessEvents.Models;

namespace FitnessEvents.Controllers
{
    public class EventRegistrationsController : Controller
    {
        private FitnessEvents_Models db = new FitnessEvents_Models();

        // GET: EventRegistrations
        [Authorize]
        public ActionResult Index()
        {
            return View(db.EventRegistrations.ToList());
        }

        // GET: EventRegistrations/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegistration eventRegistration = db.EventRegistrations.Find(id);
            if (eventRegistration == null)
            {
                return HttpNotFound();
            }
            return View(eventRegistration);
        }

        // GET: EventRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,DOB,Email,Address,SpecialRequest,EventId")] EventRegistration eventRegistration)
        {
            if (ModelState.IsValid)
            {
                ViewBag.EventId = new SelectList(db.FitnessEvents, "Id", "EventName");
                db.EventRegistrations.Add(eventRegistration);
                db.SaveChanges();
                return RedirectToAction("SuccessMessage");
            }

            return View(eventRegistration);
        }

        // GET: EventRegistrations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegistration eventRegistration = db.EventRegistrations.Find(id);
            if (eventRegistration == null)
            {
                return HttpNotFound();
            }
            return View(eventRegistration);
        }

        // POST: EventRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,DOB,Email,Address,SpecialRequest,EventId")] EventRegistration eventRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventRegistration);
        }

        // GET: EventRegistrations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventRegistration eventRegistration = db.EventRegistrations.Find(id);
            if (eventRegistration == null)
            {
                return HttpNotFound();
            }
            return View(eventRegistration);
        }

        // POST: EventRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventRegistration eventRegistration = db.EventRegistrations.Find(id);
            db.EventRegistrations.Remove(eventRegistration);
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

        public ActionResult SuccessMessage()
        {
            return View();

        }
    }
}
