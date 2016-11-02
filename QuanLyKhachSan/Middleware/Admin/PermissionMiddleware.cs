using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKhachSan.Helper;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Middleware.Admin
{
    public class PermissionMiddleware : AMiddleware
    {
        public override void Begin(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
            if (is_with())
            {
                if (AuthFactory.Auth.User != null)
                {
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    DataContext db = new DataContext();
                    User_Role User_Role = db.User_Role.Where(t => t.User_Id == AuthFactory.Auth.User.Id).First();

                    List<Permission_Role> datas = User_Role.Role.Permission_Role.ToList();
                    if (!this.check(datas, db, path))
                    {
                        context.Response.RedirectToRoute(new { Controller = "Error", Action = "Permission" });
                        return;
                    }
                }
            }
            return;
            
        }

        public override void End(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
           

        }

        public bool check(List<Permission_Role> datas,DataContext db,string path)
        {
            foreach (Permission_Role item in datas)
            {
                Permission permission = item.Permission;
                if (RegularExpression.is_path(permission.name, path))
                {
                    return true;
                }
            }
            return false;
        }
    }
}