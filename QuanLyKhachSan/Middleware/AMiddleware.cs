using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Middleware
{
    public abstract class AMiddleware : IMiddleware
    {
        enum Type { except, with, none };
        public string[] excepts = null;
        public string[] using_with = null;
        Type type = Type.none;
        public abstract void Begin(object source, EventArgs e);
        public abstract void End(object source, EventArgs e);
        public void listen(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Begin));
            application.EndRequest += (new EventHandler(this.End));
        }
        public AMiddleware except(string[] excepts)
        {
            if (this.type == Type.with)
            {
                this.type = Type.none;
            }
            else
            {
                this.type = Type.except;

            }
            this.excepts = excepts;
            return this;
        }
        public AMiddleware with(string[] using_with)
        {
            if (this.type == Type.with)
            {
                this.type = Type.none;
            }
            else
            {
                this.type = Type.with;
            }
            this.using_with = using_with;
            return this;
        }

        public bool is_route(string[] strs)
        {
            if (strs == null)
            {
                return false;
            }
            try
            {

                string path = HttpContext.Current.Request.Url.AbsolutePath;
                foreach (string str in strs)
                {
                    if (RegularExpression.is_path(str.ToUpper(), path.ToUpper())) {

                        return true;
                    }
                }
            } catch (HttpException ex)
            {
                Console.Write(ex);
            }
            return false;
        }
        public bool is_with()
        {
            if (is_route(using_with) && !is_route(excepts))
            {
                return true;
            }
            return false;
        }
        public bool is_except()
        {
            if (!is_route(using_with) || is_route(excepts))
            {
                return true;
            }
            return false;
        }
    }
}