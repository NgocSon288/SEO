using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.UI.WebControls;

namespace SEO_WEB.Models
{
    [Table("Homes")]
    public class Home
    {
        [Key]
        public int ID { get; set; }

        public string Keywords { get; set; }

        public string Image { get; set; }   //

        public string Metas { get; set; }

        [Required(ErrorMessage = "Title không được bỏ trống")]
        public string Title { get; set; }
         
        public string Alt { get; set; }

        public string Footer { get; set; }

        public string Logo { get; set; }

        public string AltLogo { get; set; }


        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUploadLogo { get; set; }
    }
}