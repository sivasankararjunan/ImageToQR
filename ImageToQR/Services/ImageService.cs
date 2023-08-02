using ImageToQR.DB;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using SQLitePCL;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageToQR.Services
{
    public interface IImageService
    {
        byte[] GetImage(Guid uid);
        byte[] SaveImage(IFormFile objFile);
        byte[] GetQr(Guid uid);
        Task deleteImageAsync(Guid uuid);
    }
    public class ImageService : IImageService
    {
        private readonly ImageDataContext _Context;
        public static List<Guid> pv = new List<Guid>();
        private readonly IServiceScopeFactory ssf;
        public ImageService(IServiceScopeFactory servicef, ImageDataContext context)
        {
            this._Context = context;
            this.ssf = servicef;
        }
        public byte[] SaveImage(IFormFile objFile)
        {
            var image = Utilities.FiletoString(objFile);
            Guid uid;
            uid=Guid.NewGuid();
            this._Context.BlobStores.Add(new BlobStore { Blob = image, Uid = uid });
            this._Context.SaveChanges();
            return Utilities.GenerateQrCode(uid);
        }
        public byte[] GetQr(Guid uid)
        {
            var image = _Context.BlobStores.FirstOrDefault(x => EF.Equals(x.Uid, uid));
            if (image == null)
            {
                throw new BadHttpRequestException("Invalid Uid");
            }
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
        public async Task deleteImageAsync(Guid uuid)
        {
            if (!pv.Contains(uuid))
            {
                pv.Add(uuid);
                
                try
                {
                    using (var context = ssf.CreateScope().ServiceProvider.GetRequiredService<ImageDataContext>())
                    {
                        await Task.Delay(240000);
                        context.BlobStores.Remove(context.BlobStores.FirstOrDefault(x => EF.Equals(x.Uid, uuid)));
                        context.SaveChanges();
                        pv.Remove(uuid);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
