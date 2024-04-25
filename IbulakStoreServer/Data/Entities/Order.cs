using System.ComponentModel.DataAnnotations.Schema;

namespace IbulakStoreServer.Data.Entities
{
    public class Order
    {
        internal int productId;
        internal int userId;

        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual User User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
    }
}
