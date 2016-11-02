using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Models
{

    public class DataContext : DbContext
    {
        public DataContext() : base("name=Database")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Register> Register { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }
        public virtual DbSet<Using_Service> Using_Service { get; set; }
        public virtual DbSet<Permission_Role> Permission_Role { get; set; }

    }
}