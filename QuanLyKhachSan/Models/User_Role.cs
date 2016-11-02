using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class User_Role
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Role_Id { get; set; }
        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }
        [ForeignKey("User_Id")]
        public virtual User User { get; set; }
    }
}