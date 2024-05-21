using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Baskets
{
    public class SearchResponseDto
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string ProductImageFileName { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
       
        public int ProductCount { get; set; }
    }
}
