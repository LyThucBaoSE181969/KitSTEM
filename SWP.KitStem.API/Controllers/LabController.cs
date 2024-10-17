using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.Services;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private readonly LabService _labService;
        private readonly LocalfileService _localfileService;

        public LabController(LabService labService, LocalfileService localfileService)
        {
            _labService = labService;
            _localfileService = localfileService;
        }

        [HttpPut("lab")]
       
        public async Task<IActionResult> UpdateAsync([FromForm] LabUpdateRequest request)
        {
            ResponseService serviceResponse;
            string? url = null;
            var nameFiles = new Dictionary<string, IFormFile>();
            if (request.File != null)
            {
                serviceResponse = await _localfileService.UploadFilesAsync(request.Id.ToString(), nameFiles);
                if (!serviceResponse.Succeeded)
                {
                    return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
                }
                url = serviceResponse.Details!["urls"].ToString();
            }

            serviceResponse = await _labService.UpdateLabsAsync(request, url);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        [HttpDelete("lab")]
        public async Task<IActionResult> DeleteLab(Guid id)
        {
            var serviceResponse = await _labService.DeleteLabsAsync(id);
            if (!serviceResponse.Succeeded)
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });

            return Ok(new { status = serviceResponse.Status, detail = serviceResponse.Details });
        }

        [HttpGet("lab/{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var serviceResponse = await _labService.GetLabByIdAsync(id);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        [HttpGet("labs")]
        public async Task<IActionResult> GetLabs()
        {
            var labs = await _labService.GetLabsAsync();
            if (!labs.Succeeded)
            {
                return StatusCode(labs.StatusCode, new { status = labs.Status, details = labs.Details });
            }
            return Ok(new { status = labs.Status, details = labs.Details });
        }

        [HttpPost("lab")]
        public async Task<IActionResult> CreateLabAsync([FromForm] LabCreateRequest request)
        {
            var labId = Guid.NewGuid();
            var nameFiles = new Dictionary<string, IFormFile>();
            var serviceResponse = await _localfileService.UploadFilesAsync(labId.ToString(), nameFiles);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            var url = serviceResponse.Details!["urls"].ToString();
            serviceResponse = await _labService.CreateLabAsync(request, labId, url!);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = labId }, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }
    }
}
