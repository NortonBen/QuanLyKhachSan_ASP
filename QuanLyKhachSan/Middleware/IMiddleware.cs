using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuanLyKhachSan.Middleware
{
    interface IMiddleware
    {
        void Begin(Object source, EventArgs e);
        void End(Object source, EventArgs e);
        void listen(HttpApplication application);
    }
}
