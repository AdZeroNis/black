using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Baskets
{
    public class BasketTotalSalesRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
