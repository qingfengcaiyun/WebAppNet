using System.Collections;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Glibs.Util
{
    public class VelocityDo
    {
        public static string BuildStringByTemplate(string templateFile, string templateDir, Hashtable content)
        {
            VelocityEngine vltEngine = new VelocityEngine();
            string dir = WebPageCore.GetMapPath(templateDir);
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, dir);
            vltEngine.SetProperty(RuntimeConstants.INPUT_ENCODING, "UTF-8");
            vltEngine.SetProperty(RuntimeConstants.OUTPUT_ENCODING, "UTF-8");

            vltEngine.Init();

            Template template = vltEngine.GetTemplate(templateFile);

            VelocityContext context = new VelocityContext();

            if (content != null && content.Count > 0)
            {
                foreach (DictionaryEntry de in content)
                {
                    context.Put(de.Key.ToString(), de.Value);
                }
            }

            StringWriter writer = new StringWriter();
            template.Merge(context, writer);

            return writer.ToString();
        }
    }
}
