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

        [HttpPut("update-lab")]
       
        public async Task<IActionResult> UpdateAsync([FromForm] LabUpdateRequest request)
        {
            
            string? url = null;
            if (request.File != null)
            {
                var uploadResponse = await _localfileService.UploadFileAsync("labs", request.Id.ToString(), request.File!);
                if (!uploadResponse.Succeeded)
                {
                    return StatusCode(uploadResponse.StatusCode, new { status = uploadResponse.Status, details = uploadResponse.Details });
                }
                url = uploadResponse.Details!["url"].ToString();
            }

            var serviceResponse = await _labService.UpdateLabsAsync(request, url);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            return Ok(new { status = serviceResponse.Status, details = serviceResponse.Details });
        }

        [HttpDelete("delete-lab")]
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

        [HttpPost("create-lab")]
        public async Task<IActionResult> CreateLabAsync([FromForm] LabCreateRequest request)
        {
            var labId = Guid.NewGuid();
            var serviceResponse = await _localfileService.UploadFileAsync("labs", labId.ToString(), request.File!);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            var url = serviceResponse.Details![ResponseService.ToKebabCase("url")].ToString();
                

            serviceResponse = await _labService.CreateLabAsync(request, labId, url!);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = labId }, new { status = serviceResponse.Status, details = serviceResponse.Details });
        }



        [HttpGet("download-lab")]
        public async Task <IActionResult> DownloadLabById(Guid id)
        {
            // Lấy thông tin URL và tên file từ LabService
            var serviceResponse = await _labService.GetFileUrlByIdAsync(id);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            // Lấy URL và tên tệp từ phản hồi của service
            var labUrl = serviceResponse.Details![ResponseService.ToKebabCase("url")].ToString();
            var labName = serviceResponse.Details![ResponseService.ToKebabCase("fileName")].ToString();

            // Tải tệp từ thư mục cục bộ
            serviceResponse = await _localfileService.DownloadFileAsync(labUrl!);
            if (!serviceResponse.Succeeded)
            {
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });
            }

            // Lấy stream và kiểu nội dung (content type) từ phản hồi của LocalfileService
            var stream = (MemoryStream)serviceResponse.Details![ResponseService.ToKebabCase("stream")];
            var contentType = serviceResponse.Details![ResponseService.ToKebabCase("contentType")].ToString();

            // Trả về tệp cho người dùng với tên file và kiểu nội dung
            return File(stream, contentType!, labName);
        }
    }
}
 