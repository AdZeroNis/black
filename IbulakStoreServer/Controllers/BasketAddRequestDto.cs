namespace IbulakStoreServer.Controllers
{
    public class BasketAddRequestDto
    {
        public int UserId { get;  set; }
        public int Count { get;  set; }
        public int ProductId { get;  set; }
        public string Name { get; internal set; }
    }
}