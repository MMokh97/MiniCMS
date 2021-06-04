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
    public class SiteNewsController : BaseController
    {
        // GET: SiteNews
        public ActionResult Index()
        {
            var AllNews = context.SiteNews.Where(x => x.UserId == LoggedUserID).ToList();
            return View(AllNews);
        }

        // GET: SiteNews/Details/5
        public ActionResult Details(int id)
        {
            var News = context.SiteNews.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(News);
        }

        // GET: SiteNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteNews/Create
        [HttpPost]
        public ActionResult Create(SiteNews News, HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            News.Image = path;
            News.UserId = LoggedUserID;


            context.SiteNews.Add(News);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SiteNews/Edit/5
        public ActionResult Edit(int id)
        {
            var news = context.SiteNews.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(news);
        }

        // POST: SiteNews/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteNews News , HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            News.Image = path;
            News.UserId = LoggedUserID;

            context.Entry(News).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: SiteNews/Delete/5
        public ActionResult Delete(int id)
        {
            var News = context.SiteNews.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            context.SiteNews.Remove(News);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
