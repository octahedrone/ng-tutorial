namespace Application.Playground;

public interface IAdventurePlaygroundService
{
    bool HasCurrentAdventure();
    void DeleteCurrentAdventure();
}