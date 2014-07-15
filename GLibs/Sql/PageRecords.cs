using System.Collections.Generic;

namespace Glibs.Sql
{
    public class PageRecords
    {
        private int pageSize; // 每页显示的记录数目
        private int recordsCount; // 总记录数目
        private int currentPage; // 当前是第几页
        private int startIndex; // 开始于第几条记录
        private int pageCount; // 一共有几页
        private int firstPage; // 第一页
        private int prevPage; // 上一页
        private int nextPage; // 下一页
        private int lastPage; // 最后一页
        private string countSql; // 计数用的SQL
        private string countKey;
        private string querySql; // 查询用的SQL
        private string sqlFields;
        private string sqlTable;
        private string sqlWhere;
        private string sqlOrderBy;
        private List<Dictionary<string, object>> pageResult; // 分页记录集
        private string pageJSON; // 分页记录集JSON

        public PageRecords()
        {
            this.firstPage = 1;
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int RecordsCount
        {
            get { return recordsCount; }
            set { recordsCount = value; }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        public int StartIndex
        {
            get { return startIndex; }
            set { startIndex = value; }
        }

        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }

        public int FirstPage
        {
            get { return firstPage; }
            set { firstPage = value; }
        }

        public int PrevPage
        {
            get { return prevPage; }
            set { prevPage = value; }
        }

        public int NextPage
        {
            get { return nextPage; }
            set { nextPage = value; }
        }

        public int LastPage
        {
            get { return lastPage; }
            set { lastPage = value; }
        }

        public string CountSql
        {
            get { return "select count(" + countKey + ") from " + sqlTable + " where " + sqlWhere; }
            set { countSql = value; }
        }

        public string QuerySql
        {
            get { return "select top " + pageSize + " " + sqlFields + " from " + sqlTable + " where " + sqlWhere + " and " + countKey + " not in (select top " + startIndex + " " + countKey + " from " + sqlTable + " where " + sqlWhere + " order by " + sqlOrderBy + ") order by " + sqlOrderBy; }
            set { querySql = value; }
        }

        public List<Dictionary<string, object>> PageResult
        {
            get { return pageResult; }
            set { pageResult = value; }
        }

        public string PageJSON
        {
            get { return "{\"total\":\"" + recordsCount + "\", \"rows\":" + JsonDo.ListToJSON(pageResult) + "}"; }
            set { pageJSON = value; }
        }

        public string SqlTable
        {
            get { return sqlTable; }
            set { sqlTable = value; }
        }

        public string SqlWhere
        {
            get { return sqlWhere; }
            set { sqlWhere = value; }
        }

        public string SqlOrderBy
        {
            get { return sqlOrderBy; }
            set { sqlOrderBy = value; }
        }

        public string CountKey
        {
            get { return countKey; }
            set { countKey = value; }
        }

        public string SqlFields
        {
            get { return sqlFields; }
            set { sqlFields = value; }
        }

        public void SetBaseParam()
        {
            this.pageCount = this.recordsCount / this.pageSize;
            if (this.recordsCount % this.pageSize > 0)
            {
                this.pageCount++;
            }

            this.firstPage = 1;

            this.prevPage = this.currentPage - 1;
            if (this.prevPage < 1)
            {
                this.prevPage = 1;
            }

            this.nextPage = this.currentPage + 1;
            if (this.nextPage > this.pageCount)
            {
                this.nextPage = this.pageCount;
            }

            this.lastPage = this.pageCount;

            this.startIndex = this.pageSize * (this.currentPage - 1);
        }
    }
}
