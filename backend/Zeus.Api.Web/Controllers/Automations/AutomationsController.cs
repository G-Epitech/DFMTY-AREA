using Microsoft.AspNetCore.Mvc;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.AutomationAggregate;
using Zeus.Api.Domain.AutomationAggregate.Entities;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Api.Infrastructure.Authentication.Context;

namespace Zeus.Api.Web.Controllers.Automations;

[Route("/automations")]
public class AutomationsController : ApiController
{
    private readonly IAutomationWriteRepository _automationWriteRepository;
    private readonly IAutomationReadRepository _automationReadRepository;
    private readonly IAuthUserContext _authUserContext;

    public AutomationsController(IAutomationWriteRepository automationWriteRepository, IAuthUserContext authUserContext, IAutomationReadRepository automationReadRepository)
    {
        _automationWriteRepository = automationWriteRepository;
        _authUserContext = authUserContext;
        _automationReadRepository = automationReadRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAutomation(CancellationToken cancellationToken)
    {
        var currentUserId = _authUserContext.User?.Id;

        if (currentUserId is null)
        {
            return Unauthorized();
        }
        var userId = new UserId(currentUserId.Value);

        var automation = Automation.Create(
            "Test",
            "Test description",
            userId,
            AutomationTrigger.Create("Test", [], []),
            []
        );
        await _automationWriteRepository.AddAutomationAsync(automation, cancellationToken);
        return Ok(automation.Id);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAutomationById(Guid id, CancellationToken cancellationToken)
    {
        var automation = await _automationReadRepository.GetByIdAsync(new AutomationId(id), cancellationToken);
        if (automation is null)
        {
            return NotFound();
        }
        return Ok(automation);
    }
}
