using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Service
    {
        public Service()
        {
            this.Using_Service = new HashSet<Using_Service>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Price")]
        public Int64 price { get; set; }
        [Display(Name = "Describe")]
        public string describe { get; set; }
        public virtual ICollection<Using_Service> Using_Service { get; set; }
    }
}