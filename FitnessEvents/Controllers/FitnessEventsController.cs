using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessEvents.Models;

namespace FitnessEvents.Controllers
{
    public class FitnessEventsController : Controller
    {
        private FitnessEvents_Models db = new FitnessEvents_Models();

        // GET: FitnessEvents
        public ActionResult Index()
        {
            return View(db.FitnessEvents.ToList());
        }

        [AllowAnonymous]
        // GET: FitnessEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitnessEvent fitnessEvent = db.FitnessEvents.Find(id);
            if (fitnessEvent == null)
            {
                return HttpNotFound();
            }
            return View(fitnessEvent);
        }

        [Authorize]
        // GET: FitnessEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FitnessEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventName,EventDetails,EventDate,EventType,PostedDate,ApplicationUserId")]
        FitnessEvent fitnessEvent, HttpPostedFileBase postedFile)
        {


            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            fitnessEvent.ImageUrl = myUniqueFileName;
            TryValidateModel(fitnessEvent);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
               // string fileExtension = ImageUrl.GetExtension(postedFile.FileName);
                string filePath = fitnessEvent.ImageUrl + ".jpg";
                fitnessEvent.ImageUrl = filePath;
                postedFile.SaveAs(serverPath + fitnessEvent.ImageUrl);

                db.FitnessEvents.Add(fitnessEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fitnessEvent);
        }

        // GET: FitnessEvents/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitnessEvent fitnessEvent = db.FitnessEvents.Find(id);
            if (fitnessEvent == null)
            {
                return HttpNotFound();
            }
            return View(fitnessEvent);
        }

        // POST: FitnessEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventName,EventDetails,EventDate,EventType,PostedDate,ApplicationUserId")]
        FitnessEvent fitnessEvent, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null)
                {
                    string serverPath = Server.MapPath("~/Uploads/");
                    // string fileExtension = ImageUrl.GetExtension(postedFile.FileName);
                    string filePath = fitnessEvent.ImageUrl + ".jpg";
                    fitnessEvent.ImageUrl = filePath;
                    postedFile.SaveAs(serverPath + fitnessEvent.ImageUrl);
                }

                
                db.Entry(fitnessEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fitnessEvent);
        }

        // GET: FitnessEvents/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FitnessEvent fitnessEvent = db.FitnessEvents.Find(id);
            if (fitnessEvent == null)
            {
                return HttpNotFound();
            }
            return View(fitnessEvent);
        }

        // POST: FitnessEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FitnessEvent fitnessEvent = db.FitnessEvents.Find(id);
            db.FitnessEvents.Remove(fitnessEvent);
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
