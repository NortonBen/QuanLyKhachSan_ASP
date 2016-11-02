using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Register
    {
        public Register()
        {
            this.Using_Service = new HashSet<Using_Service>();
        }

        public int Id { get; set; }
        [Display(Name = "User")]
        public int User_Id { get; set; }
        [Display(Name = "Room")]
        public int Room_Id { get; set; }
        [Display(Name = "Date Begin")]
        [DataType(DataType.Date)]
        public System.DateTime date_begin { get; set; }
        [Display(Name = "Date End")]
        [DataType(DataType.Date)]
        public System.DateTime date_end { get; set; }


        public virtual ICollection<Using_Service> Using_Service { get; set; }

        [ForeignKey("Room_Id")]
        public virtual Room Room { get; set; }

        [ForeignKey("User_Id")]
        public virtual User User { get; set; }

        public long total()
        {
            System.TimeSpan time = this.date_end.Subtract(this.date_begin);
            int day = (int)time.TotalDays+1;
            long price = day * this.Room.price;
            double tax = price * 0.1;
            long total = 0;
            foreach(Using_Service item in this.Using_Service)
            {
                total += item.Service.price;
            }

            return (long)tax +(long)price+ (long)total;
        }

        public bool is_del()
        {
            return this.date_begin.CompareTo(DateTime.Now) < 0;
        }
    }
}