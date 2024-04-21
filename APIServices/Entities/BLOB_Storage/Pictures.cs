using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServices.Entities.BLOB_Storage
{
    [Table("Pictures")]
    public class Pictures
    {
        [Key]
        public int PictureID { get; set; }
        public string PictureData { get; set; }
        public int UserID { get; set; }
    }
}
