using System.ComponentModel.DataAnnotations.Schema;

namespace IbulakStoreServer.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageFileName { get; set;}

        public virtual Category Category { get; set; } = default!;
        public virtual ICollection<Basket> Baskets { get; set; } = new HashSet<Basket>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
