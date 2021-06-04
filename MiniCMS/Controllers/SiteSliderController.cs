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
    public class SiteSliderController : BaseController
    {
        // GET: SiteSlider
        public ActionResult Index()
        {
            var Slider = context.SiteSlider.Where(x => x.UserId == LoggedUserID).ToList();
            return View(Slider);
        }

        // GET: SiteSlider/Details/5
        public ActionResult Details(int id)
        {
            var slider = context.SiteSlider.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(slider);
        }

        // GET: SiteSlider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteSlider/Create
        [HttpPost]
        public ActionResult Create(SiteSlider Slider,HttpPostedFileBase Image)
        {
            
                // TODO: Add insert logic here
                string path = ""; //save the logo image path
                if (Image.FileName.Length > 0)
                {
                    string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                    path = "/logo/" + Path.GetFileName(fname);
                    Image.SaveAs(Server.MapPath(path));
                }
                Slider.Image = path;
                Slider.UserId = LoggedUserID;
                

                context.SiteSlider.Add(Slider);
                context.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: SiteSlider/Edit/5
        public ActionResult Edit(int id)
        {
            var slider = context.SiteSlider.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            return View(slider);
        }

        // POST: SiteSlider/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteSlider Slider,HttpPostedFileBase Image)
        {
            
                // TODO: Add update logic here
                string path = ""; //save the logo image path
                if (Image.FileName.Length > 0)
                {
                    string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                    path = "/logo/" + Path.GetFileName(fname);
                    Image.SaveAs(Server.MapPath(path));
                }
                Slider.Image = path;
                Slider.UserId = LoggedUserID;

                context.Entry(Slider).State = EntityState.Modified;

                context.SaveChanges();

                return RedirectToAction("Index");
            
           
        }

        // GET: SiteSlider/Delete/5
        public ActionResult Delete(int id)
        {
            var slider = context.SiteSlider.Where(x => x.Id == id && x.UserId == LoggedUserID).FirstOrDefault();
            context.SiteSlider.Remove(slider);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        
    }
}
