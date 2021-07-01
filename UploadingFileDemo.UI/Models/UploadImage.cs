using System.ComponentModel.DataAnnotations;

namespace UploadingFileDemo.UI.Models
{
    public class UploadImage
    {
        public int Id { get; set; }
        //[Required]
        public string Image { get; set; }
    }
}
