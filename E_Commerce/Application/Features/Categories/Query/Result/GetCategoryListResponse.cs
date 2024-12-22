﻿namespace E_Commerce.Application.Features.Categories.Query.Result
{
    public class GetCategoryListResponse

    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public int? TotalProducts { get; set; }
        public List<ProductResponseList>? ProductResponseList { get; set; }
    }

    public class ProductResponseList
    {
        public int Id { get; set; }
        public string Item_Name { get; set; }
        public int price { get; set; }
        public string Description { get; set; }
        public int solditems { get; set; }
        public int quantity { get; set; }
        public List<string>? ImageUrls { get; set; }  // Added this property

    }

}
