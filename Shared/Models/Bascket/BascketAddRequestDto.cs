using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Bascket
{
    public class BascketAddRequestDto
    {
        /// <summary>
        /// شناسه کاربر
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// تعداد محصول
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// شناسه محصول
        /// </summary>
        public int ProductId { get; set; }
    }
}
