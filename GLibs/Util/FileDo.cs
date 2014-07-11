using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glibs.Util;

namespace GLibs.Util
{
    public class FileDo
    {
        //文件上传，将缓存文件夹“/attached/temp”中的文件复制到指定分类文件夹中，并删除源文件
        public static bool UploadFile(string fileName, FileType fileType)
        {
            DateTime now = DateTime.Now;

            string year = string.Format("{0:D4}", now.Year);
            string month = string.Format("{0:D2}", now.Month);
            string day = string.Format("{0:D2}", now.Day);
            string hour = string.Format("{0:D2}", now.Hour);

            string typeStr = string.Empty;

            switch (fileType)
            {
                case FileType.Audio: typeStr = "audio"; break;
                case FileType.Documents: typeStr = "docs"; break;
                case FileType.Images: typeStr = "images"; break;
                case FileType.Video: typeStr = "video"; break;
                default: typeStr = "others"; break;
            }

            string tagDir = "attached/" + typeStr + "/" + year + "/" + month + "/" + day + "/" + hour;

            string dirPath = WebPageCore.GetMapPath(tagDir);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string tagFilePath = WebPageCore.GetMapPath(tagDir + "/" + fileName);
            string srcFilePath = WebPageCore.GetMapPath("attached/temp/" + fileName);

            if (File.Exists(tagFilePath))
            {
                File.Delete(tagFilePath);
            }

            File.Move(srcFilePath, tagFilePath);

            if (File.Exists(srcFilePath))
            {
                File.Delete(srcFilePath);
            }

            return File.Exists(tagFilePath);
        }

        //批量写入html文件，返回未写入的html清单
        public static List<string> WriteHtmlFiles(string webPath, Dictionary<string, string> content)
        {
            if (content != null && content.Count > 0)
            {
                webPath = WebPageCore.GetMapPath(webPath);

                if (!Directory.Exists(webPath))
                {
                    Directory.CreateDirectory(webPath);
                }

                webPath = webPath + @"\";

                List<string> files = new List<string>();

                foreach (KeyValuePair<string, string> kv in content)
                {
                    File.WriteAllText(webPath + kv.Key + ".html", kv.Value, Encoding.UTF8);

                    if (!File.Exists(webPath + kv.Key + ".html"))
                    {
                        files.Add(kv.Key + ".html");
                    }
                }

                return files;
            }
            else
            {
                return null;
            }
        }


    }

    public enum FileType { Audio, Documents, Images, Others, Video }
}
