namespace E_Commerce.Application.Features.Products.Query.Result
{
    public class GetProductPaginatedListResponse
    {
        public int ProductID { get; set; }
        public int CategoryId { get; set; }
        public string Item_Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int? Solditems { get; set; }
        public int? Quantity { get; set; }
        public List<string> ImageUrls { get; set; }  


        public GetProductPaginatedListResponse(int productId, int categoryId, string itemName,
        decimal price, string? description, int? solditems, int? quantity, List<string> imageUrls)
        {
            ProductID = productId;
            CategoryId = categoryId;
            Item_Name = itemName;  
            Price = price;
            Description = description;
            Solditems = solditems;
            Quantity = quantity;
            ImageUrls = imageUrls;
        }

    }
}
