using Db;

namespace Application.Playground;

public class AdventurePlaygroundService : IAdventurePlaygroundService
{
    private readonly AdventureContext _dataContext;

    public AdventurePlaygroundService(AdventureContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public bool HasCurrentAdventure()
    {
        return _dataContext.Adventures.Any();
    }

    public void DeleteCurrentAdventure()
    {
        var currentAdventure = _dataContext.Adventures.SingleOrDefault();
        if (currentAdventure == null)
            return;

        _dataContext.Adventures.Remove(currentAdventure);
        _dataContext.SaveChanges();
    }
}