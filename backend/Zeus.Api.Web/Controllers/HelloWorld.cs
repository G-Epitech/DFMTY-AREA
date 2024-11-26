using Microsoft.AspNetCore.Mvc;

namespace Zeus.Api.Web.Controllers;

[ApiController]
[Route("/")]
public class HelloWorld : ControllerBase
{
    [HttpGet]
    public string Get() => "Hello World!";
}
