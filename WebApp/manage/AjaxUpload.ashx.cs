using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApp.manage
{
    /// <summary>
    /// AjaxUpload 的摘要说明
    /// </summary>
    public class AjaxUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection files = context.Request.Files;

            string tempDir = "attached/temp/";

            string dirPath = context.Server.MapPath(tempDir);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            if (files.Count > 0)
            {
                string extName = Path.GetExtension(files[0].FileName).ToLower();
                string fileName = Guid.NewGuid().ToString() + extName;

                string filePath = context.Server.MapPath(tempDir + fileName);
                files[0].SaveAs(filePath);

                if (File.Exists(filePath))
                {
                    var model = new { FileName = fileName, FileSize = files[0].InputStream.Length };
                    string jsonString = new JavaScriptSerializer().Serialize(model);
                    context.Response.Write(jsonString);
                }
                else
                {
                    context.Response.Write("0");
                }
                context.Response.End();
            }
            else
            {
                context.Response.Write("0");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}