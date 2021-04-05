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

        [Required(ErrorMessage ="Tên bài viết không được bỏ trống")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Mô tả không được bỏ trống")]
        public string Description { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Alt không được bỏ trống")]
        public string Alt { get; set; }

        public string Metas { get; set; }

        [Required(ErrorMessage = "Nội dung bài viết không được bỏ trống")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Title không được bỏ trống")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Title h1 không được bỏ trống")]
        public string TitleH1 { get; set; }

        [Required(ErrorMessage = "Alias không được bỏ trống")]
        public string Alias { get; set; }

        public DateTime CreatedTime { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPriority { get; set; }    //

        public int View { get; set; }   //

        [ForeignKey("City")]
        public int CityID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public City City { get; set; }

        public Category Category { get; set; }

        public List<Backlink> Backlinks { get; set; }

        public List<Comment> Comments { get; set; }  

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}