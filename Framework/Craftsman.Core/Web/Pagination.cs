using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Web
{
    public class Pagination
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }

        public Pagination()
        {
            PageNo = 1;
            PageSize = 9999;
            TotalPage = 1;
            TotalCount = 1;
        }
    }
}
