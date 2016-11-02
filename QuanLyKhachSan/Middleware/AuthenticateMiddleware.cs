using QuanLyKhachSan.Helper;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Middleware
{
    public class AuthenticateMiddleware: AMiddleware
    {
        public override void Begin(object source, EventArgs e)
        {
            AuthFactory.Auth.User = null;
            HttpContext context = ((HttpApplication)source).Context;
            HttpRequest request = context.Request;
            User user = AuthFactory.Auth.Decode(request);
        }

        public override void End(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
        }
    }
}