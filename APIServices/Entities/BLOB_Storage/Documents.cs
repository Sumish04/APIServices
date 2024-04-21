using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServices.Entities.BLOB_Storage
{
    [Table("Documents")]
    public class Documents
    {
        [Key]
        public int DocumentID { get; set; }

        public int UserID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileData { get; set; }
    }
}
