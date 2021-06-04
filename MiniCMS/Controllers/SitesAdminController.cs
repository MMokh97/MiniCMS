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
    [Authorize(Roles ="SuperAdmin")]
    public class SitesAdminController : BaseController
    {
        //ApplicationDbContext context = new ApplicationDbContext();
        // GET: SitesAdmin
        public ActionResult Index()
        {

            var Admin_Role = context.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
            var Lst_Sites_Admins =context.Users.Where(x => x.Roles.FirstOrDefault().RoleId == Admin_Role.Id).ToList();
            return View(Lst_Sites_Admins);
        }


        public ActionResult Dashboard()
        {
            var u_role = context.Roles.Where(x => x.Name == "admin").FirstOrDefault();
            var all_users = context.Users.Where(x => x.Roles.FirstOrDefault().RoleId == u_role.Id).ToList();

            var Active_users = all_users.Where(x => x.isactive == true && x.Roles.FirstOrDefault().RoleId == u_role.Id).ToList().Count;  //to avoid opening multiple connections
            var Inactive_users = all_users.Where(x => x.isactive == false && x.Roles.FirstOrDefault().RoleId == u_role.Id).ToList().Count;


            //var Active_users = context.Users.Where(x => x.isactive == true && x.Roles.FirstOrDefault().RoleId == u_role.Id).Count();
            //var Inactive_users = context.Users.Where(x => x.isactive == false && x.Roles.FirstOrDefault().RoleId == u_role.Id).Count();

            ViewBag.all = all_users.Count;
            ViewBag.active = Active_users;
            ViewBag.inactive = Inactive_users;

            return View();
        }


        [HttpGet]
        public ActionResult InsertSiteAdmin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult InsertSiteAdmin(SiteDataViewModel model , HttpPostedFileBase logoimg, HttpPostedFileBase logoimg_Ar )
        {
            
            string path = ""; //save the logo image path
            if (logoimg.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + logoimg.FileName;
                path = "/logo/" + Path.GetFileName(fname);
                logoimg.SaveAs(Server.MapPath(path));
            }
            model.SiteLogo = path;

            string path_Ar = ""; //save the logo image path
            if (logoimg_Ar.FileName.Length > 0)
            {
                string fname = DateTime.Now.ToString("yyyyMMddhhmmss") + logoimg_Ar.FileName;
                path_Ar = "/logo/" + Path.GetFileName(fname);
                logoimg_Ar.SaveAs(Server.MapPath(path_Ar));
            }
            model.SiteLogo_AR = path_Ar;

            Users App_user = new Users(); //creating an object from users
            SiteData S_data_E = new SiteData(); //creating an object from SiteData to save the image in it
            SiteData S_data_A = new SiteData();

            
            App_user.Email = model.A_Email;  //mapping &puuting the values from veiwmodel in base model "Users"
            App_user.UserName = model.A_username;
            App_user.PhoneNumber =model.A_phone;
            App_user.isactive = model.A_status;
            //Mapping English site data
            S_data_E.SiteName = model.SiteName;
            S_data_E.SiteDomain = model.SiteDomain;
            if (model.SiteLogo == null)  //if the English language is empty 
            {
                S_data_E.SiteLogo = model.SiteLogo_AR;
            }
            else
            {
                S_data_E.SiteLogo = model.SiteLogo;
            }

            //Mapping Arabic site data
            S_data_A.SiteName = model.SiteName_AR;
            S_data_A.SiteDomain = model.SiteDomain;
            if (model.SiteLogo_AR == null) //if the Arabic language is empty
            {
                S_data_A.SiteLogo = model.SiteLogo;
            }
            else
            {
                S_data_A.SiteLogo = model.SiteLogo_AR;
            }
            


            UserStore<Users> userStore = new UserStore<Users>(context);
            UserManager<Users> userManager = new UserManager<Users>(userStore);
            var result = userManager.Create(App_user, model.A_Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(App_user.Id, "Admin");
                var AL_id = context.languages.Where(x => x.LanguageName == "Arabic").FirstOrDefault();
                S_data_A.LanguageId = AL_id.ID;
                var EL_id = context.languages.Where(x => x.LanguageName == "English").FirstOrDefault();
                S_data_E.LanguageId = EL_id.ID;
                var u_id = context.Users.Where(x => x.Email == App_user.Email).FirstOrDefault();
                S_data_A.UserId = u_id.Id;
                S_data_E.UserId = u_id.Id;
                context.Sitedata.Add(S_data_A);
                context.Sitedata.Add(S_data_E);
                context.SaveChanges();

                /////Email
                MailMessage mail = new MailMessage();
                mail.To.Add(model.A_Email);
                mail.From = new MailAddress("mahmoudmokh97@gmail.com", "Email head", System.Text.Encoding.UTF8);
                mail.Subject = "This mail is send from asp.net application";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "thank you for using our site. \n your site data is \n userName:" + model.A_username + "\n Email:" +model.A_Email + "\n password:" + model.A_Password + "\n Domain:" + Request.UrlReferrer.AbsolutePath ;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("mahmoudmokh97@gmail.com", "@@@123mokh");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(mail);


            }
            else
            {
                ModelState.AddModelError("username", "this user name is already taken");
                ModelState.AddModelError("Email", "this email is already taken");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteUser( string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users userInfo = context.Users.Find(id);
            if (userInfo == null)
            {
                return HttpNotFound();
            }

            if (userInfo.isactive == true)
            {
                userInfo.isactive = false;
            }
            else
            {
                userInfo.isactive = true;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult ConfirmDeleteUser(string id)
        {
            var D_user = context.Users.Find(id);
            context.Users.Remove(D_user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Language> langs = context.languages.ToList(); //creating object from languages and it is list because i select two rows.
            int Ar_langID = langs.Where(x => x.LanguageName == "Arabic").FirstOrDefault().ID; //putting Ar lang id in a variable of int
            int En_langID = langs.Where(x => x.LanguageName == "English").FirstOrDefault().ID; //putting En lang id in a variable of int
            Users userInfo = context.Users.Find(id); //select
            List<SiteData> siteInfo = context.Sitedata.Where(x =>x.UserId == id).ToList();
            SiteDataViewModel all_data = new SiteDataViewModel();
            all_data.UserId = userInfo.Id;
            all_data.A_Email = userInfo.Email;
            all_data.A_username = userInfo.UserName;
            all_data.A_phone = userInfo.PhoneNumber;
            all_data.A_status = userInfo.isactive;
            all_data.SiteLogo = siteInfo.Where(m => m.LanguageId == En_langID).FirstOrDefault().SiteLogo;
            all_data.SiteLogo_AR = siteInfo.Where(m => m.LanguageId == Ar_langID).FirstOrDefault().SiteLogo;
            all_data.SiteDomain = siteInfo.Where(x => x.LanguageId == En_langID).FirstOrDefault().SiteDomain;
            all_data.SiteName = siteInfo.Where(x => x.LanguageId == En_langID).FirstOrDefault().SiteName;
            all_data.SiteName_AR = siteInfo.Where(x => x.LanguageId == Ar_langID).FirstOrDefault().SiteName;


            if (userInfo == null)
            {
                return HttpNotFound();
            }


            return View(all_data);
        }

        [HttpPost]
        public ActionResult Edit([Bind (Exclude = "A_Password,A_ConfPass,SiteDomain,SiteLogo,SiteLogo_AR")] SiteDataViewModel post_all ,HttpPostedFileBase logoimg, HttpPostedFileBase logoimg_Ar)
        {
            var isExisted = context.Users.Where(x => (x.Email == post_all.A_Email || x.UserName == post_all.A_username) && x.Id != post_all.UserId).ToList();
            if (isExisted.Count > 0)
            {
                ModelState.AddModelError("A_username", "this user name is already taken");
                ModelState.AddModelError("A_Email", "this email is already taken");
                return View(post_all);
            }
            else
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

                Users userInfo = context.Users.Find(post_all.UserId); //select
                
                userInfo.Email = post_all.A_Email;
                userInfo.UserName = post_all.A_username;
                userInfo.PhoneNumber = post_all.A_phone;
                userInfo.isactive = post_all.A_status;
                context.SaveChanges();


                List<Language> langs = context.languages.ToList(); //creating object from languages and it is list because i select two rows.
                int Ar_langID = langs.Where(x => x.LanguageName == "Arabic").FirstOrDefault().ID; //putting Ar lang id in a variable of int
                int En_langID = langs.Where(x => x.LanguageName == "English").FirstOrDefault().ID; //putting En lang id in a variable of int
                var SDateArabic = context.Sitedata.Where(x => x.UserId == post_all.UserId && x.LanguageId == Ar_langID).FirstOrDefault();
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


                var SDateEnglish = context.Sitedata.Where(x => x.UserId == post_all.UserId && x.LanguageId == En_langID).FirstOrDefault();
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

                return RedirectToAction("Index");
            }
        }
    }
}