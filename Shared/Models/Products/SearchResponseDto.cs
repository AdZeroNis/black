using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Products
{
    public class SearchResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductImageFileName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImageFileName { get; set; }
       
    }
}