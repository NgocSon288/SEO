using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SEO_WEB.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Description { get; set; }

        public string Avatar { get; set; }

        [Required]
        public string Alt { get; set; }

        [Required]
        public string Alias { get; set; }

        public bool IsDeleted { get; set; }


        public List<Post> Posts { get; set; }


        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}