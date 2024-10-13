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
