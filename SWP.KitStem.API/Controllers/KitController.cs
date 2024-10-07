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
    public class KitController : ControllerBase
    {
        private readonly KitService _kitService;

        public KitController(KitService kitService)
        {
            _kitService = kitService;
        }

        // GET: api/Kit
        [HttpGet("kits")]
        public async Task<ActionResult<IEnumerable<Kit>>> GetKits()
        {
            var kits = await _kitService.GetKitsAsync();
            var response = kits.Select(kit => new KitResponseModel
            {
                Id = kit.Id,
                Name = kit.Name,
                Brief = kit.Brief,
                Description = kit.Description,
                PurchaseCost = kit.PurchaseCost,
                Status = kit.Status
            });
            return Ok(response);
        }

        // GET: api/Kit/5
        [HttpGet("kit/{id}")]
        public async Task<ActionResult<Kit>> GetKitById(int id)
        {
            var kit = await _kitService.GetKitByIdAsync(id);
            if (kit == null) return NotFound();

            var response = new KitResponseModel
            {
                Id = kit.Id,
                Name = kit.Name,
                Brief = kit.Brief,
                Description = kit.Description,
                PurchaseCost = kit.PurchaseCost,
                Status = kit.Status
            };
            return Ok(response);
        }

        // PUT: api/Kit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("kit/{id}")]
        public async Task<IActionResult> UpdateKit(int id, KitRequestModel request)
        {
            var kitModel = new KitModel
            {
                Name = request.Name,
                Brief = request.Brief,
                Description = request.Description,
                PurchaseCost = request.PurchaseCost,
                Status = request.Status
            };

            var success = await _kitService.UpdateKitAsync(id, kitModel);
            if (!success) return NotFound();

            return NoContent();
        }

        // POST: api/Kit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("kits")]
        public async Task<ActionResult> CreateKit(KitRequestModel request)
        {
            var kitModel = new KitModel
            {
                Name = request.Name,
                Brief = request.Brief,
                Description = request.Description,
                PurchaseCost = request.PurchaseCost,
                Status = request.Status
            };

            var rs = await _kitService.InsertKitAsync(kitModel);
            kitModel.Id = rs;
            return CreatedAtAction(nameof(GetKitById), new { id = kitModel.Id }, kitModel);
        }

        // DELETE: api/Kit/5
        [HttpDelete("kit/{id}")]
        public async Task<IActionResult> DeleteKit(int id)
        {
            var success = await _kitService.DeleteKitAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }


    }
}
