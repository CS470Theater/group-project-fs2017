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
    public class TicketsController : Controller
    {
        private TheaterDBEntities db = new TheaterDBEntities();

        // GET: Tickets
        public ActionResult Index()
        {
            var ticket = db.Ticket.Include(t => t.Booking).Include(t => t.Price).Include(t => t.Showtime);
            return View(ticket.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.Booking_ID = new SelectList(db.Booking, "Booking_ID", "Customer_ID");
            ViewBag.Price_ID = new SelectList(db.Price, "Price_ID", "Price_Desc");
            ViewBag.Showtime_ID = new SelectList(db.Showtime, "Showtime_ID", "Showtime_ID");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ticket_ID,Booking_ID,Showtime_ID,Price_ID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Booking_ID = new SelectList(db.Booking, "Booking_ID", "Customer_ID", ticket.Booking_ID);
            ViewBag.Price_ID = new SelectList(db.Price, "Price_ID", "Price_Desc", ticket.Price_ID);
            ViewBag.Showtime_ID = new SelectList(db.Showtime, "Showtime_ID", "Showtime_ID", ticket.Showtime_ID);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.Booking_ID = new SelectList(db.Booking, "Booking_ID", "Customer_ID", ticket.Booking_ID);
            ViewBag.Price_ID = new SelectList(db.Price, "Price_ID", "Price_Desc", ticket.Price_ID);
            ViewBag.Showtime_ID = new SelectList(db.Showtime, "Showtime_ID", "Showtime_ID", ticket.Showtime_ID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ticket_ID,Booking_ID,Showtime_ID,Price_ID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Booking_ID = new SelectList(db.Booking, "Booking_ID", "Customer_ID", ticket.Booking_ID);
            ViewBag.Price_ID = new SelectList(db.Price, "Price_ID", "Price_Desc", ticket.Price_ID);
            ViewBag.Showtime_ID = new SelectList(db.Showtime, "Showtime_ID", "Showtime_ID", ticket.Showtime_ID);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
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
