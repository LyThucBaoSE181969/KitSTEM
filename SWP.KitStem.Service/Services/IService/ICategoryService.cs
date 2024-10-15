using SWP.KitStem.Service.BusinessModels.RequestModel;

namespace SWP.KitStem.Service.Services.IService
{
    public interface ICategoryService
    {
        Task<ResponseService> GetCategoriesAsync();
        Task<ResponseService> GetCategoryByIdAsync(int id);
        Task<ResponseService> CreateCategoryAsync(CategoryCreateRequest category);
        Task<ResponseService> UpdateCategoryAsync(CategoryUpdateRequest category);
        Task<ResponseService> DeleteByIdAsync(int id);
    }
}
                                        