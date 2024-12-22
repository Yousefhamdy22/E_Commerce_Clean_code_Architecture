namespace E_Commerce.Application.Dtos
{
    public class CartItemDto
    {
      //  public int ShoppingCartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public decimal Price { get; set; }

    }
}
