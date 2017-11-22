using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheaterBooking.Models;

namespace TheaterBooking.Views
{
    public class ScreensController : Controller
    {
        private TheaterDBEntities db = new TheaterDBEntities();

        // GET: Screens
        public ActionResult Index()
        {
            var screen = db.Screen.Include(s => s.Theater);
            return View(screen.ToList());
        }

        // GET: Screens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // GET: Screens/Create
        public ActionResult Create()
        {
            ViewBag.Theater_ID = new SelectList(db.Theater, "Theater_ID", "Theater_Name");
            return View();
        }

        // POST: Screens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Screen_ID,Theater_ID,Capacity")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                db.Screen.Add(screen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Theater_ID = new SelectList(db.Theater, "Theater_ID", "Theater_Name", screen.Theater_ID);
            return View(screen);
        }

        // GET: Screens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            ViewBag.Theater_ID = new SelectList(db.Theater, "Theater_ID", "Theater_Name", screen.Theater_ID);
            return View(screen);
        }

        // POST: Screens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Screen_ID,Theater_ID,Capacity")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(screen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Theater_ID = new SelectList(db.Theater, "Theater_ID", "Theater_Name", screen.Theater_ID);
            return View(screen);
        }

        // GET: Screens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // POST: Screens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Screen screen = db.Screen.Find(id);
            db.Screen.Remove(screen);
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
