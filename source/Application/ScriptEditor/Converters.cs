using Db;

namespace Application.ScriptEditor;

public static class Converters
{
    public static AdventureScript ToScriptEntity(this ScriptEditorScript script)
    {
        var result = new AdventureScript
        {
            Created = DateTime.UtcNow
        };
        result.AdventureScriptSteps = new List<AdventureScriptStep>
        {
            script.Root.ToAdventureScriptStepEntity(result, null)
        };
        return result;
    }

    private static AdventureScriptStep ToAdventureScriptStepEntity(this ScriptEditorScriptStep step, AdventureScript parentScript, AdventureScriptStep parentStep)
    {
        var result = new AdventureScriptStep
        {
            Text = step.Text,
            AdventureScript = parentScript,
            OptionText = step.OptionText,
            ParentStep = parentStep
        };

        if (step.Options != null && step.Options.Any())
        {
            result.Options = step.Options
                .Select((x, index) =>
                {
                    var step = x.ToAdventureScriptStepEntity(parentScript, result);
                    step.OrderNumber = index;
                    return step;
                })
                .ToList();
        }

        return result;
    }

    public static ScriptEditorScript ToEditorScript(this AdventureScript scriptEntity)
    {
        if (scriptEntity == null)
            return null;

        var scriptStepsRecords = scriptEntity.AdventureScriptSteps
            .Select(x => new
            {
                x.Id,
                x.ParentStepId,
                Record = new ScriptEditorScriptStep
                {
                    Text = x.Text,
                    OptionText = x.OptionText,
                    OrderNumber = x.OrderNumber
                }
            })
            .ToDictionary(x => x.Id);

        var recordsWithParent = scriptStepsRecords.Values.Where(x => x.ParentStepId != null);
        foreach (var child in recordsWithParent)
        {
            var parent = scriptStepsRecords[child.ParentStepId.Value];
            if (parent.Record.Options == null)
            {
                parent.Record.Options = new List<ScriptEditorScriptStep>();
            }

            parent.Record.Options.Add(child.Record);
        }

        return new ScriptEditorScript
        {
            Created = scriptEntity.Created,
            Root = scriptStepsRecords.Values.Single(x => x.ParentStepId == null).Record
        };
    }
}