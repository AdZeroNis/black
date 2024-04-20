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
        [ForeignKey("Categori")]
        public int CategoriId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string ImageFileName { get; set;}

        public virtual Categori Categoris { get; set; }
    }
}
