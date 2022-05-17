using Db;

namespace Application.ScriptEditor;

public class ScriptEditorService : IScriptEditorService
{
    private readonly AdventureContext _dataContext;

    public ScriptEditorService(AdventureContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool HasCurrentScript()
    {
        return _dataContext.AdventureScripts.Any();
    }

    public ScriptEditorScript GetCurrentScript()
    {
        var scriptRecord = _dataContext.AdventureScripts.FirstOrDefault();
        if (scriptRecord == null)
        {
            return null;
        }

        var scriptStepsRecords = _dataContext.AdventureScriptSteps
            .Where(x => x.AdventureScriptId == scriptRecord.Id)
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
            Created = scriptRecord.Created,
            Root = scriptStepsRecords.Values.Single(x => x.ParentStepId == null).Record
        };
    }

    public void ReplaceCurrentScript(ScriptEditorScript script)
    {
        throw new NotImplementedException();
    }
}