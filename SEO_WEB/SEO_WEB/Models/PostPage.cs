using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEO_WEB.Models
{
    [Table("PostPages")]
    public class PostPage
    {
        [Key]
        public int ID { get; set; }

        public string Metas { get; set; }

        [Required(ErrorMessage = "Title không được bỏ trống")]
        public string Title { get; set; }
    }
}