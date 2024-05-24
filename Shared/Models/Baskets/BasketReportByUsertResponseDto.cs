using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Baskets
{
    public class BasketReportByUsertResponseDto
    {

        public string UserId { get; set; }
      
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public object TotalSum { get; set; }
    }
}
