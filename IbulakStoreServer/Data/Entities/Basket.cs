using System.ComponentModel.DataAnnotations.Schema;
using IbulakStoreServer.Data.Entities;

namespace IbulakStoreServer.Data.Entities
{
    public class Basket
    {

        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int Count { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual AppUser User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public DateTime CreatedAt { get; internal set; }
      
    }
}
