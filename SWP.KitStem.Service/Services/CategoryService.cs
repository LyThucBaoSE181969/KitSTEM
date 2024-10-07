using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.KitStem.Service.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAsync(includeProperties: "Kits");
            return categories.Select(category => new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,
                Kits = category.Kits.Select(kit => new KitModel
                {
                    Id = kit.Id,
                    CategoryId = kit.CategoryId,
                    Name = kit.Name,
                    Brief = kit.Brief,
                    Description = kit.Description,
                    PurchaseCost = kit.PurchaseCost,
                    Status = kit.Status
                }).ToList()
            });
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAsync(
                filter: c => c.Id == id,
                includeProperties: "Kits"
            );

            var categoryEntity = category.FirstOrDefault(); // Lấy danh mục đầu tiên từ kết quả

            if (categoryEntity == null) return null;

            return new CategoryModel
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
                Description= categoryEntity.Description,
                Status = categoryEntity.Status,
                Kits = categoryEntity.Kits.Select(Kit => new KitModel
                {
                    Id = Kit.Id,
                    Name = Kit.Name
                }).ToList()
            };
        }

        public async Task<int> InsertCategoryAsync(CategoryModel categoryModel)
        {
            var categoryEntity = new Category
            {
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                Status = categoryModel.Status,

            };

            await _unitOfWork.Categories.InsertAsync(categoryEntity);
            await _unitOfWork.SaveAsync();
            return categoryEntity.Id;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryModel categoryModel)
        {
            var categoryToUpdate = await _unitOfWork.Categories.GetByIdAsync(id);
            if (categoryToUpdate == null) return false;

            categoryToUpdate.Name = categoryModel.Name;
            categoryToUpdate.Description = categoryModel.Description;
            categoryToUpdate.Status = categoryModel.Status;

            _unitOfWork.Categories.Update(categoryToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return false;

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _unitOfWork.Categories.IsExist(id);
        }

        public async Task<IEnumerable<KitModel>> GetKitsByCategoryIdAsync(int id)
        {
            var kits = await _unitOfWork.Kits.GetAsync(p => p.CategoryId == id);
            return kits.Select(kit => new KitModel
            {
                Id = kit.Id,
                CategoryId = kit.CategoryId,
                Name = kit.Name,
                Brief = kit.Brief,
                Description = kit.Description,
                PurchaseCost = kit.PurchaseCost,
                Status = kit.Status
            });
        }
    }
}
