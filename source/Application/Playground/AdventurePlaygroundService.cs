using Db;

namespace Application.Playground;

public class AdventurePlaygroundService : IAdventurePlaygroundService
{
    private readonly AdventureContext _dataContext;

    public AdventurePlaygroundService(AdventureContext dataContext)
    {
        _dataContext = dataContext;
    }

    public AdventureState GetAdventureState()
    {
        var query = (from step in _dataContext.AdventureScriptSteps
                     from log in _dataContext.AdventureLogs.Where(x => x.AdventureScriptStepId == step.Id).DefaultIfEmpty()
                     select new
                     {
                         StepId = step.Id,
                         LogExists = log != null
                     })
            .GroupBy(x => x.LogExists)
            .Select(g => g.Key)
            .ToList();

        switch (query.Count)
        {
            case 0:
                // AdventureScriptSteps is empty so there are no records at all
                return AdventureState.Impossible;
            case 2:
                // there are AdventureScriptSteps and AdventureLogs records
                return AdventureState.Pending;
            default:
                return query[0]
                    ? AdventureState.Finished // every AdventureScriptSteps has AdventureLogs
                    : AdventureState.NotStarted; // any AdventureScriptSteps has AdventureLogs
        }
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