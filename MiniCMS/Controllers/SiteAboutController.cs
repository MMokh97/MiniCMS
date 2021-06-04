using MiniCMS.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniCMS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SiteAboutController : BaseController
    {
        // GET: SiteAbout/Edit/5
        public ActionResult Edit(int? id)
        {
            var About = context.SiteAbout.Where(x => x.UserId == LoggedUserID).FirstOrDefault();
            if (About == null)
            {
                SiteAboutUs DefaultAbout = new SiteAboutUs();
                DefaultAbout.Details = "";
                DefaultAbout.Image = "";
                DefaultAbout.Email = "";
                DefaultAbout.FaceBook = "";
                DefaultAbout.Instagram = "";
                DefaultAbout.Telegram = "";
                DefaultAbout.twitter = "";
                DefaultAbout.WhatsApp = "";
                DefaultAbout.UserId = LoggedUserID;
                DefaultAbout.LangId = 1;

                context.SiteAbout.Add(DefaultAbout);
                context.SaveChanges();
                return View(DefaultAbout);
            }
            else
            {
                return View(About);
            }
        }

        // POST: SiteAbout/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteAboutUs About , HttpPostedFileBase Image)
        {
            var Item = context.SiteAbout.Where(x => x.UserId == LoggedUserID).FirstOrDefault();

            string path = ""; //save the logo image path
            try
            {
                if (Image.FileName.Length > 0)
                {
                    string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + Image.FileName;
                    path = "/logo/" + Path.GetFileName(fname);
                    Image.SaveAs(Server.MapPath(path));
                }
            }
            catch
            {
            }
            if (path != "")
            {
                Item.Image = path;
            }
            Item.Details =About.Details;
            Item.Email =About.Email;
            Item.FaceBook =About.FaceBook;
            Item.Instagram =About.Instagram;
            Item.Telegram =About.Telegram;
            Item.twitter =About.twitter;
            Item.WhatsApp =About.WhatsApp;
            //context.Entry(About).State = EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index","Home");


        }
    }
}
