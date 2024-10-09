using SWP.KitStem.Repository;
using SWP.KitStem.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class CartService
    {
        private readonly UnitOfWork _unitOfWork;

        public CartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartModel>> GetCartAsync()
        {
            var carts = await _unitOfWork.Carts.GetAsync();
            return carts.Select(cart => new CartModel
            {
                UserId = cart.UserId,
                PackageId = cart.PackageId,
                PackageQuantity = cart.PackageQuantity,

            });
        }

        
    }
}
