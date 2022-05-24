namespace Application.Playground;

public class CurrentAdventureState
{
    public AdventureState AdventureState { get; set; }
    public int CurrentStepId { get; set; }
    public string CurrentStepText { get; set; }
    public List<AdventureStepOption> CurrentStepOptions { get; set; }
}