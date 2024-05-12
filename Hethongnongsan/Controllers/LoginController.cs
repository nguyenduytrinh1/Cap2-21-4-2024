using Hethongnongsan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;
namespace Hethongnongsan.Controllers
{
    public class LoginController : Controller
    {
        HethongnongsanContext db = new HethongnongsanContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Nguoidung nguoidung)
        {
            string url = "";
            string error = "";
            Nguoidung nd;
            nd = db.Nguoidung.FirstOrDefault(row => row.TaiKhoan == nguoidung.TaiKhoan);
            if (nd == null)
            {
                if (nguoidung.TaiKhoan.Count() < 4 && nguoidung.MatKhau.Count() < 4)
                {
                    TempData["Message"] = "Tài Khoản Và Mật Khẩu phải trên 4 ký tự";
                }
                else
                {
                    nguoidung.Roles = "User";
                    db.Nguoidung.Add(nguoidung);
                    db.SaveChanges();
                    url = "https://localhost:44345/Login/Index";
                }
            }
            else
            {
                url = "https://localhost:44345/Login/Index";
                TempData["Message"] = "Vui lòng đăng ký lại ! Tên tài khoản đã tồn tại";
            }
            ViewBag.error = error;
            return View();
        }
        [HttpPost]
        public ActionResult Process(string username, string password)
        {
            Nguoidung nguoidung = null;
            string url = "";
            nguoidung = db.Nguoidung.FirstOrDefault(row => row.TaiKhoan.Equals(username));
            if (nguoidung != null)
            {
                if (nguoidung.MatKhau.Equals(password))
                {
                    url = "https://localhost:44345/Home/Index";
                    HttpCookie cookie = new HttpCookie("nguoidung");
                    cookie.Values.Add("", nguoidung.Idnguoidung.ToString());
                    cookie.Expires = DateTime.Now.AddDays(1);

                    Response.Cookies.Add(cookie);
                }
                else
                {
                    url = "https://localhost:44345/Login/Index";
                }
            }
            else
            {
                url = "https://www.youtube.com/watch?v=BaCR9hA6Eoo&list=RDMM7iME0MldxNM&index=40";
            }
            return Redirect(url);
        }

        public ActionResult Out()
        {
            Debug.WriteLine("/***");
            foreach (string cookieName in Request.Cookies.AllKeys)
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Expires = DateTime.Now.AddDays(-1); 
                Response.Cookies.Add(cookie);
                Debug.WriteLine(cookie, "/***");
            }

            string url = "https://localhost:44345/Home/Index";
            return Redirect(url);
        }
    }

    }