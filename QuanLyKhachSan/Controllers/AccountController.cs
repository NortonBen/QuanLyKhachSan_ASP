using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();
        // GET: Account
        public ActionResult Index(int page = 0,int part = 30)
        {
            var total = db.Register.Where(t => t.User_Id == AuthFactory.Auth.User.Id).Count() / part;
            var paginate = Paginate.create(page, part, total);
            var data = db.Register.Where(t => t.User_Id == AuthFactory.Auth.User.Id).OrderBy(p => p.Id).Skip(paginate["page"] * part).Take((paginate["page"] + 1) * part).ToList();
            ViewBag.paginate = paginate;
            return View(data);
        }

        public ActionResult DeleteBR(int? id)
        {
            Register Register = db.Register.Find(id);
            if(Register != null)
            {
                db.Register.Remove(Register);
            }
            
            return RedirectToAction("index");
        }
        public ActionResult Details(int? id)
        {
            Register Register = db.Register.Find(id);
            if (Register != null)
            {
                return View(Register);
            }
            return RedirectToAction("index");
            
        }

        public ActionResult AddService(int? id)
        {
            Register register = db.Register.Find(id);
            if(register != null)
            {
                return View(register);
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult AddService(int register,int service, int count)
        {
            Using_Service Using_Service = db.Using_Service.Where(t => t.Register_Id == register && t.Service_Id == service).SingleOrDefault();
            if (Using_Service != null)
            {
                Using_Service.count += count;
                db.Entry(Using_Service).State = System.Data.Entity.EntityState.Modified;
            }else
            {
                Using_Service = new Using_Service();
                Using_Service.Register_Id = register;
                Using_Service.Service_Id = service;
                Using_Service.status = 0;
                Using_Service.date = DateTime.Now;
                Using_Service.count = count;
                db.Using_Service.Add(Using_Service);
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult DeleteSV(int? id,int? register)
        {
            Using_Service Using_Service = db.Using_Service.Find(id);
            if (Using_Service != null)
            {
                db.Using_Service.Remove(Using_Service);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = register });
        }
    }
}