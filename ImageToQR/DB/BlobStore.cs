using System.ComponentModel.DataAnnotations;

namespace ImageToQR.DB
{
    public class BlobStore
    {
        [Key]
        public Guid Uid { get; set; }
        public string Blob { get; set; }
    }
}