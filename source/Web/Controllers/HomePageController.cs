using Application.ScriptEditor;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("home")]
public sealed class HomePageController : ControllerBase
{
    private IScriptEditorService _scriptEditorService;

    public HomePageController(IScriptEditorService scriptEditorService)
    {
        _scriptEditorService = scriptEditorService;
    }

    [HttpGet("")]
    public HomeScreenData GetInitialData()
    {
        var hasCurrentScript = _scriptEditorService.HasCurrentScript();
        
        return new HomeScreenData
        (
            scenarioIsPresent: hasCurrentScript,
            activeAdventureIsPresent: false,
            adventureLogIsPresent: false
        );
    }
}

public class HomeScreenData
{
    public HomeScreenData(bool scenarioIsPresent, bool activeAdventureIsPresent, bool adventureLogIsPresent)
    {
        ScenarioIsPresent = scenarioIsPresent;
        ActiveAdventureIsPresent = activeAdventureIsPresent;
        AdventureLogIsPresent = adventureLogIsPresent;
    }

    public bool ScenarioIsPresent { get; }
    public bool ActiveAdventureIsPresent { get; }
    public bool AdventureLogIsPresent { get; }
}