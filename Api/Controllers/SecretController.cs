using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SecretController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("test test");
    }
}