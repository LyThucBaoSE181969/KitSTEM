                            using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly ComponentService _componentService;

        public ComponentController(ComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpDelete("delete-component")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var components = await _componentService.DeleteComponent(id);
            if (!components.Succeeded)
            {
                return StatusCode(components.StatusCode,
                    new { status = components.Status, details = components.Details });
            }

            return Ok(new { status = components.Status, details = components.Details });
        }

        [HttpPut("update-componet")]
        public async Task<IActionResult> UpdateComponent(ComponentUpdateRequest request)
        {
            var components = await _componentService.UpdateComponentAsync(request);
            if (!components.Succeeded)
            {
                return StatusCode(components.StatusCode,
                    new { status = components.Status, details = components.Details });
            }

            return Ok(new { status = components.Status, details = components.Details });
        }

        [HttpPost("create-component")]
        public async Task<IActionResult> CreateComponent(ComponentCreateRequest request)
        {
            var components = await _componentService.CreateComponentAsync(request);
            if (!components.Succeeded)
            {
                return StatusCode(components.StatusCode,
                    new { status = components.Status, details = components.Details });
            }

            return Ok(new { status = components.Status, details = components.Details });
        }

        [HttpGet("component/{id}")]
        public async Task<IActionResult> GetComponentById(int id)
        {
            var components = await _componentService.GetComponentByIdAsync(id);
            if (!components.Succeeded)
            {
                return StatusCode(components.StatusCode,
                    new { status = components.Status, details = components.Details });
            }

            return Ok(new { status = components.Status, details = components.Details });
        }

        [HttpGet("components")]
        public async Task<IActionResult> GetComponents()
        {
            var components = await _componentService.GetComponentsAsync();
            if (!components.Succeeded)
            {
                return StatusCode(components.StatusCode, 
                    new { status = components.Status, details = components.Details });
            }

            return Ok(new { status = components.Status, details = components.Details });
        }

        
    }
}
