using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glibs.Sql;

namespace WebDao.Dao.Renovation
{
    public class WorkPicDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public WorkPicDao()
        {
            this.db = DbUtil.CreateDatabase();
        }


    }
}
