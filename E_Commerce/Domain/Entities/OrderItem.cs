using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? totalPrice { get; set; }
        public decimal? DeliveryPrice { get; set; }


        [ForeignKey("Order")]
        public int OrderId { get; set; }  
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; }
        public  List<Product> Products { get; set; } = new List<Product>();


    }
}
