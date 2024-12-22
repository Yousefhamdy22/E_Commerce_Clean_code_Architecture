namespace E_Commerce.Application.Dtos
{
    public class ShoppingCartDto
    {

        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public decimal TotalAmount { get; set; }
    }
}
