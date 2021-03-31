using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SEO_WEB.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        public DateTime CreatedTime { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public int UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }



        [ForeignKey("Post")]
        public int PostID { get; set; }
         
        [ForeignKey("User")]
        public int UserID { get; set; }

        public Post Post { get; set; }

        public User User { get; set; }
    }
}