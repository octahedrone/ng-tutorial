using Db;
using Microsoft.EntityFrameworkCore;

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
        var adventure = _dataContext.Adventures.SingleOrDefault();
        if (adventure != null)
        {
            return (AdventureState)adventure.AdventureStateId;
        }

        return _dataContext.AdventureScriptSteps.Any()
            ? AdventureState.NotStarted
            : AdventureState.Impossible;
    }

    public AdventureStep GetCurrentStep()
    {
        var adventure = _dataContext.Adventures.SingleOrDefault();
        if (adventure == null)
        {
            // take a root step
            var rootStep = _dataContext.AdventureScriptSteps
                .Include(x => x.Options)
                .SingleOrDefault(x => x.ParentStepId == null);

            return rootStep.ToAdventureStepWithOptions();
        }

        var step = _dataContext.AdventureScriptSteps
            .Include(x => x.Options)
            .SingleOrDefault(x => x.Id == adventure.CurrentScriptStepId);

        return step.ToAdventureStepWithOptions();
    }

    public AdventureStep Advance(int selectedOptionIndex)
    {
        throw new NotImplementedException();
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