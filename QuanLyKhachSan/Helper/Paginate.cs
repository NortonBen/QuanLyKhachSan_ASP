using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Helper
{
    public class Paginate
    {
        /*
         *Paginate
         * @parma Dictionary $data
         * struc @data = [ @total , @page, @part ]
         * @return string
         */
        public static string render(Dictionary<string,int> data)
        {
            bool next = false;
            bool prev = false;
            List<int> show_page = new List<int>();
            int _part = 6;
            if (data["page"] <= 0)
            {
                for(int i = 0; i < data["total"]; i++)
                {
                    if(i < _part) {
                        show_page.Add(i);
                    } else {
                        break;
                    }
                }
                data["page"] = 0;
                if(data["total"] > 1)
                {
                    next = true;
                }
            }
            
            if(data["page"] > 0 && data["page"] < data["total"]-1)
            {
                prev = true;
                next = true;
                for (int i = data["page"]; i >= 0 ; i--)
                {
                    if (i > data["page"] - (int)(_part/2))
                    {
                        if (!show_page.Equals(i))
                        {
                            show_page.Add(i);
                        }
                    }else{
                        break;
                    }
                }

                for (int i = data["page"]+1; i < data["total"]; i++){
                    if (data["page"] + (int)(_part / 2) >= i)
                    {
                        if (show_page.IndexOf(i) == -1)
                        {
                            show_page.Add(i);
                        }
                    }else{
                        break;
                    }
                }

                if(show_page.Count < _part)
                {
                    for (int i = data["page"] - (int)(_part / 2); i >= 0; i--)
                    {
                        if (i > data["page"] - _part)
                        {
                            if (show_page.IndexOf(i) == -1 )
                            {
                                show_page.Add(i);
                            }
                            
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i = data["page"] + (int)(_part / 2); i < data["total"]; i++)
                    {
                        if (data["page"] + _part >= i)
                        {
                            if (show_page.IndexOf(i) == -1)
                            {
                                show_page.Add(i);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
               
            }

            if(data["page"] >= (data["total"] - 1)){
                for (int i = data["total"] - 1; i >= 0 ; i--){
                    if (data["total"] - _part < i) {
                        if (show_page.IndexOf(i) == -1)
                        {
                            show_page.Add(i);
                        }
                    }else{
                        break;
                    }
                }
                data["page"] = data["total"] - 1;
                if(data["total"] > 1)
                {
                    prev = true;
                }
            }
            show_page.Sort();
            string _header = "<nav><ul class=\"pagination\">";
            string _prev = " <li><a href =\"?page="+(data["page"]-1).ToString()+"\" class=\"waves-effect\"><i class=\"material-icons\">chevron_left</i></a></li>";
            string _str = "";
            foreach (int i in show_page)
            {
                _str += "<li " + ((data["page"] == i) ? "class=\"active\"" : "") + " ><a  href =\"?page=" + (i).ToString() + "\" class=\"waves-effect\">"+(i+1).ToString()+"</a></li>";

            }
            string _next = "<li><a href =\"?page=" + (data["page"] + 1).ToString() + "\" class=\"waves-effect\"><i class=\"material-icons\">chevron_right</i></a></li>";
            string _footer = "</ul></nav>";
            if (prev)
            {
                _header += _prev;
            }
            if (next)
            {
                _footer = _next+ _footer;
            }

            return _header+_str +_footer;  
        }

        public static Dictionary<string,int> create(int page, int part, int total)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data["part"] = part;
            data["total"] = total;
            data["page"] = page;
            return data;
        }

        public static Dictionary<string, int> create(int page, int part, DbSet<Object> lists )
        {
            int total = lists.Count() / part;
            Dictionary<string, int> data = new Dictionary<string, int>();
            data["part"] = part;
            data["total"] = total;
            data["page"] = page;
            return data;
        }

        public static IEnumerable<Object> paginate(DbSet<Object> data ,int page, int part)
        {
            return data.Skip(page * part).Take((page + 1) * part);
        }
    }
}