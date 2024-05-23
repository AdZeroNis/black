using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Orders
{
    public class OrderAllTotalResponseDto
    {
        public int? TotalSum { get; set; }
        public int? Count { get; set; }
    }
}
