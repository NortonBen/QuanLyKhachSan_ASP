using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    [Table("Permission_Role")]
    public class Permission_Role
    {
        public int Id { get; set; }
        public int Role_Id { get; set; }
        public int Permission_Id { get; set; }


        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }

        [ForeignKey("Permission_Id")]
        public virtual Permission Permission { get; set; }
    }
}