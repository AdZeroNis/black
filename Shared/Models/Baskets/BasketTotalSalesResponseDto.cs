using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Baskets
{
    public class BasketTotalSalesResponseDto
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int? TotalSum { get; set; }
    }
}
