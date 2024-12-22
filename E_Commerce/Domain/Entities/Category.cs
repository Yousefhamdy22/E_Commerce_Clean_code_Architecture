namespace E_Commerce.Domain.Entities
{
    public class Category
    {

        public int CategoryId { get; set; }  
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string? Picture { get; set; }
        public int? TotalProducts { get; set; } = 0;

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
