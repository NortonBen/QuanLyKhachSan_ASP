using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Middleware
{
    public class Middleware : IHttpModule
    {
        string[] site = new string[] {
               "home",
               "contact",
               ""
        };
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication application)
        {
            (new AuthenticateMiddleware()).listen(application);

            (new Admin.LoginMiddleware())
                .with(new string[]{ "/administrator/*", "/administrator" })
                .except(new string[] { "/administrator/login" })
                .listen(application);


            //(new Site.LoginMiddleware())
            //    .with(new string[] { "/account/*", "/account", "/bookroom", "/bookroom/*" })
            //    .listen(application);

            (new Admin.PermissionMiddleware())
                .with(new string[] { "/administrator/*", "/administrator" })
                .except(new string[] { "/administrator/login", "/administrator/logout" })
                .listen(application);


            (new Site.RedirectLoginMiddleware())
                .with(new string[] { "/login", "/register" })
                .listen(application);

            (new Admin.RedirectLoginMiddleware())
                .with(new string[] { "/administrator/login" })
                .listen(application);

        }

      
    }
}