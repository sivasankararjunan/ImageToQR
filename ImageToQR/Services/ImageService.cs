using System.IO;
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
                var stringValue = Encoding.Default.GetString(bytes);
            }

            using (var reader = new StreamReader(objFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }
    }
}
