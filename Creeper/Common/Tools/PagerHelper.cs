using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Tools
{
    public class PagerHelper
    {
        public PagerHelper(int count, int pageSize,int currentPage=1)
        {
            PageSize = pageSize;
            PageCount = count % pageSize == 0 ? count / pageSize : (count / pageSize + 1);
            CurrentPage = currentPage;
        }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { set; get; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { set; get; }

        /// <summary>
        /// 上一页
        /// </summary>
        public int PrePage
        {
            get
            {
                return CurrentPage > 1 ? CurrentPage - 1 : CurrentPage;
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public int NextPage {
            get
            {
                return CurrentPage < PageCount ? CurrentPage + 1 : CurrentPage;
            }
        }
    }
}