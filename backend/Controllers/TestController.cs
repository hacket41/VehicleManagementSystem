using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    // GET
    [HttpGet]
    public  IActionResult Test ()
    {
        return Ok(new { Message = "Hello World" });
    }
}