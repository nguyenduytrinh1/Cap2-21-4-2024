using Hethongnongsan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace Hethongnongsan.Controllers
{
    public class ForumController : Controller
    {
        HethongnongsanContext db = new HethongnongsanContext();
        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addForum()
        {
            List<Forum> forums = db.Forum.ToList();
           
            return View(forums);
        }
       [HttpPost]
        public ActionResult addForum(Forum forum, HttpPostedFileBase imageFile) {
            
            int fom = db.Forum.Max(item =>item.Iddiendang);

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension = Path.GetExtension(imageFile.FileName);
            string newFileName = fileName + "_" + fom + fileExtension;

            // Đường dẫn lưu file mới
            string filePath = Path.Combine(Server.MapPath("~/imgforum"), newFileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.Message = "File deleted successfully.";
            }
            else
            {
                imageFile = Request.Files["imageFile"];

                if (imageFile != null && imageFile.ContentLength > 0)
                {

                    imageFile.SaveAs(filePath);
                    
                }
                else
                {
                 
                }

            }
            forum.Img = newFileName;
            db.Forum.Add(forum);
            forum.Ngaydang = DateTime.Now;
            db.SaveChanges();

            List<Forum> forums = db.Forum.ToList();
            return View(forums);
        }
        public ActionResult editForum(int id)
        {
            Forum forum = db.Forum.FirstOrDefault(row => row.Iddiendang == id);
            return View(forum);
        }
        [HttpPost]
        public ActionResult editForum(Forum forum)
        {
            Forum forums = db.Forum.FirstOrDefault(row => row.Iddiendang == forum.Iddiendang);
            forums.Context = forum.Context;
            db.SaveChanges();
            return RedirectToAction("addForum");
        }
        public ActionResult deleteForum(int id)
        {
            Forum forum = db.Forum.FirstOrDefault(row => row.Iddiendang == id);
            db.Forum.Remove(forum);
            db.SaveChanges(true);
            return RedirectToAction("addForum", new { id = id });
        }
        
    }
}