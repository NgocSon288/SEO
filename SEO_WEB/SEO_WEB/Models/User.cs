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

        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Avatar { get; set; }

        public string Alt { get; set; }

        public bool IsMember { get; set; }  // true là admin


        public List<Comment> Comments { get; set; }
    }
}