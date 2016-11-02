using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("home");
        }

        [Route("home")]
        [HttpGet]
        public ActionResult home()
        {
            return View();
        }

        [Route("login")]
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult login(User user)
        {
            user.password = Encryption.encryp(user.password);
            User _user = db.User.FirstOrDefault(t => t.username == user.username && t.password == user.password);
            if (_user == null || _user.Id == 0)
            {
                ViewData["error"] = new string[] { "Usename or Password not math!" };
                return RedirectToAction("login");
            }
            Auth auth = new Auth();
            Response.Cookies["auth"].Value = auth.Encode(_user);

            Response.Cookies["auth"].Expires = DateTime.Now.AddDays(10);
            return RedirectToAction("index");
        }

        public ActionResult logout()
        {

            Response.Cookies["auth"].Value = "";
            Response.Cookies["auth"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("login");
        }


        [Route("resgister")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("resgister")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string repassword)
        {
            if (ModelState.IsValid)
            {
                if (user.password == repassword)
                {
                    user.password = Encryption.encryp(user.password);

                    db.User.Add(user);
                    db.SaveChanges();
                    db.User_Role.Add(new User_Role() { User_Id = user.Id, Role_Id = 2 });
                    db.SaveChanges();
                    return RedirectToAction("login");
                }
                ViewBag.error = new string[] { "Not Math Password! " };
            }
            return View(user);
        }


        [Route("detail/{$id}")]
        public ActionResult detail(int id)
        {
            return View();
        }

        [Route("rooms")]
        public ActionResult rooms()
        {
            return View();
        }

        [Route("contact")]
        public ActionResult contact()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterRoom(DateTime begin, DateTime end, int room, int[] services)
        {
            Register register = new Register();
            register.date_begin = begin;
            register.date_end = end;
            register.Room_Id = room;
            register.User_Id = AuthFactory.Auth.User.Id;

            db.Register.Add(register);
            db.SaveChanges();

            foreach (int service in services)
            {
                db.Using_Service.Add(new Using_Service() { Service_Id = service, Register_Id = register.Id, status = 0, date = new DateTime(), count = 1 });
            }
            db.SaveChanges();
            return new JsonResult() { Data = register, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}