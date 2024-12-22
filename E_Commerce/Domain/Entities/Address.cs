using E_Commerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Entities
{
    public class Address
    {

        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }  
        public virtual User User { get; set; }
    }
}
