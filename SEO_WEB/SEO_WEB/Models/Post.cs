using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SEO_WEB.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Avatar { get; set; }

        public string Alt { get; set; }

        public string Content { get; set; }

        public string Meta { get; set; }

        public string Title { get; set; }

        public string Alias { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey("City")]
        public int CityID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public City City { get; set; }

        public Category Category { get; set; }

        public List<Backlink> Backlinks { get; set; }

        public List<Comment> Comments { get; set; }
    }
}