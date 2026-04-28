using backend.Data.DTO.Request;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // All endpoints admin only
    [Authorize(Roles = "Admin")]
    public class VendorController(IVendorService vendorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await vendorService.GetAllVendors();
            return Ok(vendors);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vendor = await vendorService.GetVendorById(id);
            if (vendor == null) return NotFound();
            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await vendorService.CreateVendor(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] VendorRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await vendorService.EditVendor(id, request);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await vendorService.DeleteVendor(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
