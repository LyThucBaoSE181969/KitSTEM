using SWP.KitStem.Service.BusinessModels;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services.IService
{
    public interface ICategoryService
    {
        Task<ResponseService> GetCategoriesAsync();
        Task<ResponseService> GetCategoryByIdAsync(int id);
        Task<ResponseService> CreateCategoryAsync(CreateCategoryRequest category);
        //Task<ResponseService> UpdateAsync(CategoryUpdateDTO category);
        //Task<ResponseService> RemoveByIdAsync(int id);
        //Task<ResponseService> RestoreByIdAsync(int id);
    }
}
                                        