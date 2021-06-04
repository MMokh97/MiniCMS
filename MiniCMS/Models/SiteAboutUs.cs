using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiniCMS.Models
{
    public class SiteAboutUs
    {
        [Key]
        public int Id { get; set; }

        public string Details { get; set; }

        public string Image { get; set; }

        public string FaceBook { get; set; }

        public string twitter { get; set; }

        public string WhatsApp { get; set; }

        public string Instagram { get; set; }

        public string Telegram { get; set; }

        public string Email { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users user { get; set; }

        public int LangId { get; set; }
        [ForeignKey("LangId")]
        public Language Lang { get; set; }





    }
}