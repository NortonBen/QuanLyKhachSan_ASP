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
    [RoutePrefix("administrator/role")]
    [Route("{action=index}")]
    public class RolesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Role.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,permission")] Role role, int[] list)
        {
            if (ModelState.IsValid)
            {
                db.Role.Add(role);
                db.SaveChanges();
                foreach (int id in list)
                {
                    Permission_Role Permission_Role = new Permission_Role();
                    Permission_Role.Permission_Id = id;
                    Permission_Role.Role_Id = role.Id;
                    role.Permission_Role.Add(Permission_Role);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,permission")] Role role, int[] list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                List<Permission_Role> datas = role.Permission_Role.ToList();
                foreach(Permission_Role data in datas)
                {
                    if(list.Where(i => i == data.Id) == null)
                    {
                        db.Permission_Role.Remove(data);
                    }
                }
                foreach (int id in list)
                {
                    Permission_Role val = db.Permission_Role.FirstOrDefault(t => t.Permission_Id == id && t.Role_Id == role.Id);
                    if(val == null)
                    {
                        Permission_Role Permission_Role = new Permission_Role();
                        Permission_Role.Permission_Id = id;
                        Permission_Role.Role_Id = role.Id;
                        role.Permission_Role.Add(Permission_Role);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Role.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Role.Find(id);
            db.Role.Remove(role);
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
