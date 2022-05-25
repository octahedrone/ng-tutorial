using Db;
using Microsoft.EntityFrameworkCore;

namespace Application.AdventureLogs;

public class AdventureLogsService : IAdventureLogsService
{
    private readonly AdventureContext _dataContext;

    public AdventureLogsService(AdventureContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<AdventureLogRecord> GetCurrentAdventureLog()
    {
        var selectedOptionIds = _dataContext.AdventureLogs
            .Select(x => x.AdventureScriptStepId)
            .ToHashSet();

        if (!selectedOptionIds.Any())
        {
            // no steps taken so far, adventure might not be started
            return Enumerable.Empty<AdventureLogRecord>();
        }

        var stepsTaken = _dataContext.AdventureScriptSteps
            .Include(x => x.Options)
            .Where(x => selectedOptionIds.Contains(x.Id))
            .ToList();

        // assume root step is always taken, start from it
        var result = new List<AdventureLogRecord>(stepsTaken.Count);
        var step = stepsTaken.Single(x => x.ParentStepId == null);

        while (step.Options.Any())
        {
            var selectedOption = step.Options.SingleOrDefault(x => selectedOptionIds.Contains(x.Id));
            if (selectedOption == null)
            {
                // adventure is not complete, some options are not yet selected
                break;
            }
            result.Add(new AdventureLogRecord
            {
                CardText = step.Text,
                SelectedOptionIndex = selectedOption.OrderNumber,
                Options = step.Options.OrderBy(x => x.OrderNumber).Select(x => x.OptionText).ToList()
            });

            step = selectedOption;
        }

        result.Add(new AdventureLogRecord
        {
            CardText = step.Text
        });

        return result;
    }
}