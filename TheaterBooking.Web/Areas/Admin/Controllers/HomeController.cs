using System;
using System.Linq;
using System.Web.Mvc;
using TheaterBooking.Web.Database;

namespace TheaterBooking.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel login)
        //Used this as a guide:
        //http://www.dotnetlearners.com/blogs/view/124/Login-Page-Example-In-MVC-Using-Entity-Frame-Work.aspx
        {
            if (ModelState.IsValid)
            {
                var db = new TheaterDbEntities();
                try
                {
                    var user = (from userlist in db.Customers
                                where userlist.Username == login.Username && userlist.Password == login.Password
                                select new { userlist.Customer_ID, userlist.First_Name, userlist.Last_Name }).ToList();

                    if (user.FirstOrDefault() != null)
                    {
                        Session["Customer_ID"] = user.FirstOrDefault().Customer_ID;
                        Session["Full_Name"] = user.FirstOrDefault().First_Name + " " + user.FirstOrDefault().Last_Name;
                        return Redirect("/Home/Index");
                    }
                    ModelState.AddModelError("", "Invalid Login Credentials");
                    return View(login);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Model State");
                return View(login);
            }
        }
    }
}