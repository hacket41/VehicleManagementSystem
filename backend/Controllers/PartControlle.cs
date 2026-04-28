using backend.Data.DTO.Request;
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
        return part == null ? NotFound() : Ok(part);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllParts()
    {
        var allParts = await parts.GetAllParts();
        return Ok(allParts);
    }


    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> PurchasePart([FromBody] PartsPurchaseRequest part)
    {
        try
        {
            var newPart = await parts.PurchasePart(part);
            return CreatedAtAction(nameof(GetPartWithDetails), new {id = newPart.Id}, newPart);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> EditPart(int id, [FromBody] PartsPurchaseRequest part)
    {

        var result = await parts.EditPart(id, part);
        return result == null ? NotFound() : Ok(result);
    }


}