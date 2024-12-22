using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.Domain.Entities
{
    public class Order
    {

     
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public double Price { get; set; }
       

        //   public bool IsDelivered => DateTime.Now >= Date.
        public string OrderNumber { get; set; }
        [NotMapped]
        public List<int> Quantities { get; set; }


        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }  // Navigation property

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<PaymentDetails> PaymentDetails { get; set; }

    }
}
