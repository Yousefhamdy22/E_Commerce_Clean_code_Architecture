
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.Core.Entities
{
    public class DashProductImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public virtual DashProduct product { get; set; }
    }
    public class DashProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int? solditems { get; set; }
        public bool IsAvalibel { get; set; }
        public int? averageRate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? DisplayCategory { get; set; }


        public ICollection<DashProductImages>? Images { get; set; }

    }
}
