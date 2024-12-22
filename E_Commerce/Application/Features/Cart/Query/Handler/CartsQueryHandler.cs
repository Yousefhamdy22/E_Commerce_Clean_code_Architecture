//using AutoMapper;
//using E_Commerce.Application.Base;
//using E_Commerce.Application.Features.Cart.Query.Models;
//using E_Commerce.Application.Features.Cart.Query.Result;
//using E_Commerce.Services.Abstraction.ShoppingCartService;
//using MediatR;

//namespace E_Commerce.Application.Features.Cart.Query.Handler
//{
//    public class CartsQueryHandler : ResponseHandler,
//        IRequestHandler<GetCartsByUserIdQuery, Response<GetCartsResponse>>,
//         IRequestHandler<GetTotalPaymentQuery, Response<GetTotalPaymentResponse>>
//    {
//        #region Fields
//        private readonly IMapper _mapper;
//        private readonly IshoppingCartServices _cartServices; // Injecting the CartServices

//        #endregion

//        #region Constructor
//        public CartsQueryHandler(IMapper mapper, IshoppingCartServices cartServices)
//        {
//            _mapper = mapper;
//            _cartServices = cartServices; // Initialize the CartServices
//        }

//        public async Task<Response<GetCartsResponse>> Handle(GetCartsByUserIdQuery request, CancellationToken cancellationToken)
//        {
//            var cartItems = await _cartServices.(request.UserId);
//            var cartResponse = _mapper.Map<GetCartsResponse>(cartItems);
//            //   return Response<GetCartsResponse>.Success(cartResponse);
//            return Success<GetCartsResponse>(cartResponse);
//        }
//        public async Task<Response<GetTotalPaymentResponse>> Handle(GetTotalPaymentQuery request, CancellationToken cancellationToken)
//        {
//            // Get total payment for the user
//            var totalPayment = await _cartServices.TotalPayment(request.UserId);
//            var response = new GetTotalPaymentResponse(totalPayment);
//            //  return Response<GetTotalPaymentResponse>.Success(response);
//            return Success<GetTotalPaymentResponse>(response);

//        }

//        #endregion
//    }
//}
