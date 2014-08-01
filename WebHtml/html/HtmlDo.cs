using System.IO;
using System.Text;

namespace WebHtml.html
{
    public class HtmlDo
    {
        public static bool WriteHtml(string htmlStr, string dirPath, string fileName)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = dirPath + "/" + fileName;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, htmlStr, Encoding.UTF8);

            return File.Exists(filePath);
        }
    }
}
