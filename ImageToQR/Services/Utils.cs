using ImageToQR.DB;
using SQLitePCL;
using System.IO;
using System.Text;

namespace ImageToQR.Services
{
    public interface IImageService
    {
        Guid SaveImage(IFormFile objFile);
    }
    public class ImageService : IImageService
    {
        private readonly ImageDataContext _Context;
        public ImageService(ImageDataContext context)
        {
            this._Context = context;
        }
        public Guid SaveImage(IFormFile objFile)
        {
            var image = Utilities.FiletoString(objFile);
            var uid = Guid.NewGuid();
            this._Context.BlobStores.Add(new BlobStore { Blob = image, Uid = uid });
            this._Context.SaveChanges();
            return uid;
        }
    }
}
