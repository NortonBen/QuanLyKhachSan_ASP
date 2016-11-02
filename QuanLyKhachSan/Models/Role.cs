using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    public class Role
    {
        public Role()
        {
            this.Permission_Role = new HashSet<Permission_Role>();
            this.User_Role = new HashSet<User_Role>();
        }
        public int Id { get; set; }
        [Display(Name ="Name")]
        public string permission { get; set; }

        public virtual ICollection<User_Role> User_Role { get; set; }
        public virtual ICollection<Permission_Role> Permission_Role { get; set; }
    }
}