using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniCMS.Models
{
    public class Language
    {
        [Key]
        public int ID { get; set; }

        public string LanguageName { get; set; }
    }
}