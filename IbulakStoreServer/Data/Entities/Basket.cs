using System.ComponentModel.DataAnnotations.Schema;

namespace IbulakStoreServer.Data.Entities
{
    public class Basket
    {
        public int Id { get; set; }
      
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Count { get; set; }

        public virtual Product Product { get; set; } = default!;
        public virtual User User { get; set; } = default!;
    }
}