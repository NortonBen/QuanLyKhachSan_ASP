using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Room
    {
        public Room()
        {
            this.Register= new HashSet<Register>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Status")]
        public byte status { get; set; }
        [Display(Name = "Type")]
        public byte type { get; set; }
        [Display(Name = "Price")]
        public Int64 price { get; set; }

        public virtual ICollection<Register> Register { get; set; }
    }
}