using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Products
{
    public class SearchRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int? Count { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? SortBy { get; set; }


    }
}