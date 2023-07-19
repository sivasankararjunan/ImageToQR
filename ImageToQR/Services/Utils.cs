using ImageToQR.DB;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using SQLitePCL;
using System.IO;
using System.Text;

namespace ImageToQR.Services
{
    public interface IImageService
    {
        byte[] GetImage(Guid uid);
        byte[] SaveImage(IFormFile objFile);
    }
    public class ImageService : IImageService
    {
        private readonly ImageDataContext _Context;
        public ImageService(ImageDataContext context)
        {
            this._Context = context;
        }
        public byte[] SaveImage(IFormFile objFile)
        {
            var image = Utilities.FiletoString(objFile);
            var uid = Guid.NewGuid();
            this._Context.BlobStores.Add(new BlobStore { Blob = image, Uid = uid });
            this._Context.SaveChanges();
            return Utilities.GenerateQrCode(uid);
        }
        public byte[] GetImage(Guid uid)
        {
            var image = _Context.BlobStores.FirstOrDefault(x => EF.Equals(x.Uid, uid));
            if (image == null)
            {
                throw new BadHttpRequestException("Invalid Uid");
            }
            return Convert.FromBase64String(image.Blob);
        }

    }
}
