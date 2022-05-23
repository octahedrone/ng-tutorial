namespace Application.Playground;

public interface IAdventurePlaygroundService
{
    AdventureState GetAdventureState();

    void DeleteCurrentAdventure();
}