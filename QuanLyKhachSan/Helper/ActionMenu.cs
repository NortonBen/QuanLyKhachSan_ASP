using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuanLyKhachSan.Helper
{
    public class ActionMenu
    {
        public static string Action(String str)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            string pattent = str;
            Regex reg = new Regex(pattent);
            if (reg.IsMatch(url))
            {
                return " class=active ";
            }

            return null;
        }

        public static string Action(String[] strs)
        {
            
            foreach(string str in strs)
            {
                string output = ActionMenu.Action(str);
                if ( output != null)
                {
                    return output;
                }
            }
            return "";
        }

    }

    public class MyHelper
    {

        public static bool Show(string str)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            string pattent = str;
            Regex reg = new Regex(pattent);
            if (reg.IsMatch(url))
            {
                return true;
            }

            return false;
        }
        public static bool Show(string[] strs)
        {
            foreach (string str in strs)
            {
                string output = ActionMenu.Action(str);
                if (output != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string banner(bool bo)
        {
            if (bo)
            {
                return "style=min-height:250px;";
            }
            return "";
        }
    }
}