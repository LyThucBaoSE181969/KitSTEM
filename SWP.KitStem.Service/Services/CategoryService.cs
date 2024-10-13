using Microsoft.AspNetCore.Http;
using SWP.KitStem.Repository;
using SWP.KitStem.Service.BusinessModels;
using SWP.KitStem.Service.Services.IService;

namespace SWP.KitStem.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseService> GetCategoryByIdAsync(int id)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetByIdAsync(id);
                if (categories == null)
                {
                    return new ResponseService()
                    .SetSucceeded(true)
                    .SetStatusCode(StatusCodes.Status404NotFound)
                    .AddDetail("message", "Get kit fail")
                    .AddError("error", "Cannot find kit");
                }
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("data", new { categories });

            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get kit fail")
                    .AddError("error", "Cannot get kit");
            }
        }
        public async Task<ResponseService> GetCategoriesAsync()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAsync();
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Get kits success")   
                    .AddDetail("data", new { categories });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get kits fail")
                    .AddError("error", "Cannot get kits");
            }
            
        }

    }
}
