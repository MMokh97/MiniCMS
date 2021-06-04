using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MiniCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace MiniCMS.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminUserController : BaseController
    {
        //ApplicationDbContext context = new ApplicationDbContext();
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SiteSettings()
        {
            //List<Language> langs = context.languages.ToList(); //creating object from languages and it is list because i select two rows.
            //int Ar_langID = langs.Where(x => x.LanguageName == "Arabic").FirstOrDefault().ID; //putting Ar lang id in a variable of int
            //int En_langID = langs.Where(x => x.LanguageName == "English").FirstOrDefault().ID; //putting En lang id in a variable of int
            Users my_site = context.Users.Where(x => x.Id == LoggedUserID).ToList().FirstOrDefault();
            List<SiteData> siteInfo = context.Sitedata.Where(x => x.UserId == LoggedUserID).ToList();
            SiteDataViewModel all_data = new SiteDataViewModel();
            all_data.UserId = my_site.Id;
            all_data.A_Email = my_site.Email;
            all_data.A_username = my_site.UserName;
            all_data.A_phone = my_site.PhoneNumber;
            all_data.A_status = my_site.isactive;
            all_data.SiteLogo = siteInfo.Where(m => m.LanguageId == En_lang_Id).FirstOrDefault().SiteLogo;
            all_data.SiteLogo_AR = siteInfo.Where(m => m.LanguageId == Ar_lang_Id).FirstOrDefault().SiteLogo;
            all_data.SiteDomain = siteInfo.Where(x => x.LanguageId == En_lang_Id).FirstOrDefault().SiteDomain;
            all_data.SiteName = siteInfo.Where(x => x.LanguageId == En_lang_Id).FirstOrDefault().SiteName;
            all_data.SiteName_AR = siteInfo.Where(x => x.LanguageId == Ar_lang_Id).FirstOrDefault().SiteName;
            return View(all_data);
        }

        [HttpPost]
        public ActionResult SiteSettings([Bind(Exclude = "A_Password,A_Email,A_username,A_ConfPass,SiteLogo,SiteLogo_AR")] SiteDataViewModel post_all, HttpPostedFileBase logoimg, HttpPostedFileBase logoimg_Ar)
        {
            string path = ""; //save the English logo image path
            if (logoimg.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + logoimg.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                logoimg.SaveAs(Server.MapPath(path));
            }
            post_all.SiteLogo = path;

            string path_Ar = ""; //save the Arabic logo image path
            if (logoimg_Ar.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + logoimg_Ar.FileName;
                path_Ar = "/logo/" + Path.GetFileName(fname);
                logoimg_Ar.SaveAs(Server.MapPath(path_Ar));
            }
            post_all.SiteLogo_AR = path_Ar;

            Users userInfo = context.Users.Where(x => x.Id == LoggedUserID).ToList().FirstOrDefault(); //select

            //userInfo.Email = post_all.A_Email;
            //userInfo.UserName = post_all.A_username;
            userInfo.PhoneNumber = post_all.A_phone;
            //userInfo.isactive = post_all.A_status;
            context.SaveChanges();


            //List<Language> langs = context.languages.ToList(); //creating object from languages and it is list because i select two rows.
            //int Ar_langID = langs.Where(x => x.LanguageName == "Arabic").FirstOrDefault().ID; //putting Ar lang id in a variable of int
            //int En_langID = langs.Where(x => x.LanguageName == "English").FirstOrDefault().ID; //putting En lang id in a variable of int
            var SDateArabic = context.Sitedata.Where(x => x.UserId == userInfo.Id && x.LanguageId == Ar_lang_Id).FirstOrDefault();
            SDateArabic.SiteName = post_all.SiteName_AR;
            SDateArabic.SiteDomain = post_all.SiteDomain;
            if (post_all.SiteLogo_AR == null)
            {
                SDateArabic.SiteLogo = post_all.SiteLogo;
            }
            else
            {
                SDateArabic.SiteLogo = post_all.SiteLogo_AR;
            }
            context.SaveChanges();


            var SDateEnglish = context.Sitedata.Where(x => x.UserId == userInfo.Id && x.LanguageId == En_lang_Id).FirstOrDefault();
            SDateEnglish.SiteName = post_all.SiteName;
            SDateEnglish.SiteDomain = post_all.SiteDomain;
            if (post_all.SiteLogo == null)
            {
                SDateEnglish.SiteLogo = post_all.SiteLogo_AR;
            }
            else
            {
                SDateEnglish.SiteLogo = path;
            }
            context.SaveChanges();

            return RedirectToAction("Index","SiteSlider");
        }
    }
}