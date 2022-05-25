using System.Collections.Generic;
using System.Linq;
using Application.AdventureLogs;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("adventure/logs")]
public sealed class AdventureLogsController : ControllerBase
{
    private readonly IAdventureLogsService _adventureLogsService;

    public AdventureLogsController(IAdventureLogsService adventureLogsService)
    {
        _adventureLogsService = adventureLogsService;
    }

    [HttpGet("")]
    public QueryResponse<List<AdventureLogRecord>> GetAdventureLog()
    {
        var records = _adventureLogsService.GetCurrentAdventureLog().ToList();

        return new QueryResponse<List<AdventureLogRecord>>
        {
            Result = records
        };
    }
}