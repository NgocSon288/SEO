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

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Avatar { get; set; }

        public string Alt { get; set; }

        public string Alias { get; set; }

        public bool IsDeleted { get; set; }


        public List<Post> Posts { get; set; }
    }
}