using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Baskets
{
    public class BasketReportByUserRequestDto
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
      
        public string? UserId { get; set; }
        
    }
}
