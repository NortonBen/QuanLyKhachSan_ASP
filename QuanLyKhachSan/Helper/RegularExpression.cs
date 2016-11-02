using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuanLyKhachSan.Helper
{
    public class RegularExpression
    {
        public static bool is_path(string str,string path)
        {
            string pattent = str;
            Regex reg = new Regex(pattent);
           return reg.IsMatch(path);
        }
    }
}