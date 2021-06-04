using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiniCMS.Models
{
    public class SiteData
    {
        public int id { get; set; }


        public string SiteName { get; set; }


        public string SiteDomain { get; set; }


        public string SiteLogo { get; set; }


        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language language { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users users { get; set; }

    }
}