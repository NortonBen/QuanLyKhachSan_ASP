using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Controllers.Adminstrator
{
    [RoutePrefix("administrator/bookroom")]
    [Route("{action=index}")]
    public class RegistersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Registers
        public ActionResult Index()
        {
            var register = db.Register.Include(r => r.Room).Include(r => r.User);
            return View(register.ToList());
        }

        // GET: Registers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Register.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: Registers/Create
        public ActionResult Create()
        {
            ViewBag.Room_Id = new SelectList(db.Room, "Id", "name");
            ViewBag.User_Id = new SelectList(db.User, "Id", "username");
            return View();
        }

        // POST: Registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,User_Id,Room_Id,date_begin,date_end")] Register register)
        {
            if (ModelState.IsValid)
            {
                db.Register.Add(register);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Room_Id = new SelectList(db.Room, "Id", "name", register.Room_Id);
            ViewBag.User_Id = new SelectList(db.User, "Id", "username", register.User_Id);
            return View(register);
        }

        // GET: Registers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Register.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            ViewBag.Room_Id = new SelectList(db.Room, "Id", "name", register.Room_Id);
            ViewBag.User_Id = new SelectList(db.User, "Id", "username", register.User_Id);
            return View(register);
        }

        // POST: Registers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,User_Id,Room_Id,date_begin,date_end")] Register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Room_Id = new SelectList(db.Room, "Id", "name", register.Room_Id);
            ViewBag.User_Id = new SelectList(db.User, "Id", "username", register.User_Id);
            return View(register);
        }

        // GET: Registers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Register.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: Registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Register register = db.Register.Find(id);
            db.Register.Remove(register);
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
