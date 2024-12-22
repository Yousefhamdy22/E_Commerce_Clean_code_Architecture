namespace E_Commerce.Application.Features.Cart.Query.Result
{
    public class GetTotalPaymentResponse
    {
        public decimal TotalAmount { get; set; }

        public GetTotalPaymentResponse(decimal totalAmount)
        {
            TotalAmount = totalAmount;
        }
    }
}
