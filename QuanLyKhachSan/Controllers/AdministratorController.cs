using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Controllers
{
    public class AdministratorController : Controller
    {
        DataContext db = new DataContext();
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(User user)
        {
            user.password = Encryption.encryp(user.password);
            User _user = db.User.FirstOrDefault(t => t.username == user.username && t.password == user.password);
            if(_user == null || _user.Id == 0)
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

    }
}