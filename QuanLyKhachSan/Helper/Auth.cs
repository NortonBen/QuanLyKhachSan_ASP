using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyKhachSan.Models;
using Jose;
using System.Web.Script.Serialization;
using System.Globalization;

namespace QuanLyKhachSan.Helper
{
    public class Auth
    {
        /*
         * 
         * 
         */
        public string token { get; set; }
        public User User = null;
        private HttpRequest request = null;
        public byte[] secretKey = Base64UrlDecode("kithuatphanmenk12b");
        public string json = "";

        private static byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
                "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        private long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public DateTime issued = DateTime.Now;
        public DateTime expire = DateTime.Now.AddDays(10);

        public string Encode(User User)
        {
            this.User = User;
            var payload = new Dictionary<string, object>()
            {
                {"Id", User.Id},
                {"email", User.email},
                {"iat", ToUnixTime(issued).ToString()},
                {"exp", ToUnixTime(expire).ToString()}
            };

            this.token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            Console.Write(token);
            return this.token;
        }

        public User Decode(HttpRequest request)
        {
            this.request = request;
            string token = this.getToken();
            if(token == null)
            {
                return null;
            }
            try
            {
                this.json = Jose.JWT.Decode(token, secretKey);
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Console.Write(ex);
                return null;
            }
            catch (Jose.JoseException ex)
            {
                Console.Write(ex);
                return null;
            }
            if (json == null)
            {
                return null;
            }
            DataAuth dataAuth = new DataAuth(json);
            if(dataAuth.Id == 0)
            {
                return null;
            }
            DateTime now  = (new DateTime()).ToUniversalTime();
            if (now > dataAuth.exp)
            {
                return null;
            }
            DataContext db = new DataContext();
            this.User = db.User.FirstOrDefault(t => t.Id == dataAuth.Id && t.email == dataAuth.email);
            return this.User;
        }

        public string DecodeJson(string token)
        {
            try
            {
                this.json = Jose.JWT.Decode(token, secretKey);

            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Console.Write(ex);
            }
            catch (Jose.JoseException ex)
            {
                Console.Write(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return json;
        }

        public string getToken()
        {
            var cookie = request.Cookies.Get("auth");
            if(cookie == null)
            {
                return null;
            }
            this.token = cookie.Value;
            return this.token;
        }

        public  User getUser(string token)
        {
            User user = null;
            string json = DecodeJson(token);
            if(token == null)
            {
                return null;
            }
            DataAuth data = new DataAuth(json);
            DataContext db = new DataContext();
            user = db.User.FirstOrDefault(t => t.Id == data.Id && t.email == data.email);
            return user;
        }

    }

    public class DataAuth
    {
        public DataAuth(string json)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            this.obj = (Dictionary<string, object>)json_serializer.DeserializeObject(json);
            Id = Convert.ToInt32(obj.Where(t => t.Key == "Id").ToList()[0].Value);
            email = obj.Where(t => t.Key == "email").ToList()[0].Value.ToString();
            iat = ConvertFromUnixTimestamp(Convert.ToInt32(obj.Where(t => t.Key == "iat").ToList()[0].Value.ToString()));
            exp = ConvertFromUnixTimestamp(Convert.ToInt32(obj.Where(t => t.Key == "exp").ToList()[0].Value.ToString()));
        }
        Dictionary<string, object> obj = null;
        public int Id { get; set; }
        public string email { get; set; }
        public DateTime iat { get; set; }
        public DateTime exp { get; set; }

        public  DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }
    }

    public class AuthFactory
    {
        public static Auth Auth = new Auth();
    }

}