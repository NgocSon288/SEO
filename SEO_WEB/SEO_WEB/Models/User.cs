using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SEO_WEB.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Họ và tên không được bỏ trống")]
        public string DisplayName { get; set; }


        [Required(ErrorMessage = "Email không được bỏ trống")]
        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Alt { get; set; }

        public bool IsMember { get; set; }  // true là admin


        public List<Comment> Comments { get; set; }


        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}