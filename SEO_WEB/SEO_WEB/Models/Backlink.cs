using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SEO_WEB.Models
{
    [Table("Backlinks")]
    public class Backlink
    {
        [Key]
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Link { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }

        public Post Post { get; set; }
    }
}