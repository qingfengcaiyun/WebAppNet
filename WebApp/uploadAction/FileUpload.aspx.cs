using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.manage
{
    public partial class FileUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void UploadFile()
        {
            string rs = "0";

            HttpFileCollection files = Request.Files;

            string tempDir = "attached/temp/";

            string dirPath = Server.MapPath(tempDir);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);

            if (files.Count > 0)
            {
                string extName = Path.GetExtension(files[0].FileName).ToLower();
                string fileName = Guid.NewGuid().ToString() + extName;

                string filePath = Server.MapPath(tempDir + fileName);
                files[0].SaveAs(filePath);

                if (File.Exists(filePath))
                {
                    var model = new { FileName = fileName, FileSize = files[0].InputStream.Length };
                    rs = new JavaScriptSerializer().Serialize(model);
                }
            }

            Response.Write(rs);
        }
    }
}