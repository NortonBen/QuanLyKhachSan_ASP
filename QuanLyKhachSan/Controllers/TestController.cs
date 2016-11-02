using QuanLyKhachSan.Helper;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuanLyKhachSan.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            User user = new User();
            user.Id = 1;
            user.username = "admin";
            user.email = "admin@localhost";
            Auth auth = new Auth();
            string token =  auth.Encode(user);
            ViewBag.token = token;
            string json = auth.DecodeJson("ifgbefgousefvsgvf");
            ViewBag.json = json;
            return View();
        }
    }
}