using MiniCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniCMS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SiteVideosController : BaseController
    {
        // GET: SiteVideos
        public ActionResult Index()
        {
            var Videos = context.SiteVideos.Where(x => x.UserId == LoggedUserID).ToList();
            return View(Videos);
        }

        // GET: SiteVideos/Details/5
        public ActionResult Details(int id)
        {
            var Videos = context.SiteVideos.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(Videos);
        }

        // GET: SiteVideos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteVideos/Create
        [HttpPost]
        public ActionResult Create(SiteVideos Videos)
        {
            Videos.UserId = LoggedUserID;

            context.SiteVideos.Add(Videos);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SiteVideos/Edit/5
        public ActionResult Edit(int id)
        {
            var video = context.SiteVideos.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(video);
        }

        // POST: SiteVideos/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteVideos Videos)
        {
            Videos.UserId = LoggedUserID;

            context.Entry(Videos).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: SiteVideos/Delete/5
        public ActionResult Delete(int id)
        {
            var videos = context.SiteVideos.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            context.SiteVideos.Remove(videos);
            context.SaveChanges();
            return RedirectToAction("Index"); ;
        }

    }
}
