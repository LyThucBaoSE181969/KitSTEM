using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.API.ResponseModel;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderResponseModel>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            var response = orders.Select(order => new OrderResponseModel
            {
                Id = order.Id,
                UserId = order.UserId,
                PaymentId = order.PaymentId,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                ShippingStatus = order.ShippingStatus,
                IsLabDownloaded = order.IsLabDownloaded,
                Price = order.Price,
                Discount = order.Discount,
                TotalPrice = order.TotalPrice,
                Note = order.Note,
                Payment = order.Payment,
                User = order.User

            });

            return Ok(response);
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult<OrderResponseModel>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var response = new OrderResponseModel
            {
                Id = order.Id,
                UserId = order.UserId,
                PaymentId = order.PaymentId,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                ShippingStatus = order.ShippingStatus,
                IsLabDownloaded = order.IsLabDownloaded,
                Price = order.Price,
                Discount = order.Discount,
                TotalPrice = order.TotalPrice,
                Note = order.Note,
                Payment = order.Payment,
                User = order.User

            };

            return Ok(response);
        }
    }
}
