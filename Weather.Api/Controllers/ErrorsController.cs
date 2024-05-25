using Microsoft.AspNetCore.Mvc;

namespace Weather.Api;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
