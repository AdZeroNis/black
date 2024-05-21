using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbulakStoreServer.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get;  set; }
      
        public virtual ICollection<Basket> Baskets { get; set; } = new HashSet<Basket>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
