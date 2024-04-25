namespace IbulakStoreServer.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageFileName { get; set; }
        public virtual ICollection<Product> Products { get; set; }=new HashSet<Product>();
    }
}
