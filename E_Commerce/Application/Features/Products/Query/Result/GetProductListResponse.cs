namespace E_Commerce.Application.Features.Products.Query.Result
{
    public class GetProductListResponse
    {
        public int id { get; set; }
        public int CategoryId { get; set; }
        public string Item_Name { get; set; }
        public int price { get; set; }
        public string? Description { get; set; }
        public int? solditems { get; set; } // stock
        public int? quantity { get; set; }
        public List<string>? ImageUrls { get; set; }  

    }
}
