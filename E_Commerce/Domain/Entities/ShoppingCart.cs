using E_Commerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }  
        public User User { get; set; }  
        
        public ICollection<ShoppingCartItem> CartItems { get; set; }

    }
}
