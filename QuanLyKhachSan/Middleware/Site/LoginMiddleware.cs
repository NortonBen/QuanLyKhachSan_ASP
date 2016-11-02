using QuanLyKhachSan.Helper;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Middleware.Site
{
    public class LoginMiddleware : AMiddleware
    {
        public override void Begin(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
            if (is_with())
            {
                if (AuthFactory.Auth.User != null)
                {
                    return;
                }

                context.Response.RedirectToRoute(new { Controller = "Home", Action = "login" });
                return;
            }

        }

        public override void End(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
        }
    }
}