
using E_Commerce.Application.Dtos;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Presentation.Base;
using E_Commerce.Services.Abstraction.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.Api
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : AppControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;

        private readonly ILogger _logger;

        public OrderController(IUnitOfWork unitOfWork, ILogger<ProductController> logger , IOrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _orderService = orderService;
        }

        #region GetAll
        [HttpGet]
        public  ActionResult<IEnumerable<Order>> GetAll()
        {

            _logger.LogInformation("Getting all Orders");

            var orders =  _unitOfWork.ProductsRepo.GetAll();
              

            if (orders == null)
            {
                _logger.LogWarning("No Orders found.");
                return NotFound();
            }

          //  var filterOrderByuserid = await orders
                

            return Ok(orders);

        }
        #endregion

        #region GetById

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Getting Order with id: {id}");

            var Order = await _unitOfWork.OrdersRepo.GetById(id);
                

            if (Order == null)
            {
                _logger.LogWarning($"Order with id: {id} not found.");
                return NotFound();
            }

            return Ok();
        }
        #endregion

        #region CreateOrder
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto itemDto, [FromQuery] int userId)
        {
            _logger.LogInformation("Received request to create order for user {UserId}", userId);

            try
            {
                if (itemDto == null )
                {
                    return BadRequest("Invalid item.");
                }
                var order = await _orderService.CreateOrder(userId , itemDto);

                _logger.LogInformation("Successfully created order for user {UserId} with OrderId {OrderId}", userId, order.OrderId);

                return Ok(new
                {
                    Message = "Order created successfully",
                    orderid = order.OrderId,
                    TotalAmount = itemDto.TotalAmount,
                    order = itemDto.OrderDate,
                    OrderItems = order.OrderItems
                });


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create order for user {UserId}", userId);

              
                return BadRequest(new
                {
                    Message = "Failed to create order",
                    Error = ex.Message
                });
            }



        }

        #endregion
    }
}
