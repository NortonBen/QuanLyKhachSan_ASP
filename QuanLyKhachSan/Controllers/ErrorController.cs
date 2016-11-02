using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult notFount()
        {
            return View("404");
        }

        public ActionResult Permission()
        {
            return View("401");
        }

        public ActionResult ServerError()
        {
            return View("500");
        }
    }
}