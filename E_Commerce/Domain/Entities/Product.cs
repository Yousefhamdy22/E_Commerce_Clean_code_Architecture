using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.Identity;

namespace E_Commerce.Domain.Entities
{

    public class ProductImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int solditems { get; set; }
        public string? IsAvalible { get; set; }
        public int? averageRate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }




        // Factory that produces the product
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Category")]
        public int Categoryid { get; set; }
        public Category Category { get; set; }



        public ICollection<ProductImages>? Images { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ShoppingCartItem> CartItems { get; set; }
        
     

    }
}
