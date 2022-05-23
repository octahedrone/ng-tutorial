using Application.Playground;
using Application.ScriptEditor;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("home")]
public sealed class HomePageController : ControllerBase
{
    private readonly IScriptEditorService _scriptEditorService;
    private readonly IAdventurePlaygroundService _adventurePlaygroundService;

    public HomePageController(IScriptEditorService scriptEditorService, IAdventurePlaygroundService adventurePlaygroundService)
    {
        _scriptEditorService = scriptEditorService;
        _adventurePlaygroundService = adventurePlaygroundService;
    }

    [HttpGet("")]
    public HomeScreenData GetInitialData()
    {
        var hasCurrentScript = _scriptEditorService.HasCurrentScript();
        var adventureState = _adventurePlaygroundService.GetAdventureState();
        
        return new HomeScreenData
        (
            scenarioIsPresent: hasCurrentScript,
            adventureState: adventureState
        );
    }
}

public class HomeScreenData
{
    public HomeScreenData(bool scenarioIsPresent, AdventureState adventureState)
    {
        ScenarioIsPresent = scenarioIsPresent;
        AdventureState = adventureState;
    }

    public bool ScenarioIsPresent { get; }
    public AdventureState AdventureState { get; }
}