using MiniCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniCMS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SiteImagesController : BaseController
    {
        // GET: SiteImages
        public ActionResult Index()
        {
            var Images = context.SiteImages.Where(x => x.UserId == LoggedUserID).ToList();
            return View(Images);
        }

        // GET: SiteImages/Details/5
        public ActionResult Details(int id)
        {
            var images = context.SiteImages.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(images);
        }

        // GET: SiteImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteImages/Create
        [HttpPost]
        public ActionResult Create(SiteImages Images ,HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            Images.Image = path;
            Images.UserId = LoggedUserID;


            context.SiteImages.Add(Images);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SiteImages/Edit/5
        public ActionResult Edit(int id)
        {
            var image = context.SiteImages.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(image);
        }

        // POST: SiteImages/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteImages Images , HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            Images.Image = path;
            Images.UserId = LoggedUserID;

            context.Entry(Images).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: SiteImages/Delete/5
        public ActionResult Delete(int id)
        {
            var images = context.SiteImages.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();

            context.SiteImages.Remove(images);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
