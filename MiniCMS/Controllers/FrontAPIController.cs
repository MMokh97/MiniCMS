using MiniCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MiniCMS.Controllers
{
    public class FrontAPIController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        public List<SiteSlider> SiteSlider(string Id)
        {
            var slider = context.SiteSlider.Where(x => x.UserId == Id).ToList();
            return slider;
        }

    }
}
