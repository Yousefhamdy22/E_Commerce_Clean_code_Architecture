using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class ShoppingCartItem
    {
       
       // public int ShoppingCartItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public decimal? totalPrice { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }    
        public Product Product { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }


      
    }
}
