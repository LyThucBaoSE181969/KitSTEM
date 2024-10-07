using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.API.RequestModel;
using SWP.KitStem.API.ResponseModel;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private readonly LabService _labService;

        public LabController(LabService labService)
        {
            _labService = labService;
        }

        [HttpGet("labs")]
        public async Task<ActionResult<IEnumerable<Lab>>> GetLabs()
        {
            var labs = await _labService.GetLabsAsync();
            var response = labs.Select(lab => new LabResponseModel
            {
                Id = lab.Id,
                LevelId = lab.LevelId,
                KitId = lab.KitId,
                Name = lab.Name,
                Url = lab.Url,
                Price = lab.Price,
                MaxSupportTimes = lab.MaxSupportTimes,
                Author = lab.Author,
                Status = lab.Status,
                Kit = lab.Kit,
                Level = lab.Level,
                OrderSupports = lab.OrderSupports,
                Packages = lab.Packages
            });
            return Ok(response);
        }

        [HttpGet("lab/{id}")]
        public async Task<ActionResult<Lab>> GetLabById(Guid id)
        {
            var lab = await _labService.GetLabByIdAsync(id);
            if (lab == null) return NotFound();

            var response = new LabResponseModel
            {
                Id = lab.Id,
                LevelId = lab.LevelId,
                KitId = lab.KitId,
                Name = lab.Name,
                Url = lab.Url,
                Price = lab.Price,
                MaxSupportTimes = lab.MaxSupportTimes,
                Author = lab.Author,
                Status = lab.Status,
                Kit = lab.Kit,
                Level = lab.Level,
                OrderSupports = lab.OrderSupports,
                Packages = lab.Packages
            };
            return Ok(response);
        }

        [HttpPut("lab/{id}")]
        public async Task<IActionResult> UpdateLab(Guid id, LabRequestModel request)
        {
            var labModel = new LabModel
            {
                LevelId = request.LevelId,
                KitId = request.KitId,
                Name = request.Name,
                Url = request.Url,
                Price = request.Price,
                MaxSupportTimes = request.MaxSupportTimes,
                Author = request.Author,
                Status = request.Status,
                Kit = request.Kit,
                Level = request.Level,
                OrderSupports = request.OrderSupports,
                Packages = request.Packages
            };

            var success = await _labService.UpdateLabAsync(id, labModel);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPost("labs")]
        public async Task<ActionResult> CreateLab(LabRequestModel request)
        {
            var labModel = new LabModel
            {
                LevelId = request.LevelId,
                KitId = request.KitId,
                Name = request.Name,
                Url = request.Url,
                Price = request.Price,
                MaxSupportTimes = request.MaxSupportTimes,
                Author = request.Author,
                Status = request.Status,
                Kit = request.Kit,
                Level = request.Level,
                OrderSupports = request.OrderSupports,
                Packages = request.Packages
            };

            var rs = await _labService.InsertLabAsync(labModel);
            labModel.Id = rs;
            return CreatedAtAction(nameof(GetLabById), new { id = labModel.Id }, labModel);
        }

        [HttpDelete("lab/{id}")]
        public async Task<IActionResult> DeleteKit(Guid id)
        {
            var success = await _labService.DeleteLabAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
