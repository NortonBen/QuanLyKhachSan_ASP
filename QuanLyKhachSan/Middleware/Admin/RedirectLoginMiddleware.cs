using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKhachSan.Helper;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Middleware.Admin
{
    public class RedirectLoginMiddleware : AMiddleware
    {
       
        public override void Begin(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
            if (is_with())
            {
                if (AuthFactory.Auth.User != null)
                {
                    context.Response.RedirectToRoute(new { Controller = "Administrator", Action = "index" });
                   
                }
            }
            return;
        }

        public override void End(object source, EventArgs e)
        {
            
        }
    }
}