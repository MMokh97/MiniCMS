using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniCMS.Models
{
    public class SiteDataViewModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string SiteName { get; set; }

        [Display(Name ="Site Name (Arabic)")]
        [Required]
        public string SiteName_AR { get; set; }

        [Required]
        public string SiteDomain { get; set; }

        [Required]
        public string SiteLogo { get; set; }

        [Required]
        public string SiteLogo_AR { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string A_username { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string A_Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string A_Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("A_Password", ErrorMessage = "two passwords are not matched")]
        [Display(Name = "Confirm Password")]
        [Required]
        public string A_ConfPass { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string A_phone { get; set; }

        [Display(Name = "Admin Status")]
        [Required]
        public bool A_status { get; set; }

        [Required]
        public string Lang { get; set; }

    }
}