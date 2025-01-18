using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Presentation.Web.Manifest;

namespace Zeus.Api.Presentation.Web.Controllers.Manifest;

public class ManifestController : ApiController
{
    private readonly ApiManifestProvider _manifestProvider;

    public ManifestController(ApiManifestProvider manifestProvider)
    {
        _manifestProvider = manifestProvider;
    }

    [AllowAnonymous]
    [HttpGet("about.json")]
    [ProducesResponseType<ApiManifest>(StatusCodes.Status200OK)]
    public IActionResult GetAboutJson()
    {
        return Ok(_manifestProvider.GetManifest());
    }
}
