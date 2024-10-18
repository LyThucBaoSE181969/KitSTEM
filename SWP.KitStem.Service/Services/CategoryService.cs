using Microsoft.AspNetCore.Http;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels.RequestModel;

namespace SWP.KitStem.Service.Services
{
    public class CategoryService 
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseService> DeleteByIdAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Delete fail!")
                        .AddError("notFound", "Cannot found kit!");
                }
                
                _unitOfWork.Categories.Delete(category); 
                await _unitOfWork.SaveAsync();
                return new ResponseService()
                            .SetSucceeded(true)
                            .AddDetail("message", "Delete complete!");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Delete fail!")
                    .AddError("outOfService", "Cannot delete!");
            }
        }
        public async Task<ResponseService> UpdateCategoryAsync(CategoryUpdateRequest model)
        {
            try
            {
                var category = new KitsCategory()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Status = true
                };

                await _unitOfWork.Categories.Update(category);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Update complete");
            }
            catch
            {
                return new ResponseService()
                            .SetSucceeded(false)
                            .AddDetail("message", "Update fail")
                            .AddError("outOfService", "Cannot update");
            }
        }
        public async Task<ResponseService> CreateCategoryAsync(CategoryCreateRequest model)
        {
            try
            {
                var category = new KitsCategory()   
                {
                    Name = model.Name,
                    Description = model.Description!,
                    Status = true
                };

                await _unitOfWork.Categories.InsertAsync(category);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Create success");
            }
            catch
            {
                return new ResponseService()
                            .SetSucceeded(false)
                            .AddDetail("message", "Create fail")
                            .AddError("outOfService", "Cannot create");
            }
        }

        public async Task<ResponseService> GetCategoryByIdAsync(int id)
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetByIdAsync(id);
                if (categories == null)
                {
                    return new ResponseService()
                    .SetSucceeded(false)
                    .SetStatusCode(StatusCodes.Status404NotFound)
                    .AddDetail("message", "Get kit fail")
                    .AddError("error", "Cannot found kit");
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
                    .AddDetail("message", "Get kits successed")   
                    .AddDetail("data", new { categories });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get kits failed")
                    .AddError("error", "Cannot get kits");
            }
            
        }

    }
}
