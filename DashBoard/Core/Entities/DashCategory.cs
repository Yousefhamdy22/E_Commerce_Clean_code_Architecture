namespace DashBoard.Core.Entities
{
    public class DashCategory
    {

        public int CategorytId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Picture { get; set; }
        public int? TotalProducts { get; set; } = 0;
        public DateTime? CreatedDate { get; set; }

    }
}
