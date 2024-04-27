using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace share.Models.Basket
{
    internal class BasketAddRequestDto
    {
        public int UserId { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
