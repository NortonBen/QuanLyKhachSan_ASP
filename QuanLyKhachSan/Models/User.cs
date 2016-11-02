using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            this.User_Role = new HashSet<User_Role>();
            this.Register = new HashSet<Register>();
        }
        
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "UserName")]
        public string username { get; set; }
        [Required]
        [StringLength(50)]
        public string password { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "FullName")]
        public string fullname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime birthday { get; set; }
        [Required]
        [Display(Name = "Sex")]
        public bool sex { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "People ID")]
        public string people_id { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Address")]
        public string address { get; set; }
        [Required]
        [StringLength(12)]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string email { get; set; }

        public virtual ICollection<User_Role> User_Role { get; set; }
        public virtual ICollection<Register> Register { get; set; }
    }

}