namespace Application.Playground;

public interface IAdventurePlaygroundService
{
    AdventureState GetAdventureState();
    CurrentAdventureState GetCurrentStep();
    CurrentAdventureState Advance(int stepId, int? selectedOptionId);
    void DeleteCurrentAdventure();
}