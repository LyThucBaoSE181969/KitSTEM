using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.Services;
using SWP.KitStem.Service.Services.IService;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class KitController : ControllerBase
    {
        private readonly KitService _kitService;
        private readonly KitImageService _kitImageService;
        private readonly LocalfileService _localfileService;

        public KitController(KitService kitService, KitImageService kitImageService, LocalfileService localfileService)
        {
            _kitService = kitService;
            _kitImageService = kitImageService;
            _localfileService = localfileService;
        }

        //[HttpGet("kits")]
        //public async Task<IActionResult> GetKits([FromQuery] KitModelRequest request)
        //{
        //    var categories = await _kitService.GetKitsAsync(request);
        //    if (!categories.Succeeded)
        //    {
        //        return StatusCode(categories.StatusCode, new { status = categories.Status, details = categories.Details });
        //    }

        //    return Ok(new { status = categories.Status, details = categories.Details });
        //}

        [HttpGet("kits")]
        public async Task<IActionResult> GetKits()
        {
            var categories = await _kitService.GetKitsAsync();
            if (!categories.Succeeded)
            {
                return StatusCode(categories.StatusCode, new { status = categories.Status, details = categories.Details });
            }

            return Ok(new { status = categories.Status, details = categories.Details });
        }

        [HttpGet("kit/{id}")]
        public async Task<IActionResult> GetKitById(int id)
        {
            var categories = await _kitService.GetKitByIdAsync(id);
            if (!categories.Succeeded)
            {
                return StatusCode(categories.StatusCode, new { status = categories.Status, details = categories.Details });
            }

            return Ok(new { status = categories.Status, details = categories.Details });
        }

        [HttpPost("Kit")]
        public async Task<IActionResult> CreateAsync([FromForm] KitCreateRequest request)
        {
            var serviceResponse = await _kitService.CreateKitAsync(request);

            if (!serviceResponse.Succeeded)
                return StatusCode(serviceResponse.StatusCode, new { status = serviceResponse.Status, details = serviceResponse.Details });

            var kitId = await _kitService.GetMaxIdAsync();
            var kitIdString = kitId.ToString();
            int kitImageCount = 1;
            var nameFiles = new Dictionary<string, IFormFile>();
            List<Guid> imageGuidList = new List<Guid>();

            foreach (var image in request.KitImagesList)
            {
                var imageIdTemp = Guid.NewGuid();
                imageGuidList.Add(imageIdTemp);
                nameFiles.Add(imageIdTemp.ToString(), image);
                kitImageCount++;
            }

            // Upload file cục bộ thay vì lên cloud
            var filesServiceResponse = await _localfileService.UploadFilesAsync(kitIdString, nameFiles);

            if (!filesServiceResponse.Succeeded)
                return StatusCode(filesServiceResponse.StatusCode, new { status = filesServiceResponse.Status, details = filesServiceResponse.Details });

            List<string>? urls = filesServiceResponse.Details!["urls"] as List<string>;
            Guid imageId = Guid.Empty;
            if (urls != null)
            {
                for (int i = 0; i < kitImageCount - 1; i++)
                {
                    var url = urls.ElementAt(i);
                    foreach (var imageGuid in imageGuidList)
                    {
                        if (url.Contains(imageGuid.ToString()))
                        {
                            imageId = imageGuid;
                        }
                    }

                    var imageServiceResponse = await _kitImageService.CreateImageAsync(imageId, kitId, url);
                    if (!imageServiceResponse.Succeeded)
                        return StatusCode(imageServiceResponse.StatusCode, new { status = imageServiceResponse.Status, details = imageServiceResponse.Details });
                }
            }
            return Ok(new { status = serviceResponse.Status, detail = serviceResponse.Details });
        }
    }
}
    