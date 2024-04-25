using System.ComponentModel.DataAnnotations.Schema;
using IbulakStoreServer.Data.Entities;

namespace IbulakStoreServer.Data.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
