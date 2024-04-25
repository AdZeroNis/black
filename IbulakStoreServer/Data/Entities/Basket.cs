using System.ComponentModel.DataAnnotations.Schema;
using IbulakStoreServer.Data.Entities;

namespace IbulakStoreServer.Data.Entities
{
    public class Basket
    {
        internal int userId;

        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int Count { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual User User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public object UsertId { get; internal set; }

        public static implicit operator Basket?(Order? v)
        {
            throw new NotImplementedException();
        }
    }
}
