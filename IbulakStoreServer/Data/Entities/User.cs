namespace IbulakStoreServer.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; } = new HashSet<Basket>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}