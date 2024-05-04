using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Product
{
    public class ProductAddRequestDto
    { 
        /// <summary>
        /// نام محصول
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// تعداد محصول
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// قیمت محصول
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// ایدی دسته بندی
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// عکس محصول
        /// </summary>
        public string ImageFileName { get; set; }
    }
}
