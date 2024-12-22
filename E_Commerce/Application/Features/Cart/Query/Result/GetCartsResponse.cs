﻿namespace E_Commerce.Application.Features.Cart.Query.Result
{
    public class GetCartsResponse
    {
        public List<CartItemResponse> CartItems { get; set; } = new List<CartItemResponse>(); // List of items in the cart
        public decimal TotalPrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class CartItemResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
