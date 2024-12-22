using E_Commerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class PaymentDetails
    {
        [Key]
        public int PaymentId { get; set; } 
       
        public string PaymentMethod { get; set; }  
        public string PaymentStatus { get; set; } 
        public string TransactionId { get; set; }  

        public DateTime PaymentDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }  
        public virtual User User { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

    }
}
