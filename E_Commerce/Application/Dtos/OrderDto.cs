using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Application.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }

       
        public int UserId { get; set; }
       
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        //   public bool IsDelivered => DateTime.Now >= Date.

        [NotMapped]
        public List<int> Quantities { get; set; }

        public float TotalPrice { get; set; }
        public string OrderNumber { get; set; } = RandomOrderCode.GenerateCode(8);

      




    }
}
