using Microsoft.AspNetCore.Mvc;

namespace Zeus.Api.Web.Controllers;

public class ErrorController : ApiController
{
    [Route("error")]
    [HttpGet]
    public IActionResult Error()
    {
        return Problem();
    }
}
