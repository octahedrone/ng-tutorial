using Application.Playground;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers;

[ApiController]
[Route("adventure")]
public sealed class AdventurePlaygroundController : ControllerBase
{
    private readonly IAdventurePlaygroundService _adventurePlaygroundService;
    private readonly ILogger<AdventurePlaygroundController> _logger;
    
    public AdventurePlaygroundController(IAdventurePlaygroundService adventurePlaygroundService, ILogger<AdventurePlaygroundController> logger)
    {
        _adventurePlaygroundService = adventurePlaygroundService;
        _logger = logger;
    }

    [HttpGet("current-step")]
    public QueryResponse<CurrentAdventureState> GetCurrentStep()
    {
        try
        {
            var step = _adventurePlaygroundService.GetCurrentStep();
            return new QueryResponse<CurrentAdventureState>
            {
                Result = step
            };
        }
        catch (AdventurePlaygroundServiceException e)
        {
            _logger.Log(LogLevel.Error, "GetCurrentStep {message}", e.Message);
            return new QueryResponse<CurrentAdventureState>
            {
                ErrorMessage = e.Message
            };
        }
    }
    
    [HttpPost("current-step")]
    public QueryResponse<CurrentAdventureState> SubmitUserChoice(SubmitUserChoiceRequest request)
    {
        try
        {
            var step = _adventurePlaygroundService.Commit(request.StepId, request.OptionId);
            return new QueryResponse<CurrentAdventureState>
            {
                Result = step
            };
        }
        catch (AdventurePlaygroundServiceException e)
        {
            _logger.Log(LogLevel.Error, "SubmitUserChoice {message}", e.Message);
            return new QueryResponse<CurrentAdventureState>
            {
                ErrorMessage = e.Message
            };
        }
    }
    
    public class SubmitUserChoiceRequest
    {
        public int StepId { get; set; }
        public int? OptionId { get; set; }
    }
}