using Hethongnongsan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Web;
using System.Web.Mvc;

namespace Hethongnongsan.Controllers
{
    public class ProfilesuserController : Controller
    {
        // GET: Profilesuser
        HethongnongsanContext db = new HethongnongsanContext();
        public ActionResult Index(int id)
        {
            Nguoidung nguoidung = db.Nguoidung.FirstOrDefault(row => row.Idnguoidung == id);

            string check = "";
         
                List<Shopcart> sp = db.Shopcart.Where(row => row.Status == 1 & row.Idnguoidung == nguoidung.Idnguoidung).ToList();
                List<Sanpham> spm = new List<Sanpham>();
                List<Nguoidung> nguoidungmua = new List<Nguoidung>();
                List<Shopcart> myshopcart = new List<Shopcart>();
                foreach (var item in sp)
                {
                    Debug.WriteLine("S------------------------: " + item.Idnguoidung + "check");

                    Nguoidung nm = db.Nguoidung.FirstOrDefault(row => row.Idnguoidung == item.Idnguoidung);
                    if (nm == null){
                        nm = new Nguoidung();
                        nm.Tennguoidung = " Người dùng đã bị xóa";
                    }
                    nguoidungmua.Add(nm);
                    Sanpham sanpham = db.Sanpham.FirstOrDefault(row => row.Idsanpham == item.Idsanpham);
                    spm.Add(sanpham);
                    myshopcart.Add(item);
                    check = check + item.Idshopcart + ",";
                }

                // Debug.WriteLine("Số lượng sản phẩm đã bán: " + spm.Count + "*" + nguoidungmua.Count + "*" + "check" + check);

                Debug.WriteLine("Số lượng sản phẩm đã bán: " + spm.Count + "*" + nguoidungmua.Count + "*" + "check" + check);
                ViewBag.SanPhamn = spm;
                ViewBag.Nguoimua = nguoidungmua;

                ViewBag.Shopcart = myshopcart;
            return View(nguoidung);
        }
  
        public ActionResult Updateprofiles(int id)
        {

            Nguoidung nguoidung = db.Nguoidung.FirstOrDefault(row => row.Idnguoidung == id);

            return View(nguoidung);
        }
        [HttpPost]
        public ActionResult Updateprofiles(Nguoidung nguoidung)
        {
            Nguoidung nguoidungs = db.Nguoidung.FirstOrDefault(row => row.Idnguoidung == nguoidung.Idnguoidung);
            nguoidungs.Diachi = nguoidung.Diachi;
            nguoidungs.Gioitinh = nguoidung.Gioitinh;
            nguoidungs.Sodienthoai=nguoidung.Sodienthoai;
            nguoidungs.Ngaysinh = nguoidung.Ngaysinh;
            nguoidungs.Tennguoidung = nguoidung.Tennguoidung;
            db.SaveChanges();

            return RedirectToAction("Index", new { id = nguoidungs.Idnguoidung });
        }
    }
}