using E_Commerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class Review
    {

        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;

        [ForeignKey("user")]
        public int UserId { get; set; }
        public User user { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
