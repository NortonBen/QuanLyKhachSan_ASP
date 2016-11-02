namespace QuanLyKhachSan.Migrations
{
    using Helper;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuanLyKhachSan.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuanLyKhachSan.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Permission.AddOrUpdate(
              p => p.Id,
              new Permission { Id = 1, name = "admin" },
              new Permission { Id = 2, name = "roles" },
              new Permission { Id = 3, name = "room" },
              new Permission { Id = 4, name = "service" },
              new Permission { Id = 5, name = "user" }
            );

            context.Role.AddOrUpdate(
                p => p.Id,
                    new Role { Id = 1, permission = "admin" },
                    new Role { Id = 2, permission = "User" }
             );

            context.Permission_Role.AddOrUpdate(
                p => p.Id,
                new Permission_Role { Id = 1, Role_Id = 1, Permission_Id = 1 },
                new Permission_Role { Id = 2, Role_Id = 1, Permission_Id = 2 },
                new Permission_Role { Id = 3, Role_Id = 1, Permission_Id = 3 },
                new Permission_Role { Id = 4, Role_Id = 1, Permission_Id = 4 },
                new Permission_Role { Id = 5, Role_Id = 1, Permission_Id = 5 }
             );

            context.User.AddOrUpdate(
              p => p.Id,
              new User
              {
                  Id = 1,
                  username = "admin",
                  password = Encryption.encryp("admin"),
                  fullname = "admin",
                  email = "norton0395@gmail.com",
                  birthday = new DateTime(1995, 10, 29),
                  sex = true,
                  address = "Lý nhân, Hà Nam",
                  people_id = "168573299",
                  phone = "0964056932"
              },
               new User
               {
                   Id = 2,
                   username = "user",
                   password = Encryption.encryp("user"),
                   fullname = "user",
                   email = "norton0395@gmail.com",
                   birthday = new DateTime(1995, 10, 29),
                   sex = true,
                   address = "Lý nhân, Hà Nam",
                   people_id = "168573299",
                   phone = "0964056932"
               }
            );

            context.User_Role.AddOrUpdate(
                p => p.Id,
                new User_Role { Id = 1, Role_Id = 1, User_Id = 1 },
                new User_Role { Id = 2, Role_Id = 2, User_Id = 2 }
            );


            context.Room.AddOrUpdate(
                p => p.Id,
                new Room { Id = 1, name = "P01", price = 500000, status = 0, type = 1 },
                new Room { Id = 2, name = "P02", price = 400000, status = 0, type = 2 },
                new Room { Id = 3, name = "P03", price = 350000, status = 0, type = 3 },
                new Room { Id = 4, name = "P04", price = 300000, status = 0, type = 4 },
                new Room { Id = 5, name = "P05", price = 200000, status = 0, type = 5 }
            );

            context.Service.AddOrUpdate(
                p => p.Id,
                new Service { Id = 1, name = "Đồ Ăn", price = 100000 },
                new Service { Id = 2, name = "Giặt Là", price = 100000 },
                new Service { Id = 3, name = "Nước Uống", price = 100000 }
            );
        }
    }
}
