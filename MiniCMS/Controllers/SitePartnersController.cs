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
    public class SitePartnersController : BaseController
    {
        // GET: SitePartners
        public ActionResult Index()
        {
            var partners = context.SitePartners.Where(x => x.UserId == LoggedUserID).ToList();
            return View(partners);
        }

        // GET: SitePartners/Details/5
        public ActionResult Details(int id)
        {
            var partner = context.SitePartners.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(partner);
        }

        // GET: SitePartners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SitePartners/Create
        [HttpPost]
        public ActionResult Create(SitePartners Partners, HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            Partners.Image = path;
            Partners.UserId = LoggedUserID;


            context.SitePartners.Add(Partners);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SitePartners/Edit/5
        public ActionResult Edit(int id)
        {
            var partner = context.SitePartners.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(partner);
        }

        // POST: SitePartners/Edit/5
        [HttpPost]
        public ActionResult Edit(SitePartners Partners, HttpPostedFileBase Image)
        {
            string path = ""; //save the logo image path
            if (Image.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                Image.SaveAs(Server.MapPath(path));
            }
            Partners.Image = path;
            Partners.UserId = LoggedUserID;

            context.Entry(Partners).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: SitePartners/Delete/5
        public ActionResult Delete(int id)
        {
            var partner = context.SitePartners.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();

            context.SitePartners.Remove(partner);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
