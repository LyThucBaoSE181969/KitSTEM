using SWP.KitStem.Repository;
using SWP.KitStem.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class OrderService        
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAsync();
            return orders.Select(order => new OrderModel
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
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return null;

            return new OrderModel
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
        }
    }
}
