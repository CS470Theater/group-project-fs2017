using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TheaterBooking.Web.Areas.Admin.Models;

namespace TheaterBooking.Web.Areas.Admin.Controllers
{
    public class ScreensController : Controller
    {
        private TheaterDBEntities db = new TheaterDBEntities();

        // GET: Screens
        public ActionResult Index()
        {
            var screens = db.Screens.Include(s => s.Theater);
            return View(screens.ToList());
        }

        // GET: Screens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var screen = db.Screens.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // GET: Screens/Create
        public ActionResult Create()
        {
            ViewBag.Theater_ID = new SelectList(db.Theaters, "Theater_ID", "Theater_Name");
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
                db.Screens.Add(screen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Theater_ID = new SelectList(db.Theaters, "Theater_ID", "Theater_Name", screen.Theater_ID);
            return View(screen);
        }

        // GET: Screens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var screen = db.Screens.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            ViewBag.Theater_ID = new SelectList(db.Theaters, "Theater_ID", "Theater_Name", screen.Theater_ID);
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
            ViewBag.Theater_ID = new SelectList(db.Theaters, "Theater_ID", "Theater_Name", screen.Theater_ID);
            return View(screen);
        }

        // GET: Screens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var screen = db.Screens.Find(id);
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
            var screen = db.Screens.Find(id);
            db.Screens.Remove(screen);
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
