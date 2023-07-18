using QRCoder;
using System.Drawing;
using System.Text;

namespace ImageToQR.Services
{
    public static class Utilities
    {
        public static string FiletoString(IFormFile objFile)
        {

            var result = new StringBuilder();
            using (var stream = new MemoryStream())
            {
                objFile.CopyTo(stream);
                var bytes = stream.ToArray();
                var stringValue = Convert.ToBase64String(bytes);
            }

            using (var reader = new StreamReader(objFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }

        internal static byte[] GenerateQrCode(Guid uid)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode($"https://localhost:7274/WeatherForecast/Get/{uid}", QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            using (var stream = new MemoryStream())
            {
                QrBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            };
        }
    }
}
