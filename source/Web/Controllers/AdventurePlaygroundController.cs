using System;
using System.Reflection;
using Application.Playground;
using DotNetCore.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers;

[ApiController]
[Route("adventure")]
public sealed class AdventurePlaygroundController : ControllerBase
{
    private readonly IAdventurePlaygroundService _adventurePlaygroundService;

    public AdventurePlaygroundController(IAdventurePlaygroundService adventurePlaygroundService)
    {
        _adventurePlaygroundService = adventurePlaygroundService;
    }

    [HttpGet("current-step")]
    public QueryResponse<AdventureStep> GetCurrentStep()
    {
        var step = _adventurePlaygroundService.GetCurrentStep();
        return new QueryResponse<AdventureStep>
        {
            Result = step,
            ErrorMessage = step == null ? "Unable to find current adventure step" : null
        };
    }
}