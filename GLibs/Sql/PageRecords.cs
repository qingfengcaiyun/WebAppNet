using System.Collections.Generic;
using System.Text;

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
        private string indexPage;
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

        public string IndexPage
        {
            get { return indexPage; }
            set { indexPage = value; }
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

        public void BuildIndexPage(string pageKey)
        {
            StringBuilder indexPageT = new StringBuilder();
            if (this.lastPage > 1)
            {
                indexPageT.Append("<a href=\"");
                indexPageT.Append(pageKey);
                indexPageT.Append("_1.html\" target=\"_self\">首页</a>");

                indexPageT.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"");
                indexPageT.Append(pageKey);
                indexPageT.Append("_");
                indexPageT.Append(this.prevPage.ToString());
                indexPageT.Append(".html\" target=\"_self\">上一页</a>");

                indexPageT.Append("&nbsp;&nbsp;&nbsp;&nbsp;去第&nbsp;<select class=\"itemselect\" name=\"gpageNum\" id=\"gpageNum\" onchange=\"javascript:window.location.href=this.value\">");
                for (int j = this.firstPage; j < this.lastPage + 1; j++)
                {
                    indexPageT.Append("<option value=\"");
                    indexPageT.Append(pageKey);
                    indexPageT.Append("_");
                    indexPageT.Append(j);
                    indexPageT.Append(".html\"");
                    if (this.currentPage == j)
                    {
                        indexPageT.Append(" selected=\"selected\"");
                    }
                    indexPageT.Append(">");
                    indexPageT.Append(j.ToString());
                    indexPageT.Append("</option>");
                }
                indexPageT.Append("</select>");

                indexPageT.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"");
                indexPageT.Append(pageKey);
                indexPageT.Append("_");
                indexPageT.Append(this.nextPage);
                indexPageT.Append(".html\" target=\"_self\">下一页</a>");

                indexPageT.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"");
                indexPageT.Append(pageKey);
                indexPageT.Append("_");
                indexPageT.Append(this.lastPage);
                indexPageT.Append(".html\" target=\"_self\">末页</a>");
            }
            this.indexPage = indexPageT.ToString();
        }
    }
}
