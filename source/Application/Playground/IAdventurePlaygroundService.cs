namespace Application.Playground;

public interface IAdventurePlaygroundService
{
    AdventureState GetAdventureState();
    AdventureStep GetCurrentStep();
    AdventureStep Advance(int selectedOptionIndex);
    void DeleteCurrentAdventure();
}