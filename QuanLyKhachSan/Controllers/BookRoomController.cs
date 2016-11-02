using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Controllers
{
    public class BookRoomController : Controller
    {
        DataContext db = new DataContext();
        // GET: BookRoom
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckRom(DateTime begin, DateTime end , int type)
        {
            DateTime now = new DateTime();
            var registers = db.Register.Where(t => t.date_end.CompareTo(now) > 0 || t.date_begin.CompareTo(now) > 0).ToList();
            List<Room> using_room = new List<Room>();
            foreach (Register register in registers)
            {
                if (check_register(register, begin, end))
                {
                    using_room.Add(register.Room);
                }
            }
            var datas = db.Room.Where(
                delegate (Room room)
                {
                var a = using_room.Where(t => t.Id == room.Id).ToList();
                    if ( a.Count > 0 )
                    {
                        if(room.status == 0)
                        {
                            if(room.type == type || type == -1)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }).Select((room) => new  { room.Id, room.name, room.price } ) .ToArray();
            return new JsonResult() { Data = datas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
       }

        [HttpPost]
        public JsonResult Test(Register register,int[] list)
       {
            register.User_Id = AuthFactory.Auth.User.Id;
            db.Register.Add(register);
            db.SaveChanges();
            foreach (int service in list)
            {
                Using_Service using_sercive = db.Using_Service.Where(t => t.Register_Id == register.Id && t.Register_Id == register.User_Id).SingleOrDefault();
                if (using_sercive == null)
                {
                    using_sercive = new Using_Service();
                    using_sercive.Register_Id = register.Id;
                    using_sercive.Service_Id = service;
                    using_sercive.status = 0;
                    using_sercive.date = DateTime.Now;
                    using_sercive.count = 1;
                    db.Using_Service.Add(using_sercive);
                }
                
            }
            db.SaveChanges();

            return new JsonResult() { Data =  new { Id = register.Id, user = register.User_Id, room = register.Room_Id, begin = register.date_begin, end = register.date_end  }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult RegisterRoom(Register register)
        {
            register.User_Id = AuthFactory.Auth.User.Id;

            db.Register.Add(register);
            db.SaveChanges();

            //foreach (int service in services)
            //{
            //    db.Using_Service.Add(new Using_Service() { Service_Id = service, Register_Id = register.Id, status = 0, date = new DateTime(), count = 1 });
            //}
            //db.SaveChanges();
            return new JsonResult() { Data = register, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private bool check_register(Register register , DateTime start,DateTime end)
        {
            int s1 = register.date_begin.CompareTo(start);
            int e1 = register.date_begin.CompareTo(end);

            int s2 = register.date_end.CompareTo(start);
            int e2 = register.date_end.CompareTo(end);

            // duoi
            if ( s2 > 0 && s2 < 0)
            {
                return true;
            }

            //tren
          
            if (s1 > 0 && e1 < 0)
            {
                return true;
            }

            // con  -> true
            if ( s1 > -1 && e2 > -1)
            {
                return true;
            }

            //cha
            if ( s1 < 1 && e2 > -1)
            {
                return true;
            }

            return false;
        }


    }
}