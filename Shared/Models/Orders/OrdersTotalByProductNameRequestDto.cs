using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Orders
{
    public class OrdersTotalByProductNameRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string? ProductName { get; set; }
    }
}
