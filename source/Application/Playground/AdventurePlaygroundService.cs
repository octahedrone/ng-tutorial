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

    public CurrentAdventureState GetCurrentStep()
    {
        var adventure = _dataContext.Adventures.SingleOrDefault();
        if (adventure == null)
        {
            // take a root step
            var rootStep = _dataContext.AdventureScriptSteps
                .Include(x => x.Options)
                .SingleOrDefault(x => x.ParentStepId == null);
            
            if (rootStep == null)
            {
                throw new AdventurePlaygroundServiceException($"Script does not contain any steps");
            }

            return rootStep.ToAdventureStepWithOptions();
        }

        var step = _dataContext.AdventureScriptSteps
            .Include(x => x.Options)
            .Single(x => x.Id == adventure.CurrentScriptStepId);

        return step.ToAdventureStepWithOptions();
    }

    public CurrentAdventureState Commit(int stepId, int? selectedOptionId)
    {
        if (selectedOptionId != null)
        {
            var nextStep = _dataContext.AdventureScriptSteps
                .Include(x => x.Options)
                .SingleOrDefault(x => x.Id == selectedOptionId && x.ParentStepId == stepId);

            if (nextStep == null)
            {
                throw new AdventurePlaygroundServiceException($"Script does not contain step with id {selectedOptionId} and parent {stepId}");
            }

            var adventure = _dataContext.Adventures.SingleOrDefault();
            if (adventure == null)
            {
                // user takes first step for at least two step adventure
                adventure = new Adventure
                {
                    Started = DateTime.UtcNow,
                    AdventureScriptId = nextStep.AdventureScriptId,
                    AdventureStateId = (int)AdventureState.Pending,
                    CurrentScriptStepId = selectedOptionId.Value,
                    Logs = new List<AdventureLog>
                    {
                        new() { AdventureScriptStepId = stepId },
                        new() { AdventureScriptStepId = selectedOptionId.Value },
                    }
                };

                _dataContext.Adventures.Add(adventure);
            }
            else
            {
                // adventure is in progress and user takes another step
                if (adventure.CurrentScriptStepId != stepId)
                {
                    throw new AdventurePlaygroundServiceException($"{stepId} is not the current adventure step, which is {adventure.CurrentScriptStepId}");
                }

                adventure.CurrentScriptStepId = selectedOptionId.Value;
                _dataContext.AdventureLogs.Add(new AdventureLog
                {
                    AdventureId = adventure.Id,
                    AdventureScriptStepId = selectedOptionId.Value
                });
            }

            _dataContext.SaveChanges();
            return nextStep.ToAdventureStepWithOptions();
        }
        else
        {
            var currentStep = _dataContext.AdventureScriptSteps
                .Where(x => !_dataContext.AdventureScriptSteps.Any(c => c.ParentStepId == stepId))
                .SingleOrDefault(x => x.Id == stepId);

            if (currentStep == null)
            {
                throw new AdventurePlaygroundServiceException($"Script step {stepId} is not  present or needs child option to be selected to proceed");
            }

            var adventure = _dataContext.Adventures.SingleOrDefault();
            if (adventure == null)
            {
                // user takes first step for a single step adventure
                adventure = new Adventure
                {
                    Started = DateTime.UtcNow,
                    AdventureScriptId = currentStep.AdventureScriptId,
                    AdventureStateId = (int)AdventureState.Finished,
                    CurrentScriptStepId = stepId,
                    Logs = new List<AdventureLog>
                    {
                        new() { AdventureScriptStepId = stepId },
                    }
                };

                _dataContext.Adventures.Add(adventure);
            }
            else if(adventure.AdventureStateId != (int)AdventureState.Finished)
            {
                // taking last adventure step
                adventure.AdventureStateId = (int)AdventureState.Finished;
                adventure.Logs.Add(new AdventureLog
                {
                    AdventureScriptStepId = stepId
                });
            }
            _dataContext.SaveChanges();
            return currentStep.ToAdventureStepWithOptions();
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