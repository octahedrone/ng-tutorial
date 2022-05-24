using Db;

namespace Application.Playground;

public static class Converters
{
    public static AdventureStep ToAdventureStepWithOptions(this AdventureScriptStep entityWithOptionsIncluded)
    {
        if (entityWithOptionsIncluded == null)
            return null;

        var options = entityWithOptionsIncluded.Options is { Count: > 0 }
            ? entityWithOptionsIncluded.Options
                .OrderBy(x => x.OrderNumber)
                .Select(x => new AdventureStepOption
                {
                    Id = x.Id,
                    Text = x.OptionText
                })
                .ToList()
            : null;

        return new AdventureStep
        {
            Id = entityWithOptionsIncluded.Id,
            Text = entityWithOptionsIncluded.Text,
            Options = options
        };
    }
}