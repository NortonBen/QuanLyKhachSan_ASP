using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Controllers.Adminstrator
{
    [RoutePrefix("administrator/usingservice")]
    [Route("{action=index}")]
    public class UsingServiceController : Controller
    {
        private DataContext db = new DataContext();

        // GET: UsingService
        public ActionResult Index(int page = 0, int part = 30)
        {
            var total = db.Using_Service.Count() / part;
            var paginate = Paginate.create(page, part, total);
            var data = db.Using_Service.OrderBy(p => p.Id).Skip(paginate["page"] * part).Take((paginate["page"] + 1) * part).Include(u => u.Register).Include(u => u.Service).ToList();
            ViewBag.paginate = paginate;
            return View(data);
        }

        // GET: UsingService/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Using_Service using_Service = db.Using_Service.Find(id);
            if (using_Service == null)
            {
                return HttpNotFound();
            }
            return View(using_Service);
        }

        // GET: UsingService/Create
        public ActionResult Create()
        {
            ViewBag.Register_Id = new SelectList(db.Register, "Id", "Id");
            ViewBag.Service_Id = new SelectList(db.Service, "Id", "name");
            return View();
        }

        // POST: UsingService/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Register_Id,Service_Id,count,status,date")] Using_Service using_Service)
        {
            if (ModelState.IsValid)
            {
                db.Using_Service.Add(using_Service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Register_Id = new SelectList(db.Register, "Id", "Id", using_Service.Register_Id);
            ViewBag.Service_Id = new SelectList(db.Service, "Id", "name", using_Service.Service_Id);
            return View(using_Service);
        }

        // GET: UsingService/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Using_Service using_Service = db.Using_Service.Find(id);
            if (using_Service == null)
            {
                return HttpNotFound();
            }
            ViewBag.Register_Id = new SelectList(db.Register, "Id", "Id", using_Service.Register_Id);
            ViewBag.Service_Id = new SelectList(db.Service, "Id", "name", using_Service.Service_Id);
            return View(using_Service);
        }

        // POST: UsingService/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Register_Id,Service_Id,count,status,date")] Using_Service using_Service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(using_Service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Register_Id = new SelectList(db.Register, "Id", "Id", using_Service.Register_Id);
            ViewBag.Service_Id = new SelectList(db.Service, "Id", "name", using_Service.Service_Id);
            return View(using_Service);
        }

        // GET: UsingService/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Using_Service using_Service = db.Using_Service.Find(id);
            if (using_Service == null)
            {
                return HttpNotFound();
            }
            return View(using_Service);
        }

        // POST: UsingService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Using_Service using_Service = db.Using_Service.Find(id);
            db.Using_Service.Remove(using_Service);
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
