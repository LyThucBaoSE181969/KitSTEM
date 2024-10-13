using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categories = await _categoryService.GetCategoryByIdAsync(id);
            if (!categories.Succeeded)
            {
                return StatusCode(categories.StatusCode, new { status = categories.Status, details = categories.Details });
            }

            return Ok(new { status = categories.Status, details = categories.Details });
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if (!categories.Succeeded)
            {
                return StatusCode(categories.StatusCode, new { status = categories.Status, details = categories.Details });
            }

            return Ok(new { status = categories.Status, details = categories.Details });
        }
        
    }
}
