namespace Application.Playground;

public interface IAdventurePlaygroundService
{
    AdventureState GetAdventureState();
    CurrentAdventureState GetCurrentStep();
    CurrentAdventureState Commit(int stepId, int? selectedOptionId);
    void DeleteCurrentAdventure();
}