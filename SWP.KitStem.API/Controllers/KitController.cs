using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.KitStem.Service.BusinessModels.RequestModel;
using SWP.KitStem.Service.Services;
using SWP.KitStem.Service.Services.IService;

namespace SWP.KitStem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitController : ControllerBase
    {
        private readonly KitService _kitService;

        public KitController(KitService kitService)
        {
            _kitService = kitService;
        }

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
    }
}
