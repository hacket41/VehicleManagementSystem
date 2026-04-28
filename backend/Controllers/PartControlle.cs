using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartController(IPartsService parts) : ControllerBase

{
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPartWithDetails([FromRoute] int  id)
    {
        var part = await parts.GetPartWithDetails(id);

        if (part == null) return NotFound();

        return Ok(part);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllParts()
    {
        var allParts = await parts.GetAllParts();
        // if (allParts.Count == 0) return [];

        return Ok(allParts);
    }


}