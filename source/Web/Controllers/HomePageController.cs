using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("home")]
public sealed class HomePageController : ControllerBase
{
    [HttpGet("")]
    public HomeScreenData GetInitialData()
    {
        return new HomeScreenData
        (
            scenarioIsPresent: false,
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