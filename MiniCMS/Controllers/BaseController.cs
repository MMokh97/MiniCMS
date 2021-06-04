using Microsoft.AspNet.Identity;
using MiniCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MiniCMS.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();

        public string LoggedUserID;
        public int Ar_lang_Id;
        public int En_lang_Id;


        public bool Is_SuperAdmin;
        public bool IsAdmin;

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                LoggedUserID = requestContext.HttpContext.User.Identity.GetUserId();

               
                if (requestContext.HttpContext.User.IsInRole("Admin"))
                {
                    var D_language = context.languages.ToList();
                    Ar_lang_Id = D_language.Where(x => x.LanguageName == "Arabic").FirstOrDefault().ID;
                    En_lang_Id = D_language.Where(x => x.LanguageName == "English").FirstOrDefault().ID;

                    ViewBag.language = D_language.Select(x => new SelectListItem() { Value = x.ID.ToString(),Text=x.LanguageName}).ToList();

                    ViewBag.GeneralAdminSiteData = context.Sitedata.Where(x => x.UserId == LoggedUserID && x.LanguageId == En_lang_Id).FirstOrDefault();
                    IsAdmin = true;
                    Is_SuperAdmin = false;
                }
                else if (requestContext.HttpContext.User.IsInRole("Admin"))
                {
                    IsAdmin = false;
                    Is_SuperAdmin = true;
                }
            }
            LoggedUserID = requestContext.HttpContext.User.Identity.GetUserId();
            if (requestContext.HttpContext.User.IsInRole("Admin"))
            {
                ViewBag.AdminSiteData = context.Sitedata.Where(x => x.UserId == LoggedUserID).ToList();
            }

            base.Initialize(requestContext);    
        }

    }
}