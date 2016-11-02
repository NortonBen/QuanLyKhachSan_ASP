using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Using_Service
    {
        public int Id { get; set; }
        [Display(Name ="Register")]
        public int Register_Id { get; set; }
        [Display(Name = "Service")]
        public int Service_Id { get; set; }
        public int count { get; set; }
        public byte status { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime date { get; set; }

        [ForeignKey("Service_Id")]
        public virtual Service Service { get; set; }

        [ForeignKey("Register_Id")]
        public virtual Register Register { get; set; }
    }
}