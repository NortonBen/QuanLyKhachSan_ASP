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

namespace QuanLyKhachSan.Controllers.Administrator
{
    [RoutePrefix("administrator/user")]
    [Route("{action=index}")]
    public class UserController : Controller
    {
        private DataContext db = new DataContext();

        // GET: User
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Password/5
        public ActionResult Password(int? id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return RedirectToAction("index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Password(int id, string oldpassword,  string newpassword, string repassword)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return RedirectToAction("index");
            }
            if(user.password == Encryption.encryp(oldpassword))
            {
                if (newpassword == repassword)
                {
                    user.password = Encryption.encryp(newpassword);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.error = new string[] { "New Password and Repassword not Math!" };
                }
            }else
            {
                ViewBag.error = new string[] { "OLdPassword not Math!" };
            }

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var role = db.Role.ToList();
            ViewBag.roles = role;
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,password,fullname,birthday,sex,people_id,address,phone,email")] User user, int permissons)
        {
            if (ModelState.IsValid)
            {
                user.password = Encryption.encryp(user.password);
                db.User.Add(user);
                db.SaveChanges();
                db.User_Role.Add(new User_Role { User_Id = user.Id, Role_Id = permissons });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.User.Where(u => u.Id == id).Include(t => t.User_Role).First();
            var user_role = db.User_Role.Where(u => u.User_Id == id).ToList();
            ViewBag.user_roles = user_role;
            if (user == null)
            {
                return HttpNotFound();
            }
            var role = db.Role.ToList();
            ViewBag.roles = role;
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,fullname,birthday,sex,people_id,address,phone,email")] User user, int permissons)
        {
            if (ModelState.IsValid)
            {
                user.password = Encryption.encryp(user.password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                User_Role User_Role = db.User_Role.FirstOrDefault(t => t.User_Id == user.Id);
                User_Role.Role_Id = permissons;
                db.Entry(User_Role).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            var user_role = db.User_Role.Where(u => u.User_Id == user.Id).ToList();
            ViewBag.user_roles = user_role;
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
