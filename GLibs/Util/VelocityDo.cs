using System.Collections.Generic;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Glibs.Util
{
    public class VelocityDo
    {
        public static string BuildStringByTemplate(string templateFile, string templateDir, Dictionary<string, object> content)
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
                foreach (KeyValuePair<string, object> kv in content)
                {
                    context.Put(kv.Key, kv.Value);
                }
            }

            StringWriter writer = new StringWriter();
            template.Merge(context, writer);

            return writer.ToString();
        }

        public Dictionary<string, string> BuildStringsByOneTemplate(string templateFile, string templateDir, Dictionary<string, Dictionary<string, object>> contents)
        {
            VelocityEngine vltEngine = new VelocityEngine();

            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, WebPageCore.GetMapPath(templateDir));
            vltEngine.SetProperty(RuntimeConstants.INPUT_ENCODING, "UTF-8");
            vltEngine.SetProperty(RuntimeConstants.OUTPUT_ENCODING, "UTF-8");

            vltEngine.Init();

            Dictionary<string, string> strs = new Dictionary<string, string>();
            Template template = vltEngine.GetTemplate(templateFile);
            VelocityContext context = null;
            StringWriter writer = new StringWriter();

            if (contents != null && contents.Count > 0)
            {
                foreach (KeyValuePair<string, Dictionary<string, object>> content in contents)
                {
                    context = new VelocityContext();
                    foreach (KeyValuePair<string, object> kv in content.Value)
                    {
                        context.Put(kv.Key, kv.Value);
                    }

                    template.Merge(context, writer);

                    strs.Add(content.Key, writer.ToString());
                }
            }

            return strs;
        }
    }
}
