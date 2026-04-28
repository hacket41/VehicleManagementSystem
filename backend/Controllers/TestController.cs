using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public  IActionResult Test ()
    {
        return Ok(new { Message = "Api is Healthy"});
    }
}