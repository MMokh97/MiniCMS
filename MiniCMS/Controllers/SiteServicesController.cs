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
    public class SiteServicesController : BaseController
    {
        // GET: SiteServices
        public ActionResult Index()
        {
            var Services = context.SiteServices.Where(x => x.UserId == LoggedUserID).ToList();
            return View(Services);
        }

        // GET: SiteServices/Details/5
        public ActionResult Details(int id)
        {
            var services = context.SiteServices.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(services);
        }

        // GET: SiteServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteServices/Create
        [HttpPost]
        public ActionResult Create(SiteServices SiteService, HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            SiteService.Image = path;
            SiteService.UserId = LoggedUserID;


            context.SiteServices.Add(SiteService);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SiteServices/Edit/5
        public ActionResult Edit(int id)
        {
            var Service = context.SiteServices.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(Service);
        }

        // POST: SiteServices/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteServices SiteService , HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            SiteService.Image = path;
            SiteService.UserId = LoggedUserID;

            context.Entry(SiteService).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: SiteServices/Delete/5
        public ActionResult Delete(int id)
        {
            var Delete_service = context.SiteServices.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            context.SiteServices.Remove(Delete_service);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
