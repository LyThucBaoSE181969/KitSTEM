using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.API.RequestModel;
using SWP.KitStem.API.ResponseModel;
using SWP.KitStem.Service.BusinessModels;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryResponseModel>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var response = categories.Select(category => new CategoryResponseModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status

            });

            return Ok(response);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<CategoryResponseModel>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            var response = new CategoryResponseModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,

            };

            return Ok(response);
        }

        [HttpPost("categories")]
        public async Task<ActionResult> CreateCategory(CategoryRequestModel request)
        {
            var categoryModel = new CategoryModel
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
            };

            var rs = await _categoryService.InsertCategoryAsync(categoryModel);
            categoryModel.Id = rs;
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryModel.Id }, categoryModel);
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryRequestModel request)
        {
            var categoryModel = new CategoryModel
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
            };

            var success = await _categoryService.UpdateCategoryAsync(id, categoryModel);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
