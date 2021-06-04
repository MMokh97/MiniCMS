using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniCMS.Models
{
    
    public class AdminViewModel
    {
        [Required]
        public string username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("password",ErrorMessage ="two passwords are not matched")]
        [Display(Name ="Confirm Password")]
        public string ConfPass { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string phone { get; set; }

        public string Logo { get; set; }

        [Display(Name ="Admin Status")]
        [Required]
        public bool IsAvtive { get; set; }

    }
}