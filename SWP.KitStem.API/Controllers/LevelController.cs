using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly LevelService _levelService;

        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpPost("create-level")]
        public async Task<IActionResult> CreateLevel(LevelCreateRequest request)
        {
            var level = await _levelService.CreateLevelAsync(request);
            if (!level.Succeeded)
            {
                return StatusCode(level.StatusCode,
                   new { status = level.Status, details = level.Details });
            }

            return Ok(new { status = level.Status, details = level.Details });
        }

        [HttpGet("levels")]
        public async Task<IActionResult> GetLevels()
        {
            var levels = await _levelService.GetLevelsAsync();
            if (!levels.Succeeded)
            {
                return StatusCode(levels.StatusCode, 
                    new { status = levels.Status, details = levels.Details });
            }
            return Ok(new { status = levels.Status, details = levels.Details });
        }

        [HttpGet("level/{id}")]
        public async Task<IActionResult> GetLevelById(int id)
        {
            var level = await _levelService.GetLevelByIdAsync(id);
            if (!level.Succeeded)
            {
                return StatusCode(level.StatusCode,
                    new { status = level.Status, details = level.Details });
            }
            return Ok(new { status = level.Status, details = level.Details });
        }

        [HttpPut("update-level")]
        public async Task<IActionResult> UpdateLevel(LevelUpdateRequest request)
        {
            var level = await _levelService.UpdateLevelAsync(request);
            if (!level.Succeeded)
            {
                return StatusCode(level.StatusCode,
                    new { status = level.Status, details = level.Details });
            }
            return Ok(new { status = level.Status, details = level.Details });
        }

        [HttpDelete("delete-level/{id}")]
        public async Task<IActionResult> DeleteLevel(int id)
        {
            var level = await _levelService.DeleteLevelAsync(id);
            if (!level.Succeeded)
            {
                return StatusCode(level.StatusCode,
                    new { status = level.Status, details = level.Details });
            }
            return Ok(new { status = level.Status, details = level.Details });
        }
    }
}
