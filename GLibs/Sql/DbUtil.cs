using Glibs.Util;

namespace Glibs.Sql
{
    public class DbUtil
    {
        public static Database CreateDatabase()
        {
            return new Database(WebPageCore.GetConnectionString(WebPageCore.GetAppSetting("ConnectionStringName")));
        }

        public static Database CreateDatabase(string ConnectionStringName)
        {
            return new Database(WebPageCore.GetConnectionString(ConnectionStringName));
        }
    }
}
