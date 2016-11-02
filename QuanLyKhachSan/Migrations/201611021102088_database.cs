namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permission_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role_Id = c.Int(nullable: false),
                        Permission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Permission_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        permission = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 30),
                        password = c.String(nullable: false, maxLength: 50),
                        fullname = c.String(nullable: false, maxLength: 60),
                        birthday = c.DateTime(nullable: false),
                        sex = c.Boolean(nullable: false),
                        people_id = c.String(nullable: false, maxLength: 100),
                        address = c.String(nullable: false, maxLength: 150),
                        phone = c.String(nullable: false, maxLength: 12),
                        email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                        date_begin = c.DateTime(nullable: false),
                        date_end = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        status = c.Byte(nullable: false),
                        type = c.Byte(nullable: false),
                        price = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Using_Service",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Register_Id = c.Int(nullable: false),
                        Service_Id = c.Int(nullable: false),
                        count = c.Int(nullable: false),
                        status = c.Byte(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registers", t => t.Register_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .Index(t => t.Register_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        price = c.Long(nullable: false),
                        describe = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Role", "User_Id", "dbo.User");
            DropForeignKey("dbo.Using_Service", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Using_Service", "Register_Id", "dbo.Registers");
            DropForeignKey("dbo.Registers", "User_Id", "dbo.User");
            DropForeignKey("dbo.Registers", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.User_Role", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Permission_Role", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Permission_Role", "Permission_Id", "dbo.Permissions");
            DropIndex("dbo.Using_Service", new[] { "Service_Id" });
            DropIndex("dbo.Using_Service", new[] { "Register_Id" });
            DropIndex("dbo.Registers", new[] { "Room_Id" });
            DropIndex("dbo.Registers", new[] { "User_Id" });
            DropIndex("dbo.User_Role", new[] { "Role_Id" });
            DropIndex("dbo.User_Role", new[] { "User_Id" });
            DropIndex("dbo.Permission_Role", new[] { "Permission_Id" });
            DropIndex("dbo.Permission_Role", new[] { "Role_Id" });
            DropTable("dbo.Services");
            DropTable("dbo.Using_Service");
            DropTable("dbo.Rooms");
            DropTable("dbo.Registers");
            DropTable("dbo.User");
            DropTable("dbo.User_Role");
            DropTable("dbo.Roles");
            DropTable("dbo.Permission_Role");
            DropTable("dbo.Permissions");
        }
    }
}
