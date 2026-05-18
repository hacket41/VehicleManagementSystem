using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartController(IPartsService parts) : ControllerBase

{
    //Parts  Section
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPartWithDetails([FromRoute] int  id)
    {
        var part = await parts.GetPartWithDetails(id);
        return part == null ? NotFound() : Ok(part);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParts()
    {
        var allParts = await parts.GetAllParts();
        return Ok(allParts);
    }

    [Authorize(Roles = "Admin, Staff" )]
    [HttpPost]
    public async Task<IActionResult> PurchasePart([FromBody] PartsPurchaseRequest part)
    {
            var newPart = await parts.PurchasePart(part);
            return CreatedAtAction(nameof(GetPartWithDetails), new {id = newPart.Id}, newPart);
    }

    [Authorize(Roles = "Admin, Staff" )]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPart(int id, [FromBody] PartsPurchaseRequest part)
    {
        var result = await parts.EditPart(id, part);
        return result == null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin, Staff" )]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePart(int id)
    {
        var partToDelete = await parts.DeletePart(id);
        return !partToDelete ? NoContent() : Ok(partToDelete);
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpPost("restock")]
    public async Task<IActionResult> RestockPart([FromBody] RestockPartDto request)
    {
        var result = await parts.RestockPart(request);
        return Ok(result);
    }

    //Parts Categories Section

    [HttpGet("categories")]
    public async Task<IActionResult> GetPartsCategories()
    {
        var result = await parts.GetPartsCategories();
        return Ok(result);
    }

    [Authorize(Roles = "Admin, Staff" )]
    [HttpPost("categories")]
    public async Task<IActionResult> AddPartsCategory(PartCategoryRequest partCategory)
    {
        var result = await parts.AddPartsCategory(partCategory);
        return Ok(result);
    }

    [Authorize(Roles = "Admin, Staff" )]
    [HttpPut("categories")]
    public async Task<IActionResult> EditPartsCategory(PartCategory partCategory)
    {
        var result = await parts.EditPartsCategory(partCategory);
        return Ok(result);
    }

    [Authorize("Admin, Staff")]
    [HttpDelete("categories/{id:int}")]
    public async Task<IActionResult> DeletePartsCategory([FromRoute] int id)
    {
        var result = await parts.DeletePartsCategory(id);
        return !result? NotFound() : NoContent();
    }

}