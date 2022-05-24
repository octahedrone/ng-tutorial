using Db;

namespace Application.Playground;

public static class Converters
{
    public static CurrentAdventureState ToAdventureStepWithOptions(this AdventureScriptStep entityWithOptionsIncluded)
    {
        if (entityWithOptionsIncluded == null)
            return null;

        var options = entityWithOptionsIncluded.Options is { Count: > 0 }
            ? entityWithOptionsIncluded.Options
                .OrderBy(x => x.OrderNumber)
                .Select(x => new AdventureStepOption
                {
                    Id = x.Id,
                    Text = x.OptionText,
                })
                .ToList()
            : null;

        return new CurrentAdventureState
        {
            CurrentStepId = entityWithOptionsIncluded.Id,
            CurrentStepText = entityWithOptionsIncluded.Text,
            CurrentStepOptions = options,
        };
    }
}