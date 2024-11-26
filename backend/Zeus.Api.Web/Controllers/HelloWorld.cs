using Microsoft.AspNetCore.Mvc;

namespace Zeus.Api.Web.Controllers;

[Controller]
[Route("/")]
public class HelloWorld
{
    [HttpGet]
    public string Get() => "Hello World!";
}
