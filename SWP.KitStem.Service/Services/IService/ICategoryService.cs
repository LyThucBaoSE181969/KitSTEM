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
        //Task<ResponseService> GetByIdAsync(int id);
        //Task<ResponseService> CreateAsync(CategoryCreateDTO category);
        //Task<ResponseService> UpdateAsync(CategoryUpdateDTO category);
        //Task<ResponseService> RemoveByIdAsync(int id);
        //Task<ResponseService> RestoreByIdAsync(int id);
    }
}
