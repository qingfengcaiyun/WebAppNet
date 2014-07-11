﻿using System.Collections.Generic;
using Glibs.Sql;

namespace WedDao.Dao.System
{
    public class AreaRelationDao
    {
        private Database db = null;
        private string sql = string.Empty;
        private Dictionary<string, object> param = null;

        public AreaRelationDao()
        {
            this.db = DbUtil.CreateDatabase();
        }

        public List<Dictionary<string, object>> GetList(int areaId)
        {
            this.sql = @"select [locationId],[cnName],[enName],[levelNo],[parentNo],[levelCnName],[levelEnName],[isLeaf] from [Sys_Location] where [locationId] in (select [locationId] from [Sys_AreaRelation] where [areaId]=@areaId order by itemIndex asc)";

            this.param = new Dictionary<string, object>();
            this.param.Add("areaId", areaId);

            return this.db.GetDataTable(this.sql, this.param);
        }

        public bool SaveList(int[] locationIds, int areaId)
        {
            if (locationIds != null && locationIds.Length > 0)
            {
                this.sql = @"delete from [Sys_AreaRelation] where [areaId]=@areaId;";

                this.param = new Dictionary<string, object>();
                this.param.Add("areaId", areaId);

                this.db.Update(this.sql, this.param);

                this.sql = @"insert into [Sys_AreaRelation] ([areaId],[locationId])values(@areaId,@locationId)";
                List<Dictionary<string, object>> paramsList = new List<Dictionary<string, object>>();

                for (int i = 0, j = locationIds.Length; i < j; i++)
                {
                    this.param = new Dictionary<string, object>();
                    this.param.Add("areaId", areaId);
                    this.param.Add("locationId", locationIds[i]);

                    paramsList.Add(this.param);
                }

                return this.db.Batch(this.sql, paramsList);
            }
            else
            {
                return false;
            }
        }
    }
}
